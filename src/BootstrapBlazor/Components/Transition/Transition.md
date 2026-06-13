---
component: Transition
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Transition Skill

## Component Purpose

Transition Animation Component

Primary source directory: `src/BootstrapBlazor/Components/Transition`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Transition/Transition.razor`
- `src/BootstrapBlazor/Components/Transition/Transition.razor.cs`
- `src/BootstrapBlazor/Components/Transition/Transition.razor.js`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnTransitionEnd` | `Func<Task>?` | Callback/event parameter; Gets or sets the animation completion callback delegate |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the child content |
| `Duration` | `int` | Gets or sets the animation execution duration in milliseconds. Default is 0 |
| `Show` | `bool` | Default: `true`; Gets or sets whether to display the animation. Default is true |
| `TransitionType` | `TransitionType` | Default: `TransitionType.FadeIn`; Gets or sets the animation name. Default is FadeIn |

## Events And Callbacks

`OnTransitionEnd: Func<Task>?`

## Templates And Child Content

`ChildContent: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Transitions.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Transitions.razor.cs`

Sample analysis:

- Direct `<Transition>` tag usages detected: 3
- Observed attributes in official Sample: `Duration`, `OnTransitionEnd`, `Show`, `TransitionType`
Sample-derived snippet:

```razor
<Transition TransitionType="TransitionType.FadeOut" Show="Show" OnTransitionEnd="OnShowEnd">
        <div class="my-3">FadeOut</div>
    </Transition>
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

1. Read `src/BootstrapBlazor/Components/Transition` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.