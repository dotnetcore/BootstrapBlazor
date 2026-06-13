---
component: Typed
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Typed Skill

## Component Purpose

Typed Component Class

Primary source directory: `src/BootstrapBlazor/Components/Typed`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Typed/Typed.razor`
- `src/BootstrapBlazor/Components/Typed/Typed.razor.cs`
- `src/BootstrapBlazor/Components/Typed/Typed.razor.js`
- `src/BootstrapBlazor/Components/Typed/TypedOptions.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnCompleteAsync` | `Func<Task>?` | Callback/event parameter; Gets or sets the callback method when typing is complete. Default is null |
| `Options` | `TypedOptions?` | Gets or sets the component configuration instance. Default is null |
| `Text` | `string?` | Gets or sets the component display text. Default is null |

## Events And Callbacks

`OnCompleteAsync: Func<Task>?`

## Templates And Child Content

No RenderFragment parameters were detected in the current source scan.

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnAfterRenderAsync`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Typeds.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Typeds.razor.cs`

Sample analysis:

- Direct `<Typed>` tag usages detected: 2
- Observed attributes in official Sample: `Options`, `Text`
Sample-derived snippet:

```razor
<Typed Text="<code>BootstrapBlazor</code> is an enterprise-level UI component library"></Typed>
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

1. Read `src/BootstrapBlazor/Components/Typed` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.