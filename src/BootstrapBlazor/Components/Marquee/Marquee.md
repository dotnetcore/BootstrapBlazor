---
component: Marquee
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Marquee Skill

## Component Purpose

Marquee scrolling component

Primary source directory: `src/BootstrapBlazor/Components/Marquee`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Marquee/Marquee.razor`
- `src/BootstrapBlazor/Components/Marquee/Marquee.razor.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `BackgroundColor` | `string` | Default: `"#fff"`; Gets or sets the component's background color. Default is #fff Supports hexadecimal and color names |
| `Color` | `string` | Default: `"#000"`; Gets or sets the component's text color. Default is #000 Supports hexadecimal and color names |
| `Direction` | `MarqueeDirection` | Gets or sets the component's scroll direction. Default is LeftToRight |
| `Duration` | `int` | Default: `14`; Gets or sets the component's animation duration. Default is 14s The smaller the value, the faster the scroll |
| `FontSize` | `int` | Default: `72`; Gets or sets the component's text size. Default is 72px |
| `Text` | `string?` | Gets or sets the component's display text. Default is Empty. |

## Events And Callbacks

No callback/event parameters were detected in the current source scan.

## Templates And Child Content

No RenderFragment parameters were detected in the current source scan.

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: None detected in current source.
- JS interop or module dependency detected: `False`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Marquees.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Marquees.razor.cs`

Sample analysis:

- Direct `<Marquee>` tag usages detected: 1
- Observed attributes in official Sample: `BackgroundColor`, `Color`, `Direction`, `Duration`, `FontSize`, `Text`
Sample-derived snippet:

```razor
<Marquee Text="@Text" Color="@TextColor" BackgroundColor="@BackgroundColor"
             Duration="@Duration" Direction="@Direction" FontSize="@FontSize" />
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

1. Read `src/BootstrapBlazor/Components/Marquee` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.