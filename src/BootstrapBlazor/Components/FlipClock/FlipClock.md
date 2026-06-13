---
component: FlipClock
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# FlipClock Skill

## Component Purpose

FlipClock Component

Primary source directory: `src/BootstrapBlazor/Components/FlipClock`.

Source files reviewed:

- `src/BootstrapBlazor/Components/FlipClock/FlipClock.razor`
- `src/BootstrapBlazor/Components/FlipClock/FlipClock.razor.cs`
- `src/BootstrapBlazor/Components/FlipClock/FlipClock.razor.js`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnCompletedAsync` | `Func<Task>?` | Callback/event parameter; Gets or sets Timer Completed Callback Method Default null |
| `BackgroundColor` | `string?` | Gets or sets Component Background Color Default null Use Style Default Value if not set radial-gradient(ellipse at ce... |
| `CardBackgroundColor` | `string?` | Gets or sets Component Card Background Color Default null Use Style Default Value if not set #333; |
| `CardColor` | `string?` | Gets or sets Component Card Font Color Default null Use Style Default Value if not set #ccc; |
| `CardDividerColor` | `string?` | Gets or sets Component Card Divider Color Default null Use Style Default Value if not set rgba(0, 0, 0, .4); |
| `CardDividerHeight` | `string?` | Gets or sets Component Card Divider Height Default null Use Style Default Value if not set 1px; |
| `CardGroupMargin` | `string?` | Gets or sets Component Card Group Margin Default null Use Style Default Value if not set 20; |
| `CardHeight` | `string?` | Gets or sets Component Card Height Default null Use Style Default Value if not set 90px; |
| `CardMargin` | `string?` | Gets or sets Component Card Margin Default null Use Style Default Value if not set 5; |
| `CardWidth` | `string?` | Gets or sets Component Card Width Default null Use Style Default Value if not set 60px; |
| `FontSize` | `string?` | Gets or sets Component Font Size Default null Use Style Default Value if not set 80px; |
| `Height` | `string?` | Gets or sets Component Height Default null Use Style Default Value if not set 200px; |
| `ShowDay` | `bool` | Gets or sets Whether to Show Day Default false |
| `ShowHour` | `bool` | Default: `true`; Gets or sets Whether to Show Hour Default true |
| `ShowMinute` | `bool` | Default: `true`; Gets or sets Whether to Show Minute Default true |
| `ShowMonth` | `bool` | Gets or sets Whether to Show Month Default false |
| `ShowSecond` | `bool` | Default: `true`; Gets or sets Whether to Show Second Default true |
| `ShowYear` | `bool` | Gets or sets Whether to Show Year Default false |
| `StartValue` | `TimeSpan` | Gets or sets Start Time for Countdown or Count Default Effective in Mode |
| `ViewMode` | `FlipClockViewMode` | Gets or sets View Mode Default |

## Events And Callbacks

`OnCompletedAsync: Func<Task>?`

## Templates And Child Content

No RenderFragment parameters were detected in the current source scan.

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: None detected in current source.
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/FlipClocks.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/FlipClocks.razor.cs`

Sample analysis:

- Direct `<FlipClock>` tag usages detected: 3
- Observed attributes in official Sample: `BackgroundColor`, `CardGroupMargin`, `CardHeight`, `CardMargin`, `CardWidth`, `FontSize`, `Height`, `OnCompletedAsync`, `ShowDay`, `ShowHour`, `ShowMinute`, `ShowMonth`, `ShowSecond`, `ShowYear`, `StartValue`, `ViewMode`
Sample-derived snippet:

```razor
<FlipClock ViewMode="FlipClockViewMode.Count"></FlipClock>
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

1. Read `src/BootstrapBlazor/Components/FlipClock` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.