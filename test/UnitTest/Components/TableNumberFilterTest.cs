// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class TableNumberFilterTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task OnFilterAsync_Ok()
    {
        var cut = Context.Render<TableColumnFilter>(pb =>
        {
            pb.Add(a => a.Table, new MockTable());
            pb.Add(a => a.Column, new MockColumn());
            pb.Add(a => a.IsHeaderRow, true);
        });

        var input = cut.Find("[type=\"number\"]");
        await cut.InvokeAsync(() => input.Change("1"));

        var action = cut.Find(".dropdown-item");
        await cut.InvokeAsync(() => action.Click());

        // check filter
        var filter = cut.Instance;
        var conditions = filter.FilterAction.GetFilterConditions();
        Assert.Single(conditions.Filters);

        // trigger onclear
        var clear = cut.Find(".btn-ban");
        await cut.InvokeAsync(() => clear.Click());

        // check filter
        conditions = filter.FilterAction.GetFilterConditions();
        Assert.Empty(conditions.Filters);
    }

    [Fact]
    public async Task FilterAction_Ok()
    {
        var cut = Context.Render<NumberFilter<double?>>(pb =>
        {
            pb.Add(a => a.IsHeaderRow, true);
        });

        cut.Render(pb =>
        {
            pb.Add(a => a.IsHeaderRow, false);
        });
        var filter = cut.Instance;

        var newConditions = new FilterKeyValueAction()
        {
            Filters =
            [
                new FilterKeyValueAction() { FieldValue = 0.1d },
                new FilterKeyValueAction() { FieldValue = 0.2d }
            ]
        };
        await cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        var conditions = filter.GetFilterConditions();
        Assert.Equal(2, conditions.Filters.Count);

        await cut.InvokeAsync(() => filter.Reset());
        conditions = filter.GetFilterConditions();
        Assert.Empty(conditions.Filters);

        // Improve test coverage
        newConditions = new FilterKeyValueAction()
        {
            Filters =
            [
                new FilterKeyValueAction() { FieldValue = null },
                new FilterKeyValueAction() { FieldValue = null }
            ]
        };
        await cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        conditions = filter.GetFilterConditions();
        Assert.Empty(conditions.Filters);

        newConditions = new FilterKeyValueAction()
        {
            Filters =
            [
                new FilterKeyValueAction() { FieldValue = true },
                new FilterKeyValueAction() { FieldValue = false }
            ]
        };
        await cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        conditions = filter.GetFilterConditions();
        Assert.Empty(conditions.Filters);

        newConditions = new FilterKeyValueAction() { FieldValue = "1" };
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

    class MockColumn : TableColumn<Foo, double?>
    {
        public MockColumn()
        {
            PropertyType = typeof(double?);
            FieldName = "Double";
        }
    }
}
