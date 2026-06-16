---
component: ListView
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# ListView Skill

## Component Purpose

ListView Component Base

Primary source directory: `src/BootstrapBlazor/Components/ListView`.

Source files reviewed:

- `src/BootstrapBlazor/Components/ListView/ListView.razor`
- `src/BootstrapBlazor/Components/ListView/ListView.razor.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `BodyTemplate` | `RenderFragment<TItem>?` | Required; Template parameter; verify context type; Gets or sets Body Template |
| `CollapsedGroupCallback` | `Func<object?, bool>?` | Callback/event parameter; Gets or sets Callback delegate for whether to collapse on first render |
| `GroupHeaderTextCallback` | `Func<object?, string?>?` | Callback/event parameter; Gets or sets Get value. Default null. Use Group Key.ToString() method to get |
| `GroupItemOrderCallback` | `Func<IGrouping<object?, TItem>, IOrderedEnumerable<TItem>>?` | Callback/event parameter; Gets or sets Group item sort callback method. Default null |
| `GroupName` | `Func<TItem, object?>?` | Callback/event parameter; Gets or sets Grouping Lambda Expression. Default null |
| `GroupOrderCallback` | `Func<IEnumerable<IGrouping<object?, TItem>>, IOrderedEnumerable<IGrouping<object?, TItem>>>?` | Callback/event parameter; Gets or sets Group sort callback method. Default null. Use built-in |
| `OnCollapseChanged` | `Func<CollapseItem, Task>?` | Callback/event parameter; Gets or sets Callback method when CollapseItem is expanded/collapsed. Default false. Need to enable collapsible setting |
| `OnListViewItemClick` | `Func<TItem, Task>?` | Callback/event parameter; Gets or sets Callback delegate when ListView component element is clicked |
| `OnQueryAsync` | `Func<QueryPageOptions, Task<QueryData<TItem>>>?` | Callback/event parameter; Async query callback method |
| `EmptyTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Template when no data. Default null |
| `FooterTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Footer Template. Default null. If set, parameter will not work, please implement pagination manually |
| `HeaderTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Card Header |
| `Collapsible` | `bool` | Gets or sets Whether it is collapsible. Default false. Need to enable grouping setting |
| `EmptyText` | `string?` | Gets or sets Text to display when no data. Default null. Use resource file to set text if not set |
| `Height` | `string?` | Gets or sets Component height. Default null. Not set. e.g. 50% 100px 10rem 10vh etc |
| `IsAccordion` | `bool` | Gets or sets Accordion effect. Default false. Need to enable collapsible setting |
| `IsPagination` | `bool` | Gets or sets Whether to page. Default false. Paging is automatically disabled when is set |
| `IsVertical` | `bool` | Gets or sets Whether to arrange vertically. Default false |
| `Items` | `IEnumerable<TItem>?` | Gets or sets Data Source |
| `PageItems` | `int` | Default: `20`; Gets or sets Number of items per page. Default 20 |

## Events And Callbacks

`CollapsedGroupCallback: Func<object?, bool>?`, `GroupHeaderTextCallback: Func<object?, string?>?`, `GroupItemOrderCallback: Func<IGrouping<object?, TItem>, IOrderedEnumerable<TItem>>?`, `GroupName: Func<TItem, object?>?`, `GroupOrderCallback: Func<IEnumerable<IGrouping<object?, TItem>>, IOrderedEnumerable<IGrouping<object?, TItem>>>?`, `OnCollapseChanged: Func<CollapseItem, Task>?`, `OnListViewItemClick: Func<TItem, Task>?`, `OnQueryAsync: Func<QueryPageOptions, Task<QueryData<TItem>>>?`

## Templates And Child Content

`BodyTemplate: RenderFragment<TItem>?`, `EmptyTemplate: RenderFragment?`, `FooterTemplate: RenderFragment?`, `HeaderTemplate: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnParametersSetAsync`
- JS interop or module dependency detected: `False`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/ListViews.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/ListViews.razor.cs`

Sample analysis:

- Direct `<ListView>` tag usages detected: 5
- Observed attributes in official Sample: `GroupName`, `Height`, `IsPagination`, `Items`, `OnListViewItemClick`, `OnQueryAsync`, `PageItems`, `TItem`
Sample-derived snippet:

```razor
<ListView TItem="Product" Items="@Products" OnListViewItemClick="OnListViewItemClick" Height="620px">
        <HeaderTemplate>
            <div>@Localizer["ProductListText"]</div>
        </HeaderTemplate>
        <BodyTemplate>
            <Card>
                <BodyTemplate>
                    <img src="@context.ImageUrl" />
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

1. Read `src/BootstrapBlazor/Components/ListView` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.