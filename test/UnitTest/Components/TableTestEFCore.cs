// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.EntityFrameworkCore;

namespace UnitTest.Components;

public class TableTestEFCore : BootstrapBlazorTestBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);
        services.AddDbContextFactory<FooContext>(option =>
        {
            option.UseSqlite("Data Source=FooTest.db;");
        });
    }

    [Fact]
    public async Task SearchText_Ok()
    {
        List<Foo>? items = null;
        var context = Context.Services.GetRequiredService<IDbContextFactory<FooContext>>().CreateDbContext();
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
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

        // 由于 SearchText 是 ZhangSan 无符合条件数据
        Assert.NotNull(items);
        Assert.Empty(items);

        var table = cut.FindComponent<Table<Foo>>();
        table.Render(pb =>
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

    class FooContext(DbContextOptions<FooContext> options) : DbContext(options)
    {
        /// <summary>
        /// 
        /// </summary>
        [NotNull]
        public DbSet<Foo>? Foos { get; set; }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Foo>().ToTable("Foo");
            modelBuilder.Entity<Foo>().Ignore(f => f.DateTime);
            modelBuilder.Entity<Foo>().Ignore(f => f.Count);
            modelBuilder.Entity<Foo>().Ignore(f => f.Complete);
            modelBuilder.Entity<Foo>().Ignore(f => f.Education);
            modelBuilder.Entity<Foo>().Ignore(f => f.Hobby);
            modelBuilder.Entity<Foo>().Ignore(f => f.ReadonlyColumn);
        }
    }
}
