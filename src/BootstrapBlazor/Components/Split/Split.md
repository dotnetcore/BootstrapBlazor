---
component: Split
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Split Skill

## Component Purpose

Split Component

Primary source directory: `src/BootstrapBlazor/Components/Split`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Split/Split.razor`
- `src/BootstrapBlazor/Components/Split/Split.razor.cs`
- `src/BootstrapBlazor/Components/Split/Split.razor.js`
- `src/BootstrapBlazor/Components/Split/SplitterResizedEventArgs.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnCollapsedAsync` | `Func<bool, Task>?` | Callback/event parameter; Obsolete; do not use; Gets or sets Callback method when panel is collapsed. parameter bool value true means collapsed, false means second p... |
| `OnResizedAsync` | `Func<SplitterResizedEventArgs, Task>?` | Callback/event parameter; Gets or sets Callback method when panel size changes. Refer to |
| `FirstPaneTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets First Panel Template |
| `SecondPaneTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Second Panel Template |
| `Basis` | `string` | Default: `"50%"`; Gets or sets First panel initial position ratio. Default 50% |
| `FirstPaneMinimumSize` | `string?` | Gets or sets First Panel Minimum Size. Supports any unit e.g. 10px 20% 5em 1rem. Default unit is px |
| `IsCollapsible` | `bool` | Gets or sets Whether to enable collapsible function. Default false |
| `IsKeepOriginalSize` | `bool` | Default: `true`; Gets or sets Whether to keep original size when restoring after enabling . Default true |
| `IsVertical` | `bool` | Gets or sets Whether vertical split |
| `SecondPaneMinimumSize` | `string?` | Gets or sets Second Panel Minimum Size |
| `ShowBarHandle` | `bool` | Default: `true`; Gets or sets Whether to show drag bar. Default true |

## Events And Callbacks

`OnCollapsedAsync: Func<bool, Task>?`, `OnResizedAsync: Func<SplitterResizedEventArgs, Task>?`

## Templates And Child Content

`FirstPaneTemplate: RenderFragment?`, `SecondPaneTemplate: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnAfterRenderAsync`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Splits.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Splits.razor.cs`

Sample analysis:

- Direct `<Split>` tag usages detected: 9
- Observed attributes in official Sample: `@ref`, `Basis`, `FirstPaneMinimumSize`, `IsCollapsible`, `IsVertical`, `OnResizedAsync`, `SecondPaneMinimumSize`, `ShowBarHandle`
Sample-derived snippet:

```razor
<Split ShowBarHandle="_showBarHandle" OnResizedAsync="OnResizedAsync" IsCollapsible="_isCollapsible">
            <FirstPaneTemplate>
                <div class="d-flex justify-content-center align-items-center h-100">@Localizer["SplitsPanel1"]</div>
            </FirstPaneTemplate>
            <SecondPaneTemplate>
                <div class="d-flex justify-content-center align-items-center h-100">@Localizer["SplitsPanel2"]</div>
            </SecondPaneTemplate>
        </Split>
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

1. Read `src/BootstrapBlazor/Components/Split` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.