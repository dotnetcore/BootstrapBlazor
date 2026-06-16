---
component: SweetAlert
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# SweetAlert Skill

## Component Purpose

SweetAlert Option Configuration Class

Primary source directory: `src/BootstrapBlazor/Components/SweetAlert`.

Source files reviewed:

- `src/BootstrapBlazor/Components/SweetAlert/SwalOption.cs`
- `src/BootstrapBlazor/Components/SweetAlert/SwalService.cs`
- `src/BootstrapBlazor/Components/SweetAlert/SweetAlert.razor`
- `src/BootstrapBlazor/Components/SweetAlert/SweetAlert.razor.cs`
- `src/BootstrapBlazor/Components/SweetAlert/SweetAlertBody.razor`
- `src/BootstrapBlazor/Components/SweetAlert/SweetAlertBody.razor.cs`
- `src/BootstrapBlazor/Components/SweetAlert/SweetContext.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnCloseAsync` | `Func<Task>?` | Callback/event parameter; Gets or sets Close Callback Method |
| `OnConfirmAsync` | `Func<Task>?` | Callback/event parameter; Gets or sets Confirm Callback Method |
| `BodyTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Body Template |
| `ButtonTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Button Template |
| `FooterTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Footer Template |
| `CancelButtonText` | `string?` | Gets or sets Cancel Button Text. Default Cancel |
| `Category` | `SwalCategory` | Gets or sets Category. Default Success |
| `CloseButtonIcon` | `string?` | Gets or sets Close Button Icon |
| `CloseButtonText` | `string?` | Gets or sets Close Button Text. Default Close |
| `ConfirmButtonIcon` | `string?` | Gets or sets Confirm Button Icon |
| `ConfirmButtonText` | `string?` | Gets or sets Confirm Button Text. Default Confirm |
| `Content` | `string?` | Gets or sets Content |
| `Footer` | `string?` | Gets or sets Footer content string |
| `IsConfirm` | `bool` | Gets or sets Whether is confirm dialog mode. Default false |
| `ShowClose` | `bool` | Default: `true`; Gets or sets Whether to show close button. Default true |
| `ShowFooter` | `bool` | Gets or sets Whether to show footer. Default false |
| `Title` | `string?` | Gets or sets Title |

## Events And Callbacks

`OnCloseAsync: Func<Task>?`, `OnConfirmAsync: Func<Task>?`

## Templates And Child Content

`BodyTemplate: RenderFragment?`, `ButtonTemplate: RenderFragment?`, `FooterTemplate: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnParametersSet`, `OnAfterRenderAsync`, `Dispose`, `DisposeAsync`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/SweetAlerts.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/SweetAlerts.razor.cs`

Sample analysis:

- Direct `<SweetAlert>` tag usages detected: 0
- No direct `<SweetAlert>` tag usage was detected in the matched Sample files; inspect the Sample manually before generating code.

When the Sample and this Skill differ, prefer the current source, then the official Sample, then this Skill.

## Examples

Source-validated skeleton:

```razor
<SweetAlert>
</SweetAlert>
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

1. Read `src/BootstrapBlazor/Components/SweetAlert` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.