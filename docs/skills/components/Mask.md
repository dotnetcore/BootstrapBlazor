---
component: Mask
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Mask Skill

## Component Purpose

Mask Component

Primary source directory: `src/BootstrapBlazor/Components/Mask`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Mask/Mask.razor`
- `src/BootstrapBlazor/Components/Mask/Mask.razor.cs`
- `src/BootstrapBlazor/Components/Mask/Mask.razor.js`
- `src/BootstrapBlazor/Components/Mask/MaskOption.cs`
- `src/BootstrapBlazor/Components/Mask/MaskService.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

No [Parameter] properties were detected in the current source.

## Events And Callbacks

No callback/event parameters were detected in the current source scan.

## Templates And Child Content

No RenderFragment parameters were detected in the current source scan.

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnAfterRenderAsync`, `DisposeAsync`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Masks.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Masks.razor.cs`

Sample analysis:

- Direct `<Mask>` tag usages detected: 2
- Observed attributes in official Sample: `@ref`
Sample-derived snippet:

```razor
<Mask @ref="CustomMask1"></Mask>
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

1. Read `src/BootstrapBlazor/Components/Mask` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.