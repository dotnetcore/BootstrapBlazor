// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Bunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest.Core;
using Xunit;

namespace UnitTest.Components;

public class EmptyTest : BootstrapBlazorTestBase
{

    [Fact]
    public void Image_Ok()
    {
        var path = "/src/image/argo.png";
        var cut = Context.RenderComponent<Empty>(builder => builder.Add(p => p.Image, path));

        Assert.Contains(path, cut.Markup);
    }

    [Fact]
    public void Text_Ok()
    {
        var text = "I am an Empty";
        var cut = Context.RenderComponent<Empty>(builder => builder.Add(p => p.Text, text));

        Assert.Contains(text, cut.Markup);
    }

    [Fact]
    public void Width_Ok()
    {
        var cut = Context.RenderComponent<Empty>(builder =>
        {
            builder.Add(p => p.Width, "200");
            builder.Add(i => i.Image, "/src/image/argo.png");
        });

        Assert.Contains("width=\"200\"", cut.Markup);
    }

    [Fact]
    public void Height_Ok()
    {
        var cut = Context.RenderComponent<Empty>(builder =>
        {
            builder.Add(p => p.Height, "200");
            builder.Add(i => i.Image, "/src/image/argo.png");
        });

        Assert.Contains("height=\"200\"", cut.Markup);
    }

    [Fact]
    public void ChildContent_Ok()
    {
        var cut = Context.RenderComponent<Empty>(builder => builder.Add(p => p.ChildContent, r =>
        {
            r.OpenComponent<Button>(1);
            r.CloseComponent();
        }));

        Assert.NotNull(cut.FindComponent<Button>());
    }

    [Fact]
    public void Template_Ok()
    {
        var cut = Context.RenderComponent<Empty>(builder => builder.Add(p => p.Template, r =>
        {
            r.OpenComponent<Button>(1);
            r.CloseComponent();
        }));

        Assert.NotNull(cut.FindComponent<Button>());
    }
}
