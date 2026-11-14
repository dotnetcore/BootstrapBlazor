---
name: bootstrapblazor
description: BootstrapBlazor component API reference shipped with the BootstrapBlazor NuGet package. Use when writing or modifying Blazor/Razor component markup or code, looking up component parameters, events, methods or injectable services, or fixing compile errors involving BootstrapBlazor components. Covers core components and any installed BootstrapBlazor.* extension packages.
---

# BootstrapBlazor API reference

This skill ships inside the `BootstrapBlazor` NuGet package, so the docs always match the installed version. Component docs are generated from source and their body text is in Chinese.

Never guess an API from memory or from a web search. If a parameter, event callback or method is not in these files, say it is not documented instead of inventing it.

## Core components and services

1. Read [references/llms.txt](references/llms.txt) — the index of all core components and injectable services, grouped by category.
2. Read `references/components/<Name>.txt` for the component or service you need. Services live in the same `components/` folder.

## Extension components

Extension components are **not** bundled into this skill. Each `BootstrapBlazor.*` extension package carries its own docs under `docs/llms/` inside that package (a `llms.txt` index plus one `<Name>.txt` per type) and deliberately ships no `SKILL.md`.

To resolve an extension type, stay offline and use the consuming project's restore data:

1. Read `references/extensions/llms.txt`. Each line maps a type name to the package ID that owns it and to the doc path inside that package.
2. Open the **current project's** `obj/project.assets.json`:
   - `packageFolders` lists the NuGet global-packages roots.
   - `libraries` contains an entry keyed `<PackageId>/<Version>`; its `path` value is the package folder relative to the global-packages root and already includes the resolved version. Its `files` array lists that package's `docs/llms/*.txt` files.
3. Build the absolute path as `<global-packages root>/<libraries[...].path>/docs/llms/<Name>.txt` and read it. Start with that package's `docs/llms/llms.txt` to see everything it provides.

If the package ID is absent from `project.assets.json`, the extension package is not installed. Ask the user to add the corresponding `BootstrapBlazor.*` package reference; do not guess its API.

Always read the exact version the project resolved (`libraries[...].path` includes it). Do not pick the highest version sitting in the global cache, and do not fall back to the website docs.

## Conventions

- Match parameter names, types and event callback signatures to the docs exactly.
- For injectable services, follow the registration snippet in the service doc.
- If a type appears in both the core index and the extension route table, prefer the extension doc for that type.
- This skill is read-only reference material. It contains no scripts and requires no commands to be run.
