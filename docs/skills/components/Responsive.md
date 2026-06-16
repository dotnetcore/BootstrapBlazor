---
component: Responsive
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Responsive Skill

## Component Purpose

Responsive Component

Primary source directory: `src/BootstrapBlazor/Components/Responsive`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Responsive/Responsive.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnBreakPointChanged` | `Func<BreakPoint, Task>?` | Required; Callback/event parameter; Gets or sets the callback method triggered when browser breakpoint threshold changes. Default is null |

## Events And Callbacks

`OnBreakPointChanged: Func<BreakPoint, Task>?`

## Templates And Child Content

No RenderFragment parameters were detected in the current source scan.

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: None detected in current source.
- JS interop or module dependency detected: `True`
- Razor component file detected: `False`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Responsives.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Responsives.razor.cs`

Sample analysis:

- Direct `<Responsive>` tag usages detected: 1
- Observed attributes in official Sample: `OnBreakPointChanged`
Sample-derived snippet:

```razor
<Responsive OnBreakPointChanged="OnChanged" />
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

1. Read `src/BootstrapBlazor/Components/Responsive` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.