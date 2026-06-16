---
component: Radio
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Radio Skill

## Component Purpose

Radio Component

Primary source directory: `src/BootstrapBlazor/Components/Radio`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Radio/Radio.razor`
- `src/BootstrapBlazor/Components/Radio/Radio.razor.cs`
- `src/BootstrapBlazor/Components/Radio/RadioList.razor`
- `src/BootstrapBlazor/Components/Radio/RadioList.razor.cs`
- `src/BootstrapBlazor/Components/Radio/RadioListGeneric.razor`
- `src/BootstrapBlazor/Components/Radio/RadioListGeneric.razor.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `GroupName` | `string?` | Required; Gets or sets the Radio group name. Default is null |
| `ModelEqualityComparer` | `Func<TValue, TValue, bool>?` | Callback/event parameter; Gets or sets the callback method for comparing whether data is the same. Default is null. Ignore property when provid... |
| `OnClick` | `Func<TValue, Task>?` | Callback/event parameter; Gets or sets the click callback method |
| `OnSelectedChanged` | `Func<TValue, Task>?` | Callback/event parameter; Gets or sets the callback method when selected item changed |
| `ItemTemplate` | `RenderFragment<SelectedItem<TValue>>?` | Template parameter; verify context type; Gets or sets the item template |
| `AutoSelectFirstWhenValueIsNull` | `bool` | Default: `true`; Gets or sets whether to auto select first item when no item is selected. Default is true |
| `Color` | `Color` | Gets or sets the button color. Default is None |
| `CustomKeyAttribute` | `Type?` | Default: `typeof(KeyAttribute)`; Gets or sets the data primary key identification attribute. Default is . Used to identify data primary key attribute,... |
| `IsAutoAddNullItem` | `bool` | Gets or sets whether to auto add null value when value is nullable enum. Default is false. Custom null value display ... |
| `IsButton` | `bool` | Gets or sets whether to be button style. Default is false |
| `IsVertical` | `bool` | Gets or sets whether to be vertical layout. Default is false |
| `Items` | `IEnumerable<SelectedItem<TValue>>?` | Gets or sets the items |
| `NullItemText` | `string?` | Gets or sets the null item display text. Default is "". Whether to auto add null value, please refer to |
| `ShowBorder` | `bool` | Default: `true`; Gets or sets whether to show border. Default is true |

## Events And Callbacks

`ModelEqualityComparer: Func<TValue, TValue, bool>?`, `OnClick: Func<TValue, Task>?`, `OnSelectedChanged: Func<TValue, Task>?`

## Templates And Child Content

`ItemTemplate: RenderFragment<SelectedItem<TValue>>?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnParametersSet`
- JS interop or module dependency detected: `False`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Radios.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Radios.razor.cs`

Sample analysis:

- Direct `<Radio>` tag usages detected: 0
- No direct `<Radio>` tag usage was detected in the matched Sample files; inspect the Sample manually before generating code.

When the Sample and this Skill differ, prefer the current source, then the official Sample, then this Skill.

## Examples

Source-validated skeleton:

```razor
<Radio>
</Radio>
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

1. Read `src/BootstrapBlazor/Components/Radio` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.