---
component: Validate
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Validate Skill

## Component Purpose

IValidateCollection Interface - Supports multiple validation results and cross-component validation linkage

Primary source directory: `src/BootstrapBlazor/Components/Validate`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Validate/IValidateCollection.cs`
- `src/BootstrapBlazor/Components/Validate/IValidateComponent.cs`
- `src/BootstrapBlazor/Components/Validate/ValidateBase.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnValueChanged` | `Func<TValue?, Task>?` | Callback/event parameter; Gets or sets the callback method when value changes |
| `IsDisabled` | `bool` | Gets or sets whether the component is disabled. Default is false |
| `ParsingErrorMessage` | `string?` | Gets or sets the parsing error message format string. Default is null |
| `RequiredErrorMessage` | `string?` | Gets or sets the required field error message. Default is null |
| `ShowRequired` | `bool?` | Gets or sets whether to display the required field marker. Default is null |
| `SkipValidate` | `bool` | Gets or sets whether to skip validation. Default is false |
| `ValidateRules` | `List<IValidator>?` | Gets or sets the custom validation collection |

## Events And Callbacks

`OnValueChanged: Func<TValue?, Task>?`

## Templates And Child Content

No RenderFragment parameters were detected in the current source scan.

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnParametersSet`, `OnAfterRenderAsync`, `DisposeAsync`
- JS interop or module dependency detected: `True`
- Razor component file detected: `False`
- Keep generated code aligned with the files listed above.

## Sample Mapping

No official Sample was matched for `Validate`.

Use examples in this Skill only after validating the current source. If the directory contains infrastructure rather than a public Razor component, do not generate direct Razor usage.

## Examples

No direct Razor example is generated because this directory does not expose an authoritative `Validate.razor` usage pattern in the current repository.

## Common Mistakes

- Do not invent parameters, events, two-way bindings, or template context types.
- Do not copy examples from older BootstrapBlazor versions.
- Do not use obsolete members.
- If a Sample exists, analyze it before relying on Skill examples.
- If no Sample exists, validate every generated parameter against the current source.

## Obsolete Members

No obsolete members were detected in the current source scan.

## Agent Rules

1. Read `src/BootstrapBlazor/Components/Validate` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.