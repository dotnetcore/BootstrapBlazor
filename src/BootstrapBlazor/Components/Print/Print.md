---
component: Print
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Print Skill

## Component Purpose

PrintButton Component

Primary source directory: `src/BootstrapBlazor/Components/Print`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Print/PrintButton.razor`
- `src/BootstrapBlazor/Components/Print/PrintButton.razor.cs`
- `src/BootstrapBlazor/Components/Print/PrintButton.razor.js`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `PreviewUrl` | `string?` | Gets or sets the preview template URL. Default is null |

## Events And Callbacks

No callback/event parameters were detected in the current source scan.

## Templates And Child Content

No RenderFragment parameters were detected in the current source scan.

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnParametersSet`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Print.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Print.razor.cs`

Sample analysis:

- Direct `<Print>` tag usages detected: 1
- No direct `<Print>` tag usage was detected in the matched Sample files; inspect the Sample manually before generating code.
Sample-derived snippet:

```razor
<Print> Localizer
@inject DialogService DialogService
@inject PrintService PrintService

<h3>@Localizer["PrintsTitle"]</h3>

<h4>@Localizer["PrintsSubTitle"]</h4>

<Tips>
    <ul class="ul-demo">
        <li>@((MarkupString)Localizer["PrintsTips1"].Value)</li>
        <li>@((MarkupString)Localizer["PrintsTips2"].Value)</li>
    </ul>
</Tips>

<Pre>&lt;PrintButton Icon="fa-solid fa-print" Text="@Localizer["PrintsButtonText"]" PreviewUrl="/print-view" /&gt;</Pre>

<DemoBlock Title="@Localizer["PrintButtonTitle"]" Introduction="@Localizer["PrintButtonIntro"]" Name="PrintButton">
    <section ignore>@Localizer["PrintsButtonDescription"]</section>
    <PrintButton Icon="fa-solid fa-print" Text="@Localizer["PrintsButtonText"]" PreviewUrl="/print-view" />
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

1. Read `src/BootstrapBlazor/Components/Print` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.