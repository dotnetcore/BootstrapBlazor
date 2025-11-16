// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class TableNotSupportFilterTest : BootstrapBlazorTestBase
{
    [Fact]
    public void OnFilterAsync_Ok()
    {
        var cut = Context.Render<TableColumnFilter>(pb =>
        {
            pb.Add(a => a.NotSupportedColumnFilterMessage, "不支持的类型");
            pb.Add(a => a.Table, new MockTable());
            pb.Add(a => a.Column, new MockColumn());
        });

        cut.Contains("不支持的类型");

        var filter = cut.FindComponent<NotSupportFilter>();
        var conditions = filter.Instance.GetFilterConditions();
        Assert.Empty(conditions.Filters);

        filter.Instance.Reset();
    }

    class MockTable : ITable
    {
        public Dictionary<string, IFilterAction> Filters { get; set; } = [];

        public Func<Task>? OnFilterAsync { get; set; }

        public List<ITableColumn> Columns => [];

        public IEnumerable<ITableColumn> GetVisibleColumns() => Columns;
    }

    class MockColumn : TableColumn<Foo, List<string>>
    {
        public MockColumn()
        {
            PropertyType = typeof(List<string>);
            FieldName = "Double";
        }
    }
}
