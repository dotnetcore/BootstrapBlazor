// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web;

namespace UnitTest.Components;

public class ReconnectorTest : BootstrapBlazorTestBase
{
    [Fact]
    public void ReconnectorOutlet_Ok()
    {
        var cut = Context.Render<ReconnectorOutlet>(pb =>
        {
            pb.Add(a => a.ReconnectInterval, 5000);
            pb.Add(a => a.AutoReconnect, true);
        });
        cut.Contains("components-reconnect-modal");
    }

    [Fact]
    public void ReconnectorContent_Ok()
    {
        var cut = Context.Render<ReconnectorContent>();
        cut.Contains("components-reconnect-modal");
    }

    [Fact]
    public void Reconnector_Ok()
    {
        var connector = Context.Render<Reconnector>();
        Assert.Equal("", connector.Markup);

        var cut = Context.Render<ReconnectorOutlet>();
        connector.Render(pb =>
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
