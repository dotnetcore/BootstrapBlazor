// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;

namespace UnitTest.Components;

public class TableDateTimeFilterTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Reset_Ok()
    {
        var cut = Context.RenderComponent<DateTimeFilter>();

        var filter = cut.Instance;
        cut.InvokeAsync(() => filter.Reset());
    }

    [Fact]
    public void GetFilterConditions_Ok()
    {
        var cut = Context.RenderComponent<DateTimeFilter>();

        var filter = cut.Instance;
        IEnumerable<FilterKeyValueAction>? condtions = null;
        cut.InvokeAsync(() => condtions = filter.GetFilterConditions());
        Assert.NotNull(condtions);
        Assert.Empty(condtions);

        // Set Value
        var dt = cut.FindComponent<DateTimePicker<DateTime?>>();
        cut.InvokeAsync(() => dt.Instance.SetValue(DateTime.Now));
        cut.InvokeAsync(() => condtions = filter.GetFilterConditions());
        Assert.Single(condtions);
    }

    [Fact]
    public void Count_Ok()
    {
        var cut = Context.RenderComponent<DateTimeFilter>(pb =>
        {
            pb.Add(a => a.Count, 2);
        });

        var logic = cut.FindComponent<FilterLogicItem>();
        Assert.NotNull(logic);

        var conditions = cut.Instance.GetFilterConditions();
        Assert.Empty(conditions);

        var dt = cut.FindComponent<DateTimePicker<DateTime?>>().Instance;
        cut.InvokeAsync(() => dt.SetValue(DateTime.Now));

        conditions = cut.Instance.GetFilterConditions();
        Assert.Single(conditions);

        dt = cut.FindComponents<DateTimePicker<DateTime?>>()[1].Instance;
        cut.InvokeAsync(() => dt.SetValue(DateTime.Now));

        conditions = cut.Instance.GetFilterConditions();
        Assert.Equal(2, conditions.Count());
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
                    builder.OpenComponent<TableColumn<Foo, DateTime?>>(index++);
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, DateTime?>.Field), foo.DateTime);
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, DateTime?>.FieldExpression), foo.GenerateValueExpression(nameof(Foo.DateTime), typeof(DateTime?)));
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, DateTime?>.Filterable), true);
                    builder.CloseComponent();
                }));
            });
        });
        var filter = cut.FindComponent<DateTimeFilter>();
        var dt = filter.FindComponent<DateTimePicker<DateTime?>>();
        IEnumerable<FilterKeyValueAction>? condtions = null;

        // Click ToDay Cell
        cut.InvokeAsync(() =>
        {
            dt.Find(".current.today .cell").Click();
            dt.FindAll(".is-confirm")[1].Click();
        });

        cut.InvokeAsync(() =>
        {
            // OnFilterValueChanged
            var filterButton = cut.FindComponent<FilterButton<FilterAction>>();
            var logics = filterButton.FindAll(".dropdown-item");
            Assert.Equal(6, logics.Count);
            logics[1].Click();
            condtions = filter.Instance.GetFilterConditions();
            Assert.NotNull(condtions);
            Assert.Single(condtions);
            Assert.Equal(FilterAction.LessThanOrEqual, condtions?.First().FilterAction);
        });

        // OnClearFilter
        cut.InvokeAsync(() =>
        {
            dt.Find(".is-confirm").Click();
            condtions = filter.Instance.GetFilterConditions();
            Assert.NotNull(condtions);
            Assert.Empty(condtions);
        });
    }

    [Fact]
    public async Task SetFilterConditions_Ok()
    {
        var cut = Context.RenderComponent<DateTimeFilter>();
        var filter = cut.Instance;
        var conditions = filter.GetFilterConditions();
        Assert.Empty(conditions);

        var newConditions = new List<FilterKeyValueAction>();
        DateTime dati = DateTime.Now;
        newConditions.Add(new FilterKeyValueAction() { FieldValue = dati });
        newConditions.Add(new FilterKeyValueAction() { FieldValue = dati });
        await filter.SetFilterConditionsAsync(newConditions);
        conditions = filter.GetFilterConditions();

        Assert.NotNull(conditions);
        Assert.Equal(2, conditions.Count());

        newConditions.Clear();
        newConditions.Add(new FilterKeyValueAction() { FieldValue = null });
        newConditions.Add(new FilterKeyValueAction() { FieldValue = null });
        await filter.SetFilterConditionsAsync(newConditions);
        conditions = filter.GetFilterConditions();
        Assert.Empty(conditions);
    }
}
