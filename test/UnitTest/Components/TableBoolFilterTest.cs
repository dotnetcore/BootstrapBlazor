// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using UnitTest.Extensions;

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
        var filter = cut.FindComponent<BoolFilter>().Instance;
        var items = cut.FindAll(".dropdown-item");
        IEnumerable<FilterKeyValueAction>? condtions = null;
        cut.InvokeAsync(() => items[1].Click());
        cut.InvokeAsync(() => condtions = filter.GetFilterConditions());
        Assert.Single(condtions);
    }

    [Fact]
    public void SetFilterConditions_Ok()
    {
        var cut = Context.RenderComponent<BoolFilter>();

        var filter = cut.Instance;
        IEnumerable<FilterKeyValueAction>? conditions = null;
        cut.InvokeAsync(() => conditions = filter.GetFilterConditions());
        Assert.Empty(conditions);

        List<FilterKeyValueAction>? newConditions = new(1);

        newConditions.Add(new FilterKeyValueAction() { FieldValue = true });

        cut.InvokeAsync(() => filter.SetFilterConditions(newConditions));

        cut.InvokeAsync(() => conditions = filter.GetFilterConditions());
        Assert.Single(conditions);
        Assert.True(conditions?.First().FieldValue is bool);
        Assert.True((bool?)conditions?.First().FieldValue);
    }
}
