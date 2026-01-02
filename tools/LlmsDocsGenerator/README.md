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
│              Output: wwwroot/llms/                       │
├─────────────────────────────────────────────────────────────┤
│  llms.txt              → Index with quick start guide       │
│  llms-table.txt        → Table component documentation      │
│  llms-input.txt        → Input component documentation      │
│  llms-select.txt       → Selection component documentation  │
│  llms-button.txt       → Button component documentation     │
│  llms-dialog.txt       → Dialog component documentation     │
│  llms-nav.txt          → Navigation component documentation │
│  llms-card.txt         → Container component documentation  │
│  llms-treeview.txt     → Tree component documentation       │
│  llms-form.txt         → Form component documentation       │
│  llms-other.txt        → Other component documentation      │
│  llms-example-project.txt → Template for user projects      │
└─────────────────────────────────────────────────────────────┘
```

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

### 3. Component Categorization

Components are automatically grouped into categories:

| Category | Components |
|----------|------------|
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

## Usage

### Generate All Documentation

```bash
dotnet run --project tools/LlmsDocsGenerator
```

### Generate Specific Component

```bash
dotnet run --project tools/LlmsDocsGenerator -- --component Table
```

### Generate Index Only

```bash
dotnet run --project tools/LlmsDocsGenerator -- --index-only
```

### Check Freshness (CI/CD)

```bash
dotnet run --project tools/LlmsDocsGenerator -- --check
```

Returns exit code 1 if documentation is outdated.

### Custom Output Directory

```bash
dotnet run --project tools/LlmsDocsGenerator -- --output ./docs
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
- Documentation: https://www.blazor.zone/llms/llms.txt
- Table: https://www.blazor.zone/llms/llms-table.txt
```

See `llms-example-project.txt` for a complete template.
