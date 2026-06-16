---
component: Camera
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Camera Skill

## Component Purpose

Camera component

Primary source directory: `src/BootstrapBlazor/Components/Camera`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Camera/Camera.razor`
- `src/BootstrapBlazor/Components/Camera/Camera.razor.cs`
- `src/BootstrapBlazor/Components/Camera/Camera.razor.js`
- `src/BootstrapBlazor/Components/Camera/DeviceItem.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnClose` | `Func<Task>?` | Callback/event parameter; Gets or sets allback method |
| `OnError` | `Func<string, Task>?` | Callback/event parameter; Gets or sets callback method |
| `OnInit` | `Func<List<DeviceItem>, Task>?` | Callback/event parameter; Gets or sets callback method |
| `OnOpen` | `Func<Task>?` | Callback/event parameter; Gets or sets allback method |
| `AutoStart` | `bool` | Gets or sets whether Default is?false |
| `CaptureJpeg` | `bool` | Gets or sets ?Jpeg Default is?false  png  |
| `DeviceId` | `string?` | Gets or sets  Id Default is null |
| `Quality` | `float` | Default: `0.9f`; Gets or sets  Default is?0.9 |
| `VideoHeight` | `int?` | Default: `240`; Gets or sets eight Default is 240 |
| `VideoWidth` | `int?` | Default: `320`; Gets or sets idth Default is 320 |

## Events And Callbacks

`OnClose: Func<Task>?`, `OnError: Func<string, Task>?`, `OnInit: Func<List<DeviceItem>, Task>?`, `OnOpen: Func<Task>?`

## Templates And Child Content

No RenderFragment parameters were detected in the current source scan.

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnAfterRenderAsync`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Cameras.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Cameras.razor.cs`

Sample analysis:

- Direct `<Camera>` tag usages detected: 1
- Observed attributes in official Sample: `@ref`, `DeviceId`, `OnClose`, `OnError`, `OnInit`, `OnOpen`
Sample-derived snippet:

```razor
<Camera @ref="Camera" OnInit="@OnInit" OnOpen="@OnOpen" OnClose="@OnClose" OnError="@OnError" DeviceId="@DeviceId" />
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

1. Read `src/BootstrapBlazor/Components/Camera` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.