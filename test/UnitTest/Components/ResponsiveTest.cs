// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class ResponsiveTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Responsive_Ok()
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
        cut.InvokeAsync(() => resp.OnResize("Large"));
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
