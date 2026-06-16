---
component: ListGroup
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# ListGroup Skill

## Component Purpose

ListGroup Component

Primary source directory: `src/BootstrapBlazor/Components/ListGroup`.

Source files reviewed:

- `src/BootstrapBlazor/Components/ListGroup/ListGroup.razor`
- `src/BootstrapBlazor/Components/ListGroup/ListGroup.razor.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `GetItemDisplayText` | `Func<TItem, string>?` | Callback/event parameter; Gets or sets Callback method to get item display text |
| `OnClickItem` | `Func<TItem, Task>?` | Callback/event parameter; Gets or sets Callback method when List item is clicked |
| `OnDoubleClickItem` | `Func<TItem, Task>?` | Callback/event parameter; Gets or sets Callback method when List item is double-clicked |
| `HeaderTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Header Template. Default null |
| `ItemTemplate` | `RenderFragment<TItem>?` | Template parameter; verify context type; Gets or sets Item Template. Default null |
| `HeaderText` | `string?` | Gets or sets Header Text. Default null |

## Events And Callbacks

`GetItemDisplayText: Func<TItem, string>?`, `OnClickItem: Func<TItem, Task>?`, `OnDoubleClickItem: Func<TItem, Task>?`

## Templates And Child Content

`HeaderTemplate: RenderFragment?`, `ItemTemplate: RenderFragment<TItem>?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnParametersSet`
- JS interop or module dependency detected: `False`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/ListGroups.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/ListGroups.razor.cs`

Sample analysis:

- Direct `<ListGroup>` tag usages detected: 3
- Observed attributes in official Sample: `@bind-Value`, `GetItemDisplayText`, `HeaderText`, `Items`, `TItem`, `Value`
Sample-derived snippet:

```razor
<ListGroup TItem="Foo" Items="Items" Value="Value" GetItemDisplayText="@GetItemDisplayText"></ListGroup>
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

1. Read `src/BootstrapBlazor/Components/ListGroup` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.