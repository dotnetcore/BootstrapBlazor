// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;

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

        // 测试 FilterLogicItem LogicChanged 代码覆盖率
        var logicItem = cut.FindComponent<FilterLogicItem>();
        var item = logicItem.FindAll(".dropdown-item")[0];
        cut.InvokeAsync(() => item.Click());
        Assert.Equal(FilterLogic.And, logicItem.Instance.Logic);
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

        cut.InvokeAsync(() =>
        {
            var filter = cut.FindComponent<BootstrapInput<string>>().Instance;
            filter.SetValue("test");

            var items = cut.FindAll(".dropdown-item");
            items[1].Click();
            var condtions = cut.FindComponent<StringFilter>().Instance.GetFilterConditions();
            Assert.Single(condtions);
        });
    }

    [Fact]
    public void SearchFilterAction_Ok()
    {
        var searchFilterAction = new SearchFilterAction("Test-Search", "1", FilterAction.NotEqual);

        var condtion = searchFilterAction.GetFilterConditions();
        Assert.Single(condtion);
        Assert.Equal("Test-Search", condtion.First().FieldKey);
        Assert.Equal("1", condtion.First().FieldValue);
        Assert.Equal(FilterAction.NotEqual, condtion.First().FilterAction);
        Assert.Equal(FilterLogic.And, condtion.First().FilterLogic);

        searchFilterAction.Reset();
        Assert.Null(searchFilterAction.Value);

        searchFilterAction.SetFilterConditionsAsync(new List<FilterKeyValueAction>()
        {
            new FilterKeyValueAction()
            {
                FieldKey = "Test-Search",
                FieldValue = "test"
            }
        });
        Assert.Equal("test", searchFilterAction.Value);
    }

    [Fact]
    public async Task SetFilterConditions_Ok()
    {
        var cut = Context.RenderComponent<StringFilter>();
        var filter = cut.Instance;
        var conditions = filter.GetFilterConditions();
        Assert.Empty(conditions);

        var newConditions = new List<FilterKeyValueAction>
        {
            new FilterKeyValueAction() { FieldValue = "test1" },
            new FilterKeyValueAction() { FieldValue = "test2" }
        };
        await filter.SetFilterConditionsAsync(newConditions);
        conditions = filter.GetFilterConditions();
        Assert.Equal(2, conditions.Count());

        newConditions = new List<FilterKeyValueAction>
        {
            new FilterKeyValueAction() { FieldValue = true },
            new FilterKeyValueAction() { FieldValue = false }
        };
        await filter.SetFilterConditionsAsync(newConditions);
        conditions = filter.GetFilterConditions();
        Assert.Empty(conditions);

        newConditions = new List<FilterKeyValueAction>
        {
            new FilterKeyValueAction() { FieldValue = "" },
            new FilterKeyValueAction() { FieldValue = "" }
        };
        await filter.SetFilterConditionsAsync(newConditions);
        conditions = filter.GetFilterConditions();
        Assert.Empty(conditions);
    }

    [Fact]
    public void HasFilter_Ok()
    {
        var cut = Context.RenderComponent<MockStringFilter>();
        var filter = cut.Instance;
        Assert.False(filter.HasFilterTest());
    }

    private class MockStringFilter : StringFilter
    {
        public bool HasFilterTest() => HasFilter;
    }
}
