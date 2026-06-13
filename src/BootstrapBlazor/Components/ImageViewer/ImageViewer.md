---
component: ImageViewer
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# ImageViewer Skill

## Component Purpose

Image Component

Primary source directory: `src/BootstrapBlazor/Components/ImageViewer`.

Source files reviewed:

- `src/BootstrapBlazor/Components/ImageViewer/ImageViewer.razor`
- `src/BootstrapBlazor/Components/ImageViewer/ImageViewer.razor.cs`
- `src/BootstrapBlazor/Components/ImageViewer/ImageViewer.razor.js`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `Url` | `string?` | Required; Gets or sets Image Url Default null Required |
| `OnErrorAsync` | `Func<string, Task>?` | Callback/event parameter; Gets or sets Callback method when image loading fails |
| `OnLoadAsync` | `Func<string, Task>?` | Callback/event parameter; Gets or sets Callback method when image loading succeeds |
| `ErrorTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Error Template. Default null |
| `PlaceHolderTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Placeholder Template. Default null |
| `Alt` | `string?` | Gets or sets Native alt Attribute Default null |
| `FileIcon` | `string?` | Gets or sets Image File Icon |
| `FitMode` | `ObjectFitMode` | Gets or sets Native object-fit Attribute. Default fill |
| `HandleError` | `bool` | Gets or sets Whether to show error placeholder when loading fails. Default false |
| `IsAsync` | `bool` | Gets or sets whether the image is loaded asynchronously |
| `IsIntersectionObserver` | `bool` | Gets or sets Whether Intersection Observer. Default false |
| `PreviewIndex` | `int` | Default: `0`; Gets or sets Index of the currently opened link in the preview image list Default 0 |
| `PreviewList` | `List<string>?` | Gets or sets Preview Image List Default null |
| `ShowPlaceHolder` | `bool` | Gets or sets Whether to show placeholder. Suitable for large image loading. Default false |
| `ZIndex` | `int` | Default: `2050`; Gets or sets Native z-index Attribute. Default 2050 |
| `ZoomSpeed` | `double?` | Gets or sets Zoom Speed Default null 0.015 if not set |

## Events And Callbacks

`OnErrorAsync: Func<string, Task>?`, `OnLoadAsync: Func<string, Task>?`

## Templates And Child Content

`ErrorTemplate: RenderFragment?`, `PlaceHolderTemplate: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnParametersSet`, `OnAfterRenderAsync`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/ImageViewers.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/ImageViewers.razor.cs`

Sample analysis:

- Direct `<ImageViewer>` tag usages detected: 13
- Observed attributes in official Sample: `FitMode`, `HandleError`, `IsAsync`, `IsIntersectionObserver`, `PreviewList`, `ShowPlaceHolder`, `style`, `Url`, `ZoomSpeed`
Sample-derived snippet:

```razor
<ImageViewer Url="@WebsiteOption.Value.GetAssetUrl("images/bird.jpeg")" FitMode="ObjectFitMode.Fill" />
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

1. Read `src/BootstrapBlazor/Components/ImageViewer` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.