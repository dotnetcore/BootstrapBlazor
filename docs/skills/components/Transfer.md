---
component: Transfer
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Transfer Skill

## Component Purpose

Transfer Component

Primary source directory: `src/BootstrapBlazor/Components/Transfer`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Transfer/Transfer.razor`
- `src/BootstrapBlazor/Components/Transfer/Transfer.razor.cs`
- `src/BootstrapBlazor/Components/Transfer/TransferPanel.razor`
- `src/BootstrapBlazor/Components/Transfer/TransferPanel.razor.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `Items` | `IEnumerable<SelectedItem>?` | Required; Gets or sets the component bound data item collection |
| `OnSelectedItemsChanged` | `Func<IEnumerable<SelectedItem>, Task>?` | Callback/event parameter; Gets or sets the callback method when the selected items collection changes |
| `OnSetItemClass` | `Func<SelectedItem, string?>?` | Callback/event parameter; Gets or sets the data style callback method. Default is null |
| `HeaderTemplate` | `RenderFragment<List<SelectedItem>>?` | Template parameter; verify context type; Gets or sets the header template |
| `ItemTemplate` | `RenderFragment<SelectedItem>?` | Template parameter; verify context type; Gets or sets the item template |
| `LeftHeaderTemplate` | `RenderFragment<List<SelectedItem>>?` | Template parameter; verify context type; Gets or sets the left Panel Header template |
| `LeftItemTemplate` | `RenderFragment<SelectedItem>?` | Template parameter; verify context type; Gets or sets the left Panel Item template |
| `RightHeaderTemplate` | `RenderFragment<List<SelectedItem>>?` | Template parameter; verify context type; Gets or sets the right Panel Header template |
| `RightItemTemplate` | `RenderFragment<SelectedItem>?` | Template parameter; verify context type; Gets or sets the right Panel Item template |
| `Height` | `string?` | Gets or sets the component height. Default is null (not set) |
| `IsDisabled` | `bool` | Gets or sets whether to disable. Default is false |
| `IsWrapItem` | `bool` | Default: `true`; Gets or sets whether the items are in wrap mode. Default is false (no wrap) |
| `IsWrapItemText` | `bool` | Gets or sets whether the item text is wrapped. Default is false (no wrap) |
| `ItemWidth` | `string?` | Gets or sets the item width. Default is null (not set) |
| `LeftButtonText` | `string?` | Gets or sets the left button display text |
| `LeftIcon` | `string?` | Gets or sets the left transfer icon |
| `LeftPanelSearchPlaceHolderString` | `string?` | Gets or sets the left panel search box placeholder text |
| `LeftPanelText` | `string?` | Gets or sets the left panel header display text |
| `Max` | `int` | Gets or sets the maximum number of items in the right panel. Default is 0 (no limit) |
| `MaxErrorMessage` | `string?` | Gets or sets the error message text when setting the maximum value |
| `Min` | `int` | Gets or sets the minimum number of items in the right panel. Default is 0 (no limit) |
| `MinErrorMessage` | `string?` | Gets or sets the error message text when setting the minimum value |
| `RightButtonText` | `string?` | Gets or sets the right button display text |
| `RightIcon` | `string?` | Gets or sets the right transfer icon |
| `RightPanelSearchPlaceHolderString` | `string?` | Gets or sets the right panel search box placeholder text |
| `RightPanelText` | `string?` | Gets or sets the right panel header display text |
| `SearchIcon` | `string?` | Gets or sets the search box icon |
| `SearchPlaceHolderString` | `string?` | Gets or sets the search box placeholder string |
| `ShowSearch` | `bool` | Gets or sets whether to display the search box |
| `Text` | `string?` | Gets or sets the panel display text |

## Events And Callbacks

`OnSelectedItemsChanged: Func<IEnumerable<SelectedItem>, Task>?`, `OnSetItemClass: Func<SelectedItem, string?>?`

## Templates And Child Content

`HeaderTemplate: RenderFragment<List<SelectedItem>>?`, `ItemTemplate: RenderFragment<SelectedItem>?`, `LeftHeaderTemplate: RenderFragment<List<SelectedItem>>?`, `LeftItemTemplate: RenderFragment<SelectedItem>?`, `RightHeaderTemplate: RenderFragment<List<SelectedItem>>?`, `RightItemTemplate: RenderFragment<SelectedItem>?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnParametersSet`
- JS interop or module dependency detected: `False`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Transfers.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Transfers.razor.cs`

Sample analysis:

- Direct `<Transfer>` tag usages detected: 8
- Observed attributes in official Sample: `@bind-Value`, `IsDisabled`, `IsWrapItem`, `IsWrapItemText`, `Items`, `ItemWidth`, `LeftButtonText`, `LeftPanelSearchPlaceHolderString`, `LeftPanelText`, `Max`, `Min`, `OnSelectedItemsChanged`, `OnSetItemClass`, `RightButtonText`, `RightPanelSearchPlaceHolderString`, `RightPanelText`, `ShowSearch`, `TValue`
Sample-derived snippet:

```razor
<Transfer TValue="string" Items="@Items"
              IsWrapItem="@_isWrapItem" IsWrapItemText="@_isWrapItemText" ItemWidth="@_itemWidth"
              OnSelectedItemsChanged="@OnSelectedItemsChanged" />
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

1. Read `src/BootstrapBlazor/Components/Transfer` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.