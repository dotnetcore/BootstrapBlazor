---
component: ContextMenu
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# ContextMenu Skill

## Component Purpose

A component that represents a context menu

Primary source directory: `src/BootstrapBlazor/Components/ContextMenu`.

Source files reviewed:

- `src/BootstrapBlazor/Components/ContextMenu/ContextMenu.razor`
- `src/BootstrapBlazor/Components/ContextMenu/ContextMenu.razor.cs`
- `src/BootstrapBlazor/Components/ContextMenu/ContextMenu.razor.js`
- `src/BootstrapBlazor/Components/ContextMenu/ContextMenuDivider.cs`
- `src/BootstrapBlazor/Components/ContextMenu/ContextMenuItem.cs`
- `src/BootstrapBlazor/Components/ContextMenu/ContextMenuTrigger.cs`
- `src/BootstrapBlazor/Components/ContextMenu/ContextMenuZone.razor`
- `src/BootstrapBlazor/Components/ContextMenu/ContextMenuZone.razor.cs`
- `src/BootstrapBlazor/Components/ContextMenu/IContextMenuItem.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnBeforeShowCallback` | `Func<object?, Task>?` | Callback/event parameter; Defines the callback that is executed before showing the context menu. Default is |
| `OnClick` | `Func<ContextMenuItem, object?, Task>?` | Callback/event parameter; Defines the click callback. Default is |
| `OnDisabledCallback` | `Func<ContextMenuItem, object?, bool>?` | Callback/event parameter; Defines the callback to determine if the item is disabled. Default is . It has a higher priority than |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; The that represents the child content |
| `ContextItem` | `object?` | Gets or sets the context data |
| `Disabled` | `bool` | Flags whether the item is disabled. Default is . It has a lower priority than |
| `Icon` | `string?` | The CSS class name that represents an icon (if any) |
| `IsInvisibleWhenTouchMove` | `bool` | Flags whether the context menu should be invisible while scrolling. Default is false. |
| `IsShow` | `bool` | Default: `true` |
| `OnTouchDelay` | `int?` | The timeout duration for touch events to trigger the context menu (in milliseconds). Default is milliseconds. Must be... |
| `ShowShadow` | `bool` | Default: `true`; Flags whether to show a shadow around the context menu. Default is |
| `Text` | `string?` | The text to display |
| `WrapperTag` | `string` | Default: `"div"`; The HTML tag name to use for the trigger. Default is &lt;div&gt; |

## Events And Callbacks

`OnBeforeShowCallback: Func<object?, Task>?`, `OnClick: Func<ContextMenuItem, object?, Task>?`, `OnDisabledCallback: Func<ContextMenuItem, object?, bool>?`

## Templates And Child Content

`ChildContent: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnParametersSet`, `OnAfterRenderAsync`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/ContextMenus.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/ContextMenus.razor.cs`

Sample analysis:

- Direct `<ContextMenu>` tag usages detected: 6
- Observed attributes in official Sample: `OnBeforeShowCallback`
Sample-derived snippet:

```razor
<ContextMenu>
            <ContextMenuItem Icon="fa-solid fa-copy" Text="@Localizer["ContextMenuItemCopy"]" OnClick="OnCopy"></ContextMenuItem>
            <ContextMenuItem Icon="fa-solid fa-paste" Text="@Localizer["ContextMenuItemPast"]" OnClick="OnPaste"></ContextMenuItem>
        </ContextMenu>
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

1. Read `src/BootstrapBlazor/Components/ContextMenu` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.