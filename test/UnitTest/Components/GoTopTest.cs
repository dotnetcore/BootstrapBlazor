// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

using System.Web;

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
