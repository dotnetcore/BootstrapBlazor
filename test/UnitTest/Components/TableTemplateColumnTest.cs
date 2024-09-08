// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
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
}
