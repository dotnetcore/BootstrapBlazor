// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Components;

public class MessageTest : MessageTestBase
{
    [Fact]
    public void Message_Ok()
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

        cut.InvokeAsync(() =>
        {
            var btn = cut.Find("button");
            btn.Click();
        });
        Assert.Contains("data-bb-autohide", cut.Markup);
        Assert.Contains("data-bb-delay=\"4000\"", cut.Markup);

        cut.InvokeAsync(() =>
        {
            var btnClose = cut.Find(".btn-close");
            btnClose.Click();
        });
        Assert.True(dismiss);

        cut.InvokeAsync(() =>
        {
            var message = cut.FindComponent<Message>();
            message.Instance.Clear();
        });
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
    public void Placement_Ok()
    {
        var dismiss = false;
        var service = Context.Services.GetRequiredService<MessageService>();
        var cut = Context.RenderComponent<Message>(pb =>
        {
            pb.Add(a => a.Placement, Placement.Bottom);
        });
        cut.InvokeAsync(() => service.Show(new MessageOption()
        {
            Content = "Test Content",
            IsAutoHide = false,
            ShowDismiss = true,
            Icon = "fa-solid fa-font-awesome",
            OnDismiss = () =>
            {
                dismiss = true;
                return Task.CompletedTask;
            }
        }, cut.Instance));
        Assert.DoesNotContain("data-bb-autohide", cut.Markup);

        cut.InvokeAsync(() =>
        {
            var btnClose = cut.Find(".btn-close");
            btnClose.Click();
        });
        Assert.True(dismiss);
    }
}
