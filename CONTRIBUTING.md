# Contributing & releasing

## Branch strategy

A deliberately simple two-branch model:

| Branch | Role |
|--------|------|
| **`main`** | Default, always-green integration branch. All work lands here via PR. The [`ci`](.github/workflows/ci.yml) workflow builds + tests every push and PR. |
| **`release`** | The publish trigger. Pushing to it runs the [`publish`](.github/workflows/publish.yml) workflow, which packs and ships the version currently in `Directory.Build.props`. |

Feature work uses short-lived branches off `main`, named by intent:

```
feat/<thing>     # new feature
fix/<thing>      # bug fix
chore/<thing>    # tooling, docs, deps
```

Open a PR into `main`; merge once CI is green.

## Versioning

The version is single-sourced in **`Directory.Build.props`** (`<Version>`), so all NuGet
packages release in lockstep. Keep **`src/DaisyBlazor.Tailwind/package.json`** (the npm preset)
on the same version by hand.

## Cutting a release

1. On `main`, bump `<Version>` in `Directory.Build.props` (and the `version` in
   `src/DaisyBlazor.Tailwind/package.json`). Commit via PR.
2. Fast-forward the `release` branch to that commit:
   ```bash
   git push origin main:release
   ```
3. The **publish** workflow then:
   - runs the tests,
   - packs `DaisyBlazor.Components`, `DaisyBlazor.Charts`, and `DaisyBlazor.Templates`,
   - pushes them to NuGet (`--skip-duplicate`, so re-runs are safe),
   - publishes `@daisyblazor/tailwind` to npm (only if the `NPM_TOKEN` secret is set),
   - tags `v<version>` and creates a GitHub release with the `.nupkg` files attached.

`workflow_dispatch` is also enabled, so you can trigger a publish manually from the Actions tab.

## Required repository secrets

Set these under **Settings → Secrets and variables → Actions**:

| Secret | Used for | Notes |
|--------|----------|-------|
| `NUGET_API_KEY` | `dotnet nuget push` | nuget.org API key with **Push → "Push new packages and package versions"** and glob pattern `*` (or `DaisyBlazor.*`). |
| `NPM_TOKEN` | `npm publish` | npm **automation** access token with publish rights to the `@daisyblazor` scope. Optional — the npm step is skipped when it isn't set. |

## Local checks before pushing

```bash
dotnet build           # builds everything incl. the gallery CSS
dotnet test            # bUnit component tests
./scripts/build-css.sh # (re)compile the gallery stylesheet
./scripts/pack.sh 0.1.1 # pack locally into ./artifacts
```
