# BootstrapBlazor MCP Server

BootstrapBlazor MCP Server exposes the repository's component source, official Samples, and AI Skill files through HTTP MCP tools.

It enforces the same priority used by `AGENTS.md`:

1. Current component source
2. Official Sample
3. Component Skill

## Build

```powershell
dotnet build .\tools\BootstrapBlazor.Mcp\BootstrapBlazor.Mcp.csproj
```

## Run From Source Repository

```powershell
dotnet run --project .\tools\BootstrapBlazor.Mcp\BootstrapBlazor.Mcp.csproj -- --repo-root D:\Fork\BootstrapBlazor --urls http://127.0.0.1:5178
```

MCP endpoint: `http://127.0.0.1:5178/mcp`

## Run From A Consumer Project

Restore the project first so `obj/project.assets.json` exists.

```powershell
bootstrapblazor-mcp --project-dir D:\Path\To\Your\BlazorProject --urls http://127.0.0.1:5178
```

The server locates the installed `BootstrapBlazor` NuGet package through `obj/project.assets.json`, then reads the package's `skill-index.json`.

## Run From A Package Root

```powershell
bootstrapblazor-mcp --package-root C:\Users\<user>\.nuget\packages\bootstrapblazor\10.7.2-beta03 --urls http://127.0.0.1:5178
```

## Debug Logging

Debug logs are off by default. Add `--log-messages` only for local debugging, and run the server in `Development` environment:

```powershell
$env:ASPNETCORE_ENVIRONMENT = "Development"
dotnet run --project .\tools\BootstrapBlazor.Mcp\BootstrapBlazor.Mcp.csproj -- --repo-root D:\Fork\BootstrapBlazor --urls http://127.0.0.1:5178 --log-messages
```

Increase or reduce the preview size:

```powershell
$env:ASPNETCORE_ENVIRONMENT = "Development"
bootstrapblazor-mcp --project-dir D:\Path\To\Your\BlazorProject --urls http://127.0.0.1:5178 --log-messages --log-preview-chars 2000
```

The log shows JSON-RPC method, tool name, argument preview, response status, file counts, file paths, and warning counts. It does not dump full source/Sample/Skill content unless the preview limit is set very high.

`--log-messages` is ignored outside local debug state. Logs are emitted only when the environment is `Development` or a debugger is attached.

## Tools

- `list_components`: list indexed components.
- `search_components`: search components by name or indexed path.
- `get_component_context`: read source, Sample, and Skill in the required order.
- `get_component_source`: read only component source.
- `get_component_sample`: read only official Sample files.
- `get_component_skill`: read only component Skill.
- `validate_skill_index`: run `scripts/generate-skill-index.ps1 -Check`.
- `check_skill_sync`: run `scripts/check-skill-sync.ps1`.
- `generate_skill_index`: update `skill-index.json`; `dryRun` defaults to `true`.

Validation and generation tools are available only in repository mode.

## Client Configuration

Example HTTP configuration:

```json
{
  "mcpServers": {
    "bootstrapblazor": {
      "type": "http",
      "url": "http://127.0.0.1:5178/mcp"
    }
  }
}
```

Start the server separately for repository development:

```powershell
dotnet run --project D:\Fork\BootstrapBlazor\tools\BootstrapBlazor.Mcp\BootstrapBlazor.Mcp.csproj -- --repo-root D:\Fork\BootstrapBlazor --urls http://127.0.0.1:5178
```

## Tool Package

Create a local tool package:

```powershell
dotnet pack .\tools\BootstrapBlazor.Mcp\BootstrapBlazor.Mcp.csproj -c Release -o .\artifacts\mcp-tool
```

Install from the local package source:

```powershell
dotnet tool install --global BootstrapBlazor.Mcp --add-source .\artifacts\mcp-tool --version 10.7.2-beta03
```
