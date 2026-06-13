---
component: Card
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Card Skill

## Component Purpose

Card component

Primary source directory: `src/BootstrapBlazor/Components/Card`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Card/Card.razor`
- `src/BootstrapBlazor/Components/Card/Card.razor.cs`
- `src/BootstrapBlazor/Components/Card/Card.razor.js`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `CollapsedChanged` | `EventCallback<bool>` | Callback/event parameter; Gets or sets whether it is collapsed. Default is false (expanded) |
| `BodyTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the BodyTemplate template |
| `FooterTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the FooterTemplate template |
| `HeaderTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the CardHeader template |
| `Collapsed` | `bool` | Gets or sets whether it is collapsed. Default is false (expanded) |
| `CollapseIcon` | `string?` | Gets or sets the collapse/expand arrow icon. Default is fa-solid fa-circle-chevron-right |
| `Color` | `Color` | Gets or sets the Card color |
| `HeaderPaddingY` | `string?` | Gets or sets the Card Header height padding Y-axis value. Default is null |
| `HeaderText` | `string?` | Gets or sets the HeaderTemplate display text |
| `IsCenter` | `bool` | Gets or sets whether to center. Default is false |
| `IsCollapsible` | `bool` | Gets or sets whether it is collapsible. Default is false |
| `IsShadow` | `bool` | Gets or sets whether to show shadow. Default is false |

## Events And Callbacks

`CollapsedChanged: EventCallback<bool>`

## Templates And Child Content

`BodyTemplate: RenderFragment?`, `FooterTemplate: RenderFragment?`, `HeaderTemplate: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnParametersSet`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Cards.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Cards.razor.cs`

Sample analysis:

- Direct `<Card>` tag usages detected: 17
- Observed attributes in official Sample: `class`, `Collapsed`, `Color`, `HeaderText`, `IsCenter`, `IsCollapsible`, `IsShadow`
Sample-derived snippet:

```razor
<Card>
        <BodyTemplate>
            <h5>Card title</h5>
            <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
            <a class="btn btn-primary">Go somewhere</a>
        </BodyTemplate>
    </Card>
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

1. Read `src/BootstrapBlazor/Components/Card` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.