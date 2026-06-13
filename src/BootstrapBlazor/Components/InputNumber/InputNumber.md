---
component: InputNumber
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# InputNumber Skill

## Component Purpose

BootstrapInputNumber component

Primary source directory: `src/BootstrapBlazor/Components/InputNumber`.

Source files reviewed:

- `src/BootstrapBlazor/Components/InputNumber/BootstrapInputNumber.razor`
- `src/BootstrapBlazor/Components/InputNumber/BootstrapInputNumber.razor.cs`
- `src/BootstrapBlazor/Components/InputNumber/BootstrapInputNumberBase.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnDecrement` | `Func<TValue?, Task>?` | Callback/event parameter; Gets or sets Callback delegate when value decreases |
| `OnIncrement` | `Func<TValue?, Task>?` | Callback/event parameter; Gets or sets Callback delegate when value increases |
| `Max` | `string?` | Gets or sets Maximum Value |
| `Min` | `string?` | Gets or sets Minimum Value |
| `MinusIcon` | `string?` | Gets or sets Decrement Icon |
| `PlusIcon` | `string?` | Gets or sets Increment Icon |
| `ShowButton` | `bool` | Gets or sets Whether to show increment/decrement buttons |
| `Step` | `string?` | Gets or sets Step. Default null |

## Events And Callbacks

`OnDecrement: Func<TValue?, Task>?`, `OnIncrement: Func<TValue?, Task>?`

## Templates And Child Content

No RenderFragment parameters were detected in the current source scan.

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnParametersSet`, `OnAfterRender`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/InputNumbers.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/InputNumbers.razor.cs`

Sample analysis:

- Direct `<InputNumber>` tag usages detected: 0
- No direct `<InputNumber>` tag usage was detected in the matched Sample files; inspect the Sample manually before generating code.

When the Sample and this Skill differ, prefer the current source, then the official Sample, then this Skill.

## Examples

No direct Razor example is generated because this directory does not expose an authoritative `InputNumber.razor` usage pattern in the current repository.

## Common Mistakes

- Do not invent parameters, events, two-way bindings, or template context types.
- Do not copy examples from older BootstrapBlazor versions.
- Do not use obsolete members.
- If a Sample exists, analyze it before relying on Skill examples.
- If no Sample exists, validate every generated parameter against the current source.

## Obsolete Members

No obsolete members were detected in the current source scan.

## Agent Rules

1. Read `src/BootstrapBlazor/Components/InputNumber` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.