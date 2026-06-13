---
component: Captcha
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Captcha Skill

## Component Purpose

Captcha component

Primary source directory: `src/BootstrapBlazor/Components/Captcha`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Captcha/Captcha.razor`
- `src/BootstrapBlazor/Components/Captcha/Captcha.razor.cs`
- `src/BootstrapBlazor/Components/Captcha/Captcha.razor.js`
- `src/BootstrapBlazor/Components/Captcha/CaptchaOption.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `GetImageName` | `Func<string>?` | Callback/event parameter; Gets or sets elegate |
| `OnValidAsync` | `Func<bool, Task>?` | Callback/event parameter; Gets or sets elegate |
| `BarIcon` | `string?` | Gets or sets buttonicon Default is?fa-solid fa-arrow-right |
| `BarText` | `string?` | Gets or sets Bar display |
| `Diameter` | `int` | Default: `9`; Gets or sets  |
| `FailedText` | `string?` | Gets or sets Bar display |
| `HeaderText` | `string?` | Gets or sets Header display |
| `Height` | `int` | Default: `155`; Gets or sets height |
| `ImagesName` | `string` | Default: `"Pic.jpg"`; Gets or sets  Default is Pic.jpg |
| `ImagesPath` | `string` | Default: `"images"`; Gets or sets  Default is images |
| `LoadText` | `string?` | Gets or sets Bar display |
| `Max` | `int` | Default: `1024`; Gets or sets ?Default is 1024 |
| `Offset` | `int` | Default: `5`; Gets or sets  |
| `RefreshIcon` | `string?` | Gets or sets buttonicon Default is?fa-solid fa-arrows-rotate |
| `SideLength` | `int` | Default: `42`; Gets or sets  |
| `Width` | `int` | Default: `280`; Gets or sets width |

## Events And Callbacks

`GetImageName: Func<string>?`, `OnValidAsync: Func<bool, Task>?`

## Templates And Child Content

No RenderFragment parameters were detected in the current source scan.

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnParametersSet`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Captchas.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Captchas.razor.cs`

Sample analysis:

- Direct `<Captcha>` tag usages detected: 3
- Observed attributes in official Sample: `@ref`, `GetImageName`, `ImagesName`, `ImagesPath`, `Max`, `OnValidAsync`
Sample-derived snippet:

```razor
<Captcha ImagesPath="@ImagesPath" @ref="NormalCaptcha" OnValidAsync="@OnValidAsync" Max="9" />
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

1. Read `src/BootstrapBlazor/Components/Captcha` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.