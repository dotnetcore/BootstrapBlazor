---
component: Nav
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Nav Skill

## Component Purpose

NavMenu Component Base Class

Primary source directory: `src/BootstrapBlazor/Components/Nav`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Nav/Nav.razor`
- `src/BootstrapBlazor/Components/Nav/Nav.razor.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Component Content |
| `Alignment` | `Alignment` | Default: `Alignment.Left`; Gets or sets Component Alignment |
| `IsFill` | `bool` | Gets or sets Whether to fill |
| `IsJustified` | `bool` | Gets or sets Whether to be equal width |
| `IsPills` | `bool` | Gets or sets Whether it is pills |
| `IsVertical` | `bool` | Gets or sets Whether to distribute vertically |
| `Items` | `IEnumerable<NavLink>?` | Gets or sets Component Data Source |

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

No official Sample was matched for `Nav`.

Use examples in this Skill only after validating the current source. If the directory contains infrastructure rather than a public Razor component, do not generate direct Razor usage.

## Examples

Source-validated skeleton:

```razor
<Nav>
</Nav>
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

1. Read `src/BootstrapBlazor/Components/Nav` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.