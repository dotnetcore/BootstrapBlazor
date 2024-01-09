// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Web;

namespace UnitTest.Components;

public class SwitchButtonTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Text_Ok()
    {
        var cut = Context.RenderComponent<SwitchButton>();
        cut.Contains("关");
    }

    [Fact]
    public void ToggleState_Ok()
    {
        var cut = Context.RenderComponent<SwitchButton>(pb =>
        {
            pb.Add(a => a.ToggleState, true);
        });
        cut.Contains("开");
    }

    [Fact]
    public async Task ToggleStateChanged_Ok()
    {
        var state = false;
        var cut = Context.RenderComponent<SwitchButton>(pb =>
        {
            pb.Add(a => a.OnText, "Test_OnTest");
            pb.Add(a => a.OffText, "Test_OffTest");
            pb.Add(a => a.ToggleStateChanged, EventCallback.Factory.Create<bool>(this, v =>
            {
                state = v;
            }));
        });
        cut.Contains("Test_OffTest");

        // Click
        await cut.InvokeAsync(() => cut.Find("a").Click());
        Assert.True(state);
    }

    [Fact]
    public async Task Click_Ok()
    {
        var clicked = false;
        var cut = Context.RenderComponent<SwitchButton>(pb =>
        {
            pb.Add(a => a.OnText, "Test_OnTest");
            pb.Add(a => a.OffText, "Test_OffTest");
            pb.Add(a => a.OnClick, EventCallback.Factory.Create<MouseEventArgs>(this, e =>
            {
                clicked = true;
            }));
        });
        cut.Contains("Test_OffTest");

        // Click
        await cut.InvokeAsync(() => cut.Find("a").Click());
        Assert.True(clicked);
    }
}
