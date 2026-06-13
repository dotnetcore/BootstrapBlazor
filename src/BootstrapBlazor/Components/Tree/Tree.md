---
component: Tree
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Tree Skill

## Component Purpose

Tree Component

Primary source directory: `src/BootstrapBlazor/Components/Tree`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Tree/Tree.razor`
- `src/BootstrapBlazor/Components/Tree/Tree.razor.cs`
- `src/BootstrapBlazor/Components/Tree/TreeItem.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnExpandNode` | `Func<TreeItem, Task>?` | Callback/event parameter; Gets or sets elegate |
| `OnTreeItemChecked` | `Func<List<TreeItem>, Task>?` | Callback/event parameter; Gets or sets elegate |
| `OnTreeItemClick` | `Func<TreeItem, Task>?` | Callback/event parameter; Gets or sets elegate |
| `ActiveItem` | `TreeItem?` | Gets or sets  Default is null |
| `ClickToggleNode` | `bool` | Gets or sets whether?Default is false |
| `ExpandNodeIcon` | `string?` | Gets or sets Tree Node icon |
| `IsAccordion` | `bool` | Gets or sets whether Default is?false |
| `Items` | `List<TreeItem>?` | Gets or sets datacollection |
| `NodeIcon` | `string?` | Gets or sets Tree Node icon |
| `ShowCheckbox` | `bool` | Gets or sets whetherdisplay CheckBox Default is false isplay |
| `ShowIcon` | `bool` | Gets or sets whetherdisplay Icon icon Default is false isplay |
| `ShowRadio` | `bool` | Gets or sets whetherdisplay Radio Default is false isplay |
| `ShowSkeleton` | `bool` | Gets or sets whetherdisplay?Default is false isplay |

## Events And Callbacks

`OnExpandNode: Func<TreeItem, Task>?`, `OnTreeItemChecked: Func<List<TreeItem>, Task>?`, `OnTreeItemClick: Func<TreeItem, Task>?`

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

No official Sample was matched for `Tree`.

Use examples in this Skill only after validating the current source. If the directory contains infrastructure rather than a public Razor component, do not generate direct Razor usage.

## Examples

Source-validated skeleton:

```razor
<Tree>
</Tree>
```

This skeleton is not a substitute for source validation. Add only parameters that exist in the current source.

## Common Mistakes

- Do not invent parameters, events, two-way bindings, or template context types.
- Do not copy examples from older BootstrapBlazor versions.
- Do not use obsolete members.
- If a Sample exists, analyze it before relying on Skill examples.
- If no Sample exists, validate every generated parameter against the current source.

## Obsolete Members

- partial class Tree - ?TreeView eprecated Please use TreeView component

## Agent Rules

1. Read `src/BootstrapBlazor/Components/Tree` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.