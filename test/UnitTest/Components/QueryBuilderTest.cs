// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;

namespace UnitTest.Components;

public class QueryBuilderTest : BootstrapBlazorTestBase
{
    [Fact]
    public void QueryBuilder_Ok()
    {
        var cut = Context.RenderComponent<QueryBuilder<Foo>>();
        cut.Contains("query-builder");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.AdditionalAttributes, new Dictionary<string, object>
            {
                { "class", "test" }
            });
        });
        cut.Contains("query-builder test");
    }

    [Fact]
    public void HeaderTemplate_Ok()
    {
        var cut = Context.RenderComponent<QueryBuilder<Foo>>(pb =>
        {
            pb.Add(a => a.HeaderTemplate, new RenderFragment<FilterKeyValueAction>(filter => builder =>
            {
                builder.AddContent(0, "test-header-template");
            }));
        });
        cut.Contains("test-header-template");
    }

    [Fact]
    public void Dropdown_Ok()
    {
        var cut = Context.RenderComponent<QueryBuilder<Foo>>();
        var items = cut.FindAll(".dropdown-item");
        Assert.Equal(2, items.Count);
        var groups = cut.FindAll("ul");
        Assert.Equal(1, groups.Count);

        // 点击组合条件 dropdown-item
        items[0].Click();
        groups = cut.FindAll("ul");

        // 两个组合条件
        Assert.Equal(2, groups.Count);

        // 点击删除按钮
        var btn = cut.Find(".btn-remove");
        btn.Click();
        groups = cut.FindAll("ul");

        // 还有一个组合条件
        Assert.Equal(1, groups.Count);

        // 点击单行条件 dropdown-item
        items = cut.FindAll(".dropdown-item");
        items[1].Click();
        var rows = cut.FindAll(".qb-row");

        // 有一个单行条件
        Assert.Equal(1, rows.Count);

        // 点击 btn-plus 按钮
        var plus = cut.Find(".btn-plus");
        plus.Click();
        rows = cut.FindAll(".qb-row");

        // 有两个单行条件
        Assert.Equal(2, rows.Count);

        // 点击 btn-minus 按钮
        var buttons = cut.FindAll(".btn-minus");
        Assert.Equal(2, buttons.Count);
        buttons[1].Click();

        // 有一个单行条件
        rows = cut.FindAll(".qb-row");
        Assert.Single(rows);
    }

    [Fact]
    public void Header_Ok()
    {
        var cut = Context.RenderComponent<QueryBuilder<Foo>>();
        var header = cut.Find(".qb-header");
        var group = header.QuerySelector(".btn-group");
        Assert.NotNull(group);

        var buttons = group.QuerySelectorAll(".btn");
        Assert.Equal(2, buttons.Length);

        buttons[0].Click();
        Assert.Equal(FilterLogic.And, cut.Instance.Value.FilterLogic);

        buttons[1].Click();
        Assert.Equal(FilterLogic.Or, cut.Instance.Value.FilterLogic);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Logic, FilterLogic.And);
        });
        Assert.Equal(FilterLogic.And, cut.Instance.Value.FilterLogic);

        buttons[1].Click();
        Assert.Equal(FilterLogic.Or, cut.Instance.Value.FilterLogic);
        Assert.Equal(FilterLogic.Or, cut.Instance.Logic);
    }

    [Fact]
    public void QueryGroup_Ok()
    {
        var filter = new FilterKeyValueAction();
        var foo = new Foo();
        var cut = Context.RenderComponent<QueryBuilder<Foo>>(pb =>
        {
            pb.Add(a => a.Value, filter);
            pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<FilterKeyValueAction>(this, v => filter = v));
            pb.Add(a => a.ChildContent, new RenderFragment<Foo>(foo => builder =>
            {
                builder.OpenComponent<QueryColumn<string>>(0);
                builder.AddAttribute(1, "Field", foo.Name);
                builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, nameof(Foo.Name), typeof(string)));
                builder.AddAttribute(3, "Operator", FilterAction.Equal);
                builder.CloseComponent();

                builder.OpenComponent<QueryGroup>(10);
                builder.AddAttribute(11, "Logic", FilterLogic.Or);
                builder.AddAttribute(12, "ChildContent", new RenderFragment(builder =>
                {
                    builder.OpenComponent<QueryColumn<int>>(20);
                    builder.AddAttribute(21, "Field", foo.Count);
                    builder.AddAttribute(22, "FieldExpression", Utility.GenerateValueExpression(foo, nameof(Foo.Count), typeof(int)));
                    builder.AddAttribute(23, "Operator", FilterAction.GreaterThanOrEqual);
                    builder.AddAttribute(24, "Value", 1);
                    builder.CloseComponent();

                    builder.OpenComponent<QueryColumn<string>>(30);
                    builder.AddAttribute(31, "Field", foo.Address);
                    builder.AddAttribute(32, "FieldExpression", Utility.GenerateValueExpression(foo, nameof(Foo.Address), typeof(string)));
                    builder.AddAttribute(33, "Operator", FilterAction.Contains);
                    builder.AddAttribute(34, "Value", "10");
                    builder.CloseComponent();
                }));
                builder.CloseComponent();
            }));
        });
        cut.WaitForAssertion(() => cut.DoesNotContain("table-loading"));

        Assert.NotNull(cut.Instance.Value.Filters);
        Assert.Equal(2, cut.Instance.Value.Filters.Count);

        Assert.NotNull(cut.Instance.Value.Filters[1].Filters);
        Assert.Equal(2, cut.Instance.Value.Filters[1].Filters?.Count);
    }

    [Fact]
    public void QueryGroup_Null()
    {
        var foo = new Foo();
        var cut = Context.RenderComponent<QueryGroup>(pb =>
        {
            pb.Add(a => a.Logic, FilterLogic.Or);
            pb.Add(a => a.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<QueryColumn<int>>(20);
                builder.AddAttribute(21, "Field", foo.Count);
                builder.AddAttribute(22, "FieldExpression", Utility.GenerateValueExpression(foo, nameof(Foo.Count), typeof(int)));
                builder.AddAttribute(23, "Operator", FilterAction.GreaterThanOrEqual);
                builder.AddAttribute(24, "Value", 1);
                builder.CloseComponent();
            }));
        });

        Assert.Equal(FilterLogic.Or, cut.Instance.Logic);
    }

    [Fact]
    public void QueryColumn_Ok()
    {
        var foo = new Foo() { Count = 10 };
        var cut = Context.RenderComponent<QueryColumn<int>>(pb =>
        {
            pb.Add(a => a.Field, foo.Count);
            pb.Add(a => a.Logic, FilterLogic.Or);
            pb.Add(a => a.Operator, FilterAction.NotEqual);
        });

        Assert.Equal(FilterLogic.Or, cut.Instance.Logic);
        Assert.Equal(FilterAction.NotEqual, cut.Instance.Operator);
        Assert.Equal(10, cut.Instance.Field);
    }
}
