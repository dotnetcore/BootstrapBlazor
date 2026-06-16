---
component: Footer
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Footer Skill

## Component Purpose

Footer Component

Primary source directory: `src/BootstrapBlazor/Components/Footer`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Footer/Footer.razor`
- `src/BootstrapBlazor/Components/Footer/Footer.razor.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Content |
| `ShowGoto` | `bool` | Default: `true`; Gets or sets Whether to Show Goto Widget Default true |
| `Target` | `string?` | Gets or sets The component where the scrollbar controlled by the back-to-top button in the Footer component is locate... |
| `Text` | `string?` | Gets or sets Footer Text |

## Events And Callbacks

No callback/event parameters were detected in the current source scan.

## Templates And Child Content

`ChildContent: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: None detected in current source.
- JS interop or module dependency detected: `False`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Footers.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Footers.razor.cs`

Sample analysis:

- Direct `<Footer>` tag usages detected: 3
- Observed attributes in official Sample: `ShowGoto`, `Target`, `Text`
Sample-derived snippet:

```razor
<Footer Text="Bootstrap Blazor" Target="#scroll" />
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

1. Read `src/BootstrapBlazor/Components/Footer` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.