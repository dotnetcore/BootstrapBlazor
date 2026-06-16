---
component: Checkbox
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Checkbox Skill

## Component Purpose

Checkbox component

Primary source directory: `src/BootstrapBlazor/Components/Checkbox`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Checkbox/Checkbox.razor`
- `src/BootstrapBlazor/Components/Checkbox/Checkbox.razor.cs`
- `src/BootstrapBlazor/Components/Checkbox/Checkbox.razor.js`
- `src/BootstrapBlazor/Components/Checkbox/CheckboxList.razor`
- `src/BootstrapBlazor/Components/Checkbox/CheckboxList.razor.cs`
- `src/BootstrapBlazor/Components/Checkbox/CheckboxListGeneric.razor`
- `src/BootstrapBlazor/Components/Checkbox/CheckboxListGeneric.razor.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `ModelEqualityComparer` | `Func<TValue, TValue, bool>?` | Callback/event parameter; Gets or sets the callback method to compare whether the data is the same. Default is null |
| `OnBeforeStateChanged` | `Func<CheckboxState, Task<bool>>?` | Callback/event parameter; Gets or sets the callback method before the selected state changes. Returning false can prevent the state change |
| `OnMaxSelectedCountExceed` | `Func<Task>?` | Callback/event parameter; Gets or sets the callback delegate when the maximum number of selected items is exceeded |
| `OnSelectedChanged` | `Func<IEnumerable<SelectedItem>, TValue, Task>?` | Callback/event parameter; Gets or sets the SelectedItemChanged method |
| `OnStateChanged` | `Func<CheckboxState, TValue, Task>?` | Callback/event parameter; Gets or sets the callback method when the checkbox state changes |
| `StateChanged` | `EventCallback<CheckboxState>` | Callback/event parameter; Gets or sets the state change callback method |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the child component RenderFragment instance |
| `ItemTemplate` | `RenderFragment<SelectedItem>?` | Template parameter; verify context type; Gets or sets the item template |
| `CheckboxItemClass` | `string?` | Gets or sets the Checkbox component layout style |
| `Color` | `Color` | Gets or sets the button color. Default is None (not set) |
| `CustomKeyAttribute` | `Type?` | Default: `typeof(KeyAttribute)`; Gets or sets the data primary key attribute tag. Default is Used to judge the data primary key tag. If the model does... |
| `IsButton` | `bool` | Gets or sets whether it is a button style. Default is false |
| `IsVertical` | `bool` | Gets or sets whether to arrange vertically. Default is false |
| `Items` | `IEnumerable<SelectedItem<TValue>>?` | Gets or sets the data source |
| `MaxSelectedCount` | `int` | Gets or sets the maximum number of selected items |
| `ShowAfterLabel` | `bool` | Gets or sets whether to show the checkbox post label text. Default is false |
| `ShowBorder` | `bool` | Default: `true`; Gets or sets whether to show the component border in non-button mode. Default is true |
| `ShowButtonBorderColor` | `bool` | Gets or sets whether to show the button border color. Default is false |
| `Size` | `Size` | Gets or sets the size. Default is |
| `State` | `CheckboxState` | Gets or sets the checkbox state |
| `StopPropagation` | `bool` | Gets or sets whether event bubbling. Default is false |

## Events And Callbacks

`ModelEqualityComparer: Func<TValue, TValue, bool>?`, `OnBeforeStateChanged: Func<CheckboxState, Task<bool>>?`, `OnMaxSelectedCountExceed: Func<Task>?`, `OnSelectedChanged: Func<IEnumerable<SelectedItem>, TValue, Task>?`, `OnStateChanged: Func<CheckboxState, TValue, Task>?`, `StateChanged: EventCallback<CheckboxState>`

## Templates And Child Content

`ChildContent: RenderFragment?`, `ItemTemplate: RenderFragment<SelectedItem>?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnParametersSet`, `OnAfterRender`, `OnAfterRenderAsync`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Checkboxs.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Checkboxs.razor.cs`

Sample analysis:

- Direct `<Checkbox>` tag usages detected: 29
- Observed attributes in official Sample: `@bind-Value`, `Color`, `DisplayText`, `IsDisabled`, `OnBeforeStateChanged`, `OnStateChanged`, `ShowAfterLabel`, `ShowLabel`, `ShowLabelTooltip`, `Size`, `State`, `TValue`
Sample-derived snippet:

```razor
<Checkbox TValue="string" OnStateChanged="@OnStateChanged" DisplayText="@Localizer["StatusText1"]" ShowLabel="true" Color="Color.Danger" State="CheckboxState.Checked" />
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

1. Read `src/BootstrapBlazor/Components/Checkbox` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.