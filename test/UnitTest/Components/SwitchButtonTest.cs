// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web;

namespace UnitTest.Components;

public class SwitchButtonTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Text_Ok()
    {
        var cut = Context.Render<SwitchButton>();
        cut.Contains("关");
    }

    [Fact]
    public void ToggleState_Ok()
    {
        var cut = Context.Render<SwitchButton>(pb =>
        {
            pb.Add(a => a.ToggleState, true);
        });
        cut.Contains("开");
    }

    [Fact]
    public async Task ToggleStateChanged_Ok()
    {
        var state = false;
        var cut = Context.Render<SwitchButton>(pb =>
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
        var cut = Context.Render<SwitchButton>(pb =>
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
