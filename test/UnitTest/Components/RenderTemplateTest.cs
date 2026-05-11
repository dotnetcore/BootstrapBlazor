// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class RenderTemplateTest : TestBase
{
    [Fact]
    public void OnRenderAsync_Ok()
    {
        var firstRender = false;
        var cut = Context.Render<RenderTemplate>(pb =>
        {
            pb.Add(a => a.OnRenderAsync, first =>
            {
                firstRender = first;
                return Task.CompletedTask;
            });
        });

        Assert.True(firstRender);

        cut.Render();
        Assert.False(firstRender);
    }

    [Fact]
    public void OnBeforeRenderAsync_Ok()
    {
        var beforeRender = false;
        var content = "before";
        var cut = Context.Render<RenderTemplate>(pb =>
        {
            pb.Add(a => a.OnBeforeRenderAsync, () =>
            {
                beforeRender = true;
                content = "after";
                return Task.CompletedTask;
            });
            pb.Add(a => a.ChildContent, builder => builder.AddContent(0, content));
        });

        Assert.True(beforeRender);
        Assert.Equal("after", cut.Markup);
    }
}
