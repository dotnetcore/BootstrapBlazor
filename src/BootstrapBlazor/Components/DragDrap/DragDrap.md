---
component: DragDrap
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# DragDrap Skill

## Component Purpose

DragDropService component

Primary source directory: `src/BootstrapBlazor/Components/DragDrap`.

Source files reviewed:

- `src/BootstrapBlazor/Components/DragDrap/DragDropService.cs`
- `src/BootstrapBlazor/Components/DragDrap/Dropzone.razor`
- `src/BootstrapBlazor/Components/DragDrap/Dropzone.razor.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `Accepts` | `Func<TItem?, TItem?, bool>?` | Callback/event parameter; Gets or sets Accepts Delegate |
| `AllowsDrag` | `Func<TItem, bool>?` | Callback/event parameter; Gets or sets Whether current item allows drag |
| `CopyItem` | `Func<TItem, TItem>?` | Callback/event parameter; Gets or sets Copy Item Delegate |
| `ItemWrapperClass` | `Func<TItem, string>?` | Callback/event parameter; Gets or sets Item Wrapper Class |
| `OnItemDrop` | `EventCallback<TItem>` | Callback/event parameter; Gets or sets Callback for Item Drop |
| `OnItemDropRejected` | `EventCallback<TItem>` | Callback/event parameter; Gets or sets Callback for drop rejection |
| `OnItemDropRejectedByMaxItemLimit` | `EventCallback<TItem>` | Callback/event parameter; Gets or sets Callback for drop rejection by max item limit |
| `OnReplacedItemDrop` | `EventCallback<TItem>` | Callback/event parameter; Gets or sets Callback for Replaced Item Drop |
| `ChildContent` | `RenderFragment<TItem>?` | Template parameter; verify context type; Gets or sets Child Content |
| `Items` | `List<TItem>?` | Gets or sets Items to Drag |
| `MaxItems` | `int?` | Gets or sets Max Items. Default is null (unlimited) |

## Events And Callbacks

`Accepts: Func<TItem?, TItem?, bool>?`, `AllowsDrag: Func<TItem, bool>?`, `CopyItem: Func<TItem, TItem>?`, `ItemWrapperClass: Func<TItem, string>?`, `OnItemDrop: EventCallback<TItem>`, `OnItemDropRejected: EventCallback<TItem>`, `OnItemDropRejectedByMaxItemLimit: EventCallback<TItem>`, `OnReplacedItemDrop: EventCallback<TItem>`

## Templates And Child Content

`ChildContent: RenderFragment<TItem>?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

No official Sample was matched for `DragDrap`.

Use examples in this Skill only after validating the current source. If the directory contains infrastructure rather than a public Razor component, do not generate direct Razor usage.

## Examples

No direct Razor example is generated because this directory does not expose an authoritative `DragDrap.razor` usage pattern in the current repository.

## Common Mistakes

- Do not invent parameters, events, two-way bindings, or template context types.
- Do not copy examples from older BootstrapBlazor versions.
- Do not use obsolete members.
- If a Sample exists, analyze it before relying on Skill examples.
- If no Sample exists, validate every generated parameter against the current source.

## Obsolete Members

No obsolete members were detected in the current source scan.

## Agent Rules

1. Read `src/BootstrapBlazor/Components/DragDrap` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.