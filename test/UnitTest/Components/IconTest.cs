// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class IconTest : BootstrapBlazorTestBase
{
    [Fact]
    public void FontIcon_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorIcon>(pb =>
        {
            pb.Add(a => a.Name, "fa-solid fa-home");
        });
        cut.Contains("i class=\"fa-solid fa-home\"");
    }

    [Fact]
    public void SvgIcon_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorIcon>(pb =>
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
        var cut = Context.RenderComponent<BootstrapBlazorIcon>(pb =>
        {
            pb.Add(a => a.ChildContent, builder => builder.AddMarkupContent(0, "<img src=\"test.png\" />"));
        });
        cut.Contains("<img src=\"test.png\" />");
    }
}
