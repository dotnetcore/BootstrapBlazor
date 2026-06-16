---
component: AutoComplete
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# AutoComplete Skill

## Component Purpose

AutoComplete component

Primary source directory: `src/BootstrapBlazor/Components/AutoComplete`.

Source files reviewed:

- `src/BootstrapBlazor/Components/AutoComplete/AutoComplete.razor`
- `src/BootstrapBlazor/Components/AutoComplete/AutoComplete.razor.cs`
- `src/BootstrapBlazor/Components/AutoComplete/AutoComplete.razor.js`
- `src/BootstrapBlazor/Components/AutoComplete/PopoverCompleteBase.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnCustomFilter` | `Func<string, Task<IEnumerable<string>>>?` | Callback/event parameter; Gets or sets custom collection filtering rules, default is null |
| `OnSelectedItemChanged` | `Func<TValue, Task>?` | Callback/event parameter; Gets or sets the callback method for dropdown item selection. Default is null |
| `ItemTemplate` | `RenderFragment<TValue>?` | Template parameter; verify context type; Gets or sets the item template. Default is null |
| `ClearIcon` | `string?` | Gets or sets the right-side clear icon. Default is fa-solid fa-angle-up |
| `CustomClass` | `string?` | Verify current source before use |
| `Debounce` | `int` | Gets or sets the debounce time. Default is 0 (disabled) |
| `DisplayCount` | `int?` | Gets or sets the number of items to display when matching data |
| `Icon` | `string?` | Gets or sets the icon |
| `IgnoreCase` | `bool` | Default: `true`; Gets or sets whether to ignore case when matching, default is true |
| `IsClearable` | `bool` | Gets or sets whether the select component is clearable. Default is false |
| `IsLikeMatch` | `bool` | Gets or sets whether to enable fuzzy search, default is false |
| `IsPopover` | `bool` | Verify current source before use |
| `Items` | `IEnumerable<string>?` | Gets or sets the collection of matching data obtained by inputting a string |
| `LoadingIcon` | `string?` | Gets or sets the loading icon |
| `NoDataTip` | `string?` | Gets or sets the tip info when no matching data. Default is "No matched data" |
| `Offset` | `string?` | Verify current source before use |
| `Placement` | `Placement` | Default: `Placement.Bottom` |
| `ScrollIntoViewBehavior` | `ScrollIntoViewBehavior` | Default: `ScrollIntoViewBehavior.Smooth`; Gets or sets the scroll behavior. Default is |
| `ShowDropdownListOnFocus` | `bool` | Default: `true`; Gets or sets whether to expand the dropdown candidate menu when focused, default is true |
| `ShowNoDataTip` | `bool` | Default: `true`; Gets or sets whether to show the no matching data option, default is true |
| `ShowShadow` | `bool` | Default: `true` |
| `SkipEnter` | `bool` | Gets or sets whether to skip Enter key processing. Default is false |
| `SkipEsc` | `bool` | Gets or sets whether to skip Esc key processing. Default is false |
| `SkipMatch` | `bool` | Gets or sets whether to skip matching operations, default is false |

## Events And Callbacks

`OnCustomFilter: Func<string, Task<IEnumerable<string>>>?`, `OnSelectedItemChanged: Func<TValue, Task>?`

## Templates And Child Content

`ItemTemplate: RenderFragment<TValue>?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnParametersSet`, `OnAfterRenderAsync`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/AutoCompletes.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/AutoCompletes.razor.cs`

Sample analysis:

- Direct `<AutoComplete>` tag usages detected: 14
- Observed attributes in official Sample: `@bind-Value`, `Debounce`, `DisplayText`, `IgnoreCase`, `IsClearable`, `IsLikeMatch`, `IsPopover`, `IsSelectAllTextOnFocus`, `Items`, `NoDataTip`, `OnCustomFilter`, `OnSelectedItemChanged`, `ShowLabel`, `Value`
Sample-derived snippet:

```razor
<AutoComplete Value="@_value" Items="@StaticItems" IsSelectAllTextOnFocus="true" IsClearable></AutoComplete>
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

1. Read `src/BootstrapBlazor/Components/AutoComplete` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.