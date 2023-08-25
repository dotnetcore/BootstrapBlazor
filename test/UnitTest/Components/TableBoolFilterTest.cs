// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;

namespace UnitTest.Components;

public class TableBoolFilterTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Reset_Ok()
    {
        var cut = Context.RenderComponent<BoolFilter>();

        var filter = cut.Instance;
        cut.InvokeAsync(() => filter.Reset());
    }

    [Fact]
    public void GetFilterConditions_Ok()
    {
        var cut = Context.RenderComponent<BoolFilter>();

        var filter = cut.Instance.GetFilterConditions();
        Assert.NotNull(filter.Filters);
        Assert.Empty(filter.Filters);

        // Set Value
        var items = cut.FindAll(".dropdown-item");
        cut.InvokeAsync(() => items[1].Click());
        filter = cut.Instance.GetFilterConditions();
        Assert.NotNull(filter.Filters);
        Assert.Single(filter.Filters);
    }

    [Fact]
    public void IsHeaderRow_OnSelectedItemChanged()
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
                    builder.OpenComponent<TableColumn<Foo, bool>>(index++);
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, bool>.Field), foo.Complete);
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, bool>.FieldExpression), foo.GenerateValueExpression(nameof(Foo.Complete), typeof(bool)));
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, bool>.Filterable), true);
                    builder.CloseComponent();
                }));
            });
        });
        var items = cut.FindAll(".dropdown-item");
        cut.InvokeAsync(() => items[1].Click());
        var filter = cut.FindComponent<BoolFilter>().Instance.GetFilterConditions();
        Assert.NotNull(filter.Filters);
        Assert.Single(filter.Filters);
    }

    [Fact]
    public void SetFilterConditions_Ok()
    {
        var cut = Context.RenderComponent<BoolFilter>();

        var filter = cut.Instance.GetFilterConditions();
        Assert.NotNull(filter.Filters);
        Assert.Empty(filter.Filters);

        var newConditions = new FilterKeyValueAction
        {
            Filters = new() { new FilterKeyValueAction() { FieldValue = true } }
        };
        cut.InvokeAsync(() => cut.Instance.SetFilterConditionsAsync(newConditions));
        filter = cut.Instance.GetFilterConditions();
        Assert.NotNull(filter.Filters);
        Assert.Single(filter.Filters);
        Assert.True((bool?)filter.Filters.First().FieldValue);

        newConditions.Filters[0].FieldValue = false;
        cut.InvokeAsync(() => cut.Instance.SetFilterConditionsAsync(newConditions));
        filter = cut.Instance.GetFilterConditions();
        Assert.NotNull(filter.Filters);
        Assert.False((bool?)filter.Filters.First().FieldValue);

        newConditions.Filters[0].FieldValue = null;
        cut.InvokeAsync(() => cut.Instance.SetFilterConditionsAsync(newConditions));
        filter = cut.Instance.GetFilterConditions();
        Assert.NotNull(filter.Filters);
        Assert.Empty(filter.Filters);

        newConditions = new FilterKeyValueAction() { FieldValue = true };
        cut.InvokeAsync(() => cut.Instance.SetFilterConditionsAsync(newConditions));
        filter = cut.Instance.GetFilterConditions();
        Assert.NotNull(filter.Filters);
        Assert.Single(filter.Filters);
        Assert.True((bool?)filter.Filters.First().FieldValue);
    }
}
