---
component: LazyLoad
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# LazyLoad Skill

## Component Purpose

Lazy load component

Primary source directory: `src/BootstrapBlazor/Components/LazyLoad`.

Source files reviewed:

- `src/BootstrapBlazor/Components/LazyLoad/LazyLoad.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnFirstLoadCallbackAsync` | `Func<Task>?` | Callback/event parameter; Gets or sets first display callback method. Can be used for component initialization data. Only triggered once. |
| `OnLoadConditionCheckAsync` | `Func<Task<bool>>?` | Callback/event parameter; Gets or sets component load condition callback method. Default is null. Once it returns true, this callback will no l... |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets child component |

## Events And Callbacks

`OnFirstLoadCallbackAsync: Func<Task>?`, `OnLoadConditionCheckAsync: Func<Task<bool>>?`

## Templates And Child Content

`ChildContent: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnParametersSetAsync`
- JS interop or module dependency detected: `False`
- Razor component file detected: `False`
- Keep generated code aligned with the files listed above.

## Sample Mapping

No official Sample was matched for `LazyLoad`.

Use examples in this Skill only after validating the current source. If the directory contains infrastructure rather than a public Razor component, do not generate direct Razor usage.

## Examples

No direct Razor example is generated because this directory does not expose an authoritative `LazyLoad.razor` usage pattern in the current repository.

## Common Mistakes

- Do not invent parameters, events, two-way bindings, or template context types.
- Do not copy examples from older BootstrapBlazor versions.
- Do not use obsolete members.
- If a Sample exists, analyze it before relying on Skill examples.
- If no Sample exists, validate every generated parameter against the current source.

## Obsolete Members

No obsolete members were detected in the current source scan.

## Agent Rules

1. Read `src/BootstrapBlazor/Components/LazyLoad` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.