---
component: Switch
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Switch Skill

## Component Purpose

Nullable Boolean Component

Primary source directory: `src/BootstrapBlazor/Components/Switch`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Switch/NullSwitch.razor`
- `src/BootstrapBlazor/Components/Switch/NullSwitch.razor.cs`
- `src/BootstrapBlazor/Components/Switch/Switch.razor`
- `src/BootstrapBlazor/Components/Switch/Switch.razor.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `DefaultValueWhenNull` | `bool` | Gets or sets Default value when null. Default false |
| `Height` | `int` | Default: `20`; Gets or sets Component Height. Default 20px |
| `OffColor` | `Color` | Gets or sets Off Color |
| `OffInnerText` | `string?` | Gets or sets Off Inner Text |
| `OnColor` | `Color` | Default: `Color.Success`; Gets or sets On Color |
| `OnInnerText` | `string?` | Gets or sets On Inner Text |
| `ShowInnerText` | `bool` | Gets or sets Whether to show inner text. Default false |
| `Width` | `override int` | Default: `40`; Gets or sets Component Width. Default 40 |

## Events And Callbacks

No callback/event parameters were detected in the current source scan.

## Templates And Child Content

No RenderFragment parameters were detected in the current source scan.

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Switches.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Switches.razor.cs`

Sample analysis:

- Direct `<Switch>` tag usages detected: 14
- Observed attributes in official Sample: `@bind-Value`, `DisplayText`, `IsDisabled`, `OffColor`, `OffInnerText`, `OffText`, `OnColor`, `OnInnerText`, `OnText`, `ShowInnerText`, `ShowLabel`, `Value`, `ValueChanged`
Sample-derived snippet:

```razor
<Switch ValueChanged="@OnValueChanged" Value="@BindValue" OnColor="Color.Secondary" OnText="@Localizer["SwitchesOnText"]" OffText="@Localizer["SwitchesOffText"]"></Switch>
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

1. Read `src/BootstrapBlazor/Components/Switch` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.