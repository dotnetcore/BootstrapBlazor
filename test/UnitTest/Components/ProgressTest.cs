// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class ProgressTest : TestBase
{
    [Fact]
    public void ShowValue_Ok()
    {
        var cut = Context.RenderComponent<Progress>(pb =>
        {
            pb.Add(a => a.IsShowValue, true);
            pb.Add(a => a.Value, 50);
        });

        Assert.Contains("50%", cut.Markup);
    }

    [Fact]
    public void IsStriped_Ok()
    {
        var cut = Context.RenderComponent<Progress>(pb =>
        {
            pb.Add(a => a.IsStriped, true);
            pb.Add(a => a.Value, 50);
        });

        Assert.Contains("progress-bar-striped", cut.Markup);
    }

    [Fact]
    public void IsAnimated_Ok()
    {
        var cut = Context.RenderComponent<Progress>(pb =>
        {
            pb.Add(a => a.IsAnimated, true);
            pb.Add(a => a.Value, 50);
        });

        Assert.Contains("progress-bar-animated", cut.Markup);
    }
}
