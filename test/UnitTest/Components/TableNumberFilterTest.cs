// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class TableNumberFilterTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Reset_Ok()
    {
        var cut = Context.RenderComponent<NumberFilter<int>>();

        var filter = cut.Instance;
        cut.InvokeAsync(() => filter.Reset());
    }

    [Fact]
    public void GetFilterConditions_Ok()
    {
        var cut = Context.RenderComponent<NumberFilter<int?>>();

        var filter = cut.Instance;
        var conditions = filter.GetFilterConditions();
        Assert.NotNull(conditions.Filters);
        Assert.Empty(conditions.Filters);

        // Set Value
        var dt = cut.FindComponent<BootstrapInputNumber<int?>>();
        cut.InvokeAsync(() => dt.Instance.SetValue(10));
        conditions = filter.GetFilterConditions();
        Assert.NotNull(conditions.Filters);
        Assert.Single(conditions.Filters);
    }

    [Fact]
    public async Task Misc_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.Items, new List<Foo>() { new() });
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.ShowFilterHeader, true);
                pb.Add(a => a.TableColumns, new RenderFragment<Foo>(foo => builder =>
                {
                    var index = 0;
                    builder.OpenComponent<TableColumn<Foo, int>>(index++);
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, int>.Field), foo.Count);
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, int>.FieldExpression), foo.GenerateValueExpression(nameof(Foo.Count), typeof(int)));
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, int>.Filterable), true);
                    builder.CloseComponent();
                }));
            });
        });
        var filter = cut.FindComponent<NumberFilter<int?>>();
        var input = filter.FindComponent<BootstrapInputNumber<int?>>();

        // Click ToDay Cell
        await cut.InvokeAsync(() =>
        {
            input.Instance.SetValue(10);
        });

        var filterButton = cut.FindComponent<FilterButton<FilterAction>>();
        await cut.InvokeAsync(() =>
        {
            // OnFilterValueChanged
            var logics = filterButton.FindAll(".dropdown-item");
            Assert.Equal(6, logics.Count);

            logics[1].Click();
        });
        var conditions = filter.Instance.GetFilterConditions();
        Assert.NotNull(conditions.Filters);
        Assert.Single(conditions.Filters);
        Assert.Equal(10, conditions.Filters[0].FieldValue);
        Assert.Equal(FilterAction.LessThanOrEqual, conditions.Filters[0].FilterAction);

        // OnClearFilter
        await cut.InvokeAsync(() =>
        {
            filterButton.Find(".fa-ban").Click();
        });
        conditions = filter.Instance.GetFilterConditions();
        Assert.NotNull(conditions.Filters);
        Assert.Empty(conditions.Filters);
    }

    [Fact]
    public void NotNumberType_OnFilterValueChanged()
    {
        var cut = Context.RenderComponent<TableFilter>(pb =>
        {
            var foo = new Foo();
            var column = new TableColumn<Foo, string>();
            column.SetParametersAsync(ParameterView.FromDictionary(new Dictionary<string, object?>()
            {
                [nameof(TableColumn<Foo, string>.Field)] = foo.Name,
                [nameof(TableColumn<Foo, string>.FieldExpression)] = foo.GenerateValueExpression(),
                [nameof(TableColumn<Foo, string>.FilterTemplate)] = new RenderFragment(builder =>
                {
                    builder.OpenComponent<NumberFilter<string>>(0);
                    builder.CloseComponent();
                })
            }));
            pb.Add(a => a.IsHeaderRow, true);
            pb.Add(a => a.Column, column);
        });

        // InHeaderRow 非数字类型过滤器测试
        var input = cut.FindComponent<BootstrapInput<string>>().Instance;
        cut.InvokeAsync(() => input.SetValue("10"));
    }

    [Fact]
    public void SetFilterConditions_Ok()
    {
        var cut = Context.RenderComponent<NumberFilter<int?>>();
        var filter = cut.Instance;
        var conditions = filter.GetFilterConditions();
        Assert.NotNull(conditions.Filters);
        Assert.Empty(conditions.Filters);

        var newConditions = new FilterKeyValueAction()
        {
            Filters =
            [
                new FilterKeyValueAction() { FieldValue = 1 },
                new FilterKeyValueAction() { FieldValue = 2 }
            ]
        };
        cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        conditions = filter.GetFilterConditions();
        Assert.NotNull(conditions.Filters);
        Assert.Equal(2, conditions.Filters.Count);

        newConditions = new FilterKeyValueAction()
        {
            Filters =
            [
                new FilterKeyValueAction() { FieldValue = null },
                new FilterKeyValueAction() { FieldValue = null }
            ]
        };
        cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        conditions = filter.GetFilterConditions();
        Assert.NotNull(conditions.Filters);
        Assert.Empty(conditions.Filters);

        newConditions = new FilterKeyValueAction() { FieldValue = 1 };
        cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        conditions = filter.GetFilterConditions();
        Assert.NotNull(conditions.Filters);
        Assert.Single(conditions.Filters);
    }
}
