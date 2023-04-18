// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class ReponsiveTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Resposive_Ok()
    {
        BreakPoint? point = null;
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Responsive>(pb =>
            {
                pb.Add(a => a.OnBreakPointChanged, b =>
                {
                    point = b;
                    return Task.CompletedTask;
                });
            });
        });

        var resp = cut.FindComponent<ResizeNotification>().Instance;
        cut.InvokeAsync(() => resp.OnResize(BreakPoint.Large));
        Assert.Equal(BreakPoint.Large, point);
    }

    [Fact]
    public void Service_Ok()
    {
        var service = new ResizeNotificationService();
        service.Subscribe(this, b => Task.CompletedTask);
        service.Subscribe(this, b => Task.CompletedTask);
    }
}
