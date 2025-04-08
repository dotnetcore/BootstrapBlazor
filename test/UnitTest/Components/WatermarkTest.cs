// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class WatermarkTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Watermark_Ok()
    {
        var cut = Context.RenderComponent<Watermark>(pb =>
        {
            pb.Add(a => a.Text, null);
            pb.Add(a => a.FontSize, 16);
            pb.Add(a => a.Gap, 40);
            pb.Add(a => a.Color, "#0000000D");
            pb.Add(a => a.Rotate, 40);
            pb.Add(a => a.ZIndex, 10);
            pb.Add(a => a.ChildContent, b => b.AddMarkupContent(0, "<span>Test</span>"));
        });
        cut.MarkupMatches("<div id:ignore class=\"bb-watermark\"><span>Test</span></div>");

        var ex = Assert.ThrowsAny<InvalidOperationException>(() => cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsPage, true);
        }));
        Assert.Equal($"IsPage is true, ChildContent cannot be set.", ex.Message);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsPage, true);
            pb.Add(a => a.ChildContent, (RenderFragment?)null);
        });
        cut.MarkupMatches("<div id:ignore class=\"bb-watermark is-page\"></div>");
    }
}
