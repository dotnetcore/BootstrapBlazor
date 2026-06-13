---
component: Tooltip
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Tooltip Skill

## Component Purpose

ITooltip Interface

Primary source directory: `src/BootstrapBlazor/Components/Tooltip`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Tooltip/ITooltip.cs`
- `src/BootstrapBlazor/Components/Tooltip/Tooltip.razor`
- `src/BootstrapBlazor/Components/Tooltip/Tooltip.razor.cs`
- `src/BootstrapBlazor/Components/Tooltip/Tooltip.razor.js`
- `src/BootstrapBlazor/Components/Tooltip/TooltipWrapperBase.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `GetTitleCallback` | `Func<Task<string>>?` | Callback/event parameter; Gets or sets the callback method to get display content asynchronously. Default is null |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the child component |
| `CustomClass` | `string?` | Verify current source before use |
| `Delay` | `string?` | Verify current source before use |
| `FallbackPlacements` | `string[]?` | Gets or sets the placement. Default is null |
| `IsHtml` | `bool` | Verify current source before use |
| `Offset` | `string?` | Gets or sets the offset. Default is null |
| `Placement` | `Placement` | Default: `Placement.Top` |
| `Sanitize` | `bool` | Default: `true` |
| `Selector` | `string?` | Verify current source before use |
| `Title` | `string?` | Verify current source before use |
| `TooltipPlacement` | `Placement` | Default: `Placement.Top`; Gets or sets the Tooltip display position. Default is Top |
| `TooltipText` | `string?` | Gets or sets the Tooltip display text. Default is null |
| `TooltipTrigger` | `string?` | Gets or sets the Tooltip trigger method. Default is hover focus |
| `Trigger` | `string?` | Verify current source before use |

## Events And Callbacks

`GetTitleCallback: Func<Task<string>>?`

## Templates And Child Content

`ChildContent: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnParametersSet`, `OnParametersSetAsync`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Tooltips.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Tooltips.razor.cs`

Sample analysis:

- Direct `<Tooltip>` tag usages detected: 12
- Observed attributes in official Sample: `@ref`, `CustomClass`, `IsHtml`, `Placement`, `Sanitize`, `Selector`, `Title`, `Trigger`
Sample-derived snippet:

```razor
<Tooltip Placement="Placement.Bottom" Title="Tooltip">
                <BootstrapInput Value="@BottomString" aria-label="@BottomString" />
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

1. Read `src/BootstrapBlazor/Components/Tooltip` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.