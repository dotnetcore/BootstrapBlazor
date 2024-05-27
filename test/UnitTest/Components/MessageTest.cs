// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Components;

public class MessageTest : MessageTestBase
{
    [Fact]
    public async Task Message_Ok()
    {
        var dismiss = false;
        var service = Context.Services.GetRequiredService<MessageService>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Button>(pb =>
            {
                pb.Add(a => a.OnClick, EventCallback.Factory.Create<MouseEventArgs>(this, async e =>
                {
                    await service.Show(new MessageOption()
                    {
                        Color = Color.Danger,
                        Content = "Test Content",
                        Delay = 4000,
                        ForceDelay = false,
                        Icon = "fa-solid fa-font-awesome",
                        IsAutoHide = true,
                        ShowBar = true,
                        ShowBorder = true,
                        ShowShadow = true,
                        ShowDismiss = true,
                        ShowMode = MessageShowMode.Multiple,
                        OnDismiss = () =>
                        {
                            dismiss = true;
                            return Task.CompletedTask;
                        }
                    });
                }));
            });
        });
        Assert.NotNull(cut.Instance.MessageContainer);

        await cut.InvokeAsync(() =>
        {
            var btn = cut.Find("button");
            btn.Click();
        });
        Assert.Contains("data-bb-autohide", cut.Markup);
        Assert.Contains("data-bb-delay=\"4000\"", cut.Markup);

        var alert = cut.Find(".alert");
        Assert.NotNull(alert);
        Assert.NotNull(alert.Id);

        var message = cut.FindComponent<Message>();
        await message.Instance.Dismiss(alert.Id);
        Assert.True(dismiss);

        await cut.InvokeAsync(() => message.Instance.Clear());
    }

    [Fact]
    public void SetPlacement_Ok()
    {
        var cut = Context.RenderComponent<Message>(pb =>
        {
            pb.Add(a => a.Placement, Placement.Bottom);
        });

        cut.InvokeAsync(() => cut.Instance.SetPlacement(Placement.Top));
        Assert.Equal(Placement.Top, cut.Instance.Placement);
    }

    [Fact]
    public async Task Placement_Ok()
    {
        var service = Context.Services.GetRequiredService<MessageService>();
        var cut = Context.RenderComponent<Message>(pb =>
        {
            pb.Add(a => a.Placement, Placement.Bottom);
        });
        await cut.InvokeAsync(() => service.Show(new MessageOption()
        {
            Content = "Test Content",
            IsAutoHide = false,
            ShowDismiss = true,
            Icon = "fa-solid fa-font-awesome"
        }, cut.Instance));
        Assert.DoesNotContain("data-bb-autohide", cut.Markup);

        var alert = cut.Find(".alert");
        Assert.NotNull(alert);
        Assert.NotNull(alert.Id);

        await cut.Instance.Dismiss(alert.Id);
        await cut.Instance.Dismiss("test_id");
        await cut.InvokeAsync(() => cut.Instance.Clear());

        await cut.InvokeAsync(() => service.Show(new MessageOption()
        {
            ChildContent = builder => builder.AddContent(0, new MarkupString("<div class=\"custom-message-template\">Custom Message</div>"))
        }, cut.Instance));
        Assert.Contains("<div class=\"custom-message-template\">Custom Message</div>", cut.Markup);
    }

    [Fact]
    public async Task ChildContent_Ok()
    {
        var service = Context.Services.GetRequiredService<MessageService>();
        var cut = Context.RenderComponent<Message>();
        await cut.InvokeAsync(() => service.Show(new MessageOption()
        {
            ChildContent = builder => builder.AddContent(0, new MarkupString("<div class=\"custom-message-template\">Custom Message</div>"))
        }, cut.Instance));
        Assert.Contains("<div class=\"custom-message-template\">Custom Message</div>", cut.Markup);
    }

    [Fact]
    public async Task ShowMode_Ok()
    {
        var service = Context.Services.GetRequiredService<MessageService>();
        var cut = Context.RenderComponent<Message>();
        await cut.InvokeAsync(() => service.Show(new MessageOption()
        {
            Content = "Test Content",
            IsAutoHide = false,
            ShowDismiss = true,
            Icon = "fa-solid fa-font-awesome",
            ShowMode = MessageShowMode.Single
        }, cut.Instance));
    }
}
