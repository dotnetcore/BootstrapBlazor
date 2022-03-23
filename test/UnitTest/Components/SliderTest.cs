// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class SliderTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Value_OK()
    {
        var cut = Context.RenderComponent<Slider>(builder => builder.Add(s => s.Value, 10));
    }

    [Fact]
    public void ValueChanged_OK()
    {
        var ret = false;
        var cut = Context.RenderComponent<Slider>(builder =>
        {
            builder.Add(s => s.Value, 10);
            builder.Add(s => s.ValueChanged, v =>
            {
                ret = true;
            });
        });
        cut.Instance.SetValue(20);
        Assert.True(ret);
    }

    [Fact]
    public void IsDisabled_OK()
    {
        var cut = Context.RenderComponent<Slider>(builder =>
        {
            builder.Add(s => s.Value, 10);
            builder.Add(s => s.IsDisabled, true);
        });
        cut.Contains("slider-runway disabled");
    }
}
