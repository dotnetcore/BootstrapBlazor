// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class TableLookupFilterTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task OnFilterAsync_Ok()
    {
        var cut = Context.RenderComponent<TableColumnFilter>(pb =>
        {
            pb.Add(a => a.Table, new MockTable());
            pb.Add(a => a.Column, new MockColumn());
            pb.Add(a => a.IsHeaderRow, true);
        });

        var items = cut.FindAll(".dropdown-item");
        await cut.InvokeAsync(() => { items[1].Click(); });
    }

    [Fact]
    public async Task FilterAction_Ok()
    {
        var cut = Context.RenderComponent<TableColumnFilter>(pb =>
        {
            pb.Add(a => a.Table, new MockTable());
            pb.Add(a => a.Column, new MockColumn());
        });
        var lookup = cut.FindComponent<LookupFilter>();
        var filter = lookup.Instance;

        var newConditions = new FilterKeyValueAction()
        {
            Filters =
            [
                new FilterKeyValueAction() { FieldValue = "1" },
            ]
        };
        await cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        var conditions = filter.GetFilterConditions();
        Assert.Single(conditions.Filters);
        await cut.InvokeAsync(() => filter.Reset());
        conditions = filter.GetFilterConditions();
        Assert.Empty(conditions.Filters);

        // Improve test coverage
        newConditions = new FilterKeyValueAction()
        {
            Filters =
            [
                new FilterKeyValueAction() { FieldValue = false },
            ]
        };
        await cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        conditions = filter.GetFilterConditions();
        Assert.Empty(conditions.Filters);

        newConditions = new FilterKeyValueAction() { FieldValue = null };
        await cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        conditions = filter.GetFilterConditions();
        Assert.Empty(conditions.Filters);
    }

    class MockTable : ITable
    {
        public Dictionary<string, IFilterAction> Filters { get; set; } = [];

        public Func<Task>? OnFilterAsync { get; set; }

        public List<ITableColumn> Columns => [];

        public IEnumerable<ITableColumn> GetVisibleColumns() => Columns;
    }

    class MockColumn : TableColumn<Foo, string>
    {
        public MockColumn()
        {
            PropertyType = typeof(string);
            FieldName = "Lookup";
            Lookup = new List<SelectedItem>()
            {
                new("1", "Test-1"),
                new("2", "Test-2"),
                new("3", "Test-3")
            };
        }
    }
}
