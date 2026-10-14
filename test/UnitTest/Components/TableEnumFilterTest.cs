// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class TableEnumFilterTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Type_Ok()
    {
        Assert.ThrowsAny<InvalidOperationException>(() => Context.Render<EnumFilter>());

        var cut = Context.Render<TableColumnFilter>(pb =>
        {
            pb.Add(a => a.Column, new MockColumn());
        });
        var filter = cut.FindComponent<EnumFilter>();
        Assert.Equal(typeof(EnumEducation), filter.Instance.Type);
    }

    [Fact]
    public async Task OnFilterAsync_Ok()
    {
        var cut = Context.Render<TableColumnFilter>(pb =>
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
        var cut = Context.Render<EnumFilter>(pb =>
        {
            pb.Add(a => a.Type, typeof(EnumEducation));
        });
        var filter = cut.Instance;

        var newConditions = new FilterKeyValueAction()
        {
            Filters =
            [
                new FilterKeyValueAction() { FieldValue = EnumEducation.Primary },
                new FilterKeyValueAction() { FieldValue = EnumEducation.Middle }
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

        foreach (var action in new[] { FilterAction.IsNull, FilterAction.IsNotNull })
        {
            newConditions = new FilterKeyValueAction()
            {
                Filters =
                [
                    new FilterKeyValueAction() { FilterAction = action }
                ]
            };
            await cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
            conditions = filter.GetFilterConditions();
            Assert.Single(conditions.Filters);
            Assert.Equal(action, conditions.Filters[0].FilterAction);
            Assert.Null(conditions.Filters[0].FieldValue);
        }

        newConditions = new FilterKeyValueAction()
        {
            Filters =
            [
                new FilterKeyValueAction() { FilterAction = FilterAction.IsNull },
                new FilterKeyValueAction() { FilterAction = FilterAction.IsNotNull }
            ]
        };
        await cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        conditions = filter.GetFilterConditions();
        Assert.Equal(2, conditions.Filters.Count);
        Assert.Equal(FilterAction.IsNull, conditions.Filters[0].FilterAction);
        Assert.Equal(FilterAction.IsNotNull, conditions.Filters[1].FilterAction);

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

        public List<ITableColumn> GetVisibleColumns() => Columns;
    }

    class MockColumn : TableColumn<Foo, EnumEducation>
    {
        public MockColumn()
        {
            PropertyType = typeof(EnumEducation);
            FieldName = "Education";
        }
    }
}
