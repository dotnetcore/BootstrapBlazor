---
component: Reconnector
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Reconnector Skill

## Component Purpose

IReconnector Interface

Primary source directory: `src/BootstrapBlazor/Components/Reconnector`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Reconnector/IReconnector.cs`
- `src/BootstrapBlazor/Components/Reconnector/IReconnectorProvider.cs`
- `src/BootstrapBlazor/Components/Reconnector/Reconnector.cs`
- `src/BootstrapBlazor/Components/Reconnector/ReconnectorContent.razor`
- `src/BootstrapBlazor/Components/Reconnector/ReconnectorContent.razor.cs`
- `src/BootstrapBlazor/Components/Reconnector/ReconnectorContent.razor.js`
- `src/BootstrapBlazor/Components/Reconnector/ReconnectorOutlet.cs`
- `src/BootstrapBlazor/Components/Reconnector/ReconnectorProvider.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `ReconnectFailedTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets ReconnectFailedTemplate |
| `ReconnectingTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Reconnecting Template |
| `ReconnectRejectedTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets ReconnectRejectedTemplate |
| `AutoReconnect` | `bool` | Default: `true`; Gets or sets whether to auto reconnect. Default is true |
| `ReconnectInterval` | `int` | Default: `5000`; Gets or sets the auto reconnect interval. Default is 5000ms. Minimum is 1000ms |

## Events And Callbacks

No callback/event parameters were detected in the current source scan.

## Templates And Child Content

`ReconnectFailedTemplate: RenderFragment?`, `ReconnectingTemplate: RenderFragment?`, `ReconnectRejectedTemplate: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnAfterRender`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Reconnectors.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Reconnectors.razor.cs`

Sample analysis:

- Direct `<Reconnector>` tag usages detected: 0
- No direct `<Reconnector>` tag usage was detected in the matched Sample files; inspect the Sample manually before generating code.

When the Sample and this Skill differ, prefer the current source, then the official Sample, then this Skill.

## Examples

No direct Razor example is generated because this directory does not expose an authoritative `Reconnector.razor` usage pattern in the current repository.

## Common Mistakes

- Do not invent parameters, events, two-way bindings, or template context types.
- Do not copy examples from older BootstrapBlazor versions.
- Do not use obsolete members.
- If a Sample exists, analyze it before relying on Skill examples.
- If no Sample exists, validate every generated parameter against the current source.

## Obsolete Members

No obsolete members were detected in the current source scan.

## Agent Rules

1. Read `src/BootstrapBlazor/Components/Reconnector` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.