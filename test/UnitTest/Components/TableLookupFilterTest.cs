// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class TableLookupFilterTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Lookup_Ok()
    {
        var cut = Context.RenderComponent<LookupFilter>();
        var items = cut.FindAll(".dropdown-item");
        Assert.Equal(3, items.Count);
        Assert.Contains("LookupService-Test-2", items[items.Count - 1].InnerHtml);

        cut.WaitForAssertion(() =>
        {
            items = cut.FindAll(".dropdown-item");
            Assert.Equal(3, items.Count);
            Assert.Contains("LookupService-Test-2-async", items[items.Count - 1].InnerHtml);
        });
    }

    [Fact]
    public void Reset_Ok()
    {
        var cut = Context.RenderComponent<LookupFilter>();

        var filter = cut.Instance;
        cut.InvokeAsync(() => filter.Reset());
    }

    [Fact]
    public void GetFilterConditions_Ok()
    {
        var cut = Context.RenderComponent<LookupFilter>();

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
    public void IsHeaderRow_OnFilterValueChanged()
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
                    builder.OpenComponent<TableColumn<Foo, bool>>(index++);
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, bool>.Field), foo.Complete);
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, bool>.FieldExpression), foo.GenerateValueExpression(nameof(foo.Complete), typeof(bool)));
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, bool>.Filterable), true);
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, bool>.LookupStringComparison), StringComparison.OrdinalIgnoreCase);
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, bool>.Lookup), new List<SelectedItem>()
                    {
                        new("true", "True"),
                        new("false", "False")
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
        var cut = Context.RenderComponent<LookupFilter>();

        var filter = cut.Instance;
        var conditions = filter.GetFilterConditions();
        Assert.NotNull(conditions.Filters);
        Assert.Empty(conditions.Filters);

        var newConditions = new FilterKeyValueAction()
        {
            Filters = [new FilterKeyValueAction() { FieldValue = true }]
        };
        cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        conditions = filter.GetFilterConditions();
        Assert.NotNull(conditions.Filters);
        Assert.Single(conditions.Filters);

        newConditions = new FilterKeyValueAction()
        {
            Filters = [new FilterKeyValueAction() { FieldValue = null }]
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
