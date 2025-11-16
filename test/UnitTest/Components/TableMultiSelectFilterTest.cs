// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class TableMultiSelectFilterTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task IsHeaderRow_Ok()
    {
        var cut = Context.Render<TableColumnFilter>(pb =>
        {
            pb.Add(a => a.Table, new MockTable());
            pb.Add(a => a.Column, new MockColumn());
            pb.Add(a => a.IsHeaderRow, true);
        });
        cut.Contains("filter-row");

        var actions = cut.FindAll(".dropdown-item");
        await cut.InvokeAsync(() => actions[1].Click());

        // check filter
        var filter = cut.Instance;
        var conditions = filter.FilterAction.GetFilterConditions();
        Assert.Single(conditions.Filters);
    }

    [Fact]
    public async Task OnFilterAsync_Ok()
    {
        var cut = Context.Render<TableColumnFilter>(pb =>
        {
            pb.Add(a => a.Table, new MockTable());
            pb.Add(a => a.Column, new MockColumn());
            pb.Add(a => a.IsHeaderRow, false);
        });
        var item = cut.Find(".dropdown-menu .dropdown-item");
        await cut.InvokeAsync(() => item.Click());

        var filter = cut.FindComponent<MultiSelectFilter<string>>();
        var conditions = filter.Instance.GetFilterConditions();

        Assert.Single(conditions.Filters);

        await cut.InvokeAsync(() => filter.Instance.Reset());
        conditions = filter.Instance.GetFilterConditions();
        Assert.Empty(conditions.Filters);

        await cut.InvokeAsync(() => filter.Instance.SetFilterConditionsAsync(new FilterKeyValueAction()
        {
            FieldValue = "v1,v2",
        }));
        conditions = filter.Instance.GetFilterConditions();
        Assert.Equal(2, conditions.Filters.Count);

        await cut.InvokeAsync(() => filter.Instance.SetFilterConditionsAsync(new FilterKeyValueAction()
        {
            FieldValue = true,
        }));
        conditions = filter.Instance.GetFilterConditions();
        Assert.Empty(conditions.Filters);
    }

    [Fact]
    public async Task OnFilterAsync_List()
    {
        var cut = Context.Render<TableColumnFilter>(pb =>
        {
            pb.Add(a => a.Table, new MockTable());
            pb.Add(a => a.Column, new MockColumnList());
            pb.Add(a => a.IsHeaderRow, false);
        });
        var item = cut.Find(".dropdown-menu .dropdown-item");
        await cut.InvokeAsync(() => item.Click());

        var filter = cut.FindComponent<MultiSelectFilter<List<string>>>();
        var conditions = filter.Instance.GetFilterConditions();

        Assert.Single(conditions.Filters);

        await cut.InvokeAsync(() => filter.Instance.Reset());
        conditions = filter.Instance.GetFilterConditions();
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
            FieldName = "MultiSelect";

            FilterTemplate = new RenderFragment(builder =>
            {
                builder.OpenComponent<FilterProvider>(0);
                builder.AddAttribute(1, nameof(FilterProvider.ChildContent), new RenderFragment(builder =>
                {
                    builder.OpenComponent<MultiSelectFilter<string>>(2);
                    builder.AddAttribute(3, nameof(MultiSelectFilter<string>.Items), new List<SelectedItem>
                    {
                        new("v1", "Test-1"),
                        new("v2", "Test-2")
                    });
                    builder.CloseComponent();
                }));
                builder.CloseComponent();
            });
        }
    }

    class MockColumnList : TableColumn<Foo, List<string>>
    {
        public MockColumnList()
        {
            PropertyType = typeof(List<string>);
            FieldName = "MultiSelect";

            FilterTemplate = new RenderFragment(builder =>
            {
                builder.OpenComponent<FilterProvider>(0);
                builder.AddAttribute(1, nameof(FilterProvider.ChildContent), new RenderFragment(builder =>
                {
                    builder.OpenComponent<MultiSelectFilter<List<string>>>(2);
                    builder.AddAttribute(3, nameof(MultiSelectFilter<List<string>>.Items), new List<SelectedItem>
                    {
                        new("v1", "Test-1"),
                        new("v2", "Test-2")
                    });
                    builder.CloseComponent();
                }));
                builder.CloseComponent();
            });
        }
    }

}
