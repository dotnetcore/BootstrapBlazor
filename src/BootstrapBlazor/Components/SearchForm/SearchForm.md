---
component: SearchForm
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# SearchForm Skill

## Component Purpose

SearchItem interface

Primary source directory: `src/BootstrapBlazor/Components/SearchForm`.

Source files reviewed:

- `src/BootstrapBlazor/Components/SearchForm/ISearchItem.cs`
- `src/BootstrapBlazor/Components/SearchForm/SearchForm.razor`
- `src/BootstrapBlazor/Components/SearchForm/SearchForm.razor.cs`
- `src/BootstrapBlazor/Components/SearchForm/SearchItem.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `Items` | `IEnumerable<ISearchItem>?` | Required; Gets or sets the items collection. |
| `OnChanged` | `Func<FilterKeyValueAction, Task>?` | Callback/event parameter; Gets or sets the filter changed callback event Func version |
| `Buttons` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Buttons Template |
| `GroupType` | `EditorFormGroupType` | Gets or sets group type. Default is |
| `ItemsPerRow` | `int?` | Gets or sets Items Per Row. Default is null |
| `LabelAlign` | `Alignment` | Gets or sets Label Alignment in Inline mode. Default is None, equivalent to Left |
| `LabelWidth` | `int?` | Gets or sets Label Width. Default is null, use global setting --bb-row-label-width if not set |
| `RowType` | `RowType` | Gets or sets Row Type. Default is Row |
| `SearchFormLocalizerOptions` | `SearchFormLocalizerOptions?` | Gets or sets Search Form Localization Options |
| `ShowLabel` | `bool?` | Gets or sets Whether to Show Label. Default is null, show label if not set |
| `ShowLabelTooltip` | `bool?` | Gets or sets Whether to Show Label Tooltip. Default is null |
| `ShowUnsetGroupItemsOnTop` | `bool` | Gets or sets Whether to show unset GroupName items on top. Default is false |

## Events And Callbacks

`OnChanged: Func<FilterKeyValueAction, Task>?`

## Templates And Child Content

`Buttons: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnParametersSet`
- JS interop or module dependency detected: `False`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

No official Sample was matched for `SearchForm`.

Use examples in this Skill only after validating the current source. If the directory contains infrastructure rather than a public Razor component, do not generate direct Razor usage.

## Examples

Source-validated skeleton:

```razor
<SearchForm>
</SearchForm>
```

This skeleton is not a substitute for source validation. Add only parameters that exist in the current source.

## Common Mistakes

- Do not invent parameters, events, two-way bindings, or template context types.
- Do not copy examples from older BootstrapBlazor versions.
- Do not use obsolete members.
- If a Sample exists, analyze it before relying on Skill examples.
- If no Sample exists, validate every generated parameter against the current source.

## Obsolete Members

No obsolete members were detected in the current source scan.

## Agent Rules

1. Read `src/BootstrapBlazor/Components/SearchForm` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.