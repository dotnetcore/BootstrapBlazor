---
component: ErrorLogger
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# ErrorLogger Skill

## Component Purpose

Internal Use Custom Exception Component

Primary source directory: `src/BootstrapBlazor/Components/ErrorLogger`.

Source files reviewed:

- `src/BootstrapBlazor/Components/ErrorLogger/BootstrapBlazorErrorBoundary.cs`
- `src/BootstrapBlazor/Components/ErrorLogger/ErrorLogger.cs`
- `src/BootstrapBlazor/Components/ErrorLogger/ErrorRender.cs`
- `src/BootstrapBlazor/Components/ErrorLogger/IErrorLogger.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnErrorHandleAsync` | `Func<ILogger, Exception, Task>?` | Callback/event parameter; Gets or sets Custom Error Handler |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Child Content |
| `ErrorContent` | `RenderFragment<Exception>?` | Template parameter; verify context type; Gets or sets Exception Display Template Default null |
| `EnableErrorLogger` | `bool` | Default: `true` |
| `EnableILogger` | `bool` | Default: `true` |
| `ShowToast` | `bool` | Default: `true`; Gets or sets Whether to Show Toast. Default true |
| `ToastTitle` | `string?` | Gets or sets Toast Title |

## Events And Callbacks

`OnErrorHandleAsync: Func<ILogger, Exception, Task>?`

## Templates And Child Content

`ChildContent: RenderFragment?`, `ErrorContent: RenderFragment<Exception>?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`
- JS interop or module dependency detected: `False`
- Razor component file detected: `False`
- Keep generated code aligned with the files listed above.

## Sample Mapping

No official Sample was matched for `ErrorLogger`.

Use examples in this Skill only after validating the current source. If the directory contains infrastructure rather than a public Razor component, do not generate direct Razor usage.

## Examples

No direct Razor example is generated because this directory does not expose an authoritative `ErrorLogger.razor` usage pattern in the current repository.

## Common Mistakes

- Do not invent parameters, events, two-way bindings, or template context types.
- Do not copy examples from older BootstrapBlazor versions.
- Do not use obsolete members.
- If a Sample exists, analyze it before relying on Skill examples.
- If no Sample exists, validate every generated parameter against the current source.

## Obsolete Members

No obsolete members were detected in the current source scan.

## Agent Rules

1. Read `src/BootstrapBlazor/Components/ErrorLogger` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.