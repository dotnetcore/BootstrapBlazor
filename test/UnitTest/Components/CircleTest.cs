// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Components;

public class CircleTest : TestBase
{
    [Fact]
    public void Value_Ok()
    {
        var cut = Context.RenderComponent<Circle>(builder => builder.Add(a => a.Value, 100));

        Assert.Contains("100%", cut.Markup);
    }

    [Fact]
    public void Width_Ok()
    {
        var cut = Context.RenderComponent<Circle>(builder => builder.Add(a => a.Width, 100));

        Assert.Contains("width: 100px", cut.Markup);
    }

    [Fact]
    public void StrokeWidth_Ok()
    {
        var cut = Context.RenderComponent<Circle>(builder => builder.Add(a => a.StrokeWidth, 5));

        Assert.Contains("stroke-width=\"5\"", cut.Markup);

        // 增加代码覆盖率
        //Width / 2 < StrokeWidth
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Width, 6);
            pb.Add(a => a.StrokeWidth, 6);
        });
        Assert.Equal(2, cut.Instance.StrokeWidth);
    }

    [Fact]
    public void Color_Ok()
    {
        var cut = Context.RenderComponent<Circle>(builder => builder.Add(a => a.Color, Color.Success));

        var element = cut.Find(".circle-success");

        Assert.NotNull(element);
    }

    [Fact]
    public void ShowProgress_Ok()
    {
        var cut = Context.RenderComponent<Circle>(builder => builder.Add(a => a.ShowProgress, false));

        var element = cut.Find(".d-none");

        Assert.NotNull(element);
    }

    [Fact]
    public void Value_ChildContent()
    {
        var cut = Context.RenderComponent<Circle>(builder => builder.Add(a => a.ChildContent, s =>
        {
            s.OpenElement(1, "div");
            s.AddContent(2, "I am cricle");
            s.CloseElement();
        }));

        Assert.Contains("I am cricle", cut.Markup);
    }
}
