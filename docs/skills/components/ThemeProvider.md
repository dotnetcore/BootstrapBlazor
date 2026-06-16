---
component: ThemeProvider
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# ThemeProvider Skill

## Component Purpose

ThemeProvider Component

Primary source directory: `src/BootstrapBlazor/Components/ThemeProvider`.

Source files reviewed:

- `src/BootstrapBlazor/Components/ThemeProvider/ThemeProvider.razor`
- `src/BootstrapBlazor/Components/ThemeProvider/ThemeProvider.razor.cs`
- `src/BootstrapBlazor/Components/ThemeProvider/ThemeProvider.razor.js`
- `src/BootstrapBlazor/Components/ThemeProvider/ThemeValue.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnThemeChangedAsync` | `Func<ThemeValue, Task>?` | Callback/event parameter; Gets or sets the theme changed callback method |
| `ThemeValueChanged` | `EventCallback<ThemeValue>` | Callback/event parameter; Gets or sets the theme type changed callback method |
| `ActiveIcon` | `string?` | Gets or sets the active mode icon. Default is null |
| `Alignment` | `Alignment` | Default: `Alignment.Right`; Gets or sets the dropdown alignment. Default is Right |
| `AutoModeIcon` | `string?` | Gets or sets the auto mode icon. Default is null |
| `AutoModeText` | `string?` | Gets or sets the auto mode text. Default is null (uses localized resource) |
| `DarkModeIcon` | `string?` | Gets or sets the dark mode icon. Default is null |
| `DarkModeText` | `string?` | Gets or sets the dark mode text. Default is null (uses localized resource) |
| `LightModeIcon` | `string?` | Gets or sets the light mode icon. Default is null |
| `LightModeText` | `string?` | Gets or sets the light mode text. Default is null (uses localized resource) |
| `ShowShadow` | `bool` | Default: `true`; Gets or sets whether the dropdown shows shadow. Default is true |
| `ThemeValue` | `ThemeValue` | Default: `ThemeValue.UseLocalStorage`; Gets or sets the theme type |

## Events And Callbacks

`OnThemeChangedAsync: Func<ThemeValue, Task>?`, `ThemeValueChanged: EventCallback<ThemeValue>`

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

- `src/BootstrapBlazor.Server/Components/Samples/ThemeProviders.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/ThemeProviders.razor.cs`

Sample analysis:

- Direct `<ThemeProvider>` tag usages detected: 0
- No direct `<ThemeProvider>` tag usage was detected in the matched Sample files; inspect the Sample manually before generating code.

When the Sample and this Skill differ, prefer the current source, then the official Sample, then this Skill.

## Examples

Source-validated skeleton:

```razor
<ThemeProvider>
</ThemeProvider>
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

1. Read `src/BootstrapBlazor/Components/ThemeProvider` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.