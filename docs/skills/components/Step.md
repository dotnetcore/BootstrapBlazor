---
component: Step
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Step Skill

## Component Purpose

Step Component Class

Primary source directory: `src/BootstrapBlazor/Components/Step`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Step/Step.razor`
- `src/BootstrapBlazor/Components/Step/Step.razor.cs`
- `src/BootstrapBlazor/Components/Step/StepItem.cs`
- `src/BootstrapBlazor/Components/Step/StepOption.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnFinishedCallback` | `Func<Task>?` | Callback/event parameter; Gets or sets Callback method when all steps are finished |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Child Content |
| `FinishedTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Finished Template. Default null |
| `HeaderTemplate` | `RenderFragment<StepOption>?` | Template parameter; verify context type; Gets or sets Header Template |
| `TitleTemplate` | `RenderFragment<StepOption>?` | Template parameter; verify context type; Gets or sets Title Template |
| `Description` | `string?` | Gets or sets Description |
| `FinishedIcon` | `string?` | Gets or sets Finished Icon |
| `Icon` | `string?` | Gets or sets Icon |
| `IsVertical` | `bool` | Gets or sets Is Vertical. Default false (Horizontal) |
| `Items` | `List<StepOption>?` | Gets or sets Items |
| `StepIndex` | `int` | Gets or sets Current Step Index. Default 0 |
| `Text` | `string?` | Gets or sets Text |
| `Title` | `string?` | Gets or sets Title |

## Events And Callbacks

`OnFinishedCallback: Func<Task>?`

## Templates And Child Content

`ChildContent: RenderFragment?`, `FinishedTemplate: RenderFragment?`, `HeaderTemplate: RenderFragment<StepOption>?`, `TitleTemplate: RenderFragment<StepOption>?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnParametersSet`, `Dispose`
- JS interop or module dependency detected: `False`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Steps.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Steps.razor.cs`

Sample analysis:

- Direct `<Step>` tag usages detected: 6
- Observed attributes in official Sample: `@ref`, `IsVertical`, `Items`
Sample-derived snippet:

```razor
<Step @ref="@_step1" Items="@Items">
        <FinishedTemplate>
            <div style="height: 100px; margin-top: 1rem;">@((MarkupString)Localizer["StepsNormalFinishedTemplateDesc"].Value)</div>
        </FinishedTemplate>
    </Step>
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

1. Read `src/BootstrapBlazor/Components/Step` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.