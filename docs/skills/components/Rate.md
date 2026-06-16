---
component: Rate
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Rate Skill

## Component Purpose

Rate Component

Primary source directory: `src/BootstrapBlazor/Components/Rate`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Rate/Rate.razor`
- `src/BootstrapBlazor/Components/Rate/Rate.razor.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnValueChanged` | `Func<double, Task>?` | Callback/event parameter; Gets or sets the value changed callback method |
| `ValueChanged` | `EventCallback<double>` | Callback/event parameter; Gets or sets the value changed callback delegate |
| `ItemTemplate` | `RenderFragment<double>?` | Template parameter; verify context type; Gets or sets the item template |
| `IsDisable` | `bool` | Gets or sets whether disabled. Default is false |
| `IsReadonly` | `bool` | Gets or sets whether readonly. Default is false |
| `IsWrap` | `bool` | Gets or sets whether to disable wrap. Default is true |
| `Max` | `int` | Default: `5`; Gets or sets the max value. Default is 5 |
| `ShowValue` | `bool` | Gets or sets whether to show Value. Default is false |
| `StarIcon` | `string?` | Gets or sets the checked icon |
| `UnStarIcon` | `string?` | Gets or sets the unchecked icon |
| `Value` | `double` | Gets or sets the value |

## Events And Callbacks

`OnValueChanged: Func<double, Task>?`, `ValueChanged: EventCallback<double>`

## Templates And Child Content

`ItemTemplate: RenderFragment<double>?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnParametersSet`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Rates.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Rates.razor.cs`

Sample analysis:

- Direct `<Rate>` tag usages detected: 6
- Observed attributes in official Sample: `@bind-Value`, `class`, `IsDisable`, `IsReadonly`, `IsWrap`, `ShowValue`, `Value`, `ValueChanged`
Sample-derived snippet:

```razor
<Rate Value="@BindValue" ValueChanged="@OnValueChanged" />
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

1. Read `src/BootstrapBlazor/Components/Rate` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.