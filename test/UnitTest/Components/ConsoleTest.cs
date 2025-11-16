// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections;
using Console = BootstrapBlazor.Components.Console;

namespace UnitTest.Components;

public class ConsoleTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Height_OK()
    {
        var cut = Context.Render<Console>(builder => builder.Add(a => a.Height, 100));

        Assert.Contains("style=\"height: 100px;\"", cut.Markup);
    }

    [Fact]
    public void HeaderText_OK()
    {
        var cut = Context.Render<Console>(builder => builder.Add(a => a.HeaderText, "HeaderText"));

        Assert.Contains("HeaderText", cut.Markup);
    }

    [Fact]
    public void Items_OK()
    {
        var cut = Context.Render<Console>(builder =>
        {
            builder.Add(a => a.Items, new List<ConsoleMessageItem>()
            {
                new() { Message = "Test1" }, new() { Message = "Test2" }
            });
        });

        var res = cut.Find(".console-window").HasChildNodes;
        Assert.True(res);

        cut.Render(pb =>
        {
            pb.Add(a => a.ItemTemplate, item => builder =>
            {
                builder.AddContent(0, $"test-{item.Message}-end");
            });
        });
        cut.Contains("test-Test1-end");
        cut.Contains("test-Test2-end");
    }

    [Fact]
    public async Task OnClear_OK()
    {
        var clearClicked = false;
        var cut = Context.Render<Console>(builder =>
        {
            builder.Add(a => a.Items, new List<ConsoleMessageItem>()
            {
                new() { Message = "Test1" }, new() { Message = "Test2" }
            });
        });
        Assert.DoesNotContain("btn-secondary", cut.Markup);
        Assert.False(clearClicked);

        // 实例触发 OnClear 方法
        await cut.Instance.OnClearConsole();

        cut.Render(pb =>
        {
            pb.Add(a => a.OnClear, new Func<Task>(() =>
            {
                clearClicked = true;
                return Task.CompletedTask;
            }));
        });
        cut.Find(".btn-secondary").Click();
        Assert.True(clearClicked);
    }

    [Fact]
    public void ClearButtonText_OK()
    {
        var cut = Context.Render<Console>(builder =>
        {
            builder.Add(a => a.Items, new List<ConsoleMessageItem>()
            {
                new() { Message = "Test1" }, new() { Message = "Test2" }
            });
            builder.Add(a => a.OnClear, () => Task.CompletedTask);
            builder.Add(a => a.ClearButtonText, "Console Clear");
        });

        Assert.Contains("Console Clear", cut.Markup);
    }

    [Fact]
    public void OnClearButtonText_OK()
    {
        var cut = Context.Render<Console>(builder =>
        {
            builder.Add(a => a.Items, new List<ConsoleMessageItem>()
            {
                new() { Message = "Test1" }, new() { Message = "Test2" }
            });
            builder.Add(a => a.OnClear, () => Task.CompletedTask);
            builder.Add(a => a.ClearButtonIcon, "fa-solid fa-xmark");
        });

        Assert.Contains("fa-solid fa-xmark", cut.Markup);
    }


    [Fact]
    public void ClearButtonColor_OK()
    {
        var cut = Context.Render<Console>(builder =>
        {
            builder.Add(a => a.Items, new List<ConsoleMessageItem>()
            {
                new() { Message = "Test1" }, new() { Message = "Test2" }
            });
            builder.Add(a => a.OnClear, () => Task.CompletedTask);
            builder.Add(a => a.ClearButtonColor, Color.Primary);
        });

        Assert.Contains("btn-primary", cut.Markup);
    }

    [Fact]
    public void ShowAutoScroll_OK()
    {
        var cut = Context.Render<Console>(builder =>
        {
            builder.Add(a => a.Items, new List<ConsoleMessageItem>()
            {
                new() { Message = "Test1" }, new() { Message = "Test2" }
            });
            builder.Add(a => a.ShowAutoScroll, true);
        });

        Assert.Contains("form-check", cut.Markup);
    }

    [Fact]
    public void AutoScrollString_OK()
    {
        var cut = Context.Render<Console>(builder =>
        {
            builder.Add(a => a.Items, new List<ConsoleMessageItem>()
            {
                new() { Message = "Test1" }, new() { Message = "Test2" }
            });
            builder.Add(a => a.IsAutoScroll, true);
        });
        Assert.Contains("data-bb-scroll=\"auto\"", cut.Markup);

        cut.Render(pb =>
        {
            pb.Add(a => a.IsAutoScroll, false);
        });
        Assert.DoesNotContain("data-bb-scroll=\"auto\"", cut.Markup);
    }

    [Fact]
    public void AutoScrollText_OK()
    {
        var cut = Context.Render<Console>(builder =>
        {
            builder.Add(a => a.Items, new List<ConsoleMessageItem>()
            {
                new() { Message = "Test1" }, new() { Message = "Test2" }
            });
            builder.Add(a => a.AutoScrollText, "AutoScrollText");
            builder.Add(a => a.ShowAutoScroll, true);
        });

        Assert.Contains("AutoScrollText", cut.Markup);
    }

    [Fact]
    public void LightTitle_OK()
    {
        var cut = Context.Render<Console>(builder =>
        {
            builder.Add(a => a.Items, new List<ConsoleMessageItem>()
            {
                new() { Message = "Test1" }, new() { Message = "Test2" }
            });
            builder.Add(a => a.LightTitle, "LightTitle");
        });

        Assert.Contains("LightTitle", cut.Markup);
    }

    [Fact]
    public async Task ClickAutoScroll_OK()
    {
        var cut = Context.Render<Console>(builder =>
        {
            builder.Add(a => a.Items, new List<ConsoleMessageItem>()
            {
                new() { Message = "Test1" }, new() { Message = "Test2" }
            });
            builder.Add(a => a.ShowAutoScroll, true);
        });

        var item = cut.FindComponent<Checkbox<bool>>();
        await cut.InvokeAsync(item.Instance.OnToggleClick);
        var res = cut.Instance.IsAutoScroll;
        Assert.False(res);
    }

    [Fact]
    public void MessageItemColor_OK()
    {
        var cut = Context.Render<Console>(builder =>
        {
            builder.Add(a => a.Items, new List<ConsoleMessageItem>()
            {
                new() { Message = "Test1", Color = Color.Danger }, new() { Message = "Test2" }
            });
        });

        Assert.Contains("text-danger", cut.Markup);
    }

    [Fact]
    public void MessageItemHtml_OK()
    {
        var cut = Context.Render<Console>(builder =>
        {
            builder.Add(a => a.Items, new List<ConsoleMessageItem>()
            {
                new() { Message = "<div class=\"html\">Test1</div>", Color = Color.Danger, IsHtml = true }, new() { Message = "Test2" }
            });
        });
        cut.Contains("<div class=\"html\">Test1</div>");
    }

    [Fact]
    public void FooterTemplate_OK()
    {
        var cut = Context.Render<Console>(pb =>
        {
            pb.Add(a => a.Items, new List<ConsoleMessageItem>()
            {
                new() { Message = "Test1", Color = Color.Danger }, new() { Message = "Test2" }
            });
            pb.Add(a => a.FooterTemplate, builder =>
            {
                builder.AddContent(0, "test-footer-template");
            });
        });
        Assert.Contains("test-footer-template", cut.Markup);
    }

    [Fact]
    public void ShowLight_OK()
    {
        var cut = Context.Render<Console>(pb =>
        {
            pb.Add(a => a.Items, new List<ConsoleMessageItem>()
            {
                new() { Message = "Test1", Color = Color.Danger }, new() { Message = "Test2" }
            });
            pb.Add(a => a.ShowLight, false);
        });
        Assert.DoesNotContain("light", cut.Markup);
    }

    [Fact]
    public void LightColor_OK()
    {
        var cut = Context.Render<Console>(pb =>
        {
            pb.Add(a => a.Items, new List<ConsoleMessageItem>()
            {
                new() { Message = "Test1", Color = Color.Danger }, new() { Message = "Test2" }
            });
            pb.Add(a => a.LightColor, Color.Danger);
        });
        Assert.Contains("light-danger", cut.Markup);
    }

    [Fact]
    public void IsFlashLight_OK()
    {
        var cut = Context.Render<Console>(pb =>
        {
            pb.Add(a => a.Items, new List<ConsoleMessageItem>()
            {
                new() { Message = "Test1", Color = Color.Danger }, new() { Message = "Test2" }
            });
            pb.Add(a => a.IsFlashLight, false);
        });
        Assert.DoesNotContain("flash", cut.Markup);
    }

    [Fact]
    public void HeaderTemplate_OK()
    {
        var cut = Context.Render<Console>(pb =>
        {
            pb.Add(a => a.Items, new List<ConsoleMessageItem>()
            {
                new() { Message = "Test1", Color = Color.Danger }, new() { Message = "Test2" }
            });
            pb.Add(a => a.HeaderTemplate, builder =>
            {
                builder.AddContent(0, "test-header-template");
            });
        });
        Assert.Contains("test-header-template", cut.Markup);
    }

    [Fact]
    public void CssClass_Ok()
    {
        var cut = Context.Render<Console>(pb =>
        {
            pb.Add(a => a.Items, new List<ConsoleMessageItem>()
            {
                new() { Message = "Test1", CssClass = "test-css-class" }, new() { Message = "Test2" }
            });
        });
        Assert.Contains("test-css-class", cut.Markup);
    }

    [Fact]
    public void Collection_Ok()
    {
        var items = new ConsoleMessageCollection(2)
        {
            new() { Message = "Test1", CssClass = "test-css-class" }
        };
        Assert.Single(items);

        items.Add(new ConsoleMessageItem() { Message = "Text2" });
        Assert.Equal(2, items.Count());

        items.Add(new ConsoleMessageItem() { Message = "Text3" });
        Assert.Equal(2, items.Count());

        items.Clear();
        Assert.Empty(items);

        items.Dispose();
    }

    [Fact]
    public void CollectionMaxCount_Ok()
    {
        var items = new ConsoleMessageCollection() { MaxCount = 2 };
        Assert.Empty(items);
    }

    [Fact]
    public void Collection_GetEnumerator()
    {
        var items = new ConsoleMessageCollection()
        {
            new() { Message = "Test1", CssClass = "test-css-class" }
        };
        IEnumerable collection = items;
        Assert.NotNull(collection.GetEnumerator());
    }
}
