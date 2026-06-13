# BootstrapBlazor Agent Rules

Before generating BootstrapBlazor component code, read `skill-index.json`.

Read order:

1. Read the current component source: `src/BootstrapBlazor/Components/<ComponentName>`.
2. If the index contains `sample`, read the official Sample before Skill examples.
3. If the index contains `skill`, read the component Skill.
4. If no Sample exists, use Skill examples only after validating the current source.

Conflict priority:

1. Current source
2. Official Sample
3. Component Skill

Do not generate code only from model memory, internet examples, or historical BootstrapBlazor APIs. Do not guess parameters, events, or obsolete APIs.
