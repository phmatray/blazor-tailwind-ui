#!/usr/bin/env bash
# ---------------------------------------------------------------------------
# DaisyBlazor — update front-end (Tailwind/daisyUI) and .NET dependencies.
#
#   ./scripts/update-deps.sh            # update npm deps to latest, report .NET outdated
#   ./scripts/update-deps.sh --dotnet   # also bump .NET PackageVersion entries (interactive)
#
# The Tailwind/daisyUI versions are the source of truth in:
#   src/DaisyBlazor.Tailwind/package.json   (the shipped preset package)
#   samples/DaisyBlazor.Gallery/package.json (the showcase app)
# Keep daisyUI in sync with the safelist tested in
#   src/DaisyBlazor.Components/styles/preset.css
# ---------------------------------------------------------------------------
set -euo pipefail

ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$ROOT"

# Packages we manage on the front-end side.
NPM_DEV_PKGS=(tailwindcss @tailwindcss/cli daisyui)

bold() { printf '\033[1m%s\033[0m\n' "$*"; }

update_npm_project() {
  local dir="$1"
  [ -f "$dir/package.json" ] || return 0
  bold ">> npm: updating ${NPM_DEV_PKGS[*]} in $dir"
  ( cd "$dir" && npm install --save-dev "${NPM_DEV_PKGS[@]/%/@latest}" )
}

# 1. Front-end deps in every project that has a package.json.
while IFS= read -r pkg; do
  update_npm_project "$(dirname "$pkg")"
done < <(find . -name package.json -not -path '*/node_modules/*')

# 2. Report the resolved daisyUI version so preset.css / docs can be synced.
DAISY_VER="$(node -e "try{console.log(require('./src/DaisyBlazor.Tailwind/node_modules/daisyui/package.json').version)}catch(e){console.log('unknown')}" 2>/dev/null || echo unknown)"
bold ">> daisyUI resolved version: $DAISY_VER"
echo "   (verify the @plugin \"daisyui\" theme list + safelist in src/DaisyBlazor.Components/styles/preset.css)"

# 3. .NET dependencies (Central Package Management).
bold ">> dotnet: outdated NuGet packages"
dotnet restore DaisyBlazor.sln >/dev/null 2>&1 || true
dotnet list DaisyBlazor.sln package --outdated || true

if [[ "${1:-}" == "--dotnet" ]]; then
  bold ">> Edit Directory.Packages.props to bump the versions listed above, then re-run 'dotnet restore'."
fi

bold "Done. Review changes, run ./scripts/build-css.sh, then 'dotnet build'."
