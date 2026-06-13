---
component: Upload
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Upload Skill

## Component Purpose

Avatar Upload Component

Primary source directory: `src/BootstrapBlazor/Components/Upload`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Upload/AvatarUpload.razor`
- `src/BootstrapBlazor/Components/Upload/AvatarUpload.razor.cs`
- `src/BootstrapBlazor/Components/Upload/ButtonUpload.razor`
- `src/BootstrapBlazor/Components/Upload/ButtonUpload.razor.cs`
- `src/BootstrapBlazor/Components/Upload/CardUpload.razor`
- `src/BootstrapBlazor/Components/Upload/CardUpload.razor.cs`
- `src/BootstrapBlazor/Components/Upload/DropUpload.razor`
- `src/BootstrapBlazor/Components/Upload/DropUpload.razor.cs`
- `src/BootstrapBlazor/Components/Upload/FileListUploadBase.cs`
- `src/BootstrapBlazor/Components/Upload/InputUpload.razor`
- `src/BootstrapBlazor/Components/Upload/InputUpload.razor.cs`
- `src/BootstrapBlazor/Components/Upload/IUpload.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `CanPreviewCallback` | `Func<UploadFile, bool>?` | Callback/event parameter; Gets or sets the callback method to determine whether preview is allowed. Default is null |
| `OnAllFileUploaded` | `Func<IReadOnlyCollection<UploadFile>, Task>?` | Callback/event parameter; Gets or sets the callback method when all files have been uploaded. Default is null |
| `OnCancel` | `Func<UploadFile, Task>?` | Callback/event parameter; Gets or sets the callback method for the cancel button click event. Default is null |
| `OnChange` | `Func<UploadFile, Task>?` | Callback/event parameter; Gets or sets the callback method when the browse button is clicked. This may be called multiple times for multiple fi... |
| `OnDelete` | `Func<UploadFile, Task<bool>>?` | Callback/event parameter; Gets or sets the callback method when the delete button is clicked. Default is null |
| `OnDownload` | `Func<UploadFile, Task>?` | Callback/event parameter; Gets or sets the callback method for the download button click event. Default is null |
| `OnGetFileFormat` | `Func<string?, string>?` | Callback/event parameter; Gets or sets the upload file format callback method |
| `OnZoomAsync` | `Func<UploadFile, Task>?` | Callback/event parameter; Gets or sets the callback method for the zoom icon click event |
| `ActionButtonTemplate` | `RenderFragment<UploadFile>?` | Template parameter; verify context type; Gets or sets the action button template |
| `BeforeActionButtonTemplate` | `RenderFragment<UploadFile>?` | Template parameter; verify context type; Gets or sets the before action button template |
| `BodyTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the body template. Default is null. When BodyTemplate is set, IconTemplate and TextTemplate are not effe... |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the child content |
| `FooterTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the footer template. Default is null |
| `IconTemplate` | `RenderFragment<UploadFile>?` | Template parameter; verify context type; Gets or sets the icon template |
| `TextTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the text template. Default is null |
| `Accept` | `string?` | Gets or sets the accepted file format. Default is null (accepts all formats) |
| `AddIcon` | `string?` | Gets or sets the add icon |
| `AllowExtensions` | `List<string>?` | Gets or sets the allowed file extensions collection. ".png" |
| `BorderRadius` | `string?` | Gets or sets the border radius. Default is null |
| `BrowserButtonClass` | `string?` | Gets or sets the upload button style. Default is null, uses Button Primary color |
| `BrowserButtonColor` | `Color` | Default: `Color.Primary`; Gets or sets the browse button color |
| `BrowserButtonIcon` | `string?` | Gets or sets the browse button icon |
| `BrowserButtonText` | `string?` | Gets or sets the browse button display text |
| `CancelIcon` | `string?` | Gets or sets the cancel button icon |
| `Capture` | `string?` | Gets or sets the preferred facing mode for media capture. Default is null |
| `DefaultFileList` | `List<UploadFile>?` | Gets or sets the uploaded file collection for component initialization |
| `DeleteButtonClass` | `string` | Default: `"btn-danger"`; Gets or sets the delete button style. Default is btn-danger |
| `DeleteButtonIcon` | `string?` | Gets or sets the delete button icon |
| `DeleteButtonText` | `string?` | Gets or sets the delete button display text |
| `DeleteCloseButtonText` | `string?` | Gets or sets the cancel button display text in the delete confirmation dialog. Default is null |
| `DeleteConfirmButtonColor` | `Color` | Default: `Color.Danger`; Gets or sets the color of the confirmation button in the delete confirmation dialog. Default is Color.Danger |
| `DeleteConfirmButtonIcon` | `string?` | Gets or sets the confirmation button icon in the delete confirmation dialog. Default is null |
| `DeleteConfirmButtonText` | `string?` | Gets or sets the confirmation button display text in the delete confirmation dialog. Default is null |
| `DeleteConfirmContent` | `string?` | Gets or sets the confirmation text content in the delete confirmation dialog. Default is null (uses built-in text fro... |
| `DeleteIcon` | `string?` | Gets or sets the delete button icon |
| `DownloadIcon` | `string?` | Gets or sets the download button icon |
| `FileIconArchive` | `string?` | Gets or sets the archive file type icon |
| `FileIconAudio` | `string?` | Gets or sets the audio file type icon |
| `FileIconCode` | `string?` | Gets or sets the code file type icon |
| `FileIconDocx` | `string?` | Gets or sets the Word document file type icon |

Only the first 40 important parameters are listed. Inspect the current source for the remaining 38 parameters before generating code.

## Events And Callbacks

`CanPreviewCallback: Func<UploadFile, bool>?`, `OnAllFileUploaded: Func<IReadOnlyCollection<UploadFile>, Task>?`, `OnCancel: Func<UploadFile, Task>?`, `OnChange: Func<UploadFile, Task>?`, `OnDelete: Func<UploadFile, Task<bool>>?`, `OnDownload: Func<UploadFile, Task>?`, `OnGetFileFormat: Func<string?, string>?`, `OnZoomAsync: Func<UploadFile, Task>?`

## Templates And Child Content

`ActionButtonTemplate: RenderFragment<UploadFile>?`, `BeforeActionButtonTemplate: RenderFragment<UploadFile>?`, `BodyTemplate: RenderFragment?`, `ChildContent: RenderFragment?`, `FooterTemplate: RenderFragment?`, `IconTemplate: RenderFragment<UploadFile>?`, `TextTemplate: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnParametersSet`, `DisposeAsync`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

No official Sample was matched for `Upload`.

Use examples in this Skill only after validating the current source. If the directory contains infrastructure rather than a public Razor component, do not generate direct Razor usage.

## Examples

No direct Razor example is generated because this directory does not expose an authoritative `Upload.razor` usage pattern in the current repository.

## Common Mistakes

- Do not invent parameters, events, two-way bindings, or template context types.
- Do not copy examples from older BootstrapBlazor versions.
- Do not use obsolete members.
- If a Sample exists, analyze it before relying on Skill examples.
- If no Sample exists, validate every generated parameter against the current source.

## Obsolete Members

No obsolete members were detected in the current source scan.

## Agent Rules

1. Read `src/BootstrapBlazor/Components/Upload` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.