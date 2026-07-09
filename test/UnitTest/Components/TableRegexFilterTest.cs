// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class TableRegexFilterTest : BootstrapBlazorTestBase
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
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, string>.FilterTemplate), new RenderFragment(builder =>
                    {
                        builder.OpenComponent<FilterProvider>(0);
                        builder.AddAttribute(1, nameof(FilterProvider.ShowMoreButton), true);
                        builder.AddAttribute(2, nameof(FilterProvider.ChildContent), new RenderFragment(builder =>
                        {
                            builder.OpenComponent<RegexFilter>(0);
                            builder.CloseComponent();
                        }));
                        builder.CloseComponent();
                    }));
                    builder.CloseComponent();
                }));
            });
        });

        await cut.InvokeAsync(() =>
        {
            var filter = cut.FindComponent<BootstrapInput<string>>().Instance;
            filter.SetValue("^test\\d+$");

            var items = cut.FindAll(".dropdown-item");
            items[1].Click();
        });
        var conditions = cut.FindComponent<RegexFilter>().Instance.GetFilterConditions();
        Assert.Single(conditions.Filters);
        Assert.Equal(FilterAction.NotMatch, conditions.Filters[0].FilterAction);
    }

    [Fact]
    public async Task FilterAction_Ok()
    {
        var cut = Context.Render<RegexFilter>();
        var filter = cut.Instance;

        var newConditions = new FilterKeyValueAction()
        {
            Filters =
            [
                new FilterKeyValueAction() { FieldValue = "test1", FilterAction = FilterAction.Match },
                new FilterKeyValueAction() { FieldValue = "test2", FilterAction = FilterAction.NotMatch }
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

    [Fact]
    public async Task InvalidRegex_Ok()
    {
        var cut = Context.Render<RegexFilter>();
        var filter = cut.Instance;

        // 无效正则表达式不生成过滤条件
        var newConditions = new FilterKeyValueAction()
        {
            Filters =
            [
                new FilterKeyValueAction() { FieldValue = "(", FilterAction = FilterAction.Match },
                new FilterKeyValueAction() { FieldValue = "[", FilterAction = FilterAction.NotMatch }
            ]
        };
        await cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        var conditions = filter.GetFilterConditions();
        Assert.Empty(conditions.Filters);
    }
}
