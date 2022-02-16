// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using UnitTest.Extensions;

namespace UnitTest.Components;

public class TableStringFilterTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Count_Ok()
    {
        var cut = Context.RenderComponent<StringFilter>(pb =>
        {
            pb.Add(a => a.Count, 2);
        });

        var logic = cut.FindComponent<FilterLogicItem>();
        Assert.NotNull(logic);

        var conditions = cut.Instance.GetFilterConditions();
        Assert.Empty(conditions);

        var dt = cut.FindComponent<BootstrapInput<string>>().Instance;
        cut.InvokeAsync(() => dt.SetValue("Test"));

        conditions = cut.Instance.GetFilterConditions();
        Assert.Single(conditions);

        dt = cut.FindComponents<BootstrapInput<string>>()[1].Instance;
        cut.InvokeAsync(() => dt.SetValue("Test"));

        conditions = cut.Instance.GetFilterConditions();
        Assert.Equal(2, conditions.Count());
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
                    builder.OpenComponent<TableColumn<Foo, string>>(index++);
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, string>.Field), foo.Name);
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, string>.FieldExpression), foo.GenerateValueExpression());
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, string>.Filterable), true);
                    builder.CloseComponent();
                }));
            });
        });
        var filter = cut.FindComponent<BootstrapInput<string>>().Instance;
        cut.InvokeAsync(() => filter.SetValue("test"));

        var items = cut.FindAll(".dropdown-item");
        IEnumerable<FilterKeyValueAction>? condtions = null;
        cut.InvokeAsync(() => items[1].Click());
        cut.InvokeAsync(() => condtions = cut.FindComponent<StringFilter>().Instance.GetFilterConditions());
        Assert.Single(condtions);
    }
}
