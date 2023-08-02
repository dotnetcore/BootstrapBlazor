// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class LazyLoadTest : TestBase
{
    [Fact]
    public void LazyLoading_Ok()
    {
        var cut = Context.RenderComponent<LazyLoad>(pb =>
        {
            pb.Add(a => a.ChildContent, "<div>content</div>");
        });
        Assert.Equal("<div>content</div>", cut.Markup);
    }

    [Fact]
    public void OnLoadConditionCheckAsync_Ok()
    {
        var cut = Context.RenderComponent<LazyLoad>(pb =>
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
        var cut = Context.RenderComponent<LazyLoad>(pb =>
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
