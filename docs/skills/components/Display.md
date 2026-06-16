---
component: Display
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Display Skill

## Component Purpose

Display Component

Primary source directory: `src/BootstrapBlazor/Components/Display`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Display/Display.razor`
- `src/BootstrapBlazor/Components/Display/Display.razor.cs`
- `src/BootstrapBlazor/Components/Display/DisplayBase.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `FormatterAsync` | `Func<TValue, Task<string?>>?` | Callback/event parameter; Gets or sets Async Format String |
| `TypeResolver` | `Func<Assembly?, string, bool, Type?>?` | Callback/event parameter; Gets or sets Type Resolver Callback Method. Called internally when component generic type is Array |
| `ValueChanged` | `EventCallback<TValue?>` | Callback/event parameter; Gets or sets a callback that updates the bound value |
| `ValueExpression` | `Expression<Func<TValue?>>?` | Callback/event parameter; Gets or sets an expression that identifies the bound value |
| `DisplayText` | `string?` | Gets or sets Display Text |
| `FormatString` | `string?` | Gets or sets Format String. e.g. yyyy-MM-dd for DateTime |
| `Lookup` | `IEnumerable<SelectedItem>?` | Verify current source before use |
| `LookupService` | `ILookupService?` | Verify current source before use |
| `LookupServiceData` | `object?` | Verify current source before use |
| `LookupServiceKey` | `string?` | Verify current source before use |
| `LookupStringComparison` | `StringComparison` | Default: `StringComparison.OrdinalIgnoreCase` |
| `ShowLabel` | `bool?` | Gets or sets Whether to Show Label. Default is null, not show label when null |
| `ShowLabelTooltip` | `bool?` | Gets or sets Whether to Show Tooltip. Default is null |
| `ShowTooltip` | `bool` | Gets or sets Whether to Show Tooltip. Default is false |
| `Value` | `TValue?` | Gets or sets the value of the input. This should be used with two-way binding |

## Events And Callbacks

`FormatterAsync: Func<TValue, Task<string?>>?`, `TypeResolver: Func<Assembly?, string, bool, Type?>?`, `ValueChanged: EventCallback<TValue?>`, `ValueExpression: Expression<Func<TValue?>>?`

## Templates And Child Content

No RenderFragment parameters were detected in the current source scan.

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnParametersSet`, `OnParametersSetAsync`
- JS interop or module dependency detected: `False`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Displays.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Displays.razor.cs`

Sample analysis:

- Direct `<Display>` tag usages detected: 19
- Observed attributes in official Sample: `@bind-Value`, `DisplayText`, `FormatString`, `FormatterAsync`, `Lookup`, `LookupServiceKey`, `ShowLabel`, `ShowTooltip`, `TValue`, `Value`
Sample-derived snippet:

```razor
<Display TValue="string" Value="@Model.Name" />
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

1. Read `src/BootstrapBlazor/Components/Display` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.