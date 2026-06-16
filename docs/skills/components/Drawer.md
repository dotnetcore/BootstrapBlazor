---
component: Drawer
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Drawer Skill

## Component Purpose

Drawer component

Primary source directory: `src/BootstrapBlazor/Components/Drawer`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Drawer/Drawer.razor`
- `src/BootstrapBlazor/Components/Drawer/Drawer.razor.cs`
- `src/BootstrapBlazor/Components/Drawer/Drawer.razor.js`
- `src/BootstrapBlazor/Components/Drawer/DrawerContainer.cs`
- `src/BootstrapBlazor/Components/Drawer/DrawerOption.cs`
- `src/BootstrapBlazor/Components/Drawer/DrawerService.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `IsOpenChanged` | `EventCallback<bool>` | Callback/event parameter; Gets or sets Callback for IsOpen Property Change |
| `OnClickBackdrop` | `Func<Task>?` | Callback/event parameter; Gets or sets Callback for Backdrop Click. Default is null |
| `OnCloseAsync` | `Func<Task>?` | Callback/event parameter; Gets or sets Close Drawer Callback Delegate. Default is null |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Child Content |
| `AllowResize` | `bool` | Gets or sets Whether to Allow Resize. Default is false |
| `BodyContext` | `object?` | Gets or sets Drawer Context Data. Used for passing values |
| `BodyScroll` | `bool` | Gets or sets Whether to allow body scrolling when drawer is shown. Default is false |
| `Height` | `string` | Default: `"290px"`; Gets or sets Drawer Height. Effective when layout is Top/Bottom |
| `IsBackdrop` | `bool` | Gets or sets Whether to Close Drawer on Backdrop Click. Default is false |
| `IsKeyboard` | `bool` | Gets or sets Whether to support ESC key to close. Default is false |
| `IsOpen` | `bool` | Gets or sets Whether Drawer is Open. Default is false |
| `Placement` | `Placement` | Default: `Placement.Left`; Gets or sets Component Placement. Default is Left |
| `Position` | `string?` | Gets or sets Component Position. Default is null (Fixed). Can be set to absolute |
| `ShowBackdrop` | `bool` | Default: `true`; Gets or sets Whether to Show Backdrop. Default is true |
| `Width` | `string` | Default: `"360px"`; Gets or sets Drawer Width. Effective when layout is Left/Right |
| `ZIndex` | `int?` | Gets or sets z-index parameter. Default is null |

## Events And Callbacks

`IsOpenChanged: EventCallback<bool>`, `OnClickBackdrop: Func<Task>?`, `OnCloseAsync: Func<Task>?`

## Templates And Child Content

`ChildContent: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnAfterRenderAsync`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Drawers.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Drawers.razor.cs`

Sample analysis:

- Direct `<Drawer>` tag usages detected: 5
- Observed attributes in official Sample: `@bind-IsOpen`, `AllowResize`, `BodyScroll`, `IsBackdrop`, `IsKeyboard`, `Placement`, `ShowBackdrop`
Sample-derived snippet:

```razor
<Drawer Placement="@DrawerAlign" @bind-IsOpen="@IsOpen" AllowResize="true">
        <div class="d-flex justify-content-center align-items-center flex-column" style="height: 290px;">
            <p>@Localizer["Content"]</p>
            <button type="button" class="btn btn-primary" @onclick="@(e => IsOpen = false)">@Localizer["Close"]</button>
        </div>
    </Drawer>
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

1. Read `src/BootstrapBlazor/Components/Drawer` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.