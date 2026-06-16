---
component: SelectGeneric
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# SelectGeneric Skill

## Component Purpose

ISelect Interface

Primary source directory: `src/BootstrapBlazor/Components/SelectGeneric`.

Source files reviewed:

- `src/BootstrapBlazor/Components/SelectGeneric/ISelectGeneric.cs`
- `src/BootstrapBlazor/Components/SelectGeneric/MultiSelectGeneric.razor`
- `src/BootstrapBlazor/Components/SelectGeneric/MultiSelectGeneric.razor.cs`
- `src/BootstrapBlazor/Components/SelectGeneric/SelectGeneric.razor`
- `src/BootstrapBlazor/Components/SelectGeneric/SelectGeneric.razor.cs`
- `src/BootstrapBlazor/Components/SelectGeneric/SelectOptionGeneric.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnBeforeSelectedItemChange` | `Func<SelectedItem<TValue>, Task<bool>>?` | Callback/event parameter; Gets or sets Callback delegate before dropdown item changes. Return true to change option value, otherwise value rema... |
| `OnEditCallback` | `Func<string, Task<SelectedItem>>?` | Callback/event parameter; Gets or sets Callback method when input option is updated in edit mode. Default null. Return instance to take effect,... |
| `OnInputChangedCallback` | `Func<string, Task>?` | Callback/event parameter; Gets or sets Callback method after option input update. Default null. Effective when is set |
| `OnQueryAsync` | `Func<VirtualizeQueryOption, Task<QueryData<SelectedItem<TValue>>>>?` | Callback/event parameter; Gets or sets the callback method for loading virtualized items |
| `OnSearchTextChanged` | `Func<string, IEnumerable<SelectedItem<TValue>>>?` | Callback/event parameter; Gets or sets Callback method when search text changes |
| `OnSelectedItemChanged` | `Func<SelectedItem<TValue>, Task>?` | Callback/event parameter; SelectedItemChanged Callback Method |
| `OnSelectedItemsChanged` | `Func<IEnumerable<SelectedItem<TValue>>, Task>?` | Callback/event parameter; Gets or sets Callback method when selected items collection changes |
| `TextConvertToValueCallback` | `Func<string, Task<TValue?>>?` | Callback/event parameter; Gets or sets Callback method to convert option input update to Value. Default null. Discard operation when return val... |
| `ValueEqualityComparer` | `Func<TValue, TValue, bool>?` | Callback/event parameter; Gets or sets Value Equality Comparer. Default null |
| `ButtonTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Button Template |
| `DisplayTemplate` | `RenderFragment<List<SelectedItem<TValue>>>?` | Template parameter; verify context type; Gets or sets Display Template. Default null |
| `ItemTemplate` | `RenderFragment<SelectedItem<TValue>>?` | Template parameter; verify context type; Gets or sets Item Template |
| `Options` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Option template supports static data |
| `Active` | `bool` | Gets or sets Whether selected. Default false |
| `ClearText` | `string?` | Gets or sets Clear Text |
| `CloseButtonIcon` | `string?` | Gets or sets Close button icon. Default null |
| `CustomKeyAttribute` | `Type?` | Default: `typeof(KeyAttribute)`; Gets or sets Identifier tag for data primary key. Default is . Used to determine date primary key tag. If the model d... |
| `DefaultVirtualizeItemText` | `string?` | Gets or sets the default virtualize items text |
| `DisableItemChangedWhenFirstRender` | `bool` | Gets or sets Disable triggering OnSelectedItemChanged callback on first load. Default false |
| `EditSubmitKey` | `EditSubmitKey` | Gets or sets Edit Submit Key. Default Enter |
| `GroupName` | `string?` | Gets or sets Group Name |
| `IsDisabled` | `bool` | Gets or sets Whether disabled. Default false |
| `IsEditable` | `bool` | Gets or sets Whether editable. Default false |
| `IsFixedHeight` | `bool` | Gets or sets Whether fixed height. Default false |
| `IsSingleLine` | `bool` | Gets or sets Whether single line mode. Default false |
| `Items` | `IEnumerable<SelectedItem<TValue>>?` | Gets or sets Bound Dataset |
| `Max` | `int` | Gets or sets Max items. Default 0 (no limit) |
| `MaxErrorMessage` | `string?` | Gets or sets Error message when max value is set |
| `Min` | `int` | Gets or sets Min items. Default 0 (no limit) |
| `MinErrorMessage` | `string?` | Gets or sets Error message when min value is set |
| `ReverseSelectText` | `string?` | Gets or sets Reverse Select Text |
| `SelectAllText` | `string?` | Gets or sets Select All Text |
| `ShowCloseButton` | `bool` | Default: `true`; Gets or sets Whether to show close button. Default true |
| `ShowDefaultButtons` | `bool` | Default: `true`; Gets or sets Whether to show default buttons. Default true |
| `ShowToolbar` | `bool` | Gets or sets Whether to show toolbar. Default false |
| `SwalCategory` | `SwalCategory` | Default: `SwalCategory.Question`; Gets or sets Swal Icon. Default Question |
| `SwalContent` | `string?` | Gets or sets Swal Content. Default null |
| `SwalFooter` | `string?` | Gets or sets Footer. Default null |
| `SwalTitle` | `string?` | Gets or sets Swal Title. Default null |
| `Text` | `string?` | Gets or sets Display Name |

Only the first 40 important parameters are listed. Inspect the current source for the remaining 1 parameters before generating code.

## Events And Callbacks

`OnBeforeSelectedItemChange: Func<SelectedItem<TValue>, Task<bool>>?`, `OnEditCallback: Func<string, Task<SelectedItem>>?`, `OnInputChangedCallback: Func<string, Task>?`, `OnQueryAsync: Func<VirtualizeQueryOption, Task<QueryData<SelectedItem<TValue>>>>?`, `OnSearchTextChanged: Func<string, IEnumerable<SelectedItem<TValue>>>?`, `OnSelectedItemChanged: Func<SelectedItem<TValue>, Task>?`, `OnSelectedItemsChanged: Func<IEnumerable<SelectedItem<TValue>>, Task>?`, `TextConvertToValueCallback: Func<string, Task<TValue?>>?`, `ValueEqualityComparer: Func<TValue, TValue, bool>?`

## Templates And Child Content

`ButtonTemplate: RenderFragment?`, `DisplayTemplate: RenderFragment<List<SelectedItem<TValue>>>?`, `ItemTemplate: RenderFragment<SelectedItem<TValue>>?`, `Options: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnParametersSet`, `OnAfterRender`, `OnAfterRenderAsync`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/SelectGenerics.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/SelectGenerics.razor.cs`

Sample analysis:

- Direct `<SelectGeneric>` tag usages detected: 44
- Observed attributes in official Sample: `@bind-Value`, `Color`, `DisplayText`, `IsClearable`, `IsDisabled`, `IsEditable`, `IsPopover`, `IsVirtualize`, `Items`, `OnBeforeSelectedItemChange`, `OnInputChangedCallback`, `OnQueryAsync`, `OnSelectedItemChanged`, `OnValueChanged`, `PlaceHolder`, `ShowLabel`, `ShowSearch`, `SwalContent`, `SwalFooter`, `SwalTitle`, `TextConvertToValueCallback`, `TValue`, `Value`
Sample-derived snippet:

```razor
<SelectGeneric Items="Items" OnSelectedItemChanged="OnItemChanged" @bind-Value="Model.Name"></SelectGeneric>
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

1. Read `src/BootstrapBlazor/Components/SelectGeneric` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.