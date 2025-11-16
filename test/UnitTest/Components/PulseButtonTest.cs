// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class PulseButtonTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Icon_Ok()
    {
        var cut = Context.Render<PulseButton>(pb =>
        {
            pb.Add(a => a.Icon, "fa-solid fa-user");
        });
        cut.Contains("fa-solid fa-user");
    }

    [Fact]
    public void ImageUrl_Ok()
    {
        var cut = Context.Render<PulseButton>(pb =>
        {
            pb.Add(a => a.ImageUrl, "../images/logo.png");
        });
        cut.Contains("../images/logo.png");
    }

    [Fact]
    public void Text_Ok()
    {
        var cut = Context.Render<PulseButton>(pb =>
        {
            pb.Add(a => a.Text, "button-text");
        });
        cut.Contains("button-text");
    }

    [Fact]
    public void PulseColor_Ok()
    {
        var cut = Context.Render<PulseButton>(pb =>
        {
            pb.Add(a => a.Text, "button-text");
            pb.Add(a => a.PulseColor, Color.Warning);
        });
        cut.Contains("border-warning");
    }
}
