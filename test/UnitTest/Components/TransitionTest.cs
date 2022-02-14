// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor;

namespace UnitTest.Components;

public class TransitionTest : TestBase
{
    [Fact]
    public void ShowAnimate_Ok()
    {
        var cut = Context.RenderComponent<Transition>();
        Assert.Contains("animate__animated", cut.Markup);
        Assert.Contains("animate__fadeIn", cut.Markup);

        cut.SetParametersAndRender(builder => builder.Add(s => s.Show, false));
        Assert.DoesNotContain("animate__fadeIn", cut.Markup);
    }

    [Fact]
    public void TransitionType_Ok()
    {
        var cut = Context.RenderComponent<Transition>(builder => builder.Add(s => s.TransitionType, TransitionType.FadeOut));
        Assert.Contains("animate__animated animate__fadeOut", cut.Markup);
    }

    [Fact]
    public void Duration_Ok()
    {
        var cut = Context.RenderComponent<Transition>(builder => builder.Add(s => s.Duration, 3000));
        Assert.Contains("--animate-duration: 3s", cut.Markup);
    }

    [Fact]
    public void ChildContent_Ok()
    {
        var cut = Context.RenderComponent<Transition>(builder => builder.Add(a => a.ChildContent, CreateComponent()));
        Assert.Contains("test", cut.Markup);

        static RenderFragment CreateComponent() => builder =>
        {
            builder.OpenElement(1, "div");
            builder.AddContent(2, "test");
            builder.CloseComponent();
        };
    }

    [Fact]
    public void OnTransitionEnd_Ok()
    {
        var transitionEnd = false;
        var cut = Context.RenderComponent<Transition>(builder =>
        {
            builder.Add(a => a.OnTransitionEnd, () =>
            {
                transitionEnd = true;
                return Task.FromResult(true);
            });
        });

        cut.InvokeAsync(() => cut.Instance.TransitionEndAsync());
        Assert.True(transitionEnd);
    }
}
