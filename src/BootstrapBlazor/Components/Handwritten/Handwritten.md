---
component: Handwritten
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Handwritten Skill

## Component Purpose

Handwritten Signature

Primary source directory: `src/BootstrapBlazor/Components/Handwritten`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Handwritten/Handwritten.razor`
- `src/BootstrapBlazor/Components/Handwritten/Handwritten.razor.cs`
- `src/BootstrapBlazor/Components/Handwritten/Handwritten.razor.js`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `HandwrittenBase64` | `EventCallback<string>` | Callback/event parameter; Handwritten Result Callback Method |
| `ClearButtonText` | `string?` | Clear Button Text |
| `Result` | `string?` | Handwritten Signature imgBase64 String |
| `SaveButtonText` | `string?` | Save Button Text |

## Events And Callbacks

`HandwrittenBase64: EventCallback<string>`

## Templates And Child Content

No RenderFragment parameters were detected in the current source scan.

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Handwrittens.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Handwrittens.razor.cs`

Sample analysis:

- Direct `<Handwritten>` tag usages detected: 0
- No direct `<Handwritten>` tag usage was detected in the matched Sample files; inspect the Sample manually before generating code.

When the Sample and this Skill differ, prefer the current source, then the official Sample, then this Skill.

## Examples

Source-validated skeleton:

```razor
<Handwritten>
</Handwritten>
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

1. Read `src/BootstrapBlazor/Components/Handwritten` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.