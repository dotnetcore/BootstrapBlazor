// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;

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
        IEnumerable<FilterKeyValueAction>? condtions = null;
        cut.InvokeAsync(() => condtions = filter.GetFilterConditions());
        Assert.Empty(condtions);

        // Set Value
        var items = cut.FindAll(".dropdown-item");
        cut.InvokeAsync(() => items[1].Click());
        cut.InvokeAsync(() => condtions = filter.GetFilterConditions());
        Assert.Single(condtions);
    }

    [Fact]
    public void InvalidOperationException_Exception()
    {
        Assert.ThrowsAny<InvalidOperationException>(() => Context.RenderComponent<EnumFilter>());
    }

    [Fact]
    public void IsHeaderRow_OnFilterValueChanged()
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
                    builder.OpenComponent<TableColumn<Foo, EnumEducation?>>(index++);
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, EnumEducation?>.Field), foo.Education);
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, EnumEducation?>.FieldExpression), foo.GenerateValueExpression(nameof(foo.Education), typeof(EnumEducation?)));
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, EnumEducation?>.Filterable), true);
                    builder.CloseComponent();
                }));
            });
        });

        var items = cut.FindAll(".dropdown-item");
        IEnumerable<FilterKeyValueAction>? condtions = null;
        cut.InvokeAsync(() => items[1].Click());
        cut.InvokeAsync(() => condtions = cut.FindComponent<EnumFilter>().Instance.GetFilterConditions());
        Assert.Single(condtions);
    }

    [Fact]
    public async Task SetFilterConditions_Ok()
    {
        var cut = Context.RenderComponent<EnumFilter>(pb =>
        {
            pb.Add(a => a.Type, typeof(EnumEducation));
        });
        var filter = cut.Instance;
        var conditions = filter.GetFilterConditions();
        Assert.Empty(conditions);

        var newConditions = new List<FilterKeyValueAction>
        {
            new FilterKeyValueAction() { FieldValue = EnumEducation.Middle }
        };
        await filter.SetFilterConditionsAsync(newConditions);

        conditions = filter.GetFilterConditions();
        Assert.Equal(EnumEducation.Middle, conditions.First().FieldValue);

        newConditions = new List<FilterKeyValueAction>
        {
            new FilterKeyValueAction() { FieldValue = null }
        };
        await filter.SetFilterConditionsAsync(newConditions);
        conditions = filter.GetFilterConditions();
        Assert.Empty(conditions);
    }
}
