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

        var filter = cut.Instance.GetFilterConditions();
        Assert.NotNull(filter.Filters);
        Assert.Empty(filter.Filters);

        // Set Value
        var dt = cut.FindComponent<DateTimePicker<DateTime?>>();
        cut.InvokeAsync(() => dt.Instance.SetValue(DateTime.Now));
        filter = cut.Instance.GetFilterConditions();
        Assert.NotNull(filter.Filters);
        Assert.Single(filter.Filters);
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

        var filter = cut.Instance.GetFilterConditions();
        Assert.NotNull(filter.Filters);
        Assert.Empty(filter.Filters);

        var dt = cut.FindComponent<DateTimePicker<DateTime?>>().Instance;
        cut.InvokeAsync(() => dt.SetValue(DateTime.Now));

        filter = cut.Instance.GetFilterConditions();
        Assert.NotNull(filter.Filters);
        Assert.Single(filter.Filters);

        dt = cut.FindComponents<DateTimePicker<DateTime?>>()[1].Instance;
        cut.InvokeAsync(() => dt.SetValue(DateTime.Now));

        filter = cut.Instance.GetFilterConditions();
        Assert.NotNull(filter.Filters);
        Assert.Equal(2, filter.Filters.Count);
    }

    [Fact]
    public void SetFilterConditions_Ok()
    {
        var cut = Context.RenderComponent<DateTimeFilter>();
        var filter = cut.Instance;
        var conditions = filter.GetFilterConditions();
        Assert.NotNull(conditions.Filters);
        Assert.Empty(conditions.Filters);

        var newConditions = new FilterKeyValueAction() { Filters = new() };
        DateTime now = DateTime.Now;
        newConditions.Filters.Add(new FilterKeyValueAction() { FieldValue = now });
        newConditions.Filters.Add(new FilterKeyValueAction() { FieldValue = now });
        cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        conditions = filter.GetFilterConditions();

        Assert.NotNull(conditions.Filters);
        Assert.Equal(2, conditions.Filters.Count);

        newConditions.Filters.Clear();
        newConditions.Filters.Add(new FilterKeyValueAction() { FieldValue = null });
        newConditions.Filters.Add(new FilterKeyValueAction() { FieldValue = null });
        cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        conditions = filter.GetFilterConditions();
        Assert.NotNull(conditions.Filters);
        Assert.Empty(conditions.Filters);

        newConditions = new FilterKeyValueAction() { FieldValue = now };
        cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        conditions = filter.GetFilterConditions();
        Assert.NotNull(conditions.Filters);
        Assert.Single(conditions.Filters);
    }
}
