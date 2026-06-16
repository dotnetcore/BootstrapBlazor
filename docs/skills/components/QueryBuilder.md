---
component: QueryBuilder
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# QueryBuilder Skill

## Component Purpose

QueryBuilder Component

Primary source directory: `src/BootstrapBlazor/Components/QueryBuilder`.

Source files reviewed:

- `src/BootstrapBlazor/Components/QueryBuilder/GroupFilterKeyValueAction.cs`
- `src/BootstrapBlazor/Components/QueryBuilder/QueryBuilder.razor`
- `src/BootstrapBlazor/Components/QueryBuilder/QueryBuilder.razor.cs`
- `src/BootstrapBlazor/Components/QueryBuilder/QueryColumn.cs`
- `src/BootstrapBlazor/Components/QueryBuilder/QueryGroup.razor`
- `src/BootstrapBlazor/Components/QueryBuilder/QueryGroup.razor.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `Value` | `FilterKeyValueAction?` | Required; Callback/event parameter; Gets or sets the filter model value |
| `FieldExpression` | `Expression<Func<TType>>?` | Callback/event parameter; Gets or sets the FieldExpression |
| `HeaderTemplate` | `RenderFragment<FilterKeyValueAction>?` | Callback/event parameter; Template parameter; verify context type; Gets or sets the header template. Default is null |
| `Operator` | `FilterAction` | Callback/event parameter; Gets or sets the condition operator |
| `ValueChanged` | `EventCallback<FilterKeyValueAction>` | Callback/event parameter; Gets or sets the filter callback method. Supports two-way binding |
| `ChildContent` | `RenderFragment<TModel>?` | Template parameter; verify context type; Gets or sets the template |
| `Field` | `TType?` | Gets or sets the condition field name |
| `GroupText` | `string?` | Gets or sets the group filter condition text |
| `ItemText` | `string?` | Gets or sets the filter condition text |
| `Logic` | `FilterLogic` | Gets or sets the logic operator |
| `MinusIcon` | `string?` | Gets or sets the reduce filter condition icon |
| `PlusIcon` | `string?` | Gets or sets the add filter condition icon |
| `RemoveIcon` | `string?` | Gets or sets the remove filter condition icon |
| `ShowHeader` | `bool` | Default: `true`; Gets or sets whether to show Header area. Default is true |

## Events And Callbacks

`FieldExpression: Expression<Func<TType>>?`, `HeaderTemplate: RenderFragment<FilterKeyValueAction>?`, `Operator: FilterAction`, `Value: FilterKeyValueAction?`, `ValueChanged: EventCallback<FilterKeyValueAction>`

## Templates And Child Content

`ChildContent: RenderFragment<TModel>?`, `HeaderTemplate: RenderFragment<FilterKeyValueAction>?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnParametersSet`, `OnAfterRender`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/QueryBuilders.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/QueryBuilders.razor.cs`

Sample analysis:

- Direct `<QueryBuilder>` tag usages detected: 3
- Observed attributes in official Sample: `@bind-Value`, `class`, `ShowHeader`, `TModel`
Sample-derived snippet:

```razor
<QueryBuilder TModel="Foo" @bind-Value="_filter1">
        <QueryColumn @bind-Field="@context.Name" Operator="@FilterAction.Equal"></QueryColumn>

        <QueryGroup Logic="@FilterLogic.Or">
            <QueryColumn @bind-Field="@context.Address" Operator="@FilterAction.Contains"></QueryColumn>
            <QueryColumn @bind-Field="@context.Education" Operator="@FilterAction.Equal"></QueryColumn>
        </QueryGroup>

        <QueryColumn @bind-Field="@context.Complete" Operator="@FilterAction.Equal"></QueryColumn>
        <QueryColumn @bind-Field="@context.DateTime" Operator="@FilterAction.GreaterThanOrEqual"></QueryColumn>
    </QueryBuilder>
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

1. Read `src/BootstrapBlazor/Components/QueryBuilder` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.