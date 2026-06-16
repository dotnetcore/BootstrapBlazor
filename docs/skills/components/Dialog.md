---
component: Dialog
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Dialog Skill

## Component Purpose

Dialog component

Primary source directory: `src/BootstrapBlazor/Components/Dialog`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Dialog/Dialog.razor`
- `src/BootstrapBlazor/Components/Dialog/Dialog.razor.cs`
- `src/BootstrapBlazor/Components/Dialog/DialogBase.cs`
- `src/BootstrapBlazor/Components/Dialog/DialogCloseButton.cs`
- `src/BootstrapBlazor/Components/Dialog/DialogOption.cs`
- `src/BootstrapBlazor/Components/Dialog/DialogResult.cs`
- `src/BootstrapBlazor/Components/Dialog/DialogSaveButton.cs`
- `src/BootstrapBlazor/Components/Dialog/DialogService.cs`
- `src/BootstrapBlazor/Components/Dialog/EditDialog.razor`
- `src/BootstrapBlazor/Components/Dialog/EditDialog.razor.cs`
- `src/BootstrapBlazor/Components/Dialog/EditDialog.razor.js`
- `src/BootstrapBlazor/Components/Dialog/EditDialogOption.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnChanged` | `Func<FilterKeyValueAction, Task>?` | Callback/event parameter; Gets or sets the filter changed callback event Func version |
| `OnClickClose` | `Func<Task>?` | Callback/event parameter; Obsolete; do not use; Gets or sets Click Close Button Callback Method |
| `OnClickNo` | `Func<Task>?` | Callback/event parameter; Obsolete; do not use; Gets or sets Click No Button Callback Method |
| `OnClickYes` | `Func<Task>?` | Callback/event parameter; Obsolete; do not use; Gets or sets Click Yes Button Callback Method |
| `OnCloseAsync` | `Func<Task>?` | Callback/event parameter; Gets or sets Close Dialog Callback Method |
| `OnResetSearchClick` | `Func<Task>?` | Callback/event parameter; Gets or sets Reset Callback Delegate |
| `OnSaveAsync` | `Func<Task<bool>>?` | Callback/event parameter; Gets or sets Save Callback Method. Close Dialog when return true |
| `OnSearchClick` | `Func<Task>?` | Callback/event parameter; Gets or sets Search Callback Delegate |
| `BodyTemplate` | `RenderFragment<TModel>?` | Template parameter; verify context type; Gets or sets BodyTemplate Instance |
| `FooterTemplate` | `RenderFragment<TModel>?` | Template parameter; verify context type; Gets or sets DialogFooterTemplate Instance |
| `ButtonCloseColor` | `Color` | Obsolete; do not use; Default: `Color.Secondary`; Close Button Color |
| `ButtonCloseIcon` | `string?` | Obsolete; do not use; Close Button Icon |
| `ButtonCloseText` | `string?` | Obsolete; do not use; Close Button Text |
| `ButtonNoColor` | `Color` | Default: `Color.Danger`; No Button Color |
| `ButtonNoIcon` | `string?` | No Button Icon |
| `ButtonNoText` | `string?` | No Button Text |
| `ButtonText` | `string?` | Gets or sets Copy Button Text |
| `ButtonYesIcon` | `string?` | Yes Button Icon |
| `ButtonYesText` | `string?` | Yes Button Text |
| `ClearIcon` | `string?` | Gets or sets Clear Button Icon |
| `CloseButtonIcon` | `string?` | Gets or sets Close Button Icon |
| `CloseButtonText` | `string?` | Gets or sets Close Button Text |
| `CloseConfirmContent` | `string?` | Gets or sets Close Confirm Dialog Content |
| `CloseConfirmTitle` | `string?` | Gets or sets Close Confirm Dialog Title |
| `Color` | `override Color` | Default: `Color.Secondary`; Gets or sets Button Color |
| `CopiedTooltipText` | `string?` | Gets or sets Copied Tooltip Text |
| `DisableAutoSubmitFormByEnter` | `bool?` | Gets or sets Whether to Disable Auto Submit Form By Enter. Default is null |
| `IsTracking` | `bool` | Gets or sets Whether Component Uses Tracking Mode to Update Editing Items Directly. Default is false |
| `ItemChangedType` | `ItemChangedType` | Gets or sets Item Changed Type (Add or Update) |
| `Items` | `IEnumerable<IEditorItem>?` | Get Items |
| `ItemsPerRow` | `int?` | Gets or sets Items Per Row. Default is null |
| `LabelAlign` | `Alignment` | Gets or sets Label Alignment in Inline Mode. Default is None, equivalent to Left |
| `LabelFullText` | `string?` | Gets or sets Label Text |
| `LabelText` | `string?` | Gets or sets Label Text |
| `LabelWidth` | `int?` | Gets or sets Label Width. Default is 120 |
| `Model` | `TModel?` | Gets or sets EditModel Instance |
| `QueryButtonText` | `string?` | Gets or sets Query Button Text |
| `ResetButtonText` | `string?` | Gets or sets Reset Button Text |
| `RowType` | `RowType` | Gets or sets Row Layout. Default is Row |
| `SaveButtonIcon` | `string?` | Gets or sets Save Button Icon |

Only the first 40 important parameters are listed. Inspect the current source for the remaining 12 parameters before generating code.

## Events And Callbacks

`OnChanged: Func<FilterKeyValueAction, Task>?`, `OnClickClose: Func<Task>?`, `OnClickNo: Func<Task>?`, `OnClickYes: Func<Task>?`, `OnCloseAsync: Func<Task>?`, `OnResetSearchClick: Func<Task>?`, `OnSaveAsync: Func<Task<bool>>?`, `OnSearchClick: Func<Task>?`

## Templates And Child Content

`BodyTemplate: RenderFragment<TModel>?`, `FooterTemplate: RenderFragment<TModel>?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnParametersSet`, `OnAfterRenderAsync`, `Dispose`, `DisposeAsync`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Dialogs.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Dialogs.razor.cs`

Sample analysis:

- Direct `<Dialog>` tag usages detected: 0
- No direct `<Dialog>` tag usage was detected in the matched Sample files; inspect the Sample manually before generating code.

When the Sample and this Skill differ, prefer the current source, then the official Sample, then this Skill.

## Examples

Source-validated skeleton:

```razor
<Dialog>
</Dialog>
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

1. Read `src/BootstrapBlazor/Components/Dialog` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.