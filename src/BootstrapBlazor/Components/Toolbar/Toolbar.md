---
component: Toolbar
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Toolbar Skill

## Component Purpose

Toolbar Component for displaying toolbar content

Primary source directory: `src/BootstrapBlazor/Components/Toolbar`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Toolbar/Toolbar.razor`
- `src/BootstrapBlazor/Components/Toolbar/Toolbar.razor.cs`
- `src/BootstrapBlazor/Components/Toolbar/ToolbarButtonGroup.razor`
- `src/BootstrapBlazor/Components/Toolbar/ToolbarButtonGroup.razor.cs`
- `src/BootstrapBlazor/Components/Toolbar/ToolbarItem.razor`
- `src/BootstrapBlazor/Components/Toolbar/ToolbarItem.razor.cs`
- `src/BootstrapBlazor/Components/Toolbar/ToolbarSeparator.razor`
- `src/BootstrapBlazor/Components/Toolbar/ToolbarSeparator.razor.cs`
- `src/BootstrapBlazor/Components/Toolbar/ToolbarSpace.razor`
- `src/BootstrapBlazor/Components/Toolbar/ToolbarSpace.razor.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the child component template |
| `IsWrap` | `bool` | Gets or sets whether to allow toolbar content wrapping. Default is false |

## Events And Callbacks

No callback/event parameters were detected in the current source scan.

## Templates And Child Content

`ChildContent: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: None detected in current source.
- JS interop or module dependency detected: `False`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Toolbars.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Toolbars.razor.cs`

Sample analysis:

- Direct `<Toolbar>` tag usages detected: 1
- Observed attributes in official Sample: `IsWrap`

When the Sample and this Skill differ, prefer the current source, then the official Sample, then this Skill.

## Examples

Source-validated skeleton:

```razor
<Toolbar>
</Toolbar>
```

This skeleton is not a substitute for source validation. Add only parameters that exist in the current source.

## Common Mistakes

- Do not invent parameters, events, two-way bindings, or template context types.
- Do not copy examples from older BootstrapBlazor versions.
- Do not use obsolete members.
- If a Sample exists, analyze it before relying on Skill examples.
- If no Sample exists, validate every generated parameter against the current source.

## Obsolete Members

No obsolete members were detected in the current source scan.

## Agent Rules

1. Read `src/BootstrapBlazor/Components/Toolbar` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.