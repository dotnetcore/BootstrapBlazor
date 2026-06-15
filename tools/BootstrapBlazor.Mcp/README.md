# BootstrapBlazor MCP

BootstrapBlazor MCP is an HTTP MCP server for BootstrapBlazor component development.

It exposes component source, official Samples, and AI Skill files from `skill-index.json`, preserving this priority:

1. Current component source
2. Official Sample
3. Component Skill

Run against a restored consumer project:

```powershell
bootstrapblazor-mcp --project-dir D:\Path\To\BlazorProject --urls http://127.0.0.1:5178
```

Run against the BootstrapBlazor source repository:

```powershell
bootstrapblazor-mcp --repo-root D:\Fork\BootstrapBlazor --urls http://127.0.0.1:5178
```

MCP endpoint:

```text
http://127.0.0.1:5178/mcp
```

See `docs/bootstrapblazor-mcp.md` in the BootstrapBlazor repository for full client configuration.
