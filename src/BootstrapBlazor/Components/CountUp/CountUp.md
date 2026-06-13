---
component: CountUp
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# CountUp Skill

## Component Purpose

CountUp component

Primary source directory: `src/BootstrapBlazor/Components/CountUp`.

Source files reviewed:

- `src/BootstrapBlazor/Components/CountUp/CountUp.razor`
- `src/BootstrapBlazor/Components/CountUp/CountUp.razor.cs`
- `src/BootstrapBlazor/Components/CountUp/CountUp.razor.js`
- `src/BootstrapBlazor/Components/CountUp/CountUpOption.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnCompleted` | `Func<Task>?` | Callback/event parameter; Gets or sets callback method when counting ends, default is null |
| `Option` | `CountUpOption?` | Gets or sets count configuration item, default is null |
| `Value` | `TValue?` | Gets or sets Value |

## Events And Callbacks

`OnCompleted: Func<Task>?`

## Templates And Child Content

No RenderFragment parameters were detected in the current source scan.

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnParametersSet`, `OnAfterRenderAsync`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/CountUps.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/CountUps.razor.cs`

Sample analysis:

- Direct `<CountUp>` tag usages detected: 1
- Observed attributes in official Sample: `class`, `OnCompleted`, `Option`, `TValue`, `Value`
Sample-derived snippet:

```razor
<CountUp TValue="double" Value="@Value" Option="_option" OnCompleted="OnCompleted" class="fw-bold fs-1 mb-2 d-block"></CountUp>
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

1. Read `src/BootstrapBlazor/Components/CountUp` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.