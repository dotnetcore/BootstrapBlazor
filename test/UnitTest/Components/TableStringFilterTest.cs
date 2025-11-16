// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class TableStringFilterTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task OnFilterAsync_Ok()
    {
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.Items, new List<Foo>() { new() });
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.ShowFilterHeader, true);
                pb.Add(a => a.TableColumns, new RenderFragment<Foo>(foo => builder =>
                {
                    var index = 0;
                    builder.OpenComponent<TableColumn<Foo, string>>(index++);
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, string>.Field), foo.Name);
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, string>.FieldExpression), foo.GenerateValueExpression());
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, string>.Filterable), true);
                    builder.CloseComponent();
                }));
            });
        });

        await cut.InvokeAsync(() =>
        {
            var filter = cut.FindComponent<BootstrapInput<string>>().Instance;
            filter.SetValue("test");

            var items = cut.FindAll(".dropdown-item");
            items[1].Click();
        });
        var conditions = cut.FindComponent<StringFilter>().Instance.GetFilterConditions();
        Assert.Single(conditions.Filters);
    }

    [Fact]
    public async Task FilterAction_Ok()
    {
        var cut = Context.Render<StringFilter>();
        var filter = cut.Instance;

        var newConditions = new FilterKeyValueAction()
        {
            Filters =
            [
                new FilterKeyValueAction() { FieldValue = "test1" },
                new FilterKeyValueAction() { FieldValue = "test2" }
            ]
        };
        await cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        var conditions = filter.GetFilterConditions();
        Assert.NotNull(conditions.Filters);
        Assert.Equal(2, conditions.Filters.Count);

        await cut.InvokeAsync(() => filter.Reset());
        conditions = filter.GetFilterConditions();
        Assert.Empty(conditions.Filters);

        // Improve test coverage
        newConditions = new FilterKeyValueAction()
        {
            Filters =
            [
                new FilterKeyValueAction() { FieldValue = true },
                new FilterKeyValueAction() { FieldValue = false }
            ]
        };
        await cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        conditions = filter.GetFilterConditions();
        Assert.Empty(conditions.Filters);

        newConditions = new FilterKeyValueAction()
        {
            Filters =
            [
                new FilterKeyValueAction() { FieldValue = "" },
                new FilterKeyValueAction() { FieldValue = "" }
            ]
        };
        await cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        conditions = filter.GetFilterConditions();
        Assert.Empty(conditions.Filters);

        newConditions = new FilterKeyValueAction() { FieldValue = "1" };
        await cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        conditions = filter.GetFilterConditions();
        Assert.Single(conditions.Filters);
    }
}
