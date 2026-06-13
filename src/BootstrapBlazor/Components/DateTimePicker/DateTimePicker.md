---
component: DateTimePicker
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# DateTimePicker Skill

## Component Purpose

Date Picker component

Primary source directory: `src/BootstrapBlazor/Components/DateTimePicker`.

Source files reviewed:

- `src/BootstrapBlazor/Components/DateTimePicker/DatePickerBody.razor`
- `src/BootstrapBlazor/Components/DateTimePicker/DatePickerBody.razor.cs`
- `src/BootstrapBlazor/Components/DateTimePicker/DatePickerCell.razor`
- `src/BootstrapBlazor/Components/DateTimePicker/DatePickerCell.razor.cs`
- `src/BootstrapBlazor/Components/DateTimePicker/DateTimePicker.razor`
- `src/BootstrapBlazor/Components/DateTimePicker/DateTimePicker.razor.cs`
- `src/BootstrapBlazor/Components/DateTimePicker/DateTimePicker.razor.js`
- `src/BootstrapBlazor/Components/DateTimePicker/PickTimeMode.cs`
- `src/BootstrapBlazor/Components/DateTimePicker/PopoverDropdownBase.cs`
- `src/BootstrapBlazor/Components/DateTimePicker/TimePickerOption.cs`
- `src/BootstrapBlazor/Components/DateTimePicker/TimePickerSetting.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnBlurAsync` | `Func<TValue, Task>?` | Callback/event parameter; Gets or sets OnBlur Callback Method. Default is null |
| `OnClear` | `Func<Task>?` | Callback/event parameter; Gets or sets Clear Button Callback Delegate |
| `OnClick` | `Func<DateTime, Task>?` | Callback/event parameter; Gets or sets Button Click Callback Method. Default is null |
| `OnConfirm` | `Func<Task>?` | Callback/event parameter; Gets or sets Confirm Button Callback Delegate |
| `OnDateChanged` | `Func<DateTime, Task>?` | Callback/event parameter; Gets or sets Callback Method When Year/Month Changed |
| `OnGetDisabledDaysCallback` | `Func<DateTime, DateTime, Task<List<DateTime>>>?` | Callback/event parameter; Gets or sets Callback Method to Get Custom Disabled Days of Month. Default is null. Internal Default Enable Data Cach... |
| `SidebarTemplate` | `RenderFragment<Func<DateTime, Task>>?` | Callback/event parameter; Template parameter; verify context type; Gets or sets Sidebar Template. Default is null |
| `ValueChanged` | `EventCallback<DateTime>` | Callback/event parameter; Gets or sets Value Changed Callback Delegate for Two-Way Binding |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Child Content Template |
| `DayDisabledTemplate` | `RenderFragment<DateTime>?` | Template parameter; verify context type; Gets or sets Disabled Day Cell Template |
| `DayTemplate` | `RenderFragment<DateTime>?` | Template parameter; verify context type; Gets or sets Day Cell Template |
| `Template` | `RenderFragment<DateTime>?` | Template parameter; verify context type; Gets or sets Cell Template. Default is null |
| `AutoClose` | `bool` | Gets or sets Whether to Auto Close Popup When Date Clicked. Default is false |
| `AutoToday` | `bool` | Default: `true`; Gets or sets Whether to Auto Set Value to Current Time. Default is true |
| `ButtonColor` | `Color` | Default: `Color.Primary`; Gets or sets Button Color. Default is |
| `ClearButtonText` | `string?` | Gets or sets Clear Button Text |
| `Color` | `Color` | Default: `Color.None`; Gets or sets Component Border Color Style. Default is None |
| `ConfirmButtonText` | `string?` | Gets or sets Confirm Button Text |
| `CustomClass` | `string?` | Gets or sets Custom Class. Default is null |
| `DateFormat` | `string?` | Gets or sets Date Format String. Default is null |
| `DatePlaceHolder` | `string?` | Gets or sets Date Placeholder String |
| `DatePlaceHolderText` | `string?` | Gets or sets Date Placeholder Text. Default is null. Read from resource file |
| `DateTimeFormat` | `string?` | Gets or sets Date Time Format String. Default is "yyyy-MM-dd HH:mm:ss" |
| `DateTimePlaceHolderText` | `string?` | Gets or sets Date Time Placeholder Text. Default is null. Read from resource file |
| `DisplayDisabledDayAsEmpty` | `bool` | Gets or sets Whether to Display Disabled Day as Empty. Default is false. When enabled, component will frequently call... |
| `DisplayMinValueAsEmpty` | `bool` | Default: `true`; Gets or sets Whether to Display as Empty String. Default is true |
| `EnableDisabledDaysCache` | `bool` | Default: `true`; Gets or sets Whether to Enable Custom Disabled Days Cache of Year |
| `FirstDayOfWeek` | `DayOfWeek` | Default: `DayOfWeek.Sunday`; Gets or sets First Day of Week. Default is |
| `Icon` | `string?` | Gets or sets Component Icon. Default is fa-regular fa-calendar-days |
| `IsAutoSwitch` | `bool` | Default: `true`; Whether to Auto Switch Hour, Minute, Second. Default is true |
| `IsButton` | `bool` | Gets or sets Whether to Show as Button. Default is false |
| `IsEditable` | `bool` | Gets or sets Whether to Allow Edit. Default is false |
| `MaxValue` | `DateTime?` | Gets or sets Max Date |
| `MinValue` | `DateTime?` | Gets or sets Min Date |
| `NextMonthIcon` | `string?` | Gets or sets Next Month Icon |
| `NextYearIcon` | `string?` | Gets or sets Next Year Icon |
| `NowButtonText` | `string?` | Gets or sets Now Button Text |
| `PickerButtonText` | `string?` | Gets or sets Picker Button Text. Default is null. Read from resource file |
| `PickTimeMode` | `PickTimeMode` | Default: `PickTimeMode.Dropdown`; Gets or sets Pick Time Mode. Default is |
| `Placement` | `Placement` | Default: `Placement.Bottom`; Gets or sets Popover Placement. Default is Bottom |

Only the first 40 important parameters are listed. Inspect the current source for the remaining 21 parameters before generating code.

## Events And Callbacks

`OnBlurAsync: Func<TValue, Task>?`, `OnClear: Func<Task>?`, `OnClick: Func<DateTime, Task>?`, `OnConfirm: Func<Task>?`, `OnDateChanged: Func<DateTime, Task>?`, `OnGetDisabledDaysCallback: Func<DateTime, DateTime, Task<List<DateTime>>>?`, `SidebarTemplate: RenderFragment<Func<DateTime, Task>>?`, `ValueChanged: EventCallback<DateTime>`

## Templates And Child Content

`ChildContent: RenderFragment?`, `DayDisabledTemplate: RenderFragment<DateTime>?`, `DayTemplate: RenderFragment<DateTime>?`, `SidebarTemplate: RenderFragment<Func<DateTime, Task>>?`, `Template: RenderFragment<DateTime>?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnParametersSet`, `OnParametersSetAsync`, `OnAfterRenderAsync`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/DateTimePickers.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/DateTimePickers.razor.cs`

Sample analysis:

- Direct `<DateTimePicker>` tag usages detected: 22
- Observed attributes in official Sample: `@bind-Value`, `@ref`, `AutoClose`, `CustomClass`, `DateFormat`, `DateTimeFormat`, `DisplayDisabledDayAsEmpty`, `DisplayText`, `IsButton`, `IsDisabled`, `IsEditable`, `MaxValue`, `MinValue`, `OnGetDisabledDaysCallback`, `OnValueChanged`, `ShowIcon`, `ShowLabel`, `ShowSidebar`, `TimeFormat`, `TValue`, `Value`, `ViewMode`
Sample-derived snippet:

```razor
<DateTimePicker ViewMode="DatePickerViewMode.DateTime" TimeFormat="hh\:mm" IsButton="_isButton"
					Value="@DateTimePickerValue" OnValueChanged="@TimePickerValueChanged">
		<TimePickerSetting ShowClockScale="true" IsAutoSwitch="false" />
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

1. Read `src/BootstrapBlazor/Components/DateTimePicker` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.