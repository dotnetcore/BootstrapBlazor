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
        var cut = Context.Render<TableColumnFilter>(pb =>
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
                new FilterKeyValueAction() { FieldValue = "2" },
            ]
        };
        await cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        var conditions = filter.GetFilterConditions();
        Assert.Single(conditions.Filters);
        Assert.Equal("2", conditions.Filters[0].FieldValue);

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

    [Fact]
    public async Task LookupService_Ok()
    {
        var cut = Context.Render<TableColumnFilter>(pb =>
        {
            pb.Add(a => a.Table, new MockTable());
            pb.Add(a => a.Column, new MockLookupServiceColumn());
        });
        var lookup = cut.FindComponent<LookupFilter>();
        var filter = lookup.Instance;

        // 由于 LookupFilter 默认值未设置使用候选项第一个
        cut.WaitForAssertion(() => cut.Contains("value=\"LookupService-Test-1-async\""), TimeSpan.FromMilliseconds(100));
        var newConditions = new FilterKeyValueAction()
        {
            Filters =
            [
                new FilterKeyValueAction() { FieldValue = "v2" },
            ]
        };
        await cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        var conditions = filter.GetFilterConditions();
        Assert.Single(conditions.Filters);
        Assert.Equal("v2", conditions.Filters[0].FieldValue);

        await cut.InvokeAsync(() => filter.Reset());
        conditions = filter.GetFilterConditions();
        Assert.Empty(conditions.Filters);
    }

    [Fact]
    public async Task LookupService_Empty()
    {
        var column = new MockEmptyLookupServiceColumn();
        var cut = Context.Render<TableColumnFilter>(pb =>
        {
            pb.Add(a => a.Table, new MockTable());
            pb.Add(a => a.Column, column);
        });
        var lookup = cut.FindComponent<LookupFilter>();
        var filter = lookup.Instance;

        await column.Task;
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

    class MockLookupServiceColumn : TableColumn<Foo, string>
    {
        public MockLookupServiceColumn()
        {
            PropertyType = typeof(string);
            FieldName = "Lookup";
            LookupService = new LookupFilterService();
            LookupServiceKey = "LookupKey";
        }
    }

    class MockEmptyLookupServiceColumn : TableColumn<Foo, string>
    {
        private LookupFilterService _service = new LookupFilterService();

        public MockEmptyLookupServiceColumn()
        {
            PropertyType = typeof(string);
            FieldName = "Lookup";
            LookupService = _service;
            LookupServiceKey = "LookupEmptyKey";
        }

        public Task Task => _service.Task;
    }

    class LookupFilterService : LookupServiceBase
    {
        private TaskCompletionSource _taskCompletionSource = new();

        public override IEnumerable<SelectedItem>? GetItemsByKey(string? key, object? data) => null;

        public override async Task<IEnumerable<SelectedItem>?> GetItemsByKeyAsync(string? key, object? data)
        {
            IEnumerable<SelectedItem>? ret = null;

            if (key == "LookupKey")
            {
                await Task.Delay(30);
                ret =
                [
                    new SelectedItem("v1", "LookupService-Test-1-async"),
                    new SelectedItem("v2", "LookupService-Test-2-async")
                ];
            }
            else if (key == "LookupEmptyKey")
            {
                ret = [];
                _taskCompletionSource.TrySetResult();
            }
            return ret;
        }

        public Task Task => _taskCompletionSource.Task;
    }
}
