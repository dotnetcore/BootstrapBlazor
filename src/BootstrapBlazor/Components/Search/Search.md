---
component: Search
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Search Skill

## Component Purpose

Search component

Primary source directory: `src/BootstrapBlazor/Components/Search`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Search/Search.razor`
- `src/BootstrapBlazor/Components/Search/Search.razor.cs`
- `src/BootstrapBlazor/Components/Search/SearchContext.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnClear` | `Func<Task>?` | Callback/event parameter; Obsolete; do not use; Gets or sets the event callback when the clear button is clicked. Default is null |
| `OnGetDisplayText` | `Func<TValue, string?>?` | Callback/event parameter; Gets or sets the callback method to get display text. Default is null, using ToString() method |
| `OnSearch` | `Func<string?, Task<IEnumerable<TValue>>>?` | Callback/event parameter; Gets or sets the callback delegate when the search button is clicked |
| `ButtonTemplate` | `RenderFragment<SearchContext<TValue>>?` | Template parameter; verify context type; Gets or sets the button template. Default is null |
| `IconTemplate` | `RenderFragment<SearchContext<TValue>>?` | Template parameter; verify context type; Gets or sets the icon template. Default is null if not set |
| `PrefixButtonTemplate` | `RenderFragment<SearchContext<TValue>>?` | Template parameter; verify context type; Gets or sets the prefix button template. Default is null |
| `PrefixIconTemplate` | `RenderFragment<SearchContext<TValue>>?` | Template parameter; verify context type; Gets or sets the prefix icon template. Default is null |
| `ClearButtonColor` | `Color` | Default: `Color.Primary`; Gets or sets the color of clear button. Default is |
| `ClearButtonIcon` | `string?` | Gets or sets the icon of clear button. Default is null |
| `ClearButtonText` | `string?` | Gets or sets the text of clear button. Default is null |
| `IsAutoClearAfterSearch` | `bool` | Obsolete; do not use; Gets or sets whether to automatically clear the search box after searching. Deprecated |
| `IsTriggerSearchByInput` | `bool` | Default: `true`; Gets or sets whether the search is triggered by input. Default is true. If false, the search button must be clicked t... |
| `PrefixIcon` | `string?` | Gets or sets the prefix icon. Default is null |
| `SearchButtonColor` | `Color` | Default: `Color.Primary`; Gets or sets the search button color. Default is |
| `SearchButtonIcon` | `string?` | Gets or sets the search button icon. Default is null |
| `SearchButtonLoadingIcon` | `string?` | Gets or sets the loading icon for the search button. Default is null |
| `SearchButtonText` | `string?` | Gets or sets the search button text. Default is null |
| `ShowClearButton` | `bool` | Gets or sets whether to show the clear button. Default is false |
| `ShowPrefixIcon` | `bool` | Gets or sets whether to show the prefix icon. Default is false |
| `ShowSearchButton` | `bool` | Default: `true`; Gets or sets whether to show the search button. Default is true |

## Events And Callbacks

`OnClear: Func<Task>?`, `OnGetDisplayText: Func<TValue, string?>?`, `OnSearch: Func<string?, Task<IEnumerable<TValue>>>?`

## Templates And Child Content

`ButtonTemplate: RenderFragment<SearchContext<TValue>>?`, `IconTemplate: RenderFragment<SearchContext<TValue>>?`, `PrefixButtonTemplate: RenderFragment<SearchContext<TValue>>?`, `PrefixIconTemplate: RenderFragment<SearchContext<TValue>>?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnParametersSet`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Searches.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Searches.razor.cs`

Sample analysis:

- Direct `<Search>` tag usages detected: 10
- Observed attributes in official Sample: `@bind-Value`, `Debounce`, `IsAutoFocus`, `IsClearable`, `IsSelectAllTextOnFocus`, `IsTriggerSearchByInput`, `OnGetDisplayText`, `OnSearch`, `PlaceHolder`, `PrefixIcon`, `ShowClearButton`, `ShowPrefixIcon`, `ShowSearchButton`
Sample-derived snippet:

```razor
<Search IsAutoFocus="true"
            PlaceHolder="@Localizer["SearchesPlaceHolder"]"
            OnSearch="@OnSearch"
            IsSelectAllTextOnFocus="true"></Search>
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

1. Read `src/BootstrapBlazor/Components/Search` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.