// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
                pb.Add(a => a.OnClick, async () =>
                {
                    await service.Show(new MessageOption()
                    {
                        Color = Color.Danger,
                        Content = "Test Content",
                        Delay = 4000,
                        ForceDelay = false,
                        Icon = "fa fa-fa",
                        IsAutoHide = true,
                        ShowBar = true,
                        ShowDismiss = true,
                        OnDismiss = () =>
                        {
                            dismiss = true;
                            return Task.CompletedTask;
                        }
                    });
                });
            });
        });
        Assert.NotNull(cut.Instance.MessageContainer);

        var btn = cut.Find("button");
        await cut.InvokeAsync(() => btn.Click());

        var btnClose = cut.Find(".btn-close");
        await cut.InvokeAsync(() => btnClose.Click());
        Assert.True(dismiss);

        var message = cut.FindComponent<Message>();
        await message.InvokeAsync(() => message.Instance.Clear());
    }

    [Fact]
    public async Task SetPlacement_Ok()
    {
        var cut = Context.RenderComponent<Message>(pb =>
        {
            pb.Add(a => a.Placement, Placement.Left);
        });

        await cut.InvokeAsync(() => cut.Instance.SetPlacement(Placement.Top));
        Assert.Equal(Placement.Top, cut.Instance.Placement);
    }
}
