---
component: Row
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Row Skill

## Component Purpose

Row Component

Primary source directory: `src/BootstrapBlazor/Components/Row`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Row/Row.razor`
- `src/BootstrapBlazor/Components/Row/Row.razor.cs`
- `src/BootstrapBlazor/Components/Row/Row.razor.js`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Child Content |
| `ColSpan` | `int?` | Gets or sets Child Row span parent Row columns. Default null |
| `ItemsPerRow` | `ItemsPerRow` | Gets or sets Items per row |
| `RowType` | `RowType` | Gets or sets Row Type. Default Row Layout |

## Events And Callbacks

No callback/event parameters were detected in the current source scan.

## Templates And Child Content

`ChildContent: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: None detected in current source.
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Rows.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Rows.razor.cs`

Sample analysis:

- Direct `<Row>` tag usages detected: 18
- Observed attributes in official Sample: `ColSpan`, `ItemsPerRow`, `RowType`
Sample-derived snippet:

```razor
<Row ItemsPerRow="ItemsPerRow.Two" RowType="RowType.Inline">
            <CheckboxList @bind-Value="@Model.Hobby" Items="Hobbies1" />
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

1. Read `src/BootstrapBlazor/Components/Row` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.