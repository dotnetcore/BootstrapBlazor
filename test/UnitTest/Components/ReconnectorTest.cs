// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Web;

namespace UnitTest.Components;

public class ReconnectorTest : BootstrapBlazorTestBase
{
    [Fact]
    public void ReconnectorOutlet_Ok()
    {
        var cut = Context.RenderComponent<ReconnectorOutlet>(pb =>
        {
            pb.Add(a => a.ReconnectInterval, 5000);
            pb.Add(a => a.AutoReconnect, true);
        });
        cut.Contains("components-reconnect-modal");
    }

    [Fact]
    public void ReconnectorContent_Ok()
    {
        var cut = Context.RenderComponent<ReconnectorContent>();
        cut.Contains("components-reconnect-modal");
    }

    [Fact]
    public void Reconnector_Ok()
    {
        var connector = Context.RenderComponent<Reconnector>();
        Assert.Equal("", connector.Markup);

        var cut = Context.RenderComponent<ReconnectorOutlet>();
        connector.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ReconnectingTemplate, builder => builder.AddContent(0, "Test-ReconnectingTemplate"));
            pb.Add(a => a.ReconnectFailedTemplate, builder => builder.AddContent(0, "Test-ReconnectFailedTemplate"));
            pb.Add(a => a.ReconnectRejectedTemplate, builder => builder.AddContent(0, "Test-ReconnectRejectedTemplate"));
        });
        cut.Contains("Test-ReconnectingTemplate");
        cut.Contains("Test-ReconnectFailedTemplate");
        cut.Contains("Test-ReconnectRejectedTemplate");
    }
}
