---
component: IFrame
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# IFrame Skill

## Component Purpose

Frame component encapsulates the Html iframe element

Primary source directory: `src/BootstrapBlazor/Components/IFrame`.

Source files reviewed:

- `src/BootstrapBlazor/Components/IFrame/IFrame.razor`
- `src/BootstrapBlazor/Components/IFrame/IFrame.razor.cs`
- `src/BootstrapBlazor/Components/IFrame/IFrame.razor.js`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnPostDataAsync` | `Func<object?, Task>?` | Callback/event parameter; Gets or sets Frame loads the data passed by the page |
| `OnReadyAsync` | `Func<Task>?` | Callback/event parameter; Gets or sets Callback method after the page is loaded |
| `Data` | `object?` | Gets or sets the data to be passed |
| `Src` | `string?` | Gets or sets the URL of the webpage to be loaded in the Frame |

## Events And Callbacks

`OnPostDataAsync: Func<object?, Task>?`, `OnReadyAsync: Func<Task>?`

## Templates And Child Content

No RenderFragment parameters were detected in the current source scan.

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnAfterRenderAsync`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/IFrames.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/IFrames.razor.cs`

Sample analysis:

- Direct `<IFrame>` tag usages detected: 1
- Observed attributes in official Sample: `Src`
Sample-derived snippet:

```razor
<IFrame Src="https://pro.blazor.zone"></IFrame>
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

1. Read `src/BootstrapBlazor/Components/IFrame` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.