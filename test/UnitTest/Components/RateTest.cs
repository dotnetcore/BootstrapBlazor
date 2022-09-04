// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class RateTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Value_Ok()
    {
        var cut = Context.RenderComponent<Rate>(builder => builder.Add(s => s.Value, 5));
        cut.Contains("rate");
    }

    [Fact]
    public void IsDisable_Ok()
    {
        var cut = Context.RenderComponent<Rate>(builder =>
        {
            builder.Add(s => s.IsDisable, true);
            builder.Add(s => s.Value, 5);
        });

        var ele = cut.Find(".disabled");
        Assert.NotNull(ele);
    }

    [Fact]
    public async Task ValueChanged_Ok()
    {
        var ret = false;
        var cut = Context.RenderComponent<Rate>(builder =>
        {
            builder.Add(s => s.Value, 3);
            builder.Add(s => s.ValueChanged, EventCallback.Factory.Create<int>(this, v =>
            {
                ret = true;
            }));
        });

        var span = cut.Find("span");
        await cut.InvokeAsync(() => span.Click());
        Assert.True(ret);
    }

    [Fact]
    public async Task OnValueChanged_Ok()
    {
        var ret = false;
        var cut = Context.RenderComponent<Rate>(builder =>
        {
            builder.Add(s => s.OnValueChanged, new Func<int, Task>(v =>
            {
                ret = true;
                return Task.CompletedTask;
            }));
            builder.Add(s => s.Value, 5);
        });

        var span = cut.Find("span");
        await cut.InvokeAsync(() => span.Click());
        Assert.True(ret);
    }

    [Fact]
    public void ItemTemplate_Ok()
    {
        var cut = Context.RenderComponent<Rate>(pb =>
        {
            pb.Add(s => s.ItemTemplate, index => builder =>
            {
                builder.AddContent(index, $"<div>item-template-{index}</div>");
            });
        });
        cut.Contains("item-template-1");
        cut.Contains("item-template-5");
    }

    [Fact]
    public void Max_Ok()
    {
        var cut = Context.RenderComponent<Rate>(pb =>
        {
            pb.Add(s => s.Max, 0);
        });
        Assert.Equal(5, cut.Instance.Max);
    }
}
