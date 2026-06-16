---
component: TimePicker
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# TimePicker Skill

## Component Purpose

TimePicker Component

Primary source directory: `src/BootstrapBlazor/Components/TimePicker`.

Source files reviewed:

- `src/BootstrapBlazor/Components/TimePicker/TimePicker.razor`
- `src/BootstrapBlazor/Components/TimePicker/TimePicker.razor.cs`
- `src/BootstrapBlazor/Components/TimePicker/TimePickerCell.razor`
- `src/BootstrapBlazor/Components/TimePicker/TimePickerCell.razor.cs`
- `src/BootstrapBlazor/Components/TimePicker/TimePickerCell.razor.js`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnClose` | `Func<Task>?` | Callback/event parameter; Gets or sets the cancel button callback delegate |
| `OnConfirm` | `Func<TimeSpan, Task>?` | Callback/event parameter; Gets or sets the confirm button callback delegate |
| `ValueChanged` | `EventCallback<TimeSpan>` | Callback/event parameter; Gets or sets the delegate method when the component value changes |
| `CancelButtonText` | `string?` | Gets or sets the cancel button display text |
| `ConfirmButtonText` | `string?` | Gets or sets the confirm button display text |
| `DownIcon` | `string?` | Gets or sets the down arrow icon |
| `HasSeconds` | `bool` | Default: `true`; Gets or sets whether to display seconds. Default is true |
| `UpIcon` | `string?` | Gets or sets the up arrow icon |
| `Value` | `TimeSpan` | Gets or sets the component value |
| `ViewMode` | `TimePickerCellViewMode` | Gets or sets the time picker view mode |

## Events And Callbacks

`OnClose: Func<Task>?`, `OnConfirm: Func<TimeSpan, Task>?`, `ValueChanged: EventCallback<TimeSpan>`

## Templates And Child Content

No RenderFragment parameters were detected in the current source scan.

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnParametersSet`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/TimePickers.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/TimePickers.razor.cs`

Sample analysis:

- Direct `<TimePicker>` tag usages detected: 2
- Observed attributes in official Sample: `@bind-Value`, `HasSeconds`, `OnConfirm`, `Value`
Sample-derived snippet:

```razor
<TimePicker Value="@Value" OnConfirm="OnConfirm" />
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

1. Read `src/BootstrapBlazor/Components/TimePicker` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.