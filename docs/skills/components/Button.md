---
component: Button
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Button Skill

## Component Purpose

Button component

Primary source directory: `src/BootstrapBlazor/Components/Button`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Button/Button.razor`
- `src/BootstrapBlazor/Components/Button/Button.razor.cs`
- `src/BootstrapBlazor/Components/Button/Button.razor.js`
- `src/BootstrapBlazor/Components/Button/ButtonBase.cs`
- `src/BootstrapBlazor/Components/Button/CountButton.cs`
- `src/BootstrapBlazor/Components/Button/DialButton.razor`
- `src/BootstrapBlazor/Components/Button/DialButton.razor.cs`
- `src/BootstrapBlazor/Components/Button/DialButton.razor.js`
- `src/BootstrapBlazor/Components/Button/DialButtonItem.cs`
- `src/BootstrapBlazor/Components/Button/ExportPdfButton.cs`
- `src/BootstrapBlazor/Components/Button/ExportPdfButtonSettings.cs`
- `src/BootstrapBlazor/Components/Button/LinkButton.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `CountTextCallback` | `Func<int, string>?` | Callback/event parameter; Countdown format callback method |
| `IsActiveChanged` | `EventCallback<bool>` | Callback/event parameter; Gets or sets the active state callback method |
| `OnAfterDownload` | `Func<string, Task>?` | Callback/event parameter; Gets or sets the callback delegate after download PDF. Default is null |
| `OnBeforeClick` | `Func<Task<bool>>?` | Callback/event parameter; Gets or sets the callback method before showing the confirm popup. Returns true to show, false to prevent. Default is... |
| `OnBeforeDownload` | `Func<Stream, Task>?` | Callback/event parameter; Gets or sets the callback delegate before download PDF. Default is null |
| `OnBeforeExport` | `Func<Task>?` | Callback/event parameter; Gets or sets the callback delegate before export PDF. Default is null |
| `OnClick` | `EventCallback<MouseEventArgs>` | Callback/event parameter; Gets or sets the OnClick event |
| `OnClickWithoutRender` | `Func<Task>?` | Callback/event parameter; Gets or sets the OnClickWithoutRender event |
| `OnClose` | `Func<Task>?` | Callback/event parameter; Gets or sets the callback method when close is clicked |
| `OnConfirm` | `Func<Task>?` | Callback/event parameter; Gets or sets the confirm callback |
| `OnToggleAsync` | `Func<bool, Task>?` | Callback/event parameter; Gets or sets the state toggle callback method |
| `ToggleStateChanged` | `EventCallback<bool>` | Callback/event parameter; State toggle callback method |
| `BodyTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the expanded body template |
| `ButtonItemTemplate` | `RenderFragment<SelectedItem>?` | Template parameter; verify context type; Gets or sets the expanded button item template |
| `ButtonTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the button template |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the RenderFragment instance |
| `HeaderTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the header template |
| `ItemTemplate` | `RenderFragment<DialButtonItem>?` | Template parameter; verify context type; Expanded section template |
| `SlideButtonItems` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the data item template |
| `AutoDownload` | `bool` | Default: `true`; Gets or sets whether to auto download PDF. Default is true |
| `ButtonStyle` | `ButtonStyle` | Gets or sets the button style enum |
| `ButtonType` | `ButtonType` | Default: `ButtonType.Button`; Gets or sets the button type. Submit for form submission, Reset for form reset. Default is Button |
| `CloseButtonColor` | `Color` | Default: `Color.Secondary`; Gets or sets the close button color |
| `CloseButtonIcon` | `string?` | Gets or sets the close button icon |
| `CloseButtonText` | `string?` | Gets or sets the close button text |
| `Color` | `override Color` | Default: `Color.None`; Gets or sets the button color |
| `ConfirmButtonColor` | `Color` | Default: `Color.Primary`; Gets or sets the confirm button color |
| `ConfirmButtonIcon` | `string?` | Gets or sets the confirm button icon |
| `ConfirmButtonText` | `string?` | Gets or sets the confirm button text. Default is OK |
| `ConfirmIcon` | `string?` | Gets or sets the confirm icon |
| `Content` | `string?` | Gets or sets the display text |
| `Count` | `int` | Default: `5`; Countdown seconds. Default is 5 |
| `CountText` | `string?` | Countdown text. Default is null (uses ) |
| `CustomClass` | `string?` | Gets or sets the custom class. Default is null |
| `DialMode` | `DialMode` | Gets or sets the presentation mode. Default is Linear |
| `Duration` | `int` | Default: `400`; Gets or sets the animation duration. Default is 400ms |
| `ElementId` | `string?` | Gets or sets the export PDF element Id. Default is null |
| `FileName` | `string?` | Gets or sets the export PDF file name. Default is null (uses pdf-timestamp.pdf) |
| `HeaderText` | `string?` | Gets or sets the header text |
| `Icon` | `string?` | Gets or sets the icon |

Only the first 40 important parameters are listed. Inspect the current source for the remaining 37 parameters before generating code.

## Events And Callbacks

`CountTextCallback: Func<int, string>?`, `IsActiveChanged: EventCallback<bool>`, `OnAfterDownload: Func<string, Task>?`, `OnBeforeClick: Func<Task<bool>>?`, `OnBeforeDownload: Func<Stream, Task>?`, `OnBeforeExport: Func<Task>?`, `OnClick: EventCallback<MouseEventArgs>`, `OnClickWithoutRender: Func<Task>?`, `OnClose: Func<Task>?`, `OnConfirm: Func<Task>?`, `OnToggleAsync: Func<bool, Task>?`, `ToggleStateChanged: EventCallback<bool>`

## Templates And Child Content

`BodyTemplate: RenderFragment?`, `ButtonItemTemplate: RenderFragment<SelectedItem>?`, `ButtonTemplate: RenderFragment?`, `ChildContent: RenderFragment?`, `HeaderTemplate: RenderFragment?`, `ItemTemplate: RenderFragment<DialButtonItem>?`, `SlideButtonItems: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnParametersSet`, `OnAfterRenderAsync`, `Dispose`, `DisposeAsync`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Buttons.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Buttons.razor.cs`

Sample analysis:

- Direct `<Button>` tag usages detected: 51
- Observed attributes in official Sample: `@ref`, `ButtonStyle`, `class`, `Color`, `Icon`, `IsAsync`, `IsBlock`, `IsDisabled`, `IsOutline`, `OnClick`, `OnClickWithoutRender`, `Size`, `Text`, `TooltipPlacement`, `TooltipText`, `TooltipTrigger`
Sample-derived snippet:

```razor
<Button OnClick="@ButtonClick" Color="Color.Primary">@Localizer["PrimaryButton"]</Button>
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

1. Read `src/BootstrapBlazor/Components/Button` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.