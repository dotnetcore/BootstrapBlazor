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
        cut.InvokeAsync(() => items[1].Click());
        var conditions = cut.FindComponent<LookupFilter>().Instance.GetFilterConditions();
        Assert.NotNull(conditions.Filters);
        Assert.Single(conditions.Filters);
    }

    [Fact]
    public void SetFilterConditions_Ok()
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
        Assert.NotNull(conditions.Filters);
        Assert.Empty(conditions.Filters);

        var newConditions = new FilterKeyValueAction()
        {
            Filters = new() { new FilterKeyValueAction() { FieldValue = true } }
        };
        cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        conditions = filter.GetFilterConditions();
        Assert.NotNull(conditions.Filters);
        Assert.Single(conditions.Filters);

        newConditions = new FilterKeyValueAction()
        {
            Filters = new() { new FilterKeyValueAction() { FieldValue = null } }
        };
        cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        conditions = filter.GetFilterConditions();
        Assert.NotNull(conditions.Filters);
        Assert.Empty(conditions.Filters);

        newConditions = new FilterKeyValueAction() { FieldValue = true };
        cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        conditions = filter.GetFilterConditions();
        Assert.NotNull(conditions.Filters);
        Assert.Single(conditions.Filters);
    }
}
