// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class IconTest : BootstrapBlazorTestBase
{
    [Fact]
    public void FontIcon_Ok()
    {
        var cut = Context.Render<BootstrapBlazorIcon>(pb =>
        {
            pb.Add(a => a.Name, "fa-solid fa-home");
        });
        cut.Contains("i class=\"fa-solid fa-home\"");
    }

    [Fact]
    public void SvgIcon_Ok()
    {
        var cut = Context.Render<BootstrapBlazorIcon>(pb =>
        {
            pb.Add(a => a.Name, "home");
            pb.Add(a => a.IsSvgSprites, true);
            pb.Add(a => a.Url, "./_content/svg.svg");
        });
        cut.Contains("<use href=\"./_content/svg.svg#home\"></use>");
    }

    [Fact]
    public void ImageIcon_Ok()
    {
        var cut = Context.Render<BootstrapBlazorIcon>(pb =>
        {
            pb.Add(a => a.ChildContent, builder => builder.AddMarkupContent(0, "<img src=\"test.png\" />"));
        });
        cut.Contains("<img src=\"test.png\" />");
    }
}
