// gen-api.mjs — generate Starlight API reference pages from .NET XML docs.
//
// Reads the two XML documentation files produced by `dotnet build -c Release`,
// groups members by their declaring type, and emits one markdown page per public
// type under website/src/content/docs/api/<group>/<TypeName>.md, plus an index.
//
// Dependency-light: only fast-xml-parser. Run with: node website/scripts/gen-api.mjs

import { XMLParser } from 'fast-xml-parser';
import { readFileSync, existsSync, rmSync, mkdirSync, writeFileSync } from 'node:fs';
import { dirname, join, resolve } from 'node:path';
import { fileURLToPath } from 'node:url';

const __dirname = dirname(fileURLToPath(import.meta.url));
const repoRoot = resolve(__dirname, '..', '..');

const XML_FILES = [
  'src/DaisyBlazor.Components/bin/Release/net10.0/DaisyBlazor.Components.xml',
  'src/DaisyBlazor.Charts/bin/Release/net10.0/DaisyBlazor.Charts.xml',
];

const OUT_DIR = join(repoRoot, 'website', 'src', 'content', 'docs', 'api');
const SITE_BASE = '/daisyblazor';

// Map a namespace to a sidebar/group slug.
function namespaceToGroup(ns) {
  switch (ns) {
    case 'DaisyBlazor':
      return 'components';
    case 'DaisyBlazor.Charts':
      return 'charts';
    case 'DaisyBlazor.Data':
      return 'data';
    case 'DaisyBlazor.Feedback':
      return 'feedback';
    case 'DaisyBlazor.Layout':
      return 'layout';
    case 'DaisyBlazor.Theming':
      return 'theming';
    default:
      // Sensible fallback: last namespace segment, lower-cased.
      return ns.split('.').pop().toLowerCase();
  }
}

// ---------------------------------------------------------------------------
// Type-name decoding
// ---------------------------------------------------------------------------

// Short, friendly name for a fully-qualified CLR type used inside a signature.
// Known framework generics get readable names; everything else is reduced to
// its last segment.
const PRIMITIVE_MAP = {
  'System.String': 'string',
  'System.Int32': 'int',
  'System.Int64': 'long',
  'System.Int16': 'short',
  'System.Byte': 'byte',
  'System.Boolean': 'bool',
  'System.Double': 'double',
  'System.Single': 'float',
  'System.Decimal': 'decimal',
  'System.Object': 'object',
  'System.Void': 'void',
  'System.Char': 'char',
  'System.Guid': 'Guid',
  'System.DateTime': 'DateTime',
  'System.TimeSpan': 'TimeSpan',
};

// Generic type-parameter names per generic arity, used to decode `T\`1` etc.
// Pulled from the actual library where meaningful, generic fallback otherwise.
function genericParamNames(simpleName, arity) {
  const known = {
    BreadcrumbService: ['TResource'],
  };
  if (known[simpleName] && known[simpleName].length === arity) {
    return known[simpleName];
  }
  if (arity === 1) return ['T'];
  return Array.from({ length: arity }, (_, i) => `T${i + 1}`);
}

// Decode a type reference token (as found inside a method signature) into a
// short, human-friendly form. Handles arrays, nested generics ({...}), and
// type-parameter references (`0, ``0).
function shortTypeRef(token) {
  token = token.trim();

  // Array suffix.
  if (token.endsWith('[]')) {
    return shortTypeRef(token.slice(0, -2)) + '[]';
  }

  // Type / method parameter references: `0 (type param), ``0 (method param).
  const mp = token.match(/^``(\d+)$/);
  if (mp) return 'T' + (Number(mp[1]) + 1 > 1 ? Number(mp[1]) + 1 : '');
  const tp = token.match(/^`(\d+)$/);
  if (tp) return Number(tp[1]) === 0 ? 'T' : 'T' + (Number(tp[1]) + 1);

  // Nested generic: Name{arg,arg}
  const gi = token.indexOf('{');
  if (gi !== -1 && token.endsWith('}')) {
    const base = token.slice(0, gi);
    const argsRaw = token.slice(gi + 1, -1);
    const args = splitGenericArgs(argsRaw).map(shortTypeRef);
    return `${shortName(base)}<${args.join(', ')}>`;
  }

  return shortName(token);
}

// Split top-level comma-separated generic args, respecting nested { } and ( ).
function splitGenericArgs(s) {
  const out = [];
  let depth = 0;
  let cur = '';
  for (const ch of s) {
    if (ch === '{' || ch === '(') depth++;
    else if (ch === '}' || ch === ')') depth--;
    if (ch === ',' && depth === 0) {
      out.push(cur);
      cur = '';
    } else {
      cur += ch;
    }
  }
  if (cur.length) out.push(cur);
  return out;
}

// Reduce a plain (non-generic-bracket) type token to a short name.
function shortName(token) {
  token = token.trim();
  if (PRIMITIVE_MAP[token]) return PRIMITIVE_MAP[token];
  // Strip arity marker like `1 on the type itself.
  const noArity = token.replace(/`\d+$/, '');
  const last = noArity.split('.').pop();
  return last;
}

// Decode a generic type's own name + arity into TypeName<P1, P2>.
function decodeTypeDisplayName(fullName) {
  const m = fullName.match(/^(.*?)`(\d+)$/);
  if (!m) return fullName.split('.').pop();
  const simple = m[1].split('.').pop();
  const arity = Number(m[2]);
  const params = genericParamNames(simple, arity);
  return `${simple}<${params.join(', ')}>`;
}

// ---------------------------------------------------------------------------
// Member-name parsing (the `name=` attribute on <member>)
// ---------------------------------------------------------------------------

// Returns { kind, declaringType, display } for a member id, or null to skip.
function parseMemberName(id) {
  const kind = id[0]; // T, P, M, F, E
  const body = id.slice(2);

  if (kind === 'T') {
    return { kind, declaringType: body, display: decodeTypeDisplayName(body) };
  }

  if (kind === 'M') {
    // Split off a parameter list (...), if present.
    let namePart = body;
    let params = null;
    const paren = body.indexOf('(');
    if (paren !== -1) {
      namePart = body.slice(0, paren);
      // Everything between the outermost parens.
      const close = body.lastIndexOf(')');
      params = body.slice(paren + 1, close);
    }

    // namePart: Declaring.Type.MethodName  (method may carry ``N generic arity)
    const lastDot = namePart.lastIndexOf('.');
    const declaringType = namePart.slice(0, lastDot);
    let methodName = namePart.slice(lastDot + 1);

    // Method-level generic arity marker.
    let methodGenerics = '';
    const ga = methodName.match(/^(.*?)``(\d+)$/);
    if (ga) {
      methodName = ga[1];
      const n = Number(ga[2]);
      const names = n === 1 ? ['T'] : Array.from({ length: n }, (_, i) => 'T' + (i + 1));
      methodGenerics = `<${names.join(', ')}>`;
    }

    // Constructor.
    if (methodName === '#ctor') {
      methodName = shortName(declaringType.replace(/`\d+$/, ''));
    }

    let paramStr = '';
    if (params !== null) {
      const parts = params.length ? splitGenericArgs(params).map(shortTypeRef) : [];
      paramStr = `(${parts.join(', ')})`;
    } else {
      paramStr = '()';
    }

    return {
      kind,
      declaringType,
      display: `${methodName}${methodGenerics}${paramStr}`,
    };
  }

  // P / F / E: Declaring.Type.MemberName
  const lastDot = body.lastIndexOf('.');
  const declaringType = body.slice(0, lastDot);
  const memberName = body.slice(lastDot + 1);
  return { kind, declaringType, display: memberName };
}

// ---------------------------------------------------------------------------
// Doc-comment rendering (summary / param / returns + inline tags)
// ---------------------------------------------------------------------------

// fast-xml-parser is configured to preserve inline tags as child nodes and keep
// text order. We walk the parsed node and render to Markdown-ish inline text.
function renderNode(node) {
  if (node == null) return '';
  if (typeof node === 'string' || typeof node === 'number') {
    return String(node);
  }
  if (Array.isArray(node)) {
    return node.map(renderNode).join('');
  }

  let out = '';
  const order = node['#order'];
  // Reconstruct ordered children using the ordered-array form (see parser opts).
  if (Array.isArray(node['#nodes'])) {
    for (const child of node['#nodes']) {
      out += renderOrderedChild(child);
    }
    return out;
  }

  // Fallback (object form): render text then known tags. Order not guaranteed,
  // but the ordered path above is what we actually use.
  if (node['#text'] != null) out += String(node['#text']);
  return out;
}

// Render a single child from the ordered-array representation.
function renderOrderedChild(child) {
  // child is an object with exactly one key: either '#text' or a tag name.
  const key = Object.keys(child).find((k) => k !== ':@');
  const attrs = child[':@'] || {};

  if (key === '#text') {
    return String(child['#text']);
  }

  const inner = child[key]; // array of child nodes (ordered form)

  switch (key) {
    case 'see': {
      const cref = attrs['@_cref'];
      const langword = attrs['@_langword'];
      if (langword) return ' `' + langword + '` ';
      if (cref) return ' `' + crefShort(cref) + '` ';
      return ' ' + renderOrdered(inner) + ' ';
    }
    case 'paramref':
    case 'typeparamref': {
      const n = attrs['@_name'];
      return ' `' + n + '` ';
    }
    case 'c':
      return ' `' + renderOrdered(inner).trim() + '` ';
    case 'para':
      return '\n\n' + renderOrdered(inner).trim() + '\n\n';
    case 'code':
      return '\n\n```\n' + renderOrdered(inner).trim() + '\n```\n\n';
    case 'list':
    case 'item':
    case 'description':
    case 'term':
      return renderOrdered(inner);
    default:
      return renderOrdered(inner);
  }
}

function renderOrdered(nodes) {
  if (!Array.isArray(nodes)) return '';
  return nodes.map(renderOrderedChild).join('');
}

// Short member name from a cref like "T:DaisyBlazor.Foo" or "P:Ns.Type.Prop".
function crefShort(cref) {
  const kind = cref[0];
  const body = cref.slice(2);
  if (kind === 'T') return decodeTypeDisplayName(body);
  // For P/F/E/M show Type.Member with short type.
  const lastDot = body.lastIndexOf('.');
  if (lastDot === -1) return body;
  const typePart = body.slice(0, lastDot);
  let member = body.slice(lastDot + 1);
  // Strip method param lists / arity from cref members.
  member = member.replace(/\(.*\)$/, '').replace(/``\d+$/, '');
  if (member === '#ctor') member = shortName(typePart.replace(/`\d+$/, ''));
  return `${decodeTypeDisplayName(typePart)}.${member}`;
}

// Collapse runs of whitespace, trim, and tidy stray spaces around code spans.
function collapse(s) {
  return s
    .replace(/[ \t]*\n[ \t]*/g, '\n')
    .replace(/[ \t]{2,}/g, ' ')
    .replace(/ +`/g, ' `')
    .replace(/` +/g, '` ')
    .replace(/\n{3,}/g, '\n\n')
    .replace(/[ \t]+([.,;:)\]])/g, '$1')
    .replace(/([(\[])[ \t]+/g, '$1')
    .trim();
}

// First sentence of a (already plain) string.
function firstSentence(s) {
  const text = s.replace(/\n+/g, ' ').trim();
  const m = text.match(/^(.*?[.!?])(\s|$)/);
  return (m ? m[1] : text).trim();
}

// ---------------------------------------------------------------------------
// XML parsing
// ---------------------------------------------------------------------------

const parser = new XMLParser({
  ignoreAttributes: false,
  attributeNamePrefix: '@_',
  preserveOrder: true,
  trimValues: false,
  parseTagValue: false,
});

// With preserveOrder:true, fast-xml-parser returns arrays of single-key objects.
// We adapt our renderer to consume that ordered form directly. Helper to grab
// the child-node array for a given tag inside an ordered <member> body.
function findChildren(orderedBody, tagName) {
  const results = [];
  for (const node of orderedBody) {
    const key = Object.keys(node).find((k) => k !== ':@');
    if (key === tagName) {
      results.push({ attrs: node[':@'] || {}, nodes: node[key] });
    }
  }
  return results;
}

function renderTag(orderedBody, tagName) {
  const matches = findChildren(orderedBody, tagName);
  if (!matches.length) return '';
  return collapse(renderOrdered(matches[0].nodes));
}

// Parse a single XML file into a flat list of member records.
function parseXmlFile(absPath) {
  const xml = readFileSync(absPath, 'utf8');
  const tree = parser.parse(xml);
  // tree is ordered: [{ '?xml': [...] }, { doc: [...] }]
  const doc = tree.find((n) => Object.prototype.hasOwnProperty.call(n, 'doc'));
  if (!doc) return [];
  const membersNode = doc.doc.find((n) =>
    Object.prototype.hasOwnProperty.call(n, 'members')
  );
  if (!membersNode) return [];

  const records = [];
  for (const memberWrap of membersNode.members) {
    if (!Object.prototype.hasOwnProperty.call(memberWrap, 'member')) continue;
    const attrs = memberWrap[':@'] || {};
    const id = attrs['@_name'];
    if (!id) continue;
    const body = memberWrap.member; // ordered child array

    const parsed = parseMemberName(id);
    if (!parsed) continue;

    const summary = renderTag(body, 'summary');
    const returns = renderTag(body, 'returns');

    // params / typeparams keep their name + description.
    const params = findChildren(body, 'param').map((p) => ({
      name: p.attrs['@_name'],
      text: collapse(renderOrdered(p.nodes)),
    }));
    const typeparams = findChildren(body, 'typeparam').map((p) => ({
      name: p.attrs['@_name'],
      text: collapse(renderOrdered(p.nodes)),
    }));

    records.push({
      id,
      kind: parsed.kind,
      declaringType: parsed.declaringType,
      display: parsed.display,
      summary,
      returns,
      params,
      typeparams,
    });
  }
  return records;
}

// ---------------------------------------------------------------------------
// Markdown emission
// ---------------------------------------------------------------------------

function yamlEscape(s) {
  // Double-quoted YAML scalar.
  return '"' + s.replace(/\\/g, '\\\\').replace(/"/g, '\\"') + '"';
}

// A type is "public-ish" for our purposes if it appears at all in the XML doc
// (the C# compiler only emits XML for members whose visibility is documented;
// for this project that means public API surface). Compiler-generated names are
// excluded.
function isEmittableType(typeId) {
  if (typeId.includes('<') || typeId.includes('>')) return false;
  // Exclude obvious compiler/system artifacts.
  if (/[<>]/.test(typeId)) return false;
  return true;
}

function typeNamespace(typeId) {
  const noArity = typeId.replace(/`\d+$/, '');
  const lastDot = noArity.lastIndexOf('.');
  return lastDot === -1 ? '' : noArity.slice(0, lastDot);
}

function typeSlug(typeId) {
  // Starlight lowercases slugs; the file name (sans arity) drives the slug.
  return typeId.replace(/`\d+$/, '').split('.').pop().toLowerCase();
}

function memberSection(title, members) {
  if (!members.length) return '';
  let md = `\n## ${title}\n\n`;
  for (const m of members) {
    md += `### \`${m.display}\`\n\n`;
    if (m.summary) md += `${m.summary}\n\n`;
    if (m.params && m.params.length) {
      for (const p of m.params) {
        md += `- \`${p.name}\`${p.text ? ' — ' + p.text : ''}\n`;
      }
      md += '\n';
    }
    if (m.returns) md += `**Returns:** ${m.returns}\n\n`;
  }
  return md.replace(/\n{3,}/g, '\n\n');
}

function main() {
  // Resolve + validate XML inputs.
  const abs = XML_FILES.map((rel) => {
    const p = join(repoRoot, rel);
    if (!existsSync(p)) {
      console.error(
        `ERROR: XML doc file not found: ${p}\n` +
          `Run \`dotnet build src/DaisyBlazor.Components/DaisyBlazor.Components.csproj -c Release\` ` +
          `(and the Charts project) to generate it.`
      );
      process.exit(1);
    }
    return p;
  });

  let allRecords = [];
  for (const p of abs) {
    allRecords = allRecords.concat(parseXmlFile(p));
  }

  // Group records by declaring type.
  const byType = new Map();
  for (const rec of allRecords) {
    if (!isEmittableType(rec.declaringType)) continue;
    if (!byType.has(rec.declaringType)) {
      byType.set(rec.declaringType, {
        typeId: rec.declaringType,
        typeRecord: null,
        properties: [],
        methods: [],
        fields: [],
        events: [],
      });
    }
    const group = byType.get(rec.declaringType);
    if (rec.kind === 'T') group.typeRecord = rec;
    else if (rec.kind === 'P') group.properties.push(rec);
    else if (rec.kind === 'M') group.methods.push(rec);
    else if (rec.kind === 'F') group.fields.push(rec);
    else if (rec.kind === 'E') group.events.push(rec);
  }

  // Wipe + recreate output dir.
  if (existsSync(OUT_DIR)) rmSync(OUT_DIR, { recursive: true, force: true });
  mkdirSync(OUT_DIR, { recursive: true });

  // For the overview index, collect entries grouped by namespace.
  const overview = new Map(); // namespace -> [{ title, slug, group }]

  const sortedTypes = [...byType.values()].sort((a, b) =>
    a.typeId.localeCompare(b.typeId)
  );

  let pageCount = 0;
  const perNamespace = new Map();

  for (const t of sortedTypes) {
    const ns = typeNamespace(t.typeId);
    const group = namespaceToGroup(ns);
    const title = decodeTypeDisplayName(t.typeId);
    const slug = typeSlug(t.typeId);

    const summaryText = t.typeRecord && t.typeRecord.summary ? t.typeRecord.summary : '';
    const description = summaryText
      ? firstSentence(summaryText.replace(/`/g, ''))
      : `API reference for ${title}.`;

    // Sort members alphabetically by display name for stable output.
    const byName = (a, b) => a.display.localeCompare(b.display);
    t.properties.sort(byName);
    t.methods.sort(byName);
    t.fields.sort(byName);
    t.events.sort(byName);

    let body = '';
    body += '---\n';
    body += `title: ${yamlEscape(title)}\n`;
    body += `description: ${yamlEscape(description)}\n`;
    body += '---\n\n';

    if (summaryText) body += `${summaryText}\n\n`;

    // Type parameters from the type record's typeparams.
    const typeParams = (t.typeRecord && t.typeRecord.typeparams) || [];
    if (typeParams.length) {
      body += '\n## Type parameters\n\n';
      for (const tp of typeParams) {
        body += `- \`${tp.name}\`${tp.text ? ' — ' + tp.text : ''}\n`;
      }
      body += '\n';
    }

    body += memberSection('Properties', t.properties);
    body += memberSection('Methods', t.methods);
    body += memberSection('Fields', t.fields);
    body += memberSection('Events', t.events);

    const outPath = join(OUT_DIR, group, `${slug}.md`);
    mkdirSync(dirname(outPath), { recursive: true });
    writeFileSync(outPath, body.replace(/\n{3,}/g, '\n\n').trimEnd() + '\n', 'utf8');
    pageCount++;
    perNamespace.set(ns, (perNamespace.get(ns) || 0) + 1);

    if (!overview.has(ns)) overview.set(ns, []);
    overview.get(ns).push({ title, slug, group });
  }

  // Write the index/overview page.
  writeIndex(overview);

  // Report.
  console.log(`Generated ${pageCount} type pages.`);
  const nsNames = [...perNamespace.keys()].sort();
  for (const ns of nsNames) {
    console.log(`  ${ns}: ${perNamespace.get(ns)}`);
  }
  console.log(`Output: ${OUT_DIR}`);
}

function writeIndex(overview) {
  let md = '';
  md += '---\n';
  md += 'title: "API reference"\n';
  md += 'description: "Auto-generated API reference for DaisyBlazor, built from the .NET XML documentation."\n';
  md += 'sidebar:\n';
  md += '  order: 0\n';
  md += '---\n\n';
  md +=
    'Auto-generated from the .NET XML documentation comments. Each public type has its own page.\n\n';

  const nsNames = [...overview.keys()].sort();
  for (const ns of nsNames) {
    md += `## ${ns}\n\n`;
    const entries = overview.get(ns).slice().sort((a, b) => a.title.localeCompare(b.title));
    for (const e of entries) {
      const link = `${SITE_BASE}/api/${e.group}/${e.slug}/`;
      md += `- [${e.title}](${link})\n`;
    }
    md += '\n';
  }

  writeFileSync(join(OUT_DIR, 'index.md'), md.trimEnd() + '\n', 'utf8');
}

main();
