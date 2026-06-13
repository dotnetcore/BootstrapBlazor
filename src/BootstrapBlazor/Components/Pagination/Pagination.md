---
component: Pagination
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Pagination Skill

## Component Purpose

GotoNavigator component for pagination navigation

Primary source directory: `src/BootstrapBlazor/Components/Pagination`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Pagination/GotoNavigator.razor`
- `src/BootstrapBlazor/Components/Pagination/GotoNavigator.razor.cs`
- `src/BootstrapBlazor/Components/Pagination/Pagination.razor`
- `src/BootstrapBlazor/Components/Pagination/Pagination.razor.cs`
- `src/BootstrapBlazor/Components/Pagination/PaginationItem.razor`
- `src/BootstrapBlazor/Components/Pagination/PaginationItem.razor.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `PageCount` | `int` | Required; Gets or sets Total Page Count |
| `OnClick` | `EventCallback<int>` | Callback/event parameter; Callback method when page link is clicked. Parameter is current page index |
| `OnNavigation` | `Func<int, Task>?` | Callback/event parameter; Gets or sets the navigation callback. Default is null |
| `OnPageLinkClick` | `Func<int, Task>?` | Callback/event parameter; Callback method when page link is clicked. Parameter is current page index |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the child component template |
| `GotoTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Goto Navigation Template. Default null |
| `PageInfoTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Page Info Template. Default null |
| `Alignment` | `Alignment` | Default: `Alignment.Right`; Gets or sets the alignment. Default is Alignment.Right |
| `GotoNavigatorLabelText` | `string?` | Gets or sets Goto Navigator Label Text. Default Goto |
| `GotoText` | `string?` | Gets or sets the goto text. Default is null |
| `Index` | `int` | Gets or sets the navigation index. Default is null |
| `IsActive` | `bool` | Gets or sets whether active. Default is false |
| `IsDisabled` | `bool` | Gets or sets whether disabled. Default is false |
| `MaxPageLinkCount` | `int` | Default: `5`; Gets or sets Page up/down link count. Default 5 |
| `NextEllipsisPageIcon` | `string?` | Gets or sets the next ellipsis page icon |
| `NextPageIcon` | `string?` | Gets or sets the next page icon |
| `PageIndex` | `int` | Default: `1`; Gets or sets Current Page Index |
| `PageInfoText` | `string?` | Gets or sets Pagination Info Text. Default null |
| `PrevEllipsisPageIcon` | `string?` | Gets or sets the previous ellipsis page icon |
| `PrevPageIcon` | `string?` | Gets or sets the previous page icon |
| `ShowGotoNavigator` | `bool` | Gets or sets Whether to show Goto Navigator. Default false |
| `ShowPageInfo` | `bool` | Default: `true`; Gets or sets Whether to show Page Info. Default true |

## Events And Callbacks

`OnClick: EventCallback<int>`, `OnNavigation: Func<int, Task>?`, `OnPageLinkClick: Func<int, Task>?`

## Templates And Child Content

`ChildContent: RenderFragment?`, `GotoTemplate: RenderFragment?`, `PageInfoTemplate: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnParametersSet`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Paginations.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Paginations.razor.cs`

Sample analysis:

- Direct `<Pagination>` tag usages detected: 7
- Observed attributes in official Sample: `Alignment`, `class`, `MaxPageLinkCount`, `OnPageLinkClick`, `PageCount`, `PageInfoText`, `ShowGotoNavigator`, `ShowPageInfo`
Sample-derived snippet:

```razor
<Pagination PageCount="30" OnPageLinkClick="@OnPageClick" />
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

1. Read `src/BootstrapBlazor/Components/Pagination` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.