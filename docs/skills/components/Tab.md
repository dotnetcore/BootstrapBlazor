---
component: Tab
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Tab Skill

## Component Purpose

BootstrapBlazorAuthorizeView Component

Primary source directory: `src/BootstrapBlazor/Components/Tab`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Tab/BootstrapBlazorAuthorizeView.cs`
- `src/BootstrapBlazor/Components/Tab/ITabHeader.cs`
- `src/BootstrapBlazor/Components/Tab/Route/RouteConstraint.cs`
- `src/BootstrapBlazor/Components/Tab/Route/RouteContext.cs`
- `src/BootstrapBlazor/Components/Tab/Route/RouteEntry.cs`
- `src/BootstrapBlazor/Components/Tab/Route/RouteKey.cs`
- `src/BootstrapBlazor/Components/Tab/Route/RouteTable.cs`
- `src/BootstrapBlazor/Components/Tab/Route/RouteTableFactory.cs`
- `src/BootstrapBlazor/Components/Tab/Route/RouteTemplate.cs`
- `src/BootstrapBlazor/Components/Tab/Route/StringSegmentAccumulator.cs`
- `src/BootstrapBlazor/Components/Tab/Route/TemplateParser.cs`
- `src/BootstrapBlazor/Components/Tab/Route/TemplateSegment.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnBeforeShowContextMenu` | `Func<TabItem, Task<bool>>?` | Callback/event parameter; Gets or sets before popup context menu callback. Default is null |
| `OnClick` | `Func<Task>?` | Callback/event parameter; Gets or sets the callback method when the link is clicked. Default is null |
| `OnClickAsync` | `Func<Task>?` | Callback/event parameter; Gets or sets the button click event handler. Default is null |
| `OnClickTabItemAsync` | `Func<TabItem, Task>?` | Callback/event parameter; Gets or sets callback method when clicking TabItem |
| `OnCloseTabItemAsync` | `Func<TabItem, Task<bool>>?` | Callback/event parameter; Gets or sets close tab item callback |
| `OnDragItemEndAsync` | `Func<TabItem, Task>?` | Callback/event parameter; Gets or sets callback method when drag item ends |
| `OnErrorHandleAsync` | `Func<ILogger, Exception, Task>?` | Callback/event parameter; Gets or sets custom error handle callback |
| `OnTabHeaderTextLocalizer` | `Func<string?, string?>?` | Callback/event parameter; Gets or sets Tab header text localizer callback |
| `OnToolbarRefreshCallback` | `Func<Task>?` | Callback/event parameter; Gets or sets the refresh toolbar button click event callback. Default is null |
| `AfterNavigatorTemplate` | `RenderFragment<Tab>?` | Template parameter; verify context type; Gets or sets after navigator template. Default is null (before next button) |
| `BeforeContextMenuTemplate` | `RenderFragment<Tab>?` | Template parameter; verify context type; Gets or sets the template of before context menu. Default is null |
| `BeforeNavigatorTemplate` | `RenderFragment<Tab>?` | Template parameter; verify context type; Gets or sets before navigator template. Default is null (before previous button) |
| `Body` | `RenderFragment?` | Template parameter; verify context type; Gets or sets TabItems body template |
| `ButtonTemplate` | `RenderFragment<Tab>?` | Template parameter; verify context type; Gets or sets button template. Default is null |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the component content. Default is null |
| `ContextMenuTemplate` | `RenderFragment<Tab>?` | Template parameter; verify context type; Gets or sets the template of context menu. Default is null |
| `HeaderTemplate` | `RenderFragment<TabItem>?` | Template parameter; verify context type; Gets or sets the TabItem Header template. Default is null |
| `NotAuthorized` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the template to display when the user is not authorized. Default is null |
| `NotFound` | `RenderFragment?` | Template parameter; verify context type; Gets or sets NotFound template. Default is null (Valid for NET6.0/7.0) |
| `ToolbarTemplate` | `RenderFragment<Tab>?` | Template parameter; verify context type; Gets or sets the template of the toolbar button. Default is null |
| `AdditionalAssemblies` | `IEnumerable<Assembly>?` | Gets or sets a collection of additional assemblies that should be searched for components that can match URIs |
| `AllowDrag` | `bool` | Gets or sets whether to allow drag tab header to change order. Default is false |
| `AlwaysLoad` | `bool` | Gets or sets whether the current TabItem is always loaded. This parameter is used to set Tab.IsLazyLoadTabItem. Defau... |
| `ClickTabToNavigation` | `bool` | Gets or sets whether to navigate when clicking TabItem. Default is false |
| `Closable` | `bool` | Default: `true`; Gets or sets whether the current TabItem is closable. Default is true |
| `CloseAllTabsText` | `string?` | Gets or sets close all tabs menu text |
| `CloseCurrentTabText` | `string?` | Gets or sets close current tab menu text |
| `CloseIcon` | `string?` | Gets or sets close icon |
| `CloseOtherTabsText` | `string?` | Gets or sets close other tabs menu text |
| `CloseTabNavLinkTooltipText` | `string?` | Gets or sets the close tab navigation link tooltip text. Default is null |
| `ContextMenuCloseAllIcon` | `string?` | Gets or sets the icon of tab item context menu close all button. Default is null |
| `ContextMenuCloseIcon` | `string?` | Gets or sets the icon of tab item context menu close button. Default is null |
| `ContextMenuCloseOtherIcon` | `string?` | Gets or sets the icon of tab item context menu close other button. Default is null |
| `ContextMenuFullScreenIcon` | `string?` | Gets or sets the icon of tab item context menu full screen button. Default is null |
| `ContextMenuRefreshIcon` | `string?` | Gets or sets the icon of tab item context menu refresh button. Default is null |
| `CssClass` | `string?` | Gets or sets the custom CSS class. Default is null |
| `DefaultUrl` | `string?` | Gets or sets default URL. Open this url when all tabs closed. Default is null |
| `DropdownIcon` | `string?` | Gets or sets dropdown icon |
| `EnableErrorLogger` | `bool?` | Gets or sets whether to enable global error logger. Default is null (Read from BootstrapBlazorOptions.EnableErrorLogger) |
| `EnableErrorLoggerILogger` | `bool?` | Gets or sets whether to log error to ILogger. Default is null (Use BootstrapBlazorOptions.EnableErrorLoggerILogger) |

Only the first 40 important parameters are listed. Inspect the current source for the remaining 41 parameters before generating code.

## Events And Callbacks

`OnBeforeShowContextMenu: Func<TabItem, Task<bool>>?`, `OnClick: Func<Task>?`, `OnClickAsync: Func<Task>?`, `OnClickTabItemAsync: Func<TabItem, Task>?`, `OnCloseTabItemAsync: Func<TabItem, Task<bool>>?`, `OnDragItemEndAsync: Func<TabItem, Task>?`, `OnErrorHandleAsync: Func<ILogger, Exception, Task>?`, `OnTabHeaderTextLocalizer: Func<string?, string?>?`, `OnToolbarRefreshCallback: Func<Task>?`

## Templates And Child Content

`AfterNavigatorTemplate: RenderFragment<Tab>?`, `BeforeContextMenuTemplate: RenderFragment<Tab>?`, `BeforeNavigatorTemplate: RenderFragment<Tab>?`, `Body: RenderFragment?`, `ButtonTemplate: RenderFragment<Tab>?`, `ChildContent: RenderFragment?`, `ContextMenuTemplate: RenderFragment<Tab>?`, `HeaderTemplate: RenderFragment<TabItem>?`, `NotAuthorized: RenderFragment?`, `NotFound: RenderFragment?`, `ToolbarTemplate: RenderFragment<Tab>?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnInitializedAsync`, `OnParametersSet`, `OnAfterRenderAsync`, `Dispose`, `DisposeAsync`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Tabs.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Tabs.razor.cs`

Sample analysis:

- Direct `<Tab>` tag usages detected: 23
- Observed attributes in official Sample: `@ref`, `AllowDrag`, `class`, `Height`, `IsBorderCard`, `IsCard`, `IsLazyLoadTabItem`, `IsOnlyRenderActiveTab`, `Placement`, `ShowClose`, `ShowContextMenu`, `ShowContextMenuFullScreen`, `ShowExtendButtons`, `ShowToolbar`, `TabStyle`
Sample-derived snippet:

```razor
<Tab>
        <TabItem Text="@Localizer["TabItem1Text"]">
            <div>@Localizer["TabItem1Content"]</div>
        </TabItem>
        <TabItem Text="@Localizer["TabItem2Text"]">
            <div>@Localizer["TabItem2Content"]</div>
        </TabItem>
        <TabItem Text="@Localizer["TabItem3Text"]">
            <div>@Localizer["TabItem3Content"]</div>
        </TabItem>
        <TabItem Text="@Localizer["TabItem4Text"]">
            <div>@Localizer["TabItem4Content"]</div>
        </TabItem>
    </Tab>
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

1. Read `src/BootstrapBlazor/Components/Tab` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.