// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;

namespace UnitTest.Components;

public class TableLookupFilterTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Reset_Ok()
    {
        var cut = Context.RenderComponent<LookupFilter>(pb =>
        {
            pb.Add(a => a.Type, typeof(string));
            pb.Add(a => a.Lookup, new List<SelectedItem>()
            {
                new SelectedItem("true", "True"),
                new SelectedItem("false", "False")
            });
        });

        var filter = cut.Instance;
        cut.InvokeAsync(() => filter.Reset());
    }

    [Fact]
    public void GetFilterConditions_Ok()
    {
        var cut = Context.RenderComponent<LookupFilter>(pb =>
        {
            pb.Add(a => a.Type, typeof(string));
            pb.Add(a => a.Lookup, new List<SelectedItem>()
            {
                new SelectedItem("true", "True"),
                new SelectedItem("false", "False")
            });
        });

        var filter = cut.Instance;
        IEnumerable<FilterKeyValueAction>? condtions = null;
        cut.InvokeAsync(() => condtions = filter.GetFilterConditions());
        Assert.NotNull(condtions);
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
        Assert.ThrowsAny<InvalidOperationException>(() => Context.RenderComponent<LookupFilter>());
        Assert.ThrowsAny<InvalidOperationException>(() => Context.RenderComponent<LookupFilter>(pb =>
        {
            pb.Add(a => a.Lookup, new List<SelectedItem>());
        }));
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
                    builder.OpenComponent<TableColumn<Foo, bool>>(index++);
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, bool>.Field), foo.Complete);
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, bool>.FieldExpression), foo.GenerateValueExpression(nameof(foo.Complete), typeof(bool)));
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, bool>.Filterable), true);
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, bool>.LookupStringComparison), StringComparison.OrdinalIgnoreCase);
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, bool>.Lookup), new List<SelectedItem>()
                    {
                        new SelectedItem("true", "True"),
                        new SelectedItem("false", "False")
                    });
                    builder.CloseComponent();
                }));
            });
        });

        var items = cut.FindAll(".dropdown-item");
        IEnumerable<FilterKeyValueAction>? condtions = null;
        cut.InvokeAsync(() => items[1].Click());
        cut.InvokeAsync(() => condtions = cut.FindComponent<LookupFilter>().Instance.GetFilterConditions());
        Assert.NotNull(condtions);
        Assert.Single(condtions);
    }

    [Fact]
    public async Task SetFilterConditions_Ok()
    {
        var cut = Context.RenderComponent<LookupFilter>(pb =>
        {
            pb.Add(a => a.Type, typeof(bool));
            pb.Add(a => a.Lookup, new List<SelectedItem>()
            {
                new SelectedItem("true", "True"),
                new SelectedItem("false", "False")
            });
        });

        var filter = cut.Instance;
        var conditions = filter.GetFilterConditions();
        Assert.Empty(conditions);

        var newConditions = new List<FilterKeyValueAction>
        {
            new FilterKeyValueAction() { FieldValue = true }
        };
        await filter.SetFilterConditionsAsync(newConditions);
        conditions = filter.GetFilterConditions();
        Assert.Single(conditions);

        newConditions = new List<FilterKeyValueAction>
        {
            new FilterKeyValueAction() { FieldValue = null }
        };
        await filter.SetFilterConditionsAsync(newConditions);
        conditions = filter.GetFilterConditions();
        Assert.Empty(conditions);
    }
}
