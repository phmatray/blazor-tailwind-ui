#!/usr/bin/env bash
# Pack all publishable DaisyBlazor NuGet packages into ./artifacts.
#   ./scripts/pack.sh [version]
set -euo pipefail
ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$ROOT"

VERSION="${1:-}"
OUT="$ROOT/artifacts"
mkdir -p "$OUT"

PROPS=()
[ -n "$VERSION" ] && PROPS+=("-p:Version=$VERSION")

for proj in \
  src/DaisyBlazor.Components/DaisyBlazor.Components.csproj \
  src/DaisyBlazor.Charts/DaisyBlazor.Charts.csproj \
  templates/DaisyBlazor.Templates/DaisyBlazor.Templates.csproj
do
  [ -f "$proj" ] || continue
  echo ">> pack $proj"
  dotnet pack "$proj" -c Release -o "$OUT" "${PROPS[@]}"
done

echo "Packages written to $OUT:"
ls -1 "$OUT"/*.nupkg 2>/dev/null || echo "  (none)"
