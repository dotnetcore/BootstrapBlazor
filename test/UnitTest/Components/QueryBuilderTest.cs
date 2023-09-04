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
}
