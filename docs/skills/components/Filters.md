---
component: Filters
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Filters Skill

## Component Purpose

BoolFilter component is used for boolean value filtering in table column

Primary source directory: `src/BootstrapBlazor/Components/Filters`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Filters/BoolFilter.razor`
- `src/BootstrapBlazor/Components/Filters/BoolFilter.razor.cs`
- `src/BootstrapBlazor/Components/Filters/DateTimeFilter.razor`
- `src/BootstrapBlazor/Components/Filters/DateTimeFilter.razor.cs`
- `src/BootstrapBlazor/Components/Filters/EnumFilter.razor`
- `src/BootstrapBlazor/Components/Filters/EnumFilter.razor.cs`
- `src/BootstrapBlazor/Components/Filters/FilterBase.cs`
- `src/BootstrapBlazor/Components/Filters/FilterButton.razor`
- `src/BootstrapBlazor/Components/Filters/FilterButton.razor.cs`
- `src/BootstrapBlazor/Components/Filters/FilterButton.razor.js`
- `src/BootstrapBlazor/Components/Filters/FilterContext.cs`
- `src/BootstrapBlazor/Components/Filters/FilterKeyValueAction.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `LogicChanged` | `EventCallback<FilterLogic>` | Callback/event parameter; Gets or sets Logical Operator Change Callback Method |
| `OnClearFilter` | `Func<Task>?` | Callback/event parameter; Gets or sets Callback Method When Clearing Filter Conditions |
| `OnGetItemsAsync` | `Func<Task<List<SelectedItem>>>?` | Callback/event parameter; Gets or sets the callback to provide filter items, suitable for dynamic data sources |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the child content. Default is null |
| `LoadingTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets loading template |
| `AlwaysTriggerGetItems` | `bool` | Gets or sets whether to call each popup for dynamic filter conditions |
| `ClearButtonText` | `string?` | Gets or sets reset button text |
| `ClearIcon` | `string?` | Gets or sets Reset Button Icon |
| `Column` | `ITableColumn?` | Gets or sets the related instance |
| `Count` | `int` | Gets or sets condition count |
| `FieldKey` | `string?` | Gets or sets related field name |
| `FilterButtonText` | `string?` | Gets or sets filter button text |
| `FilterIcon` | `string?` | Gets or sets Filter Button Icon |
| `Icon` | `string?` | Gets or sets filter icon |
| `IsActive` | `bool` | Gets or sets whether active |
| `IsHeaderRow` | `bool` | Gets or sets whether header row mode is enabled. Default is false |
| `Items` | `IEnumerable<SelectedItem>?` | Gets or sets the filter candidate items. It is recommended to use static data to avoid performance loss |
| `Logic` | `FilterLogic` | Gets or sets Logical Operator |
| `MinusIcon` | `string?` | Gets or sets remove filter condition icon |
| `NotSupportedColumnFilterMessage` | `string?` | Gets or sets not supported filter type message. Default is null and reads resource content |
| `PlusIcon` | `string?` | Gets or sets add filter condition icon |
| `SearchPlaceHolderText` | `string?` | Gets or sets search placeholder. Default is null and uses resource value |
| `SelectAllText` | `string?` | Gets or sets select all button text. Default is null and uses resource value |
| `ShowMoreButton` | `bool` | Gets or sets whether to show the more button. Default is false |
| `ShowSearch` | `bool` | Default: `true`; Gets or sets whether to show search bar. Default is true |
| `StringComparison` | `StringComparison` | Default: `StringComparison.OrdinalIgnoreCase`; Gets or sets the string comparison option. Default is |
| `Table` | `ITable?` | Gets or sets the instance |
| `Title` | `string?` | Gets or sets the filter title. Default is null |
| `Type` | `Type?` | Gets or sets related enum type |

## Events And Callbacks

`LogicChanged: EventCallback<FilterLogic>`, `OnClearFilter: Func<Task>?`, `OnGetItemsAsync: Func<Task<List<SelectedItem>>>?`

## Templates And Child Content

`ChildContent: RenderFragment?`, `LoadingTemplate: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnParametersSet`, `OnParametersSetAsync`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

No official Sample was matched for `Filters`.

Use examples in this Skill only after validating the current source. If the directory contains infrastructure rather than a public Razor component, do not generate direct Razor usage.

## Examples

No direct Razor example is generated because this directory does not expose an authoritative `Filters.razor` usage pattern in the current repository.

## Common Mistakes

- Do not invent parameters, events, two-way bindings, or template context types.
- Do not copy examples from older BootstrapBlazor versions.
- Do not use obsolete members.
- If a Sample exists, analyze it before relying on Skill examples.
- If no Sample exists, validate every generated parameter against the current source.

## Obsolete Members

- string? NotSupportedMessage - ?NotSupportedColumnFilterMessage ; Deprecated, please use NotSupportedColumnFilterMessage parameter

## Agent Rules

1. Read `src/BootstrapBlazor/Components/Filters` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.