// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace UnitTest.Components;

public class TableTemplateColumnTest : BootstrapBlazorTestBase
{
    [Theory]
    [InlineData(TableRenderMode.CardView)]
    [InlineData(TableRenderMode.Table)]
    public void TableTemplateColumn_Ok(TableRenderMode mode)
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, mode);
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();

                    builder.OpenComponent<TableTemplateColumn<Foo>>(10);
                    builder.AddAttribute(11, "Template", new RenderFragment<TableColumnContext<Foo, object?>>(context => builder =>
                    {
                        builder.AddContent(0, $"template-{context.Row.Name}");
                    }));
                    builder.CloseComponent();
                });
            });
        });

        cut.Contains("template-张三 0001");
    }

    [Fact]
    public void TableTemplateColumn_SameFieldName()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    // 两个模板列使用相同的 FieldName
                    builder.OpenComponent<TableTemplateColumn<Foo>>(0);
                    builder.AddAttribute(1, "FieldName", "Field1");
                    builder.AddAttribute(2, "Template", new RenderFragment<TableColumnContext<Foo, object?>>(context => builder =>
                    {
                        builder.AddContent(0, $"template1-{context.Row.Name}");
                    }));
                    builder.CloseComponent();

                    builder.OpenComponent<TableTemplateColumn<Foo>>(10);
                    builder.AddAttribute(11, "FieldName", "Field1");
                    builder.AddAttribute(12, "Template", new RenderFragment<TableColumnContext<Foo, object?>>(context => builder =>
                    {
                        builder.AddContent(0, $"template2-{context.Row.Name}");
                    }));
                    builder.CloseComponent();
                });
            });
        });

        // 两个模板列均正常渲染，说明相同 FieldName 未导致列被覆盖丢失
        cut.Contains("template1-张三 0001");
        cut.Contains("template2-张三 0001");

        // 相同 FieldName 自动去重，保证列标识唯一：首列保留原名，后列采番
        var table = cut.FindComponent<Table<Foo>>().Instance;
        var fieldNames = table.Columns.Select(i => i.GetFieldName()).ToList();
        Assert.Equal(2, fieldNames.Count);
        Assert.Equal(2, fieldNames.Distinct().Count());
        Assert.Contains("Field1", fieldNames);
        Assert.Contains(fieldNames, i => i.StartsWith("__bb_template_column_"));
    }

    [Fact]
    public void TableTemplateColumn_ConflictWithBoundColumn()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    // 普通绑定列 FieldName 为 Name
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();

                    // 模板列指定与普通列相同的 FieldName
                    builder.OpenComponent<TableTemplateColumn<Foo>>(10);
                    builder.AddAttribute(11, "FieldName", "Name");
                    builder.AddAttribute(12, "Template", new RenderFragment<TableColumnContext<Foo, object?>>(context => builder =>
                    {
                        builder.AddContent(0, $"template-{context.Row.Name}");
                    }));
                    builder.CloseComponent();
                });
            });
        });

        cut.Contains("template-张三 0001");

        var table = cut.FindComponent<Table<Foo>>().Instance;

        // 普通列参与取值/编辑，其 FieldName 不被更改
        var boundColumn = table.Columns.First(i => i is not TableTemplateColumn<Foo>);
        Assert.Equal("Name", boundColumn.GetFieldName());

        // 与普通列冲突的模板列被采番，避免覆盖普通列
        var templateColumn = table.Columns.OfType<TableTemplateColumn<Foo>>().Single();
        Assert.StartsWith("__bb_template_column_", templateColumn.GetFieldName());
    }
}
