---
component: Select
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Select Skill

## Component Purpose

ISelect Interface

Primary source directory: `src/BootstrapBlazor/Components/Select`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Select/InternalSelectObjectContext.cs`
- `src/BootstrapBlazor/Components/Select/ISelect.cs`
- `src/BootstrapBlazor/Components/Select/ISelectObjectContext.cs`
- `src/BootstrapBlazor/Components/Select/MultiSelect.razor`
- `src/BootstrapBlazor/Components/Select/MultiSelect.razor.cs`
- `src/BootstrapBlazor/Components/Select/MultiSelect.razor.js`
- `src/BootstrapBlazor/Components/Select/PopoverSelectBase.cs`
- `src/BootstrapBlazor/Components/Select/Select.razor`
- `src/BootstrapBlazor/Components/Select/Select.razor.cs`
- `src/BootstrapBlazor/Components/Select/Select.razor.js`
- `src/BootstrapBlazor/Components/Select/SelectBase.cs`
- `src/BootstrapBlazor/Components/Select/SelectObject.razor`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `ChildContent` | `RenderFragment<ISelectObjectContext<TItem>>?` | Required; Template parameter; verify context type; Gets or sets Dropdown Content Template |
| `GetTextCallback` | `Func<TItem, string?>?` | Required; Callback/event parameter; Get Display Text Callback Method. Default null |
| `Items` | `List<TreeViewItem<TValue>>?` | Required; Gets or sets Hierarchical Data Collection |
| `OnQueryAsync` | `Func<QueryPageOptions, Task<QueryData<TItem>>>?` | Required; Callback/event parameter; Async Query Callback Method |
| `ModelEqualityComparer` | `Func<TValue, TValue, bool>?` | Callback/event parameter; Gets or sets Model Equality Comparer. Default null. Ignore when providing this callback |
| `OnBeforeSelectedItemChange` | `Func<SelectedItem, Task<bool>>?` | Callback/event parameter; Gets or sets the callback method before the selected item changes. Returns true to change the selected item value; ot... |
| `OnClearAsync` | `Func<Task>?` | Callback/event parameter; Gets or sets the callback method when the clear button is clicked. Default is null |
| `OnCollapsed` | `Func<Task>?` | Callback/event parameter; Gets or sets the dropdown collapsed callback method |
| `OnEditCallback` | `Func<string, Task<SelectedItem>>?` | Callback/event parameter; Gets or sets Callback method after input option updated in edit mode. Default null. Return instance to take effect, r... |
| `OnExpandNodeAsync` | `Func<TreeViewItem<TValue>, Task<IEnumerable<TreeViewItem<TValue>>>>?` | Callback/event parameter; Gets or sets OnExpandNodeAsync Callback Method |
| `OnInputChangedCallback` | `Func<string, Task>?` | Callback/event parameter; Gets or sets the callback method when the input value changes. Default is null |
| `OnSearchAsync` | `Func<string?, Task<List<TreeViewItem<TValue>>?>>?` | Callback/event parameter; Gets or sets the search callback method. Default is null |
| `OnSearchTextChanged` | `Func<string, IEnumerable<SelectedItem>>?` | Callback/event parameter; Gets or sets the callback method when the search text changes |
| `OnSelectedItemChanged` | `Func<SelectedItem, Task>?` | Callback/event parameter; Gets or sets the callback method when the selected item changes |
| `OnSelectedItemsChanged` | `Func<IEnumerable<SelectedItem>, Task>?` | Callback/event parameter; Gets or sets Selected Items Changed Callback Method |
| `SelectedItemsChanged` | `EventCallback<List<TItem>>` | Callback/event parameter; Gets or sets the callback method when selected items collection changes in multiple selection mode |
| `ButtonTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Extension Button Template |
| `CustomerSearchTemplate` | `RenderFragment<ITableSearchModel>?` | Template parameter; verify context type; Gets or sets Custom Search Model Template |
| `DisplayTemplate` | `RenderFragment<List<SelectedItem>>?` | Template parameter; verify context type; Gets or sets Display Template. Default null |
| `EmptyTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Empty Template. Default null |
| `GroupItemTemplate` | `RenderFragment<string>?` | Template parameter; verify context type; Gets or sets the group item template |
| `ItemTemplate` | `RenderFragment<SelectedItem>?` | Template parameter; verify context type; Gets or sets the item template |
| `Options` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the options template for static data |
| `SearchTemplate` | `RenderFragment<TItem>?` | Template parameter; verify context type; Gets or sets SearchTemplate Instance |
| `TableColumns` | `RenderFragment<TItem>?` | Template parameter; verify context type; Gets or sets TableHeader Instance |
| `TableExtensionToolbarTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the table toolbar right-side button template, content appears after the default buttons |
| `Template` | `RenderFragment<TItem>?` | Template parameter; verify context type; Gets or sets Value Display Template. Default null |
| `ToolbarTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the table toolbar template, content appears center of toolbar |
| `Active` | `bool` | Gets or sets a value indicating whether the option is selected. Default is false |
| `AutoGenerateColumns` | `bool` | Gets or sets Whether to auto generate columns. Default false |
| `CanExpandWhenDisabled` | `bool` | Default: `false`; Gets or sets whether nodes can be expanded or collapsed when the component is disabled. Default is false |
| `ClearIcon` | `string?` | Gets or sets Clear Icon. Default fa-solid fa-angle-up |
| `ClearText` | `string?` | Gets or sets Clear Text |
| `CloseButtonIcon` | `string?` | Gets or sets Close Button Icon. Default null |
| `CollapsedTopSearch` | `bool` | Gets or sets Whether to collapse top search box. Default false. Please set to Top if show search box |
| `Color` | `Color` | Gets or sets the color. The default is (no color) |
| `CustomerSearchModel` | `ITableSearchModel?` | Gets or sets Custom Search Model |
| `CustomKeyAttribute` | `Type` | Default: `typeof(KeyAttribute)` |
| `DefaultVirtualizeItemText` | `string?` | Gets or sets the default text for virtualized items. Default is null |
| `DisableItemChangedWhenFirstRender` | `bool` | Gets or sets whether to disable the OnSelectedItemChanged callback method on first render. Default is false |

Only the first 40 important parameters are listed. Inspect the current source for the remaining 59 parameters before generating code.

## Events And Callbacks

`GetTextCallback: Func<TItem, string?>?`, `ModelEqualityComparer: Func<TValue, TValue, bool>?`, `OnBeforeSelectedItemChange: Func<SelectedItem, Task<bool>>?`, `OnClearAsync: Func<Task>?`, `OnCollapsed: Func<Task>?`, `OnEditCallback: Func<string, Task<SelectedItem>>?`, `OnExpandNodeAsync: Func<TreeViewItem<TValue>, Task<IEnumerable<TreeViewItem<TValue>>>>?`, `OnInputChangedCallback: Func<string, Task>?`, `OnQueryAsync: Func<QueryPageOptions, Task<QueryData<TItem>>>?`, `OnSearchAsync: Func<string?, Task<List<TreeViewItem<TValue>>?>>?`, `OnSearchTextChanged: Func<string, IEnumerable<SelectedItem>>?`, `OnSelectedItemChanged: Func<SelectedItem, Task>?`, `OnSelectedItemsChanged: Func<IEnumerable<SelectedItem>, Task>?`, `SelectedItemsChanged: EventCallback<List<TItem>>`

## Templates And Child Content

`ButtonTemplate: RenderFragment?`, `ChildContent: RenderFragment<ISelectObjectContext<TItem>>?`, `CustomerSearchTemplate: RenderFragment<ITableSearchModel>?`, `DisplayTemplate: RenderFragment<List<SelectedItem>>?`, `EmptyTemplate: RenderFragment?`, `GroupItemTemplate: RenderFragment<string>?`, `ItemTemplate: RenderFragment<SelectedItem>?`, `Options: RenderFragment?`, `SearchTemplate: RenderFragment<TItem>?`, `TableColumns: RenderFragment<TItem>?`, `TableExtensionToolbarTemplate: RenderFragment?`, `Template: RenderFragment<TItem>?`, `ToolbarTemplate: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnParametersSet`, `OnParametersSetAsync`, `OnAfterRender`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Selects.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Selects.razor.cs`

Sample analysis:

- Direct `<Select>` tag usages detected: 47
- Observed attributes in official Sample: `@bind-Value`, `Color`, `DefaultVirtualizeItemText`, `DisplayText`, `IsClearable`, `IsDisabled`, `IsEditable`, `IsPopover`, `IsVirtualize`, `Items`, `OnInputChangedCallback`, `OnQueryAsync`, `OnSelectedItemChanged`, `OnValueChanged`, `PlaceHolder`, `ShowLabel`, `ShowSearch`, `ShowSwal`, `SwalContent`, `SwalFooter`, `SwalTitle`, `TValue`, `Value`
Sample-derived snippet:

```razor
<Select Items="Items" OnSelectedItemChanged="OnItemChanged" @bind-Value="Model.Name"></Select>
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

- bool IsEdit - ?IsEditable Please use IsEditable parameter

## Agent Rules

1. Read `src/BootstrapBlazor/Components/Select` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.