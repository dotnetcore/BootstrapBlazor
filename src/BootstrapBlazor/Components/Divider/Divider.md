---
component: Divider
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Divider Skill

## Component Purpose

Divider Component

Primary source directory: `src/BootstrapBlazor/Components/Divider`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Divider/Divider.razor`
- `src/BootstrapBlazor/Components/Divider/Divider.razor.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Child Content |
| `Alignment` | `Alignment` | Default: `Alignment.Center`; Gets or sets Component Alignment. Default is Center |
| `Icon` | `string?` | Gets or sets Icon |
| `IsVertical` | `bool` | Gets or sets Whether to display vertically. Default is false |
| `Text` | `string?` | Gets or sets Text |

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

- `src/BootstrapBlazor.Server/Components/Samples/Dividers.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Dividers.razor.cs`

Sample analysis:

- Direct `<Divider>` tag usages detected: 18
- Observed attributes in official Sample: `Alignment`, `Icon`, `IsVertical`, `Text`
Sample-derived snippet:

```razor
<Divider />
```

When the Sample and this Skill differ, prefer the current source, then the official Sample, then this Skill.

## Examples

Use the official Sample-derived snippet above as the starting point.

## Common Mistakes

- Do not invent parameters, events, two-way bindings, or template context types.
- Do not copy examples from older BootstrapBlazor versions.
- Do not use obsolete members.
- If a Sample exists, analyze it before relying on Skill examples.
- If no Sample exists, validate every generated parameter against the current source.

## Obsolete Members

No obsolete members were detected in the current source scan.

## Agent Rules

1. Read `src/BootstrapBlazor/Components/Divider` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.