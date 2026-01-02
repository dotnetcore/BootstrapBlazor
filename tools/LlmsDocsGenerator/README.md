# LlmsDocsGenerator

A tool that automatically generates LLM-friendly documentation for BootstrapBlazor components.

## Purpose

AI coding assistants (Claude Code, Cursor, GitHub Copilot) often generate incorrect UI code because they lack accurate component API information. This tool solves that problem by:

1. **Auto-generating parameter tables** from source code using Roslyn
2. **Providing GitHub source links** for deeper reference
3. **Integrating with CI/CD** to keep docs synchronized with code

## Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                    LlmsDocsGenerator                        │
├─────────────────────────────────────────────────────────────┤
│  ComponentAnalyzer     → Roslyn-based source code parser    │
│  MarkdownBuilder       → Generates markdown documentation   │
│  DocsGenerator         → Orchestrates the generation flow   │
└─────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────┐
│              Output: wwwroot/llms/                          │
├─────────────────────────────────────────────────────────────┤
│  llms.txt              → Index with quick start guide       │
│  components/           → Individual component documentation │
│    ├── Button.txt      → Button component API reference     │
│    ├── Table.txt       → Table component API reference      │
│    ├── Select.txt      → Select component API reference     │
│    ├── Modal.txt       → Modal component API reference      │
│    └── ...             → One file per component             │
└─────────────────────────────────────────────────────────────┘
```

### Why One File Per Component?

This design optimizes for LLM and Code Agent consumption:

| Aspect | Per-Category (Old) | Per-Component (New) |
|--------|-------------------|---------------------|
| **Precision** | ❌ Loads unrelated components | ✅ Only needed API info |
| **Token Efficiency** | ❌ Wastes tokens on irrelevant data | ✅ Minimal context loading |
| **Cache Friendly** | ❌ Regenerates entire category | ✅ Updates single file |
| **RAG Retrieval** | ❌ Coarse-grained matches | ✅ Fine-grained matches |
| **Incremental Updates** | ❌ Complex CI/CD checks | ✅ Simple file mapping |

## How It Works

### 1. Source Code Analysis

The `ComponentAnalyzer` uses Roslyn to parse C# source files:

```csharp
// Scans for [Parameter] attributes
var parameters = classDeclaration.DescendantNodes()
    .OfType<PropertyDeclarationSyntax>()
    .Where(p => HasAttribute(p, "Parameter"));

// Extracts XML documentation comments
var summary = ExtractXmlSummary(property);
```

### 2. Documentation Generation

The `MarkdownBuilder` creates structured markdown with:

- Parameter tables (name, type, default, description)
- Event callbacks section
- Public methods
- GitHub source links

### 3. Component Organization

Components are organized in the index by category for easy navigation, but each component has its own dedicated documentation file:

| Category | Example Components |
|----------|-------------------|
| table | Table, SelectTable, TableToolbar |
| input | BootstrapInput, Textarea, OtpInput |
| select | Select, AutoComplete, Cascader |
| button | Button, PopConfirmButton |
| dialog | Modal, Drawer, Toast |
| nav | Menu, Tab, Breadcrumb |
| card | Card, Collapse, GroupBox |
| treeview | TreeView, Tree |
| form | ValidateForm, EditorForm |
| other | All other components |

## Installation

### Install as Global Tool

```bash
dotnet pack tools/LlmsDocsGenerator
dotnet tool install --global --add-source ./tools/LlmsDocsGenerator/bin/Release BootstrapBlazor.LlmsDocsGenerator
```

Or install from NuGet (once published):

```bash
dotnet tool install --global BootstrapBlazor.LlmsDocsGenerator
```

### Update Tool

```bash
dotnet tool update --global BootstrapBlazor.LlmsDocsGenerator
```

### Uninstall Tool

```bash
dotnet tool uninstall --global BootstrapBlazor.LlmsDocsGenerator
```

## Usage

Once installed as a global tool, use the `llms-docs` command:

### Generate All Documentation

```bash
llms-docs
```

Or when running from source:

```bash
dotnet run --project tools/LlmsDocsGenerator
```

### Generate Specific Component

```bash
llms-docs --component Table
```

### Generate Index Only

```bash
llms-docs --index-only
```

### Check Freshness (CI/CD)

```bash
llms-docs --check
```

Returns exit code 1 if documentation is outdated.

### Custom Output Directory

```bash
llms-docs --output ./docs
```

### Show Help

```bash
llms-docs --help
```

## CI/CD Integration

### Build Workflow (build.yml)

Checks if documentation is up-to-date on every push to main:

```yaml
- name: Check LLM Documentation
  run: dotnet run --project tools/LlmsDocsGenerator -- --check
```

### Docker Workflow (docker.yml)

Regenerates documentation before building the doc site:

```yaml
- name: Generate LLM Documentation
  run: dotnet run --project tools/LlmsDocsGenerator
```

### Dockerfile

Generates documentation during container build:

```dockerfile
WORKDIR /tools/LlmsDocsGenerator
RUN dotnet run
```

## Output Format

Each component documentation includes:

```markdown
## ComponentName

Description from XML comments

### Type Parameters
- `TItem` - Generic type parameter

### Parameters
| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| Items | `List<TItem>` | - | Data source |
| ShowToolbar | `bool` | false | Show toolbar |

### Event Callbacks
| Event | Type | Description |
|-------|------|-------------|
| OnClick | `EventCallback` | Click handler |

### Public Methods
- `Task RefreshAsync()` - Refresh data

### Source
- Component: [src/.../Component.razor.cs](GitHub URL)
- Examples: [src/.../Samples/Components.razor](GitHub URL)
```

## For Library Users

Users can reference this documentation in their own projects by creating a `llms.txt`:

```markdown
# My Project

## Dependencies

### BootstrapBlazor
- Documentation Index: https://www.blazor.zone/llms/llms.txt
- Button: https://www.blazor.zone/llms/components/Button.txt
- Table: https://www.blazor.zone/llms/components/Table.txt
- Modal: https://www.blazor.zone/llms/components/Modal.txt
```

LLM agents can:
1. First read `llms.txt` to discover available components
2. Then fetch specific `components/{ComponentName}.txt` for detailed API info
