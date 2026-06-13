---
component: ImagePreviewer
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# ImagePreviewer Skill

## Component Purpose

Image Previewer Component

Primary source directory: `src/BootstrapBlazor/Components/ImagePreviewer`.

Source files reviewed:

- `src/BootstrapBlazor/Components/ImagePreviewer/ImagePreviewer.razor`
- `src/BootstrapBlazor/Components/ImagePreviewer/ImagePreviewer.razor.cs`
- `src/BootstrapBlazor/Components/ImagePreviewer/ImagePreviewer.razor.js`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `MinusIcon` | `string?` | Gets or sets Zoom Out Icon |
| `NextIcon` | `string?` | Gets or sets Next Image Icon |
| `PlusIcon` | `string?` | Gets or sets Zoom In Icon |
| `PreviousIcon` | `string?` | Gets or sets Previous Image Icon |
| `RotateLeftIcon` | `string?` | Gets or sets Rotate Left Icon |
| `RotateRightIcon` | `string?` | Gets or sets Rotate Right Icon |
| `ZIndex` | `int` | Default: `2050`; Gets or sets z-index property Default 2050 |
| `ZoomSpeed` | `double?` | Gets or sets Zoom Speed Default null 0.015 if not set |

## Events And Callbacks

No callback/event parameters were detected in the current source scan.

## Templates And Child Content

No RenderFragment parameters were detected in the current source scan.

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnParametersSet`, `OnAfterRenderAsync`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

No official Sample was matched for `ImagePreviewer`.

Use examples in this Skill only after validating the current source. If the directory contains infrastructure rather than a public Razor component, do not generate direct Razor usage.

## Examples

Source-validated skeleton:

```razor
<ImagePreviewer>
</ImagePreviewer>
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

1. Read `src/BootstrapBlazor/Components/ImagePreviewer` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.