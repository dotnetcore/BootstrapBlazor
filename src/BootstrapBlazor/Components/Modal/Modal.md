---
component: Modal
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Modal Skill

## Component Purpose

the closable interface

Primary source directory: `src/BootstrapBlazor/Components/Modal`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Modal/IClosable.cs`
- `src/BootstrapBlazor/Components/Modal/Modal.razor`
- `src/BootstrapBlazor/Components/Modal/Modal.razor.cs`
- `src/BootstrapBlazor/Components/Modal/Modal.razor.js`
- `src/BootstrapBlazor/Components/Modal/ModalDialog.razor`
- `src/BootstrapBlazor/Components/Modal/ModalDialog.razor.cs`
- `src/BootstrapBlazor/Components/Modal/ModalDialog.razor.js`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `FirstAfterRenderCallbackAsync` | `Func<Modal, Task>?` | Callback/event parameter; Gets or sets the callback method when the component has finished rendering |
| `GetResultDialog` | `Func<IResultDialog?>?` | Callback/event parameter; Gets or sets Get Modal Popup Method. Default null |
| `OnCloseAsync` | `Func<Task>?` | Callback/event parameter |
| `OnClosingAsync` | `Func<Task<bool>>?` | Callback/event parameter |
| `OnSaveAsync` | `Func<Task<bool>>?` | Callback/event parameter; Gets or sets Save button callback delegate. Returns true and automatically closes popup if is true |
| `OnShownAsync` | `Func<Task>?` | Callback/event parameter; Gets or sets the callback method when the popup is shown |
| `BodyTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets ModalBody Component |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the child component |
| `FooterContentTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the footer content template. Default is null |
| `FooterTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets ModalFooter Component |
| `HeaderTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets ModalHeader Component |
| `HeaderToolbarTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Button template in Header |
| `BodyContext` | `object?` | Gets or sets Data related to popup content. Often used for passing values |
| `Class` | `string?` | Gets or sets Popup Custom Style |
| `CloseButtonIcon` | `string?` | Gets or sets Close button icon. Use fa-solid fa-fw fa-xmark if not set |
| `CloseButtonText` | `string?` | Gets or sets Close button text. Resource file set to Close |
| `ExportPdfButtonOptions` | `ExportPdfButtonOptions?` | Gets or sets Export PDF button options |
| `FullScreenSize` | `FullScreenSize` | Gets or sets Popup Full Screen Size. Default |
| `IsAutoCloseAfterSave` | `bool` | Default: `true`; Gets or sets Whether to automatically close popup after successful save. Default true |
| `IsBackdrop` | `bool` | Gets or sets whether to close the popup in the background, default is false |
| `IsCentered` | `bool` | Default: `true`; Gets or sets Whether to center vertically. Default true |
| `IsDraggable` | `bool` | Gets or sets Whether popup can be dragged. Default false |
| `IsFade` | `bool?` | Gets or sets whether to enable fade in and out animation, default is null |
| `IsHidePreviousDialog` | `bool` | Gets or sets whether to hide the previous dialog when opening a new one, default is false |
| `IsKeyboard` | `bool` | Default: `true`; Gets or sets whether to enable keyboard support, default is true to respond to the ESC key |
| `IsScrolling` | `bool` | Gets or sets Whether to scroll when popup body is too long. Default false |
| `MaximizeWindowIcon` | `string?` | Gets or sets Maximize button icon |
| `PrintButtonColor` | `Color` | Default: `Color.Primary`; Gets or sets Print button color. Default Color.Primary |
| `PrintButtonIcon` | `string?` | Gets or sets Print button icon. If not set, use print icon from current icon theme |
| `PrintButtonText` | `string?` | Gets or sets Print button text in Header. Default from resource file "Print" |
| `RestoreWindowIcon` | `string?` | Gets or sets Restore button icon |
| `ResultTask` | `TaskCompletionSource<DialogResult>?` | Gets or sets Modal Popup Task Instance. Default null |
| `SaveButtonIcon` | `string?` | Gets or sets Save button icon. Use theme icon if not set |
| `SaveButtonText` | `string?` | Gets or sets Save button text. Resource file set to Save |
| `ShowCloseButton` | `bool` | Default: `true`; Gets or sets Whether to show close button. Default true (Show) |
| `ShowExportPdfButton` | `bool` | Gets or sets Whether to show Export PDF button. Default false (Not shown) |
| `ShowExportPdfButtonInHeader` | `bool` | Gets or sets Whether to show Export PDF button in Header. Default false (Not shown) |
| `ShowFooter` | `bool` | Default: `true`; Gets or sets Whether to show Footer. Default true |
| `ShowHeader` | `bool` | Default: `true`; Gets or sets Whether to show Header. Default true |
| `ShowHeaderCloseButton` | `bool` | Default: `true`; Gets or sets Whether to show Header Close Button |

Only the first 40 important parameters are listed. Inspect the current source for the remaining 7 parameters before generating code.

## Events And Callbacks

`FirstAfterRenderCallbackAsync: Func<Modal, Task>?`, `GetResultDialog: Func<IResultDialog?>?`, `OnCloseAsync: Func<Task>?`, `OnClosingAsync: Func<Task<bool>>?`, `OnSaveAsync: Func<Task<bool>>?`, `OnShownAsync: Func<Task>?`

## Templates And Child Content

`BodyTemplate: RenderFragment?`, `ChildContent: RenderFragment?`, `FooterContentTemplate: RenderFragment?`, `FooterTemplate: RenderFragment?`, `HeaderTemplate: RenderFragment?`, `HeaderToolbarTemplate: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnParametersSet`, `OnAfterRender`, `OnAfterRenderAsync`, `Dispose`, `DisposeAsync`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Modals.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Modals.razor.cs`

Sample analysis:

- Direct `<Modal>` tag usages detected: 17
- Observed attributes in official Sample: `@ref`, `IsBackdrop`, `IsKeyboard`, `OnShownAsync`
Sample-derived snippet:

```razor
<Modal @ref="Modal" IsKeyboard="true">
        <ModalDialog Title="@Localizer["ModalsNormalDefaultPopup"]" ShowSaveButton="true" OnSaveAsync="OnSaveAsync">
            <BodyTemplate>
                <div>@Localizer["ModalsNormalDefaultPopupText"]</div>
            </BodyTemplate>
        </ModalDialog>
    </Modal>
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

1. Read `src/BootstrapBlazor/Components/Modal` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.