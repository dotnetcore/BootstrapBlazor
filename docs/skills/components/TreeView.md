---
component: TreeView
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# TreeView Skill

## Component Purpose

Tree Component

Primary source directory: `src/BootstrapBlazor/Components/TreeView`.

Source files reviewed:

- `src/BootstrapBlazor/Components/TreeView/TreeView.razor`
- `src/BootstrapBlazor/Components/TreeView/TreeView.razor.cs`
- `src/BootstrapBlazor/Components/TreeView/TreeView.razor.js`
- `src/BootstrapBlazor/Components/TreeView/TreeViewDragContext.cs`
- `src/BootstrapBlazor/Components/TreeView/TreeViewItem.cs`
- `src/BootstrapBlazor/Components/TreeView/TreeViewRow.razor`
- `src/BootstrapBlazor/Components/TreeView/TreeViewRow.razor.cs`
- `src/BootstrapBlazor/Components/TreeView/TreeViewToolbarEditButton.razor`
- `src/BootstrapBlazor/Components/TreeView/TreeViewToolbarEditButton.razor.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `ItemChanged` | `EventCallback<TreeViewItem<TItem>>` | Callback/event parameter; Gets or sets the item changed event callback |
| `ModelEqualityComparer` | `Func<TItem, TItem, bool>?` | Callback/event parameter |
| `OnBeforeStateChangedCallback` | `Func<TreeViewItem<TItem>, CheckboxState, Task<bool>>?` | Callback/event parameter; Gets or sets the callback that is invoked before the node state changes |
| `OnBeforeTreeItemClick` | `Func<TreeViewItem<TItem>, Task<bool>>?` | Callback/event parameter; Gets or sets the callback method before a tree item is clicked |
| `OnCheckStateChanged` | `Func<TreeViewItem<TItem>, CheckboxState, Task>?` | Callback/event parameter; Gets or sets the node checkbox state change event callback |
| `OnClick` | `Func<TreeViewItem<TItem>, Task>?` | Callback/event parameter; Gets or sets the click event callback. Default is null |
| `OnDragItemEndAsync` | `Func<TreeViewDragContext<TItem>, Task>?` | Callback/event parameter; Gets or sets allback method |
| `OnExpandNodeAsync` | `Func<TreeViewItem<TItem>, Task<IEnumerable<TreeViewItem<TItem>>>>?` | Callback/event parameter; Gets or sets the callback method to get child data when a node is expanded |
| `OnMaxSelectedCountExceed` | `Func<Task>?` | Callback/event parameter; Gets or sets the callback method when the maximum number of selected items is exceeded |
| `OnSearchAsync` | `Func<string?, Task<List<TreeViewItem<TItem>>?>>?` | Callback/event parameter; Gets or sets the search callback method. Default is null |
| `OnToggleNodeAsync` | `Func<TreeViewItem<TItem>, Task>?` | Callback/event parameter; Gets or sets the node click event callback |
| `OnTreeItemChecked` | `Func<List<TreeViewItem<TItem>>, Task>?` | Callback/event parameter; Gets or sets the callback method when a tree item is checked |
| `OnTreeItemClick` | `Func<TreeViewItem<TItem>, Task>?` | Callback/event parameter; Gets or sets the callback method when a tree item is clicked |
| `OnUpdateCallbackAsync` | `Func<TItem, string?, Task<bool>>?` | Callback/event parameter; Gets or sets the update the tree text value callback. Default is null. If return true will update the tree text value... |
| `ShowToolbarCallback` | `Func<TreeViewItem<TItem>, Task<bool>>?` | Callback/event parameter; Gets or sets the callback method that determines whether to show the toolbar of the tree view item |
| `SearchTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the search bar template. Default is null |
| `ToolbarTemplate` | `RenderFragment<TItem>?` | Template parameter; verify context type; Gets or sets the toolbar content template. Default is null |
| `AllowDrag` | `bool` | Gets or sets a value indicating whether drag-and-drop operations are allowed. Default is false |
| `AutoCheckChildren` | `bool` | Gets or sets whether to automatically update child nodes when the node state changes. Default is false |
| `AutoCheckParent` | `bool` | Gets or sets whether to automatically update parent nodes when the node state changes. Default is false |
| `CanExpandWhenDisabled` | `bool` | Gets or sets whether nodes can be expanded or collapsed when the component is disabled. Default is false |
| `ClearSearchIcon` | `string?` | Gets or sets the clear search icon. Default is not set, using the built-in theme icon |
| `ClickToggleCheck` | `bool` | Gets or sets whether clicking a node toggles its checkbox state. Default is false. Effective when ShowCheckbox is true |
| `ClickToggleNode` | `bool` | Gets or sets whether clicking a node expands or collapses its children. Default is false |
| `CustomKeyAttribute` | `Type` | Default: `typeof(KeyAttribute)` |
| `EnableKeyboard` | `bool` | Gets or sets whether to enable keyboard navigation. Default is false. ArrowLeft collapses the node |
| `ExpandNodeIcon` | `string?` | Gets or sets the icon for expanded tree nodes |
| `Icon` | `string?` | Gets or sets the icon of the edit button. Default is null |
| `Index` | `int` | Gets or sets the node index. Default is 0 |
| `IsAccordion` | `bool` | Gets or sets whether the tree view has accordion behavior. Default is false. Accordion behavior is not supported in v... |
| `IsActive` | `bool` | Gets or sets whether the node is active. Default is false |
| `IsAutoScrollIntoView` | `bool` | Gets or sets whether to automatically scroll the active selected node into the visible state. |
| `IsDisabled` | `bool` | Gets or sets whether the entire component is disabled. Default is false |
| `IsVirtualize` | `bool` | Gets or sets whether to enable virtual scrolling. Default is false |
| `Items` | `List<TreeViewItem<TItem>>?` | Gets or sets the hierarchical data collection |
| `LoadingIcon` | `string?` | Gets or sets the loading icon for tree nodes |
| `MaxSelectedCount` | `int` | Gets or sets the maximum number of selected items |
| `NodeIcon` | `string?` | Gets or sets the icon for tree nodes |
| `OverscanCount` | `int` | Default: `10`; Gets or sets the overscan count for virtual scrolling. Default is 10 |
| `RowHeight` | `float` | Default: `29f`; Gets or sets the row height for virtual scrolling. Default is 29f |

Only the first 40 important parameters are listed. Inspect the current source for the remaining 12 parameters before generating code.

## Events And Callbacks

`ItemChanged: EventCallback<TreeViewItem<TItem>>`, `ModelEqualityComparer: Func<TItem, TItem, bool>?`, `OnBeforeStateChangedCallback: Func<TreeViewItem<TItem>, CheckboxState, Task<bool>>?`, `OnBeforeTreeItemClick: Func<TreeViewItem<TItem>, Task<bool>>?`, `OnCheckStateChanged: Func<TreeViewItem<TItem>, CheckboxState, Task>?`, `OnClick: Func<TreeViewItem<TItem>, Task>?`, `OnDragItemEndAsync: Func<TreeViewDragContext<TItem>, Task>?`, `OnExpandNodeAsync: Func<TreeViewItem<TItem>, Task<IEnumerable<TreeViewItem<TItem>>>>?`, `OnMaxSelectedCountExceed: Func<Task>?`, `OnSearchAsync: Func<string?, Task<List<TreeViewItem<TItem>>?>>?`, `OnToggleNodeAsync: Func<TreeViewItem<TItem>, Task>?`, `OnTreeItemChecked: Func<List<TreeViewItem<TItem>>, Task>?`, `OnTreeItemClick: Func<TreeViewItem<TItem>, Task>?`, `OnUpdateCallbackAsync: Func<TItem, string?, Task<bool>>?`, `ShowToolbarCallback: Func<TreeViewItem<TItem>, Task<bool>>?`

## Templates And Child Content

`SearchTemplate: RenderFragment?`, `ToolbarTemplate: RenderFragment<TItem>?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnParametersSet`, `OnParametersSetAsync`, `OnAfterRenderAsync`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/TreeViews.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/TreeViews.razor.cs`

Sample analysis:

- Direct `<TreeView>` tag usages detected: 20
- Observed attributes in official Sample: `@ref`, `AllowDrag`, `AutoCheckChildren`, `AutoCheckParent`, `CanExpandWhenDisabled`, `class`, `ClickToggleCheck`, `ClickToggleNode`, `EnableKeyboard`, `IsAccordion`, `IsDisabled`, `IsVirtualize`, `Items`, `MaxSelectedCount`, `OnDragItemEndAsync`, `OnExpandNodeAsync`, `OnMaxSelectedCountExceed`, `OnSearchAsync`, `OnTreeItemChecked`, `OnTreeItemClick`, `OnUpdateCallbackAsync`, `ShowCheckbox`, `ShowIcon`, `ShowSearch`, `ShowSkeleton`, `ShowToolbar`, `style`
Sample-derived snippet:

```razor
<TreeView Items="@NormalItems" OnTreeItemClick="@OnTreeItemClick" ShowToolbar="true"></TreeView>
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

1. Read `src/BootstrapBlazor/Components/TreeView` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.