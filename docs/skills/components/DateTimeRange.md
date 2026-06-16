---
component: DateTimeRange
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# DateTimeRange Skill

## Component Purpose

DateTimeRange component

Primary source directory: `src/BootstrapBlazor/Components/DateTimeRange`.

Source files reviewed:

- `src/BootstrapBlazor/Components/DateTimeRange/DateTimeRange.razor`
- `src/BootstrapBlazor/Components/DateTimeRange/DateTimeRange.razor.cs`
- `src/BootstrapBlazor/Components/DateTimeRange/DateTimeRangeSidebarItem.cs`
- `src/BootstrapBlazor/Components/DateTimeRange/DateTimeRangeValue.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnClearValue` | `Func<DateTimeRangeValue, Task>?` | Callback/event parameter; Callback Method When Clear Button Clicked |
| `OnConfirm` | `Func<DateTimeRangeValue, Task>?` | Callback/event parameter; Callback Method When Confirm Button Clicked |
| `OnDateClick` | `Func<DateTime, Task>?` | Callback/event parameter; Gets or sets the date value changed event callback |
| `AutoClose` | `bool` | Gets or sets whether to automatically close the popup after a date range is selected. Default is false |
| `AutoCloseClickSideBar` | `bool` | Gets or sets Whether to Auto Close Popup When Sidebar Item Clicked. Default is false |
| `ClearButtonText` | `string?` | Gets or sets Clear Button Text |
| `ClearIcon` | `string?` | Gets or sets Clear Icon. Default is fa-solid fa-circle-xmark |
| `ConfirmButtonText` | `string?` | Gets or sets Confirm Button Text |
| `DateFormat` | `string?` | Gets or sets Date Format String. Default is "yyyy-MM-dd" |
| `DateTimeFormat` | `string?` | Gets or sets Date Time Format String. Default is "yyyy-MM-dd HH:mm:ss" |
| `Icon` | `string?` | Gets or sets Component Icon |
| `IsEditable` | `bool` | Gets or sets Whether to Allow Edit. Default is false |
| `MaxValue` | `DateTime` | Default: `DateTime.MaxValue`; Gets or sets Max Value |
| `MinValue` | `DateTime` | Default: `DateTime.MinValue`; Gets or sets Min Value |
| `RenderMode` | `DateTimeRangeRenderMode` | Default: `DateTimeRangeRenderMode.Double`; Gets or sets Component Display Mode. Default is Date Mode |
| `ShowClearButton` | `bool` | Default: `true`; Gets or sets Whether to Show Clear Button. Default is true |
| `ShowFestivals` | `bool` | Gets or sets Whether to Show Festivals. Default is false |
| `ShowHolidays` | `bool` | Gets or sets Whether to Show Holidays. Default is false |
| `ShowLunar` | `bool` | Gets or sets Whether to Show Chinese Lunar Calendar. Default is false |
| `ShowSelectedValue` | `bool` | Gets or sets whether show the selected value. Default is false |
| `ShowSidebar` | `bool` | Gets or sets Whether to Show Sidebar. Default is not shown |
| `ShowSolarTerm` | `bool` | Gets or sets Whether to Show Chinese Solar Term. Default is false |
| `ShowToday` | `bool` | Gets or sets Whether to Show Today Button. Default is false |
| `SidebarItems` | `List<DateTimeRangeSidebarItem>?` | Gets or sets Sidebar Items |
| `TimeFormat` | `string?` | Gets or sets Time Format String. Default is "HH:mm:ss" |
| `TodayButtonText` | `string?` | Gets or sets Today Button Text |
| `ViewMode` | `DatePickerViewMode` | Default: `DatePickerViewMode.Date`; Gets or sets Component Display Mode. Default is Date Mode |

## Events And Callbacks

`OnClearValue: Func<DateTimeRangeValue, Task>?`, `OnConfirm: Func<DateTimeRangeValue, Task>?`, `OnDateClick: Func<DateTime, Task>?`

## Templates And Child Content

No RenderFragment parameters were detected in the current source scan.

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnParametersSet`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/DateTimeRanges.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/DateTimeRanges.razor.cs`

Sample analysis:

- Direct `<DateTimeRange>` tag usages detected: 11
- Observed attributes in official Sample: `@bind-Value`, `AutoCloseClickSideBar`, `DateFormat`, `IsDisabled`, `IsEditable`, `OnConfirm`, `OnValueChanged`, `RenderMode`, `ShowSidebar`, `ShowToday`, `Value`, `ViewMode`
Sample-derived snippet:

```razor
<DateTimeRange @bind-Value="@NormalDateTimeRangeValue" OnConfirm="OnNormalConfirm" ShowSidebar="true" ViewMode="DatePickerViewMode.Year" IsEditable="true" DateFormat="yyyy">
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

1. Read `src/BootstrapBlazor/Components/DateTimeRange` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.