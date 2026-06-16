# BootstrapBlazor LLMs Docs Generator

Generates `wwwroot/llms/llms.txt` and per-component files under `wwwroot/llms/components`.

The generator reads local repository data only:

- `skill-index.json`
- `src/BootstrapBlazor/Components`
- official Samples listed in `skill-index.json`
- existing Skill paths as references

It does not overwrite `docs/skills/components/*.md`. Manual Skill maintenance through `docs/bootstrapblazor-skill-maintenance-prompt.md` remains supported.

## Run

```powershell
dotnet run --project .\tools\BootstrapBlazor.LLMsDocsGenerator\BootstrapBlazor.LLMsDocsGenerator.csproj -- --repo-root . --output .\artifacts\llms-docs
```

Generate one component:

```powershell
dotnet run --project .\tools\BootstrapBlazor.LLMsDocsGenerator\BootstrapBlazor.LLMsDocsGenerator.csproj -- --repo-root . --output .\artifacts\llms-docs --component Table
```

The Server publish target runs this project automatically after `Publish`.
