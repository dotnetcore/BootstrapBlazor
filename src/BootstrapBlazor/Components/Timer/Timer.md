---
component: Timer
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Timer Skill

## Component Purpose

Timer Component

Primary source directory: `src/BootstrapBlazor/Components/Timer`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Timer/Timer.razor`
- `src/BootstrapBlazor/Components/Timer/Timer.razor.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnCancel` | `Func<Task>?` | Callback/event parameter; Gets or sets the callback delegate when cancelled |
| `OnTimeout` | `Func<Task>?` | Callback/event parameter; Gets or sets the callback delegate when the countdown ends |
| `CancelText` | `string?` | Gets or sets the cancel button display text |
| `Icon` | `string?` | Gets or sets the alert icon |
| `IsVibrate` | `bool` | Default: `true`; Gets or sets whether the device vibrates when the countdown ends |
| `PauseText` | `string?` | Gets or sets the pause button display text |
| `ResumeText` | `string?` | Gets or sets the resume button display text |
| `StarText` | `string?` | Gets or sets the start button display text |
| `StrokeWidth` | `override int` | Default: `6`; Gets or sets the progress bar width. Default is 6 |
| `Value` | `TimeSpan` | Gets or sets the current value |
| `Width` | `override int` | Default: `300`; Gets or sets the component width |

## Events And Callbacks

`OnCancel: Func<Task>?`, `OnTimeout: Func<Task>?`

## Templates And Child Content

No RenderFragment parameters were detected in the current source scan.

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnParametersSet`, `OnAfterRenderAsync`, `Dispose`, `DisposeAsync`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Timers.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Timers.razor.cs`

Sample analysis:

- Direct `<Timer>` tag usages detected: 2
- Observed attributes in official Sample: `Color`, `OnCancel`, `OnTimeout`
Sample-derived snippet:

```razor
<Timer OnTimeout="@OnTimeout" OnCancel="OnCancel" />
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

1. Read `src/BootstrapBlazor/Components/Timer` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.