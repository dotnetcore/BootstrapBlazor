// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

/// <summary>
/// 交叉检测组件单元测试
/// </summary>
public class IntersectionObserverTest : BootstrapBlazorTestBase
{
    [Fact]
    public void IntersectionObserver_Ok()
    {
        var cut = Context.RenderComponent<IntersectionObserver>(pb =>
        {
            pb.Add(a => a.UseElementViewport, false);
            pb.Add(a => a.RootMargin, "10px 20px 30px 40px");
            pb.Add(a => a.Threshold, "0.5");
            pb.Add(a => a.AutoUnobserveWhenIntersection, false);
            pb.Add(a => a.AutoUnobserveWhenNotIntersection, false);
            pb.Add(a => a.ChildContent, builder =>
            {
                builder.OpenComponent<IntersectionObserverItem>(0);
                builder.AddAttribute(1, "ChildContent", new RenderFragment(builder => builder.AddContent(0, "observer-item")));
                builder.CloseComponent();
            });
        });

        cut.MarkupMatches("<div id:ignore class=\"bb-intersection-observer\"><div class=\"bb-intersection-observer-item\">observer-item</div></div>");
    }

    [Fact]
    public async Task OnIntersecting_Ok()
    {
        int count = 0;
        var cut = Context.RenderComponent<IntersectionObserver>(pb =>
        {
            pb.Add(a => a.ChildContent, builder =>
            {
                builder.OpenComponent<IntersectionObserverItem>(0);
                builder.AddAttribute(1, "ChildContent", new RenderFragment(builder => builder.AddContent(0, "observer-item")));
                builder.CloseComponent();
            });
            pb.Add(a => a.OnIntersecting, entry =>
            {
                if (entry.IsIntersecting && entry.Time == 100 && entry.IntersectionRatio == 0.5f)
                {
                    count = entry.Index;
                }
                return Task.CompletedTask;
            });
        });

        await cut.InvokeAsync(() => cut.Instance.TriggerIntersecting(new IntersectionObserverEntry()
        {
            IsIntersecting = true,
            Index = 10,
            Time = 100.00,
            IntersectionRatio = 0.5f
        }));
        Assert.Equal(10, count);
    }

    [Fact]
    public async Task LoadMore_Ok()
    {
        var loading = false;
        var cut = Context.RenderComponent<LoadMore>(pb =>
        {
            pb.Add(a => a.Threshold, "1");
            pb.Add(a => a.CanLoading, true);
            pb.Add(a => a.OnLoadMoreAsync, () =>
            {
                loading = true;
                return Task.CompletedTask;
            });
        });
        cut.Contains("<div class=\"bb-intersection-observer-item\"><div class=\"bb-intersection-loading\"><div class=\"spinner spinner-border\" role=\"status\"><span class=\"visually-hidden\">Loading...</span></div></div></div>");

        // trigger intersecting
        var observerItem = cut.FindComponent<IntersectionObserver>();
        await cut.InvokeAsync(() => observerItem.Instance.TriggerIntersecting(new IntersectionObserverEntry()
        {
            IsIntersecting = true,
            Index = 10,
            Time = 100.00,
            IntersectionRatio = 0.5f
        }));
        Assert.True(loading);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.LoadingTemplate, new RenderFragment(builder => builder.AddContent(0, "loading template")));
        });
        cut.Contains("loading template");

        loading = false;
        cut.SetParametersAndRender(pb =>
        {
           pb.Add(a => a.CanLoading, false);
        });
        observerItem = cut.FindComponent<IntersectionObserver>();
        await cut.InvokeAsync(() => observerItem.Instance.TriggerIntersecting(new IntersectionObserverEntry()
        {
            IsIntersecting = true,
            Index = 10,
            Time = 100.00,
            IntersectionRatio = 0.5f
        }));
        Assert.False(loading);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.NoMoreTemplate, new RenderFragment(builder => builder.AddContent(0, "没有更多数据模板")));
        });
        cut.Contains("没有更多数据模板");
    }
}
