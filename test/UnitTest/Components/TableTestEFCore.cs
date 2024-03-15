// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
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
}
