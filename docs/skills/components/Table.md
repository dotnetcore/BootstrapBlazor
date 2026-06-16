---
component: Table
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Table Skill

## Component Purpose

`Table<TItem>` is the primary data grid component. It supports local items, server-side query, pagination, sorting, filtering, editing, selection, toolbar actions, templates, responsive/card rendering, and virtual scrolling.

Primary source directory: `src/BootstrapBlazor/Components/Table`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Table/Table.razor`
- `src/BootstrapBlazor/Components/Table/Table.razor.cs`
- Table partial files for edit, query, search, sort, toolbar, tree, checkbox, localization, and column behavior.

## Usage Scenarios

Use `Table<TItem>` when data needs structured columns and BootstrapBlazor table behaviors.

Do not use it for simple layout grids, card-only lists, or non-tabular content.

## Parameters

Important parameter groups:

- Data: `Items`, `ItemsChanged`, `OnQueryAsync`
- Columns: `TableColumns`, `TableColumn`, `AutoGenerateColumns`
- Pagination/query: `IsPagination`, `PageItems`, query-related options
- Display: `IsStriped`, `IsBordered`, `TableSize`, `HeaderStyle`, `ScrollMode`
- Toolbar/search: `ShowToolbar`, `ShowSearch`, toolbar templates and button callbacks
- Editing/selection: `EditMode`, selected row parameters, row/cell callbacks
- Templates: `RowTemplate`, `RowContentTemplate`, `DetailRowTemplate`, `TableFooter`

The component is split across many partial files. Inspect the current source before using advanced parameters.

## Events And Callbacks

Common callback patterns include `OnQueryAsync`, row/cell callbacks, toolbar callbacks, edit callbacks, and selection change callbacks.

Do not guess callback signatures. Verify the exact delegate type in the current partial file before generating code.

## Templates And Child Content

`TableColumns` is a `RenderFragment<TItem>` and receives a context item for strongly typed field binding.

Use `TableColumn` inside `TableColumns`, commonly with `@bind-Field="@context.Property"`.

Row, detail, footer, and toolbar templates have specific context types. Verify their signatures before use.

## Cascading Parameters

`Table` uses cascading values internally for columns and responsive/table state. Consumer code should not depend on undocumented cascading values.

## Implementation Notes

- Set `TItem` explicitly unless type inference is obvious in the exact calling context.
- For local data, pass `Items`.
- For server-side data, use `OnQueryAsync` and return `QueryData<TItem>`.
- `ScrollMode.Virtual` uses Blazor virtualization behavior; when server data is involved, validate the current `OnQueryAsync` flow.
- `Table.razor` uses `BootstrapModuleAutoLoader(JSObjectReference = true)`, so built-in behavior may rely on the component module.
- Do not replace built-in table behavior with ad hoc JS interop.

## Sample Mapping

Official Sample:

- `src/BootstrapBlazor.Server/Components/Samples/Table`

Analyze the official Sample directory before relying on Skill examples. It contains many focused examples for columns, edit modes, search, filters, toolbars, virtual scroll, selection, and responsive behavior.

When the Sample and this Skill differ, prefer the current source, then the official Sample, then this Skill.

## Examples

Minimal local data:

```razor
<Table TItem="Foo" Items="@Items" IsBordered="true" IsStriped="true">
    <TableColumns>
        <TableColumn @bind-Field="@context.DateTime" Width="180" />
        <TableColumn @bind-Field="@context.Name" />
        <TableColumn @bind-Field="@context.Address" />
    </TableColumns>
</Table>
```

Server query shape:

```razor
<Table TItem="Foo" IsPagination="true" OnQueryAsync="OnQueryAsync">
    <TableColumns>
        <TableColumn @bind-Field="@context.Name" />
        <TableColumn @bind-Field="@context.Address" />
    </TableColumns>
</Table>
```

```csharp
private Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions options)
{
    return Task.FromResult(new QueryData<Foo>
    {
        Items = Items,
        TotalCount = Items.Count
    });
}
```

## Common Mistakes

Wrong:

```razor
<Table Items="@Items">
    <TableColumn Field="Name" />
</Table>
```

Correct:

```razor
<Table TItem="Foo" Items="@Items">
    <TableColumns>
        <TableColumn @bind-Field="@context.Name" />
    </TableColumns>
</Table>
```

Do not mix local `Items` and server `OnQueryAsync` unless the current Sample or source confirms that exact scenario.

## Obsolete Members

Do not use members marked `[Obsolete]` in the current source. For example, `ITableSearchModel.GetSearchs` is obsolete; use `GetSearches`.

## Agent Rules

1. Read `src/BootstrapBlazor/Components/Table` before generating code.
2. Read `src/BootstrapBlazor.Server/Components/Samples/Table` before using examples.
3. Prefer source over Sample, and Sample over this Skill when conflicts exist.
4. Generate only APIs verified in the current repository.
5. Do not use obsolete APIs or examples from older BootstrapBlazor versions.
