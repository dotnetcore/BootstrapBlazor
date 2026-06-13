---
component: Waterfall
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Waterfall Skill

## Component Purpose

Waterfall Component

Primary source directory: `src/BootstrapBlazor/Components/Waterfall`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Waterfall/Waterfall.razor`
- `src/BootstrapBlazor/Components/Waterfall/Waterfall.razor.cs`
- `src/BootstrapBlazor/Components/Waterfall/Waterfall.razor.js`
- `src/BootstrapBlazor/Components/Waterfall/WaterfallItem.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnClickItemAsync` | `Func<WaterfallItem, Task>?` | Callback/event parameter; Gets or sets allback method |
| `OnRequestAsync` | `Func<WaterfallItem?, Task<IEnumerable<WaterfallItem>>>?` | Callback/event parameter; Gets or sets datacallback method |
| `ItemTemplate` | `RenderFragment<WaterfallItem>?` | Template parameter; verify context type; Gets or sets template Default is?null |
| `LoadTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets template |
| `Template` | `RenderFragment<(WaterfallItem Item, RenderFragment Context)>?` | Template parameter; verify context type; Gets or sets template Default is?null |
| `ItemMinHeight` | `int` | Default: `316`; Gets or sets idth Default is 316 display loading icon |
| `ItemWidth` | `int` | Default: `216`; Gets or sets idth Default is 216 |

## Events And Callbacks

`OnClickItemAsync: Func<WaterfallItem, Task>?`, `OnRequestAsync: Func<WaterfallItem?, Task<IEnumerable<WaterfallItem>>>?`

## Templates And Child Content

`ItemTemplate: RenderFragment<WaterfallItem>?`, `LoadTemplate: RenderFragment?`, `Template: RenderFragment<(WaterfallItem Item, RenderFragment Context)>?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnAfterRenderAsync`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

No official Sample was matched for `Waterfall`.

Use examples in this Skill only after validating the current source. If the directory contains infrastructure rather than a public Razor component, do not generate direct Razor usage.

## Examples

Source-validated skeleton:

```razor
<Waterfall>
</Waterfall>
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

1. Read `src/BootstrapBlazor/Components/Waterfall` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.