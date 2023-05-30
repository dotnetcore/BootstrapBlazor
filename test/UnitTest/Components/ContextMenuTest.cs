// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace UnitTest.Components;

public class ContextMenuTest : BootstrapBlazorTestBase
{
    [Theory]
    [InlineData(TableRenderMode.Table)]
    [InlineData(TableRenderMode.CardView)]
    public void ContextMenu_Table(TableRenderMode renderMode)
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var clicked = false;

        var cut = Context.RenderComponent<ContextMenuZone>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, renderMode);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
            pb.AddChildContent<ContextMenu>(pb =>
            {
                pb.AddChildContent<ContextMenuItem>(pb =>
                {
                    pb.Add(a => a.Icon, "fa fa-test");
                    pb.Add(a => a.Text, "Test");
                    pb.Add(a => a.Disabled, false);
                    pb.Add(a => a.OnClick, (item, value) =>
                    {
                        clicked = true; 
                        return Task.CompletedTask;
                    });
                });
            });
        });

        cut.InvokeAsync(() =>
        {
            var row = cut.Find(".table-row");
            row.ContextMenu(0, 10, 10, 10, 10, 2, 2);

            var menu = cut.FindComponent<ContextMenu>();
            var pi = typeof(ContextMenu).GetProperty("ContextItem", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            Assert.NotNull(pi);

            var v = pi.GetValue(menu.Instance);
            Assert.NotNull(v);

            var item = menu.Find(".dropdown-item");
            item.Click();
            Assert.True(clicked);
        });
    }
}
