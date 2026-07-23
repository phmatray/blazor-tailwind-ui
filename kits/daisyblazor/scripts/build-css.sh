#!/usr/bin/env bash
# Build the Gallery's Tailwind/daisyUI CSS once (or pass --watch to rebuild on change).
#   ./scripts/build-css.sh
#   ./scripts/build-css.sh --watch
set -euo pipefail
ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
APP="$ROOT/samples/DaisyBlazor.Gallery"

[ -d "$APP/node_modules" ] || ( cd "$APP" && npm install )

if [[ "${1:-}" == "--watch" ]]; then
  ( cd "$APP" && npm run watch:css )
else
  ( cd "$APP" && npm run build:css )
fi
