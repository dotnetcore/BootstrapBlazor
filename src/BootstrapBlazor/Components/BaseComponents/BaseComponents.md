---
component: BaseComponents
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# BaseComponents Skill

## Component Purpose

BootstrapBlazorRoot component

Primary source directory: `src/BootstrapBlazor/Components/BaseComponents`.

Source files reviewed:

- `src/BootstrapBlazor/Components/BaseComponents/BootstrapBlazorRoot.razor`
- `src/BootstrapBlazor/Components/BaseComponents/BootstrapBlazorRoot.razor.cs`
- `src/BootstrapBlazor/Components/BaseComponents/BootstrapComponentBase.cs`
- `src/BootstrapBlazor/Components/BaseComponents/BootstrapModuleComponentBase.cs`
- `src/BootstrapBlazor/Components/BaseComponents/DynamicElement.cs`
- `src/BootstrapBlazor/Components/BaseComponents/IdComponentBase.cs`
- `src/BootstrapBlazor/Components/BaseComponents/RenderTemplate.razor`
- `src/BootstrapBlazor/Components/BaseComponents/RenderTemplate.razor.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnBeforeRenderAsync` | `Func<Task>?` | Callback/event parameter; Gets or sets the callback delegate before rendering child content |
| `OnClick` | `Func<Task>?` | Callback/event parameter; Gets or sets the Click callback delegate |
| `OnContextMenu` | `Func<MouseEventArgs, Task>?` | Callback/event parameter; Gets or sets the OnContextMenu callback delegate |
| `OnDoubleClick` | `Func<Task>?` | Callback/event parameter; Gets or sets the DoubleClick callback delegate |
| `OnErrorHandleAsync` | `Func<ILogger, Exception, Task>?` | Callback/event parameter; Gets or sets custom error callback method |
| `OnRenderAsync` | `Func<bool, Task>?` | Callback/event parameter; Gets or sets the callback delegate for the first load |
| `OnTouchEnd` | `Func<TouchEventArgs, Task>?` | Callback/event parameter; Gets or sets the OnTouchEnd callback delegate |
| `OnTouchStart` | `Func<TouchEventArgs, Task>?` | Callback/event parameter; Gets or sets the OnTouchStart callback delegate |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the child component |
| `AdditionalAttributes` | `IDictionary<string, object>?` | Gets or sets user defined attributes |
| `EnableErrorLogger` | `bool?` | Gets or sets whether to enable global exception handling. Default is null (use ) |
| `EnableErrorLoggerILogger` | `bool?` | Gets or sets whether to log exceptions to . Default is null (use ) |
| `GenerateElement` | `bool` | Default: `true`; Gets or sets whether to generate the specified Tag element. Default is true |
| `Id` | `string?` | Gets or sets the component id |
| `Key` | `object?` | Gets or sets the unique key of the element. Default null |
| `PreventDefault` | `bool` | Gets or sets whether to prevent default behavior. Default is false |
| `ShowErrorLoggerToast` | `bool?` | Gets or sets whether to show Error toast. Default is null (use ) |
| `StopPropagation` | `bool` | Gets or sets whether to stop event propagation. Default is false |
| `TagName` | `string?` | Default: `"div"`; Gets or sets the TagName property. Default is div |
| `ToastTitle` | `string?` | Gets or sets Error Toast title |
| `TriggerClick` | `bool` | Default: `true`; Gets or sets whether to trigger Click event. Default is true |
| `TriggerContextMenu` | `bool` | Gets or sets whether to trigger OnContextMenu event. Default is false |
| `TriggerDoubleClick` | `bool` | Default: `true`; Gets or sets whether to trigger DoubleClick event. Default is true |
| `TriggerTouchEnd` | `bool` | Gets or sets whether to trigger OnTouchEnd events. Default is false |
| `TriggerTouchStart` | `bool` | Gets or sets whether to trigger OnTouchStart events. Default is false |

## Events And Callbacks

`OnBeforeRenderAsync: Func<Task>?`, `OnClick: Func<Task>?`, `OnContextMenu: Func<MouseEventArgs, Task>?`, `OnDoubleClick: Func<Task>?`, `OnErrorHandleAsync: Func<ILogger, Exception, Task>?`, `OnRenderAsync: Func<bool, Task>?`, `OnTouchEnd: Func<TouchEventArgs, Task>?`, `OnTouchStart: Func<TouchEventArgs, Task>?`

## Templates And Child Content

`ChildContent: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnParametersSetAsync`, `OnAfterRenderAsync`, `Dispose`, `DisposeAsync`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

No official Sample was matched for `BaseComponents`.

Use examples in this Skill only after validating the current source. If the directory contains infrastructure rather than a public Razor component, do not generate direct Razor usage.

## Examples

No direct Razor example is generated because this directory does not expose an authoritative `BaseComponents.razor` usage pattern in the current repository.

## Common Mistakes

- Do not invent parameters, events, two-way bindings, or template context types.
- Do not copy examples from older BootstrapBlazor versions.
- Do not use obsolete members.
- If a Sample exists, analyze it before relying on Skill examples.
- If no Sample exists, validate every generated parameter against the current source.

## Obsolete Members

No obsolete members were detected in the current source scan.

## Agent Rules

1. Read `src/BootstrapBlazor/Components/BaseComponents` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.