// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class DropdownWidgetTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Items_OK()
    {
        var cut = Context.RenderComponent<DropdownWidget>(builder => builder.Add(s => s.Items, GetItems()));
    }

    [Fact]
    public void ChildContent_OK()
    {
        var cut = Context.RenderComponent<DropdownWidget>(builder =>
        {
            builder.Add(s => s.Items, GetItems());
            builder.Add(s => s.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<Button>(0);
                builder.CloseComponent();
            }));
        });

        var component = cut.FindComponent<Button>();
        Assert.NotNull(component);
    }

    [Fact]
    public void Icon_OK()
    {
        var cut = Context.RenderComponent<DropdownWidget>(builder =>
        {
            builder.Add(s => s.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<DropdownWidgetItem>(0);
                builder.AddAttribute(1, nameof(DropdownWidgetItem.Icon), "fa-regular fa-bell");
                builder.CloseComponent();
            }));
        });

        var ele = cut.Find(".fa-bell");
        Assert.NotNull(ele);
    }

    [Fact]
    public void Title_OK()
    {
        var cut = Context.RenderComponent<DropdownWidget>(builder =>
        {
            builder.Add(s => s.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<DropdownWidgetItem>(0);
                builder.AddAttribute(1, nameof(DropdownWidgetItem.Title), "Widget Title");
                builder.CloseComponent();
            }));
        });

        Assert.Contains("Widget Title", cut.Markup);
    }

    [Fact]
    public void BadgeColor_OK()
    {
        var cut = Context.RenderComponent<DropdownWidget>(builder =>
        {
            builder.Add(s => s.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<DropdownWidgetItem>(0);
                builder.AddAttribute(1, nameof(DropdownWidgetItem.BadgeColor), Color.Success);
                builder.AddAttribute(2, nameof(DropdownWidgetItem.BadgeNumber), "4");
                builder.CloseComponent();
            }));
        });

        var ele = cut.Find(".text-bg-success");
        Assert.NotNull(ele);
    }

    [Fact]
    public void BadgeNumber_OK()
    {
        var cut = Context.RenderComponent<DropdownWidget>(builder =>
        {
            builder.Add(s => s.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<DropdownWidgetItem>(0);
                builder.AddAttribute(1, nameof(DropdownWidgetItem.BadgeColor), Color.Success);
                builder.AddAttribute(2, nameof(DropdownWidgetItem.BadgeNumber), "4");
                builder.CloseComponent();
            }));
        });

        var ele = cut.Find(".dropdown-toggle").Children.Last();
        var count = ele.TextContent;
        Assert.Equal("4", count);
    }

    [Fact]
    public void ShowArrow_OK()
    {
        var cut = Context.RenderComponent<DropdownWidget>(builder =>
        {
            builder.Add(s => s.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<DropdownWidgetItem>(0);
                builder.AddAttribute(1, nameof(DropdownWidgetItem.ShowArrow), true);
                builder.AddAttribute(2, nameof(DropdownWidgetItem.BadgeNumber), "4");
                builder.CloseComponent();
            }));
        });

        var ele = cut.Find(".dropdown-arrow");
        Assert.NotNull(ele);
    }

    [Fact]
    public void BodyTemplate_OK()
    {
        var cut = Context.RenderComponent<DropdownWidget>(builder =>
        {
            builder.Add(s => s.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<DropdownWidgetItem>(0);
                builder.AddAttribute(2, nameof(DropdownWidgetItem.BodyTemplate), new RenderFragment(builder =>
                {
                    builder.OpenComponent<Button>(0);
                    builder.CloseComponent();
                }));
                builder.CloseComponent();
            }));
        });

        var component = cut.FindComponent<Button>();
        Assert.NotNull(component);
    }

    [Fact]
    public void HeaderTemplate_OK()
    {
        var cut = Context.RenderComponent<DropdownWidget>(builder =>
        {
            builder.Add(s => s.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<DropdownWidgetItem>(0);
                builder.AddAttribute(2, nameof(DropdownWidgetItem.HeaderTemplate), new RenderFragment(builder =>
                {
                    builder.OpenComponent<Button>(0);
                    builder.CloseComponent();
                }));
                builder.CloseComponent();
            }));
        });

        var component = cut.FindComponent<Button>();
        Assert.NotNull(component);
    }

    [Fact]
    public void HeaderColor_OK()
    {
        var cut = Context.RenderComponent<DropdownWidget>(builder =>
        {
            builder.Add(s => s.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<DropdownWidgetItem>(0);
                builder.AddAttribute(1, nameof(DropdownWidgetItem.HeaderColor), Color.Success);
                builder.AddAttribute(2, nameof(DropdownWidgetItem.HeaderTemplate), new RenderFragment(builder =>
                {
                    builder.OpenComponent<Button>(0);
                    builder.CloseComponent();
                }));
                builder.CloseComponent();
            }));
        });

        var ele = cut.Find(".bg-success");
        Assert.NotNull(ele);
    }

    [Fact]
    public async Task OnItemAsync_OK()
    {
        var shown = false;
        var closed = false;
        var cut = Context.RenderComponent<DropdownWidget>(builder =>
        {
            builder.Add(a => a.OnItemShownAsync, item =>
            {
                shown = true;
                return Task.CompletedTask;
            });
            builder.Add(s => s.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<DropdownWidgetItem>(0);
                builder.AddAttribute(1, nameof(DropdownWidgetItem.HeaderColor), Color.Success);
                builder.AddAttribute(2, nameof(DropdownWidgetItem.Title), "Test1");
                builder.CloseComponent();

                builder.OpenComponent<DropdownWidgetItem>(0);
                builder.AddAttribute(10, nameof(DropdownWidgetItem.HeaderColor), Color.Success);
                builder.AddAttribute(11, nameof(DropdownWidgetItem.Title), "Test2");
                builder.CloseComponent();
            }));
        });

        // 索引越界
        await cut.InvokeAsync(() => cut.Instance.TriggerStateChanged(2, false));
        Assert.False(closed);

        // 未注册 OnItemCloseAsync 回调
        await cut.InvokeAsync(() => cut.Instance.TriggerStateChanged(1, false));
        Assert.False(closed);

        // 触发 OnItemShownAsync 回调
        await cut.InvokeAsync(() => cut.Instance.TriggerStateChanged(0, true));
        Assert.True(shown);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.OnItemCloseAsync, item =>
            {
                closed = true;
                return Task.CompletedTask;
            });
        });
        await cut.InvokeAsync(() => cut.Instance.TriggerStateChanged(1, false));
        Assert.True(closed);
    }

    private static List<DropdownWidgetItem> GetItems()
    {
        var ret = new List<DropdownWidgetItem>();
        var widget = new DropdownWidgetItem();
        var parameters = new Dictionary<string, object?>()
        {
            ["Icon"] = "fa-regular fa-bell",
            ["Title"] = "Title",
            ["BadgeColor"] = Color.Success,
            ["HeaderColor"] = Color.Primary,
            ["BadgeNumber"] = "10",
            ["ShowArrow"] = true,
            ["HeaderTemplate"] = new RenderFragment(builder =>
            {
                builder.AddContent(0, "HeaderTemplate");
            }),
            ["BodyTemplate"] = new RenderFragment(builder =>
            {
                builder.AddContent(0, "BodyTemplate");
            }),
            ["FooterTemplate"] = new RenderFragment(builder =>
            {
                builder.AddContent(0, "FooterTemplate");
            }),

        };
        widget.SetParametersAsync(ParameterView.FromDictionary(parameters!));
        ret.Add(widget);
        return ret;
    }
}
