// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class TableMultiFilterTest : BootstrapBlazorTestBase
{
    [Fact]
    public void IsHeaderRow_Ok()
    {
        var cut = Context.Render<MultiFilter>(pb =>
        {
            pb.Add(a => a.IsHeaderRow, true);
        });
        Assert.Equal("", cut.Markup);
    }

    [Fact]
    public void InvalidOperationException_Ok()
    {
        Assert.Throws<InvalidOperationException>(() =>
        {
            Context.Render<MultiFilter>(pb =>
            {
                pb.Add(a => a.Items, new List<SelectedItem>()
                {
                    new("1", "Test-1"),
                    new("2", "Test-2"),
                    new("3", "Test-3")
                });
                pb.Add(a => a.OnGetItemsAsync, new Func<Task<List<SelectedItem>>>(() => Task.FromResult(new List<SelectedItem>())));
            });
        });
    }

    [Fact]
    public async Task ShowSearch_Ok()
    {
        var cut = Context.Render<MultiFilter>(pb =>
        {
            pb.Add(a => a.Items, new List<SelectedItem>()
            {
                new("1", "Test-1"),
                new("2", "Test-2"),
                new("3", "Test-3")
            });
            pb.Add(a => a.StringComparison, StringComparison.OrdinalIgnoreCase);
            pb.Add(a => a.ShowSearch, true);
        });
        cut.Contains("bb-multi-filter-search");
        var input = cut.Find(".bb-multi-filter-search");
        await cut.InvokeAsync(() => input.Input("1"));
        await cut.InvokeAsync(() => input.Input(""));

        cut.Render(pb =>
        {
            pb.Add(a => a.ShowSearch, false);
        });
        cut.DoesNotContain("bb-multi-filter-search");
    }

    [Fact]
    public async Task OnGetItemsAsync_Ok()
    {
        var cut = Context.Render<MultiFilter>(pb =>
        {
            pb.Add(a => a.OnGetItemsAsync, new Func<Task<List<SelectedItem>>>(() => Task.FromResult(new List<SelectedItem>()
            {
                new("1", "Test-1"),
                new("2", "Test-2"),
                new("3", "Test-3")
            })));
            pb.Add(a => a.StringComparison, StringComparison.OrdinalIgnoreCase);
            pb.Add(a => a.AlwaysTriggerGetItems, false);
        });

        await cut.InvokeAsync(() => cut.Instance.TriggerGetItemsCallback());

        // 选中第一个
        var checkboxs = cut.FindComponents<Checkbox<bool>>();
        await cut.InvokeAsync(() => checkboxs[1].Instance.SetState(CheckboxState.Checked));
        await cut.InvokeAsync(() => cut.Instance.TriggerGetItemsCallback());
    }

    [Fact]
    public async Task SelectAll_Ok()
    {
        var items = new List<SelectedItem>()
        {
            new("1", "Test-1"),
            new("2", "Test-2"),
            new("3", "Test-3")
        };
        var cut = Context.Render<MultiFilter>(pb =>
        {
            pb.Add(a => a.Items, items);
        });
        var checkbox = cut.FindComponent<Checkbox<bool>>();

        // 全选
        await cut.InvokeAsync(() => checkbox.Instance.SetState(CheckboxState.Checked));
        Assert.All(items, i => Assert.True(i.Active));

        // 取消全选
        await cut.InvokeAsync(() => checkbox.Instance.SetState(CheckboxState.UnChecked));
        Assert.All(items, i => Assert.False(i.Active));
    }

    [Fact]
    public void Items_Ok()
    {
        var items = new List<SelectedItem>()
        {
            new("1", "Test-1") { Active = true },
            new("2", "Test-2")
        };
        var cut = Context.Render<MultiFilter>(pb =>
        {
            pb.Add(a => a.Items, items);
        });

        // 更新数据源保持选项选中状态
        items =
        [
            new("1", "Test-1") { Active = false },
            new("2", "Test-2") { Active = true }
        ];
        cut.Render(pb =>
        {
            pb.Add(a => a.Items, items);
        });
        var conditions = cut.Instance.GetFilterConditions();
        Assert.Equal(2, conditions.Filters.Count);
    }

    [Fact]
    public void LoadingTemplate_Ok()
    {
        var cut = Context.Render<MultiFilter>(pb =>
        {
            pb.Add(a => a.LoadingTemplate, new RenderFragment(builder =>
            {
                builder.AddContent(0, "Loading");
            }));
        });
        cut.Contains("Loading");
    }

    [Fact]
    public async Task FilterAction_Ok()
    {
        var cut = Context.Render<MultiFilter>(pb =>
        {
            pb.Add(a => a.Items, new List<SelectedItem>()
            {
                new("1", "Test-1"),
                new("2", "Test-2"),
                new("3", "Test-3")
            });
        });
        var filter = cut.Instance;

        var newConditions = new FilterKeyValueAction()
        {
            Filters =
            [
                new FilterKeyValueAction() { FieldValue = "1" },
                new FilterKeyValueAction() { FieldValue = "2" }
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
            ]
        };
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
}
