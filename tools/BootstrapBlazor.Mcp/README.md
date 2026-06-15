# BootstrapBlazor MCP

BootstrapBlazor MCP is an HTTP MCP server for BootstrapBlazor component development.

It exposes component source, official Samples, and AI Skill files from `skill-index.json`, preserving this priority:

1. Current component source
2. Official Sample
3. Component Skill

Run against the BootstrapBlazor source repository:

```powershell
bootstrapblazor-mcp --repo-root D:\Fork\BootstrapBlazor --urls http://127.0.0.1:5178
```

MCP endpoint:

```text
http://127.0.0.1:5178/mcp
```

Request and response summary logs are off by default. Enable them only for local debugging:

```powershell
$env:ASPNETCORE_ENVIRONMENT = "Development"
bootstrapblazor-mcp --repo-root D:\Fork\BootstrapBlazor --urls http://127.0.0.1:5178 --log-messages --log-preview-chars 2000
```

`--log-messages` is ignored unless the environment is `Development` or a debugger is attached.

See `docs/bootstrapblazor-mcp.md` in the BootstrapBlazor repository for full client configuration.
