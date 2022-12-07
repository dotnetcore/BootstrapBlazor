// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class PulseButtonTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Icon_Ok()
    {
        var cut = Context.RenderComponent<PulseButton>(pb =>
        {
            pb.Add(a => a.Icon, "fa-solid fa-user");
        });
        cut.Contains("fa-solid fa-user");
    }

    [Fact]
    public void ImageUrl_Ok()
    {
        var cut = Context.RenderComponent<PulseButton>(pb =>
        {
            pb.Add(a => a.ImageUrl, "../images/logo.png");
        });
        cut.Contains("../images/logo.png");
    }

    [Fact]
    public void Text_Ok()
    {
        var cut = Context.RenderComponent<PulseButton>(pb =>
        {
            pb.Add(a => a.Text, "button-text");
        });
        cut.Contains("button-text");
    }

    [Fact]
    public void PulseColor_Ok()
    {
        var cut = Context.RenderComponent<PulseButton>(pb =>
        {
            pb.Add(a => a.Text, "button-text");
            pb.Add(a => a.PulseColor, Color.Warning);
        });
        cut.Contains("border-warning");
    }
}
