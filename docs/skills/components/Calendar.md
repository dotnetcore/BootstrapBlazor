---
component: Calendar
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Calendar Skill

## Component Purpose

BodyTemplateContext context class

Primary source directory: `src/BootstrapBlazor/Components/Calendar`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Calendar/BodyTemplateContext.cs`
- `src/BootstrapBlazor/Components/Calendar/Calendar.razor`
- `src/BootstrapBlazor/Components/Calendar/Calendar.razor.cs`
- `src/BootstrapBlazor/Components/Calendar/CalendarCellValue.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnValueChanged` | `Func<DateTime, Task>?` | Callback/event parameter; Gets or sets the value change callback delegate |
| `ValueChanged` | `EventCallback<DateTime>` | Callback/event parameter; Gets or sets the value change callback delegate |
| `BodyTemplate` | `RenderFragment<BodyTemplateContext>?` | Template parameter; verify context type; Gets or sets the body template. Valid only when |
| `CellTemplate` | `RenderFragment<CalendarCellValue>?` | Template parameter; verify context type; Gets or sets the cell template |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the week content. Valid when |
| `HeaderTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the header template |
| `FirstDayOfWeek` | `DayOfWeek` | Default: `DayOfWeek.Sunday`; Gets or sets the first day of the week. Default is |
| `ShowYearButtons` | `bool` | Default: `true`; Gets or sets whether to show the year buttons |
| `Value` | `DateTime` | Gets or sets the component value |
| `ViewMode` | `CalendarViewMode` | Gets or sets whether to display the week view. Default is month view |

## Events And Callbacks

`OnValueChanged: Func<DateTime, Task>?`, `ValueChanged: EventCallback<DateTime>`

## Templates And Child Content

`BodyTemplate: RenderFragment<BodyTemplateContext>?`, `CellTemplate: RenderFragment<CalendarCellValue>?`, `ChildContent: RenderFragment?`, `HeaderTemplate: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Calendars.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Calendars.razor.cs`

Sample analysis:

- Direct `<Calendar>` tag usages detected: 7
- Observed attributes in official Sample: `@bind-Value`, `FirstDayOfWeek`, `ValueChanged`, `ViewMode`
Sample-derived snippet:

```razor
<Calendar ValueChanged="@OnValueChanged" FirstDayOfWeek="DayOfWeek.Monday" />
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

1. Read `src/BootstrapBlazor/Components/Calendar` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.