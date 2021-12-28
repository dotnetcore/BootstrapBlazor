// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Bunit;
using System.Web;
using UnitTest.Core;
using Xunit;

namespace UnitTest.Components;

public class GoTopTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Target_Ok()
    {
        var cut = Context.RenderComponent<GoTop>(pb =>
        {
            pb.Add(c => c.Target, "#top");
        });
        Assert.Equal("#top", cut.Instance.Target);
    }

    [Fact]
    public void TooltipText_Ok()
    {
        var cut = Context.RenderComponent<GoTop>();
        Assert.Contains("返回顶端", HttpUtility.HtmlDecode(cut.Markup));

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(c => c.TooltipText, "TooltipText");
        });
        Assert.Contains("TooltipText", cut.Markup);
    }
}
