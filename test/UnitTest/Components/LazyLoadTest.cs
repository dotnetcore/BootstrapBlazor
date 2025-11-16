// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class LazyLoadTest : TestBase
{
    [Fact]
    public void LazyLoading_Ok()
    {
        var cut = Context.Render<LazyLoad>(pb =>
        {
            pb.Add(a => a.ChildContent, "<div>content</div>");
        });
        Assert.Equal("<div>content</div>", cut.Markup);
    }

    [Fact]
    public void OnLoadConditionCheckAsync_Ok()
    {
        var cut = Context.Render<LazyLoad>(pb =>
        {
            pb.Add(a => a.OnLoadConditionCheckAsync, () => Task.FromResult(false));
            pb.Add(a => a.ChildContent, "<div>content</div>");
        });
        Assert.Equal("", cut.Markup);
    }

    [Fact]
    public void OnFirstLoadCallbackAsync_Ok()
    {
        var load = false;
        var cut = Context.Render<LazyLoad>(pb =>
        {
            pb.Add(a => a.OnLoadConditionCheckAsync, () => Task.FromResult(true));
            pb.Add(a => a.OnFirstLoadCallbackAsync, () =>
            {
                load = true;
                return Task.CompletedTask;
            });
            pb.Add(a => a.ChildContent, "<div>content</div>");
        });
        Assert.True(load);
    }
}
