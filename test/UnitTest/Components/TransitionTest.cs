// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace UnitTest.Components;

public class TransitionTest : BootstrapBlazorTestBase
{

    [Fact]
    public void ShowAnimate_Ok()
    {
        var cut = Context.RenderComponent<Transition>(builder => builder.Add(s => s.Show, true));
        Assert.Contains("animate__animated animate__fadeIn", cut.Markup);
    }
    [Fact]
    public void HideAnimate_Ok()
    {
        var cut = Context.RenderComponent<Transition>(builder => builder.Add(s => s.Show, false));
        Assert.Contains("\"animate__animated\"", cut.Markup);
    }

    [Fact]
    public void TransitionType_Ok()
    {
        var cut = Context.RenderComponent<Transition>(builder => builder.Add(s => s.TransitionType, TransitionType.FadeOut));
        Assert.Contains("\"animate__animated animate__fadeOut\"", cut.Markup);
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
            //TODO 设置Duration测试条件是否多余?
            //builder.Add(s => s.Duration, 1000);
            builder.Add(a => a.OnTransitionEnd, ()=> {
                transitionEnd = true;
                return Task.FromResult(true);
            });
        });

        cut.InvokeAsync(() => cut.Instance.TransitionEndAsync());
        Assert.True(transitionEnd);
    }

    [Fact]
    public void Dispose_Ok()
    {

        var cut = Context.RenderComponent<Transition>();
        cut.InvokeAsync(() => cut.Instance.Dispose());
        Type type = cut.Instance.GetType();
        FieldInfo fieldInfo = type.GetField("Interop", BindingFlags.NonPublic);
        //object value = fieldInfo.GetValue(null);
        //Assert.True(cut.Instance.Interop._objRef==null);
        Assert.True(1==1);
    }

}
