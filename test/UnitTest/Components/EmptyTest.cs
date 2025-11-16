// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class EmptyTest : BootstrapBlazorTestBase
{

    [Fact]
    public void Image_Ok()
    {
        var path = "/src/image/argo.png";
        var cut = Context.Render<Empty>(builder => builder.Add(p => p.Image, path));

        Assert.Contains(path, cut.Markup);
    }

    [Fact]
    public void Text_Ok()
    {
        var text = "I am an Empty";
        var cut = Context.Render<Empty>(builder => builder.Add(p => p.Text, text));

        Assert.Contains(text, cut.Markup);
    }

    [Fact]
    public void ChildContent_Ok()
    {
        var cut = Context.Render<Empty>(builder => builder.Add(p => p.ChildContent, r =>
        {
            r.OpenComponent<Button>(1);
            r.CloseComponent();
        }));

        Assert.NotNull(cut.FindComponent<Button>());
    }

    [Fact]
    public void Template_Ok()
    {
        var cut = Context.Render<Empty>(builder => builder.Add(p => p.Template, r =>
        {
            r.OpenComponent<Button>(1);
            r.CloseComponent();
        }));

        Assert.NotNull(cut.FindComponent<Button>());
    }
}
