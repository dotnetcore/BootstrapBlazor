---
component: ValidateForm
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# ValidateForm Skill

## Component Purpose

BootstrapBlazorDataAnnotationsValidator Validation Component

Primary source directory: `src/BootstrapBlazor/Components/ValidateForm`.

Source files reviewed:

- `src/BootstrapBlazor/Components/ValidateForm/BootstrapBlazorDataAnnotationsValidator.cs`
- `src/BootstrapBlazor/Components/ValidateForm/ValidateForm.razor`
- `src/BootstrapBlazor/Components/ValidateForm/ValidateForm.razor.cs`
- `src/BootstrapBlazor/Components/ValidateForm/ValidateForm.razor.js`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnFieldValueChanged` | `Action<string, object?>?` | Callback/event parameter; Gets or sets the callback method when a bound field's value has changed within the form |
| `OnInvalidSubmit` | `Func<EditContext, Task>?` | Callback/event parameter; Gets or sets the callback method when form submission is invalid |
| `OnValidSubmit` | `Func<EditContext, Task>?` | Callback/event parameter; Gets or sets the callback method when form submission is validated |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the content to be rendered inside this component |
| `DisableAutoSubmitFormByEnter` | `bool?` | Gets or sets whether to disable auto-submit form by enter key. Default is null |
| `IsFormless` | `bool` | Gets or sets whether to use a formless mode. The default is false |
| `LabelWidth` | `int?` | Gets or sets the label width. The default is null, which means the global setting --bb-row-label-width is used |
| `Model` | `object?` | Gets or sets the top-level model object for the form |
| `ShowAllInvalidResult` | `bool` | Gets or sets whether to display all validation failure messages. The default is false, which only displays the first ... |
| `ShowLabel` | `bool?` | Gets or sets whether to display labels within the validation form. The default value is null |
| `ShowLabelTooltip` | `bool?` | Gets or sets whether to display a tooltip for the label, often used when the label text is too long and gets truncate... |
| `ShowRequiredMark` | `bool` | Default: `true`; Gets or sets whether to display the required mark. The default is true, which means the required mark is displayed |
| `ValidateAllProperties` | `bool` | Gets or sets whether to validate all properties. The default is false |

## Events And Callbacks

`OnFieldValueChanged: Action<string, object?>?`, `OnInvalidSubmit: Func<EditContext, Task>?`, `OnValidSubmit: Func<EditContext, Task>?`

## Templates And Child Content

`ChildContent: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnParametersSet`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/ValidateForms.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/ValidateForms.razor.cs`

Sample analysis:

- Direct `<ValidateForm>` tag usages detected: 13
- Observed attributes in official Sample: `@ref`, `DisableAutoSubmitFormByEnter`, `Model`, `OnFieldValueChanged`, `OnInvalidSubmit`, `OnValidSubmit`, `ValidateAllProperties`
Sample-derived snippet:

```razor
<ValidateForm Model="@Model1" OnFieldValueChanged="@OnFiledChanged"
                  OnValidSubmit="@OnValidSubmit1" OnInvalidSubmit="@OnInvalidSubmit1">
        <div class="row g-3">
            <div class="col-12">
                <BootstrapInputGroup>
                    <BootstrapInputGroupLabel ShowRequiredMark DisplayText="Test"></BootstrapInputGroupLabel>
                    <BootstrapInput @bind-Value="@Model1.Name" DisplayText="@Localizer["LongDisplayText"]" />
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

1. Read `src/BootstrapBlazor/Components/ValidateForm` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.