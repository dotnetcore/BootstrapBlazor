﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using AngleSharp.Dom;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace UnitTest.Components;

public class ContextMenuTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task ContextMenu_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var foo = Foo.Generate(localizer);
        var clicked = false;

        var cut = Context.RenderComponent<ContextMenuZone>(pb =>
        {
            pb.AddChildContent<ContextMenuTrigger>(pb =>
            {
                pb.Add(a => a.WrapperTag, "div");
                pb.Add(a => a.ContextItem, foo);
                pb.AddChildContent(pb =>
                {
                    pb.OpenElement(0, "div");
                    pb.AddAttribute(1, "class", "context-trigger");
                    pb.AddContent(1, foo.Name);
                    pb.CloseElement();
                });
            });
            pb.AddChildContent<ContextMenu>(pb =>
            {
                pb.Add(a => a.ShowShadow, true);
                pb.AddChildContent<ContextMenuItem>(pb =>
                {
                    pb.Add(a => a.Icon, "fa fa-test");
                    pb.Add(a => a.Text, "Test");
                    pb.Add(a => a.Disabled, true);
                    pb.Add(a => a.OnClick, (item, value) =>
                    {
                        clicked = true;
                        return Task.CompletedTask;
                    });
                });
            });
        });

        var row = cut.Find(".context-trigger");
        row.ContextMenu(0, 10, 10, 10, 10, 2, 2);

        var menu = cut.FindComponent<ContextMenu>();
        menu.Contains("shadow");
        var pi = typeof(ContextMenu).GetField("_contextItem", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        Assert.NotNull(pi);

        var v = pi.GetValue(menu.Instance);
        Assert.NotNull(v);

        var item = menu.Find(".dropdown-item");
        Assert.DoesNotContain("blazor:onclick", item.InnerHtml);

        var contextItem = cut.FindComponent<ContextMenuItem>();
        contextItem.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Disabled, false);
            pb.Add(a => a.OnDisabledCallback, (item, v) =>
            {
                return true;
            });
        });
        item = menu.Find(".dropdown-item");
        Assert.DoesNotContain("blazor:onclick", item.InnerHtml);

        // trigger OnBeforeShowCallback
        bool menuCallback = false;
        contextItem.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Disabled, false);
            pb.Add(a => a.OnDisabledCallback, (item, v) =>
            {
                menuCallback = true;
                return false;
            });
        });
        menu.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.OnBeforeShowCallback, v =>
            {
                return Task.CompletedTask;
            });
        });
        item = menu.Find(".dropdown-item");
        item.Click();
        Assert.True(menuCallback);

        // 测试 Touch 事件
        TriggerTouchStart(row);

        await Task.Delay(500);
        row.TouchEnd();
        Assert.True(clicked);
    }

    [Theory]
    [InlineData(TableRenderMode.Table)]
    [InlineData(TableRenderMode.CardView)]
    public async Task ContextMenu_Table(TableRenderMode renderMode)
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

        var row = renderMode == TableRenderMode.CardView ? cut.Find(".table-row") : cut.Find("tbody tr");
        row.ContextMenu(0, 10, 10, 10, 10, 2, 2);

        var menu = cut.FindComponent<ContextMenu>();
        var pi = typeof(ContextMenu).GetField("_contextItem", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        Assert.NotNull(pi);

        var v = pi.GetValue(menu.Instance);
        Assert.NotNull(v);

        var item = menu.Find(".dropdown-item");
        item.Click();
        Assert.True(clicked);

        TriggerTouchStart(row);
        TriggerTouchStart(row);

        var options = Context.Services.GetRequiredService<IOptions<BootstrapBlazorOptions>>();
        await Task.Delay(100 + options.Value.ContextMenuOptions.OnTouchDelay);
        row.TouchEnd();
    }

    [Fact]
    public async Task ContextMenu_TreeView()
    {
        var items = new List<TreeFoo>
        {
            new() { Text = "Node1", Id = "1010" },
            new() { Text = "Node2", Id = "1020" },

            new() { Text = "Node11", Id = "1011", ParentId = "1010" },
            new() { Text = "Node12", Id = "1011", ParentId = "1010" },

            new() { Text = "Node21", Id = "1021", ParentId = "1020" },
            new() { Text = "Node22", Id = "1021", ParentId = "1020" }
        };

        // 根节点
        var nodes = TreeFoo.CascadingTree(items).ToList();
        var clicked = false;

        var cut = Context.RenderComponent<ContextMenuZone>(pb =>
        {
            pb.AddChildContent<TreeView<TreeFoo>>(pb =>
            {
                pb.Add(a => a.Items, nodes);
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

        var row = cut.Find(".tree-content");
        row.ContextMenu(0, 10, 10, 10, 10, 2, 2);

        var menu = cut.FindComponent<ContextMenu>();
        var pi = typeof(ContextMenu).GetField("_contextItem", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        Assert.NotNull(pi);

        var v = pi.GetValue(menu.Instance);
        Assert.NotNull(v);

        var item = menu.Find(".dropdown-item");
        item.Click();
        Assert.True(clicked);

        var options = Context.Services.GetRequiredService<IOptionsMonitor<BootstrapBlazorOptions>>();
        options.CurrentValue.ContextMenuOptions.OnTouchDelay = 100;
        TriggerTouchStart(row);
        TriggerTouchStart(row);

        await Task.Delay(100 + 2 * options.CurrentValue.ContextMenuOptions.OnTouchDelay);
        row.TouchEnd();
    }

    private static void TriggerTouchStart(IElement row)
    {
        row.TouchStart(new TouchEventArgs()
        {
            Touches =
            [
                new()
                {
                    ClientX = 10,
                    ClientY = 10,
                    ScreenX = 10,
                    ScreenY = 10
                }
            ]
        });
    }
}
