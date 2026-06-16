---
component: ColorPicker
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# ColorPicker Skill

## Component Purpose

ColorPicker component

Primary source directory: `src/BootstrapBlazor/Components/ColorPicker`.

Source files reviewed:

- `src/BootstrapBlazor/Components/ColorPicker/ColorPicker.razor`
- `src/BootstrapBlazor/Components/ColorPicker/ColorPicker.razor.cs`
- `src/BootstrapBlazor/Components/ColorPicker/ColorPicker.razor.js`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `Formatter` | `Func<string, Task<string>>?` | Callback/event parameter; Gets or sets display color value formatting callback method |
| `Template` | `RenderFragment<string>?` | Template parameter; verify context type; Gets or sets display template |
| `IsSupportOpacity` | `bool` | Gets or sets whether to support opacity, default is false(not supported) |
| `Swatches` | `List<string>?` | Gets or sets preset candidate colors, effective when is enabled, default is null |

## Events And Callbacks

`Formatter: Func<string, Task<string>>?`

## Templates And Child Content

`Template: RenderFragment<string>?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnParametersSetAsync`, `OnAfterRenderAsync`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/ColorPickers.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/ColorPickers.razor.cs`

Sample analysis:

- Direct `<ColorPicker>` tag usages detected: 8
- Observed attributes in official Sample: `@bind-Value`, `Formatter`, `IsDisabled`, `IsSupportOpacity`, `OnValueChanged`, `Value`
Sample-derived snippet:

```razor
<ColorPicker Value="@Value" OnValueChanged="@OnColorChanged" />
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

1. Read `src/BootstrapBlazor/Components/ColorPicker` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.