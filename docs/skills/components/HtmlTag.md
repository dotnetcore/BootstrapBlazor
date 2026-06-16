---
component: HtmlTag
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# HtmlTag Skill

## Component Purpose

Link Component

Primary source directory: `src/BootstrapBlazor/Components/HtmlTag`.

Source files reviewed:

- `src/BootstrapBlazor/Components/HtmlTag/Link.razor`
- `src/BootstrapBlazor/Components/HtmlTag/Link.razor.cs`
- `src/BootstrapBlazor/Components/HtmlTag/Script.razor`
- `src/BootstrapBlazor/Components/HtmlTag/Script.razor.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `Href` | `string?` | Required; Gets or sets href Property Value |
| `Src` | `string?` | Required; Gets or sets src Property Value |
| `Rel` | `string?` | Default: `"stylesheet"`; Gets or sets Rel Property Value, Default stylesheet |
| `Version` | `string?` | Gets or sets Version Number Default null Auto Generated |

## Events And Callbacks

No callback/event parameters were detected in the current source scan.

## Templates And Child Content

No RenderFragment parameters were detected in the current source scan.

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: None detected in current source.
- JS interop or module dependency detected: `False`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

No official Sample was matched for `HtmlTag`.

Use examples in this Skill only after validating the current source. If the directory contains infrastructure rather than a public Razor component, do not generate direct Razor usage.

## Examples

No direct Razor example is generated because this directory does not expose an authoritative `HtmlTag.razor` usage pattern in the current repository.

## Common Mistakes

- Do not invent parameters, events, two-way bindings, or template context types.
- Do not copy examples from older BootstrapBlazor versions.
- Do not use obsolete members.
- If a Sample exists, analyze it before relying on Skill examples.
- If no Sample exists, validate every generated parameter against the current source.

## Obsolete Members

No obsolete members were detected in the current source scan.

## Agent Rules

1. Read `src/BootstrapBlazor/Components/HtmlTag` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.