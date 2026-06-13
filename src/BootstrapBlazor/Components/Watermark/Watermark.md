---
component: Watermark
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Watermark Skill

## Component Purpose

Watermark Component

Primary source directory: `src/BootstrapBlazor/Components/Watermark`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Watermark/Watermark.razor`
- `src/BootstrapBlazor/Components/Watermark/Watermark.razor.cs`
- `src/BootstrapBlazor/Components/Watermark/Watermark.razor.js`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets component content |
| `Color` | `string?` | Gets or sets color Default is null Not set |
| `FontSize` | `int?` | Gets or sets font size Default is null Not set Default is 16px Font size Unit is px |
| `Gap` | `int?` | Gets or sets watermark gap value Default is null Not set |
| `IsPage` | `bool` | Gets or sets whether it is a full-page watermark Default is false |
| `Rotate` | `int?` | Gets or sets watermark rotation angle Default is null Not set Default is 45 |
| `Text` | `string?` | Gets or sets watermark text Default is BootstrapBlazor |
| `ZIndex` | `int?` | Gets or sets watermark element's z-index value Default is null Not set |

## Events And Callbacks

No callback/event parameters were detected in the current source scan.

## Templates And Child Content

`ChildContent: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnParametersSet`, `OnAfterRenderAsync`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Watermarks.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Watermarks.razor.cs`

Sample analysis:

- Direct `<Watermark>` tag usages detected: 1
- Observed attributes in official Sample: `Color`, `FontSize`, `Gap`, `Rotate`, `Text`
Sample-derived snippet:

```razor
<Watermark Text="@_text" FontSize="@_fontSize" Color="@_color" Rotate="@_rotate"
               Gap="@_gap">
        <div style="height: 500px; padding-top: 40px; text-align: center;">
            <p>this is a watermark demo</p>
            <div> watermark </div>
        </div>
    </Watermark>
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

1. Read `src/BootstrapBlazor/Components/Watermark` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.