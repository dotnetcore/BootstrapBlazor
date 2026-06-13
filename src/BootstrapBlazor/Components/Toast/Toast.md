---
component: Toast
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Toast Skill

## Component Purpose

Toast Component

Primary source directory: `src/BootstrapBlazor/Components/Toast`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Toast/Toast.razor`
- `src/BootstrapBlazor/Components/Toast/Toast.razor.cs`
- `src/BootstrapBlazor/Components/Toast/Toast.razor.js`
- `src/BootstrapBlazor/Components/Toast/ToastContainer.razor`
- `src/BootstrapBlazor/Components/Toast/ToastContainer.razor.cs`
- `src/BootstrapBlazor/Components/Toast/ToastOption.cs`
- `src/BootstrapBlazor/Components/Toast/ToastService.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `Options` | `ToastOption?` | Required; Gets or sets the ToastOption instance |
| `Placement` | `Placement` | Gets or sets the popup window placement |

## Events And Callbacks

No callback/event parameters were detected in the current source scan.

## Templates And Child Content

No RenderFragment parameters were detected in the current source scan.

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnParametersSet`, `OnAfterRenderAsync`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Toasts.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Toasts.razor.cs`

Sample analysis:

- Direct `<Toast>` tag usages detected: 4
- Observed attributes in official Sample: `class`, `Options`
Sample-derived snippet:

```razor
<Toast class="d-toast show" Options="@Options1" />
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

1. Read `src/BootstrapBlazor/Components/Toast` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.