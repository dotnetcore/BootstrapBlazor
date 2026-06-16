---
component: Message
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Message Skill

## Component Purpose

Message Component

Primary source directory: `src/BootstrapBlazor/Components/Message`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Message/Message.razor`
- `src/BootstrapBlazor/Components/Message/Message.razor.cs`
- `src/BootstrapBlazor/Components/Message/Message.razor.js`
- `src/BootstrapBlazor/Components/Message/MessageOption.cs`
- `src/BootstrapBlazor/Components/Message/MessageService.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `Placement` | `Placement` | Default: `Placement.Top`; Gets or sets Display placement. Default Top |

## Events And Callbacks

No callback/event parameters were detected in the current source scan.

## Templates And Child Content

No RenderFragment parameters were detected in the current source scan.

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnAfterRenderAsync`, `Dispose`, `DisposeAsync`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Messages.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Messages.razor.cs`

Sample analysis:

- Direct `<Message>` tag usages detected: 2
- Observed attributes in official Sample: `@ref`, `Placement`
Sample-derived snippet:

```razor
<Message @ref="Message" Placement="@Placement"></Message>
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

1. Read `src/BootstrapBlazor/Components/Message` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.