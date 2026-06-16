---
component: Circle
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Circle Skill

## Component Purpose

The `Circle` component is implemented in this directory.

Primary source directory: `src/BootstrapBlazor/Components/Circle`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Circle/Circle.razor`
- `src/BootstrapBlazor/Components/Circle/Circle.razor.cs`
- `src/BootstrapBlazor/Components/Circle/CircleBase.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets child content |
| `Color` | `Color` | Default: `Color.Primary`; Gets or sets component progress bar color |
| `ShowProgress` | `bool` | Default: `true`; Gets or sets whether to show progress percentage, default is true |
| `StrokeWidth` | `virtual int` | Default: `2`; Gets or sets progress bar width, default is 2 |
| `Value` | `int` | Gets or sets current value |
| `Width` | `virtual int` | Default: `120`; Gets or sets file preview box width |

## Events And Callbacks

No callback/event parameters were detected in the current source scan.

## Templates And Child Content

`ChildContent: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnParametersSet`
- JS interop or module dependency detected: `False`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Circles.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Circles.razor.cs`

Sample analysis:

- Direct `<Circle>` tag usages detected: 10
- Observed attributes in official Sample: `Color`, `ShowProgress`, `StrokeWidth`, `Value`, `Width`
Sample-derived snippet:

```razor
<Circle Value="@_circleValue" />
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

1. Read `src/BootstrapBlazor/Components/Circle` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.