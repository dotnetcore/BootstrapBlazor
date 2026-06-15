# BootstrapBlazor Consumer Agent Rules

Before generating BootstrapBlazor component code in this project:

1. Read `obj/project.assets.json`.
2. Locate the installed `BootstrapBlazor` package folder.
3. Read `skill-index.json` from that package folder.
4. For the target component, read the indexed `component`, `sample`, and `skill` paths from the package folder.
5. If `sample` exists, analyze the official Sample before relying on Skill examples.

Priority:

1. Current package component source
2. Official Sample
3. Component Skill

Do not generate code from model memory, internet examples, or historical BootstrapBlazor APIs. Do not guess parameters, events, or obsolete APIs.
