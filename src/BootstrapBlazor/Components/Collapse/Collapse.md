---
component: Collapse
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Collapse Skill

## Component Purpose

Collapse component

Primary source directory: `src/BootstrapBlazor/Components/Collapse`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Collapse/Collapse.razor`
- `src/BootstrapBlazor/Components/Collapse/Collapse.razor.cs`
- `src/BootstrapBlazor/Components/Collapse/CollapseItem.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnCollapseChanged` | `Func<CollapseItem, Task>?` | Callback/event parameter; Gets or sets callback when CollapseItem expands or collapses |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets component content |
| `CollapseItems` | `RenderFragment?` | Template parameter; verify context type; Gets or sets CollapseItems template |
| `HeaderTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets component Header template |
| `Class` | `string?` | Gets or sets CSS style name, default is null |
| `HeaderClass` | `string?` | Gets or sets Header CSS style name, default is null |
| `Icon` | `string?` | Gets or sets icon string, default is null |
| `IsAccordion` | `bool` | Gets or sets whether to use accordion effect, default is false |
| `IsCollapsed` | `bool` | Default: `true`; Gets or sets whether current status is collapsed, default is true |
| `Text` | `string?` | Gets or sets text |
| `TitleColor` | `Color` | Gets or sets title color, default is Color.None |

## Events And Callbacks

`OnCollapseChanged: Func<CollapseItem, Task>?`

## Templates And Child Content

`ChildContent: RenderFragment?`, `CollapseItems: RenderFragment?`, `HeaderTemplate: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `Dispose`
- JS interop or module dependency detected: `False`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Collapses.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Collapses.razor.cs`

Sample analysis:

- Direct `<Collapse>` tag usages detected: 7
- Observed attributes in official Sample: `IsAccordion`, `OnCollapseChanged`
Sample-derived snippet:

```razor
<Collapse OnCollapseChanged="@OnChanged">
        <CollapseItems>
            <CollapseItem Text="@Localizer["Consistency"]">
                <div>@Localizer["ConsistencyItem1"]</div>
                <div>@Localizer["ConsistencyItem2"]</div>
            </CollapseItem>
            <CollapseItem Text="@Localizer["Feedback"]" IsCollapsed="false">
                <div>@Localizer["FeedbackItem1"]</div>
                <div>@Localizer["FeedbackItem2"]</div>
            </CollapseItem>
            <CollapseItem Text="@Localizer["Efficiency"]">
                <div>@Localizer["EfficiencyItem1"]</div>
                <div>@Localizer["EfficiencyItem2"]</div>
                <div>@Localizer["EfficiencyItem3"]</div>
            </CollapseItem>
            <CollapseItem Text="@Localizer["Controllability"]">
                <div>@Localizer["ControllabilityItem1"]</div>
                <div>@Localizer["ControllabilityItem2"]</div>
            </CollapseItem>
        </CollapseItems>
    </Collapse>
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

1. Read `src/BootstrapBlazor/Components/Collapse` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.