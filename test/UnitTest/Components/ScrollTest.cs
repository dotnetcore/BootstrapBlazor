// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Components;

public class ScrollTest : TestBase
{
    [Fact]
    public void Height_Ok()
    {
        var cut = Context.RenderComponent<Scroll>(builder => builder.Add(a => a.Height, "500"));

        Assert.Contains("height: 500", cut.Markup);
    }

    [Fact]
    public void ChildContent_Ok()
    {
        var cut = Context.RenderComponent<Scroll>(builder => builder.Add(a => a.ChildContent, r =>
        {
            r.OpenElement(0, "div");
            r.AddContent(1, "I am scroll");
            r.CloseComponent();
        }));

        Assert.Contains("I am scroll", cut.Markup);
    }
}
