---
component: RibbonTab
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# RibbonTab Skill

## Component Purpose

RibbonTab Component

Primary source directory: `src/BootstrapBlazor/Components/RibbonTab`.

Source files reviewed:

- `src/BootstrapBlazor/Components/RibbonTab/RibbonTab.razor`
- `src/BootstrapBlazor/Components/RibbonTab/RibbonTab.razor.cs`
- `src/BootstrapBlazor/Components/RibbonTab/RibbonTab.razor.js`
- `src/BootstrapBlazor/Components/RibbonTab/RibbonTabHeader.razor`
- `src/BootstrapBlazor/Components/RibbonTab/RibbonTabHeader.razor.cs`
- `src/BootstrapBlazor/Components/RibbonTab/RibbonTabItem.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `Items` | `IEnumerable<RibbonTabItem>?` | Required; Gets or sets the items |
| `DecodeAnchorCallback` | `Func<string, string?>?` | Callback/event parameter; Gets or sets the decode anchor callback method |
| `EncodeAnchorCallback` | `Func<string, string?, string?>?` | Callback/event parameter; Gets or sets the encode anchor callback method. First parameter is current URL, second parameter is current item Text... |
| `OnFloatChanged` | `Func<bool, Task>?` | Callback/event parameter; Gets or sets the callback method when float state changes. Default is null |
| `OnItemClickAsync` | `Func<RibbonTabItem, Task>?` | Callback/event parameter; Gets or sets the click command button callback method |
| `OnMenuClickAsync` | `Func<RibbonTabItem, Task>?` | Callback/event parameter; Gets or sets the click tab menu callback method |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the child content |
| `RightButtonsTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the right buttons template |
| `IsBorder` | `bool` | Default: `true`; Gets or sets whether to have border. Default is true |
| `IsSupportAnchor` | `bool` | Gets or sets whether to enable URL anchor |
| `RibbonArrowDownIcon` | `string?` | Gets or sets the tab arrow down icon |
| `RibbonArrowPinIcon` | `string?` | Gets or sets the tab pin icon |
| `RibbonArrowUpIcon` | `string?` | Gets or sets the tab arrow up icon |
| `ShowFloatButton` | `bool` | Gets or sets whether to show float button. Default is false |

## Events And Callbacks

`DecodeAnchorCallback: Func<string, string?>?`, `EncodeAnchorCallback: Func<string, string?, string?>?`, `OnFloatChanged: Func<bool, Task>?`, `OnItemClickAsync: Func<RibbonTabItem, Task>?`, `OnMenuClickAsync: Func<RibbonTabItem, Task>?`

## Templates And Child Content

`ChildContent: RenderFragment?`, `RightButtonsTemplate: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnParametersSet`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/RibbonTabs.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/RibbonTabs.razor.cs`

Sample analysis:

- Direct `<RibbonTab>` tag usages detected: 5
- Observed attributes in official Sample: `DecodeAnchorCallback`, `EncodeAnchorCallback`, `IsSupportAnchor`, `Items`, `OnFloatChanged`, `OnMenuClickAsync`, `ShowFloatButton`
Sample-derived snippet:

```razor
<RibbonTab Items="@NormalItems" />
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

1. Read `src/BootstrapBlazor/Components/RibbonTab` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.