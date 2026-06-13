---
component: Cascader
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Cascader Skill

## Component Purpose

Cascader component implementation class

Primary source directory: `src/BootstrapBlazor/Components/Cascader`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Cascader/Cascader.razor`
- `src/BootstrapBlazor/Components/Cascader/Cascader.razor.cs`
- `src/BootstrapBlazor/Components/Cascader/CascaderItem.cs`
- `src/BootstrapBlazor/Components/Cascader/SubCascader.razor`
- `src/BootstrapBlazor/Components/Cascader/SubCascader.razor.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnBlurAsync` | `Func<TValue, Task>?` | Callback/event parameter; Gets or sets the callback method when losing focus. Default is null |
| `OnClearAsync` | `Func<Task>?` | Callback/event parameter; Gets or sets the OnClear callback method when clearing text content. Default is null |
| `OnClick` | `Func<CascaderItem, Task>?` | Callback/event parameter; Gets or sets the selected item click callback delegate |
| `OnSelectedItemChanged` | `Func<CascaderItem[], Task>?` | Callback/event parameter; Gets or sets the ValueChanged method |
| `ClearIcon` | `string?` | Gets or sets the clear icon on the right. Default is fa-solid fa-angle-up |
| `Color` | `Color` | Default: `Color.None`; Gets or sets the button color |
| `Icon` | `string?` | Gets or sets the menu indicator icon |
| `IsClearable` | `bool` | Gets or sets whether it is clearable. Default is false |
| `Items` | `IEnumerable<CascaderItem>?` | Gets or sets the bound data set |
| `ParentSelectable` | `bool` | Default: `true`; Gets or sets whether the parent node is selectable. Default is true |
| `PlaceHolder` | `string?` | Gets or sets the component PlaceHolder text. Default is Please select .. |
| `ShowFullLevels` | `bool` | Default: `true`; Gets or sets whether to show the full path. Default is true |
| `SubMenuIcon` | `string?` | Gets or sets the submenu indicator icon |

## Events And Callbacks

`OnBlurAsync: Func<TValue, Task>?`, `OnClearAsync: Func<Task>?`, `OnClick: Func<CascaderItem, Task>?`, `OnSelectedItemChanged: Func<CascaderItem[], Task>?`

## Templates And Child Content

No RenderFragment parameters were detected in the current source scan.

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnParametersSet`
- JS interop or module dependency detected: `False`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Cascaders.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Cascaders.razor.cs`

Sample analysis:

- Direct `<Cascader>` tag usages detected: 23
- Observed attributes in official Sample: `@bind-Value`, `Color`, `DisplayText`, `IsClearable`, `IsDisabled`, `Items`, `OnSelectedItemChanged`, `ParentSelectable`, `ShowFullLevels`, `ShowLabel`, `TValue`
Sample-derived snippet:

```razor
<Cascader TValue="string" OnSelectedItemChanged="@OnItemChanged" Items="@_items" />
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

1. Read `src/BootstrapBlazor/Components/Cascader` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.