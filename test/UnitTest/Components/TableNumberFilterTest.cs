// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;

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
    public void Count_Ok()
    {
        var cut = Context.RenderComponent<NumberFilter<int?>>(pb =>
        {
            pb.Add(a => a.Count, 2);
        });

        var logic = cut.FindComponent<FilterLogicItem>();
        Assert.NotNull(logic);

        var conditions = cut.Instance.GetFilterConditions();
        Assert.NotNull(conditions.Filters);
        Assert.Empty(conditions.Filters);

        var dt = cut.FindComponent<BootstrapInputNumber<int?>>().Instance;
        cut.InvokeAsync(() => dt.SetValue(10));

        conditions = cut.Instance.GetFilterConditions();
        Assert.NotNull(conditions.Filters);
        Assert.Single(conditions.Filters);

        dt = cut.FindComponents<BootstrapInputNumber<int?>>()[1].Instance;
        cut.InvokeAsync(() => dt.SetValue(10));

        conditions = cut.Instance.GetFilterConditions();
        Assert.NotNull(conditions.Filters);
        Assert.Equal(2, conditions.Filters.Count);
    }

    [Fact]
    public void NotNumber_Ok()
    {
        var cut = Context.RenderComponent<NumberFilter<string>>(pb =>
        {
            pb.Add(a => a.Count, 2);
        });
    }

    [Fact]
    public void Misc_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.Items, new List<Foo>() { new Foo() });
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
        var filter = cut.FindComponent<NumberFilter<int>>();
        var input = filter.FindComponent<BootstrapInputNumber<int>>();

        // Click ToDay Cell
        cut.InvokeAsync(() =>
        {
            input.Instance.SetValue(10);
        });

        var filterButton = cut.FindComponent<FilterButton<FilterAction>>();
        cut.InvokeAsync(() =>
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
        cut.InvokeAsync(() =>
        {
            filterButton.Find(".fa-ban").Click();
        });
        conditions = filter.Instance.GetFilterConditions();
        Assert.NotNull(conditions.Filters);
        Assert.Single(conditions.Filters);
        Assert.Equal(0, conditions.Filters[0].FieldValue);
        Assert.Equal(FilterAction.GreaterThanOrEqual, conditions.Filters[0].FilterAction);
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
            Filters = new()
            {
                new FilterKeyValueAction() { FieldValue = 1 },
                new FilterKeyValueAction() { FieldValue = 2 }
            }
        };
        cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        conditions = filter.GetFilterConditions();
        Assert.NotNull(conditions.Filters);
        Assert.Equal(2, conditions.Filters.Count);

        newConditions = new FilterKeyValueAction()
        {
            Filters = new()
            {
                new FilterKeyValueAction() { FieldValue = null },
                new FilterKeyValueAction() { FieldValue = null }
            }
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
