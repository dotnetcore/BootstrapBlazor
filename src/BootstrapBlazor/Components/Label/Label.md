---
component: Label
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Label Skill

## Component Purpose

BootstrapLabel component

Primary source directory: `src/BootstrapBlazor/Components/Label`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Label/BootstrapLabel.razor`
- `src/BootstrapBlazor/Components/Label/BootstrapLabel.razor.cs`
- `src/BootstrapBlazor/Components/Label/BootstrapLabelSetting.razor`
- `src/BootstrapBlazor/Components/Label/BootstrapLabelSetting.razor.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets omponent |
| `LabelWidth` | `int?` | /   null  --bb-row-label-width ?/para> Gets or sets width Default is null etsSets --bb-row-label-width ?/para> |
| `ShowLabelTooltip` | `bool?` | Gets or sets whether to display a tooltip for the label, often used when the label text is too long and gets truncate... |
| `Value` | `string?` | Gets or sets the component value display text. Default is null |

## Events And Callbacks

No callback/event parameters were detected in the current source scan.

## Templates And Child Content

`ChildContent: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnParametersSet`
- JS interop or module dependency detected: `False`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Labels.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Labels.razor.cs`

Sample analysis:

- Direct `<Label>` tag usages detected: 0
- No direct `<Label>` tag usage was detected in the matched Sample files; inspect the Sample manually before generating code.

When the Sample and this Skill differ, prefer the current source, then the official Sample, then this Skill.

## Examples

No direct Razor example is generated because this directory does not expose an authoritative `Label.razor` usage pattern in the current repository.

## Common Mistakes

- Do not invent parameters, events, two-way bindings, or template context types.
- Do not copy examples from older BootstrapBlazor versions.
- Do not use obsolete members.
- If a Sample exists, analyze it before relying on Skill examples.
- If no Sample exists, validate every generated parameter against the current source.

## Obsolete Members

No obsolete members were detected in the current source scan.

## Agent Rules

1. Read `src/BootstrapBlazor/Components/Label` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.