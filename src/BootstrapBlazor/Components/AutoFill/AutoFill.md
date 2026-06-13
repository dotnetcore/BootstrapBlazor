---
component: AutoFill
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# AutoFill Skill

## Component Purpose

AutoFill component

Primary source directory: `src/BootstrapBlazor/Components/AutoFill`.

Source files reviewed:

- `src/BootstrapBlazor/Components/AutoFill/AutoFill.razor`
- `src/BootstrapBlazor/Components/AutoFill/AutoFill.razor.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnClearAsync` | `Func<Task>?` | Callback/event parameter; Gets or sets the callback method when the clear button is clicked. Default is null |
| `OnCustomFilter` | `Func<string, Task<IEnumerable<TValue>>>?` | Callback/event parameter; Gets or sets the custom collection filtering rules |
| `OnGetDisplayText` | `Func<TValue?, string?>?` | Callback/event parameter; Gets or sets the method to get the display text from the model. Default is to use the ToString override method |
| `OnQueryAsync` | `Func<VirtualizeQueryOption, Task<QueryData<TValue>>>?` | Callback/event parameter; Gets or sets the callback method for loading virtualized items |
| `DisplayCount` | `int?` | Gets or sets the number of items to display when matching data. Default is null |
| `Icon` | `string?` | Gets or sets the icon |
| `IgnoreCase` | `bool` | Default: `true`; Gets or sets whether to ignore case when matching. Default is true |
| `IsAutoClearWhenInvalid` | `bool` | Gets or sets whether to clear the content automatically when the input is invalid. Default is false |
| `IsLikeMatch` | `bool` | Gets or sets whether to enable fuzzy search. Default is false |
| `IsVirtualize` | `bool` | Gets or sets whether virtual scrolling is enabled. Default is false |
| `Items` | `IEnumerable<TValue>?` | Gets or sets the collection of items for the component |
| `LoadingIcon` | `string?` | Gets or sets the loading icon |
| `OverscanCount` | `int` | Default: `3`; Gets or sets the overscan count for virtual scrolling. Default is 3 |
| `RowHeight` | `float` | Default: `50f`; Gets or sets the row height for virtual scrolling. Default is 50f |
| `ShowDropdownListOnFocus` | `bool` | Default: `true`; Gets or sets whether to expand the dropdown candidate menu when focused. Default is true |
| `ShowNoDataTip` | `bool` | Default: `true`; Gets or sets whether to show the no matching data option. Default is true |

## Events And Callbacks

`OnClearAsync: Func<Task>?`, `OnCustomFilter: Func<string, Task<IEnumerable<TValue>>>?`, `OnGetDisplayText: Func<TValue?, string?>?`, `OnQueryAsync: Func<VirtualizeQueryOption, Task<QueryData<TValue>>>?`

## Templates And Child Content

No RenderFragment parameters were detected in the current source scan.

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnParametersSet`, `OnAfterRenderAsync`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/AutoFills.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/AutoFills.razor.cs`

Sample analysis:

- Direct `<AutoFill>` tag usages detected: 5
- Observed attributes in official Sample: `@bind-Value`, `IsAutoClearWhenInvalid`, `IsClearable`, `IsLikeMatch`, `IsSelectAllTextOnFocus`, `IsVirtualize`, `Items`, `OnCustomFilter`, `OnGetDisplayText`, `OnQueryAsync`, `RowHeight`, `ShowDropdownListOnFocus`
Sample-derived snippet:

```razor
<AutoFill @bind-Value="Model1" Items="Items1" IsAutoClearWhenInvalid="true"
              IsLikeMatch="true" OnGetDisplayText="OnGetDisplayText" IsSelectAllTextOnFocus="true">
        <ItemTemplate>
            <div class="d-flex">
                <div>
                    <img src="@WebsiteOption.Value.GetAvatarUrl(context.Id)" class="bb-avatar" />
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

1. Read `src/BootstrapBlazor/Components/AutoFill` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.