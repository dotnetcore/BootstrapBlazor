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
        Context.RenderComponent<BootstrapBlazorRoot>(pb =>
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
        Assert.Equal(BreakPoint.None, point);
    }

    [Fact]
    public void Service_Ok()
    {
        var service = new ResizeNotificationService();
        service.Subscribe(this, b => Task.CompletedTask);
        service.Subscribe(this, b => Task.CompletedTask);
    }
}
