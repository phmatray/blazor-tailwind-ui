#!/bin/bash
set -e

echo "Building TailwindToolbox..."
dotnet build -c Release TailwindToolbox/TailwindToolbox.csproj

echo "Installing to /usr/local/bin..."
sudo cp TailwindToolbox/bin/Release/net10.0/tailwind-blazor /usr/local/bin/tailwind-blazor
sudo chmod +x /usr/local/bin/tailwind-blazor

echo "✓ Installation complete! Run 'tailwind-blazor --help' to get started."
