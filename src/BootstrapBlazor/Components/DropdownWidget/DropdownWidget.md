---
component: DropdownWidget
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# DropdownWidget Skill

## Component Purpose

DropdownWidget Component

Primary source directory: `src/BootstrapBlazor/Components/DropdownWidget`.

Source files reviewed:

- `src/BootstrapBlazor/Components/DropdownWidget/DropdownWidget.razor`
- `src/BootstrapBlazor/Components/DropdownWidget/DropdownWidget.razor.cs`
- `src/BootstrapBlazor/Components/DropdownWidget/DropdownWidget.razor.js`
- `src/BootstrapBlazor/Components/DropdownWidget/DropdownWidgetItem.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnItemCloseAsync` | `Func<DropdownWidgetItem, Task>?` | Callback/event parameter; Gets or sets Item Close Callback |
| `OnItemShownAsync` | `Func<DropdownWidgetItem, Task>?` | Callback/event parameter; Gets or sets Item Shown Callback |
| `BodyTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Body Template |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Child Content (Static Data) |
| `FooterTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Footer Template |
| `HeaderTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Header Template |
| `BadgeColor` | `Color` | Default: `Color.Success`; Gets or sets Badge Color. Default is Color.Success |
| `BadgeNumber` | `string?` | Gets or sets Badge Number |
| `HeaderColor` | `Color` | Default: `Color.Primary`; Gets or sets Header Color. Default is Color.Primary |
| `Icon` | `string?` | Gets or sets Widget Icon |
| `Items` | `IEnumerable<DropdownWidgetItem>?` | Gets or sets Widget Items |
| `ShowArrow` | `bool` | Default: `true`; Gets or sets Whether to Show Arrow. Default is true |
| `Title` | `string?` | Gets or sets Tooltip Title |

## Events And Callbacks

`OnItemCloseAsync: Func<DropdownWidgetItem, Task>?`, `OnItemShownAsync: Func<DropdownWidgetItem, Task>?`

## Templates And Child Content

`BodyTemplate: RenderFragment?`, `ChildContent: RenderFragment?`, `FooterTemplate: RenderFragment?`, `HeaderTemplate: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/DropdownWidgets.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/DropdownWidgets.razor.cs`

Sample analysis:

- Direct `<DropdownWidget>` tag usages detected: 1
- Observed attributes in official Sample: `OnItemCloseAsync`
Sample-derived snippet:

```razor
<DropdownWidget OnItemCloseAsync="OnItemCloseAsync">
            <DropdownWidgetItem Icon="fa-regular fa-envelope" BadgeNumber="4">
                <HeaderTemplate>
                    <span>@Localizer["BasicUsageMessage"]</span>
                </HeaderTemplate>
                <BodyTemplate>
                    @for (var index = 0; index < 4; index++)
                    {
                        <div class="dropdown-item dropdown-item-center">
                            <Avatar IsCircle="true" IsIcon="true" Size="Size.Small" />
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

1. Read `src/BootstrapBlazor/Components/DropdownWidget` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.