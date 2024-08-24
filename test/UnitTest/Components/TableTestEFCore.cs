﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Components;

public class TableTestEFCore : EFCoreTableTestBase
{
    [Fact]
    public async Task SearchText_Ok()
    {
        List<Foo>? items = null;
        var context = Context.Services.GetRequiredService<IDbContextFactory<FooContext>>().CreateDbContext();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.ShowSearch, true);
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.SearchMode, SearchMode.Top);
                pb.Add(a => a.SearchText, "ZhangSan");
                pb.Add(a => a.OnQueryAsync, new Func<QueryPageOptions, Task<QueryData<Foo>>>(op =>
                {
                    items = [.. context.Foos.Where(op.ToFilterLambda<Foo>())];
                    return Task.FromResult(new QueryData<Foo>() { Items = items, TotalCount = items.Count });
                }));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(3, "Searchable", true);
                    builder.CloseComponent();
                });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Address");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Address", typeof(string)));
                    builder.AddAttribute(3, "Searchable", true);
                    builder.CloseComponent();
                });
            });
        });

        Assert.NotNull(items);
        Assert.Empty(items);

        var table = cut.FindComponent<Table<Foo>>();
        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.SearchText, "Zhangsan");
        });
        await cut.InvokeAsync(() => table.Instance.QueryAsync());
        Assert.NotNull(items);
        Assert.Equal(2, items.Count);

        // 数据库 Sqlite Contains 使用 InStr 来比较，区分大小写
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
                    builder.OpenComponent<TableColumn<Foo, EnumEducation?>>(index++);
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, EnumEducation?>.Field), foo.Education);
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, EnumEducation?>.FieldExpression), foo.GenerateValueExpression(nameof(foo.Education), typeof(EnumEducation?)));
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, EnumEducation?>.Filterable), true);
                    builder.CloseComponent();
                }));
            });
        });

        var items = cut.FindAll(".dropdown-item");
        items[1].Click();
        var conditions = cut.FindComponent<EnumFilter>().Instance.GetFilterConditions();
        Assert.NotNull(conditions.Filters);
        Assert.Single(conditions.Filters);
    }

    [Fact]
    public void ButtonStyle_Ok()
    {
        var cut = Context.RenderComponent<Button>(pb =>
        {
            pb.Add(b => b.ButtonStyle, ButtonStyle.None);
        });
        Assert.DoesNotContain("btn-round", cut.Markup);
        Assert.DoesNotContain("btn-circle", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(b => b.ButtonStyle, ButtonStyle.Circle);
        });
        Assert.Contains("btn-circle", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(b => b.ButtonStyle, ButtonStyle.Round);
        });
        Assert.Contains("btn-round", cut.Markup);
    }
}
