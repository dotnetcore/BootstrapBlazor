---
component: Layout
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Layout Skill

## Component Purpose

Layout Component

Primary source directory: `src/BootstrapBlazor/Components/Layout`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Layout/Layout.razor`
- `src/BootstrapBlazor/Components/Layout/Layout.razor.cs`
- `src/BootstrapBlazor/Components/Layout/Layout.razor.js`
- `src/BootstrapBlazor/Components/Layout/LayoutHeader.cs`
- `src/BootstrapBlazor/Components/Layout/LayoutSplitBar.razor`
- `src/BootstrapBlazor/Components/Layout/LayoutSplitBar.razor.cs`
- `src/BootstrapBlazor/Components/Layout/LayoutSplitBar.razor.js`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `IsCollapsedChanged` | `EventCallback<bool>` | Callback/event parameter; Gets or sets Sidebar status |
| `OnAuthorizing` | `Func<string, Task<bool>>?` | Callback/event parameter; Gets or sets Authorization callback method, mostly used for permission control |
| `OnBeforeShowContextMenu` | `Func<TabItem, Task<bool>>?` | Callback/event parameter; Gets or sets before popup context menu callback. Default is null |
| `OnClickMenu` | `Func<MenuItem, Task>?` | Callback/event parameter; Gets or sets Callback delegate method when menu is clicked. Default null |
| `OnCloseTabItemAsync` | `Func<TabItem, Task<bool>>?` | Callback/event parameter; Gets or sets Callback method before closing tab |
| `OnCollapsed` | `Func<bool, Task>?` | Callback/event parameter; Gets or sets Collapse/Expand Callback Delegate |
| `OnErrorHandleAsync` | `Func<ILogger, Exception, Task>?` | Callback/event parameter; Gets or sets Custom error handling callback method |
| `OnTabHeaderTextLocalizer` | `Func<string?, string?>?` | Callback/event parameter; Gets or sets Localization callback method for Tab header text |
| `OnToolbarRefreshCallback` | `Func<Task>?` | Callback/event parameter; Gets or sets the refresh toolbar button click event callback. Default is null |
| `OnUpdateAsync` | `Func<string, Task>?` | Callback/event parameter; Gets or sets Update callback method. Default null |
| `BeforeTabContextMenuTemplate` | `RenderFragment<Tab>?` | Template parameter; verify context type; Gets or sets the template of before tab context menu. Default is null |
| `CollapseBarTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Collapse/Expand button template |
| `Footer` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Footer Template |
| `Header` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Header Template |
| `Main` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Main Template |
| `NotAuthorized` | `RenderFragment?` | Template parameter; verify context type; Gets or sets NotAuthorized Template. Default null. Valid in NET6.0/7.0 |
| `NotFound` | `RenderFragment?` | Template parameter; verify context type; Gets or sets NotFound Template. Default null. Valid in NET6.0/7.0 |
| `Side` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Side Template |
| `TabContextMenuTemplate` | `RenderFragment<Tab>?` | Template parameter; verify context type; Gets or sets the template of tab context menu. Default is null |
| `ToolbarTemplate` | `RenderFragment<Tab>?` | Template parameter; verify context type; Gets or sets the template of the toolbar button. Default is null |
| `AdditionalAssemblies` | `IEnumerable<Assembly>?` | Gets or sets a collection of additional assemblies that should be searched for components that can match URIs |
| `AllowDragTab` | `bool` | Default: `true`; Gets or sets Whether to allow dragging tabs. Default true |
| `ClickTabToNavigation` | `bool` | Default: `true`; Gets or sets Whether to switch address bar when clicking tab. Default true |
| `ContainerSelector` | `string?` | Gets or sets Container Selector. Default null |
| `EnableErrorLogger` | `bool?` | Gets or sets Whether to enable global exception capture. Default null. Use value |
| `EnableErrorLoggerILogger` | `bool?` | Gets or sets Whether to log exceptions to . Default null. Use value |
| `ErrorLoggerToastTitle` | `string?` | Gets or sets Error Logger Title. Default null |
| `ExcludeUrls` | `IEnumerable<string>?` | Gets or sets Exclude URLs support wildcards |
| `FullscreenToolbarButtonIcon` | `string?` | Gets or sets the full screen toolbar button icon string. Default is null |
| `FullscreenToolbarTooltipText` | `string?` | Gets or sets the full screen toolbar button tooltip string. Default is null |
| `IsAccordion` | `bool` | Gets or sets Menu Accordion effect |
| `IsCollapsed` | `bool` | Gets or sets Sidebar status |
| `IsFixedFooter` | `bool` | Gets or sets Whether to fix Footer component |
| `IsFixedHeader` | `bool` | Gets or sets Whether to fix Header component |
| `IsFixedTabHeader` | `bool` | Gets or sets Whether to fix multi-tab Header. Default false |
| `IsFullSide` | `bool` | Gets or sets Whether the sidebar fills the entire left side. Default false |
| `IsOnlyRenderActiveTab` | `bool` | Gets or sets Whether to render only Active Tab |
| `IsPage` | `bool` | Gets or sets Whether it is a full page layout. Default false |
| `Max` | `int?` | Gets or sets Maximum Width. Default null |
| `MenuBarIcon` | `string?` | Gets or sets MenuBar Icon |

Only the first 40 important parameters are listed. Inspect the current source for the remaining 32 parameters before generating code.

## Events And Callbacks

`IsCollapsedChanged: EventCallback<bool>`, `OnAuthorizing: Func<string, Task<bool>>?`, `OnBeforeShowContextMenu: Func<TabItem, Task<bool>>?`, `OnClickMenu: Func<MenuItem, Task>?`, `OnCloseTabItemAsync: Func<TabItem, Task<bool>>?`, `OnCollapsed: Func<bool, Task>?`, `OnErrorHandleAsync: Func<ILogger, Exception, Task>?`, `OnTabHeaderTextLocalizer: Func<string?, string?>?`, `OnToolbarRefreshCallback: Func<Task>?`, `OnUpdateAsync: Func<string, Task>?`

## Templates And Child Content

`BeforeTabContextMenuTemplate: RenderFragment<Tab>?`, `CollapseBarTemplate: RenderFragment?`, `Footer: RenderFragment?`, `Header: RenderFragment?`, `Main: RenderFragment?`, `NotAuthorized: RenderFragment?`, `NotFound: RenderFragment?`, `Side: RenderFragment?`, `TabContextMenuTemplate: RenderFragment<Tab>?`, `ToolbarTemplate: RenderFragment<Tab>?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnInitializedAsync`, `OnParametersSet`, `Dispose`, `DisposeAsync`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Layouts.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Layouts.razor.cs`

Sample analysis:

- Direct `<Layout>` tag usages detected: 6
- Observed attributes in official Sample: `AdditionalAssemblies`, `IsFullSide`, `Menus`, `ShowFooter`, `SideWidth`
Sample-derived snippet:

```razor
<Layout ShowFooter="true" AdditionalAssemblies="[typeof(MainLayout).Assembly]">
            <Header>
                <div>Header</div>
            </Header>
            <Main>
                <div>Main</div>
            </Main>
            <Footer>
                <div>Footer</div>
            </Footer>
        </Layout>
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

- bool ShowSplitebar - ?ShowSplitBar eprecated. Please use 'ShowSplitBar' instead. The word 'Splitebar' is misspelled.

## Agent Rules

1. Read `src/BootstrapBlazor/Components/Layout` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.