---
component: Console
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Console Skill

## Component Purpose

Console message component

Primary source directory: `src/BootstrapBlazor/Components/Console`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Console/Console.razor`
- `src/BootstrapBlazor/Components/Console/Console.razor.cs`
- `src/BootstrapBlazor/Components/Console/Console.razor.js`
- `src/BootstrapBlazor/Components/Console/ConsoleLogger.razor`
- `src/BootstrapBlazor/Components/Console/ConsoleLogger.razor.cs`
- `src/BootstrapBlazor/Components/Console/ConsoleMessageCollection.cs`
- `src/BootstrapBlazor/Components/Console/ConsoleMessageItem.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnClear` | `Func<Task>?` | Callback/event parameter; Gets or sets clear delegate method |
| `FooterTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Footer template |
| `HeaderTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Header template |
| `ItemTemplate` | `RenderFragment<ConsoleMessageItem>?` | Template parameter; verify context type; Gets or sets Item template |
| `AutoScrollText` | `string?` | Gets or sets auto scroll display text |
| `ClearButtonColor` | `Color` | Default: `Color.Secondary`; Gets or sets clear button color, default is Color.Secondary |
| `ClearButtonIcon` | `string?` | Gets or sets button display icon, default is fa-solid fa-xmark |
| `ClearButtonText` | `string?` | Gets or sets button display text, default is Clear |
| `HeaderText` | `string?` | Gets or sets Header display text, default is System Monitor |
| `Height` | `int` | Gets or sets component height, default is 126px |
| `IsAutoScroll` | `bool` | Default: `true`; Gets or sets whether to auto scroll, default is true |
| `IsFlashLight` | `bool` | Default: `true`; Gets or sets whether indicator flashes, default is true(flashing) |
| `IsHtml` | `bool` | Gets or sets whether it is Html code, default is false |
| `Items` | `IEnumerable<ConsoleMessageItem>?` | Gets or sets component data source |
| `LightColor` | `Color` | Default: `Color.Success`; Gets or sets indicator color |
| `LightTitle` | `string?` | Gets or sets indicator Title display text |
| `Max` | `int` | Default: `3`; Gets or sets max rows, default is 3 |
| `ShowAutoScroll` | `bool` | Gets or sets whether to show auto scroll option, default is false |
| `ShowLight` | `bool` | Default: `true`; Gets or sets whether to show indicator, default is true |

## Events And Callbacks

`OnClear: Func<Task>?`

## Templates And Child Content

`FooterTemplate: RenderFragment?`, `HeaderTemplate: RenderFragment?`, `ItemTemplate: RenderFragment<ConsoleMessageItem>?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnParametersSet`, `OnAfterRenderAsync`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Consoles.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Consoles.razor.cs`

Sample analysis:

- Direct `<Console>` tag usages detected: 5
- Observed attributes in official Sample: `Height`, `IsAutoScroll`, `Items`, `OnClear`, `ShowAutoScroll`
Sample-derived snippet:

```razor
<Console Items="@Messages" Height="126" IsAutoScroll="false" />
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

1. Read `src/BootstrapBlazor/Components/Console` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.