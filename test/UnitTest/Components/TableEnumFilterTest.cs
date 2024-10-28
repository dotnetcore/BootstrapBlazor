﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class TableEnumFilterTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Reset_Ok()
    {
        var cut = Context.RenderComponent<EnumFilter>(pb =>
        {
            pb.Add(a => a.Type, typeof(EnumEducation));
        });

        var filter = cut.Instance;
        cut.InvokeAsync(() => filter.Reset());
    }

    [Fact]
    public void GetFilterConditions_Ok()
    {
        var cut = Context.RenderComponent<EnumFilter>(pb =>
        {
            pb.Add(a => a.Type, typeof(EnumEducation));
        });

        var filter = cut.Instance;
        var conditions = filter.GetFilterConditions();
        Assert.NotNull(conditions.Filters);
        Assert.Empty(conditions.Filters);

        // Set Value
        var items = cut.FindAll(".dropdown-item");
        cut.InvokeAsync(() => items[1].Click());
        conditions = filter.GetFilterConditions();
        Assert.NotNull(conditions.Filters);
        Assert.Single(conditions.Filters);
    }

    [Fact]
    public async Task Count_Ok()
    {
        var cut = Context.RenderComponent<EnumFilter>(pb =>
        {
            pb.Add(a => a.Count, 2);
            pb.Add(a => a.Type, typeof(EnumEducation));
        });

        var logic = cut.FindComponent<FilterLogicItem>();
        Assert.NotNull(logic);

        var filter = cut.Instance.GetFilterConditions();
        Assert.NotNull(filter.Filters);
        Assert.Empty(filter.Filters);

        var com = cut.FindComponent<Select<string?>>().Instance;
        await cut.InvokeAsync(() => com.SetValue("Middle"));

        filter = cut.Instance.GetFilterConditions();
        Assert.NotNull(filter.Filters);
        Assert.Single(filter.Filters);

        com = cut.FindComponents<Select<string?>>()[1].Instance;
        await cut.InvokeAsync(() => com.SetValue("Primary"));

        filter = cut.Instance.GetFilterConditions();
        Assert.NotNull(filter.Filters);
        Assert.Equal(2, filter.Filters.Count);
    }

    [Fact]
    public void InvalidOperationException_Exception()
    {
        Assert.ThrowsAny<InvalidOperationException>(() => Context.RenderComponent<EnumFilter>());
    }

    [Fact]
    public void SetFilterConditions_Ok()
    {
        var cut = Context.RenderComponent<EnumFilter>(pb =>
        {
            pb.Add(a => a.Type, typeof(EnumEducation));
        });
        var filter = cut.Instance;
        var conditions = filter.GetFilterConditions();
        Assert.NotNull(conditions.Filters);
        Assert.Empty(conditions.Filters);

        var newConditions = new FilterKeyValueAction()
        {
            Filters = [new FilterKeyValueAction() { FieldValue = EnumEducation.Middle }]
        };
        cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));

        conditions = filter.GetFilterConditions();
        Assert.NotNull(conditions.Filters);
        Assert.Equal(EnumEducation.Middle, conditions.Filters.First().FieldValue);

        newConditions = new FilterKeyValueAction()
        {
            Filters = [new FilterKeyValueAction() { FieldValue = null }]
        };
        cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        conditions = filter.GetFilterConditions();
        Assert.NotNull(conditions.Filters);
        Assert.Empty(conditions.Filters);

        newConditions = new FilterKeyValueAction() { FieldValue = EnumEducation.Middle };
        cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        conditions = filter.GetFilterConditions();
        Assert.NotNull(conditions.Filters);
        Assert.Single(conditions.Filters);

        newConditions = new FilterKeyValueAction() { Filters = [], FilterLogic = FilterLogic.Or };
        newConditions.Filters.Add(new FilterKeyValueAction() { FieldValue = EnumEducation.Primary });
        newConditions.Filters.Add(new FilterKeyValueAction() { FieldValue = EnumEducation.Middle });
        cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        cut.Render();

        // 检查 UI
        var comps = cut.FindComponents<Select<string?>>();
        Assert.Equal("Primary", comps[0].Instance.Value);
        Assert.Equal("Middle", comps[1].Instance.Value);

        newConditions = new FilterKeyValueAction() { Filters = [], FilterLogic = FilterLogic.Or };
        newConditions.Filters.Add(new FilterKeyValueAction() { FieldValue = EnumEducation.Primary });
        newConditions.Filters.Add(new FilterKeyValueAction() { FieldValue = null });
        cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        cut.Render();
        comps = cut.FindComponents<Select<string?>>();
        Assert.Equal("Primary", comps[0].Instance.Value);
        Assert.Equal("", comps[1].Instance.Value);
    }

    [Fact]
    public void TableFilter_On()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.Items, new List<Foo>() { new() });
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.TableColumns, new RenderFragment<Foo>(foo => builder =>
                {
                    var index = 0;
                    builder.OpenComponent<TableColumn<Foo, int>>(index++);
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, int>.Field), foo.Count);
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, int>.FieldExpression), foo.GenerateValueExpression(nameof(Foo.Count), typeof(int)));
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, int>.Filterable), true);
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, int>.FilterTemplate), new RenderFragment(builder =>
                    {
                        builder.OpenComponent<MockFilter>(0);
                        builder.CloseComponent();
                    }));
                    builder.CloseComponent();
                }));
            });
        });

        // 测试 filter.Filters?.Count > 1 TableFilter 表达式
        var filter = cut.FindComponent<MockFilter>();
        cut.InvokeAsync(() => filter.Instance.SetFilterConditionsAsync(new FilterKeyValueAction()
        {
            FieldValue = 1
        }));
        filter.Instance.Reset();

        var tableFilter = cut.FindComponent<TableFilter>();
        tableFilter.Render();
        Assert.Equal(0, filter.Instance.Count);
    }

    class MockFilter : FilterBase
    {
        private List<FilterKeyValueAction>? _filters = [new FilterKeyValueAction() { FieldValue = 1 }, new FilterKeyValueAction() { FieldValue = 2 }];

        public override FilterKeyValueAction GetFilterConditions()
        {
            return new FilterKeyValueAction() { Filters = _filters };
        }

        public override void Reset()
        {
            _filters = null;
        }
    }
}
