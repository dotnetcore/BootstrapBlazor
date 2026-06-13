---
component: Menu
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Menu Skill

## Component Purpose

Menu Component Base

Primary source directory: `src/BootstrapBlazor/Components/Menu`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Menu/Menu.razor`
- `src/BootstrapBlazor/Components/Menu/Menu.razor.cs`
- `src/BootstrapBlazor/Components/Menu/Menu.razor.js`
- `src/BootstrapBlazor/Components/Menu/MenuItem.cs`
- `src/BootstrapBlazor/Components/Menu/MenuLink.razor`
- `src/BootstrapBlazor/Components/Menu/MenuLink.razor.cs`
- `src/BootstrapBlazor/Components/Menu/SideMenu.razor`
- `src/BootstrapBlazor/Components/Menu/SideMenu.razor.cs`
- `src/BootstrapBlazor/Components/Menu/SubMenu.razor`
- `src/BootstrapBlazor/Components/Menu/SubMenu.razor.cs`
- `src/BootstrapBlazor/Components/Menu/TopMenu.razor`
- `src/BootstrapBlazor/Components/Menu/TopMenu.razor.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnClick` | `Func<MenuItem, Task>?` | Callback/event parameter; Gets or sets Menu item click callback delegate |
| `ArrowIcon` | `string?` | Gets or sets Menu Arrow Icon |
| `DisableNavigation` | `bool` | Gets or sets Whether to disable navigation. Default false (Allow navigation) |
| `DropdownIcon` | `string?` | Gets or sets DropdownIcon Icon |
| `IndentSize` | `int` | Default: `16`; Gets or sets Indent size. Default 16px |
| `IsAccordion` | `bool` | Gets or sets Whether it is accordion effect. Default false |
| `IsBottom` | `bool` | Gets or sets Sidebar vertical mode at bottom. Default false |
| `IsCollapsed` | `bool` | Gets or sets Whether sidebar is collapsed. Default false (Not collapsed) |
| `IsExpandAll` | `bool` | Gets or sets Whether to expand all. Default false |
| `IsScrollIntoView` | `bool` | Default: `true`; Gets or sets Automatically scroll to visible area. Default true. Effective when is enabled |
| `IsVertical` | `bool` | Gets or sets Sidebar vertical mode. Default false |
| `Item` | `MenuItem?` | Gets or sets Component Data Source |
| `Items` | `IEnumerable<MenuItem>?` | Gets or sets Menu Data Collection |

## Events And Callbacks

`OnClick: Func<MenuItem, Task>?`

## Templates And Child Content

No RenderFragment parameters were detected in the current source scan.

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnParametersSet`, `OnAfterRenderAsync`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Menus.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Menus.razor.cs`

Sample analysis:

- Direct `<Menu>` tag usages detected: 11
- Observed attributes in official Sample: `class`, `DisableNavigation`, `IsAccordion`, `IsBottom`, `IsVertical`, `Items`, `OnClick`, `style`
Sample-derived snippet:

```razor
<Menu Items="@Items" DisableNavigation="true" OnClick="@OnClickMenu"></Menu>
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

1. Read `src/BootstrapBlazor/Components/Menu` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.