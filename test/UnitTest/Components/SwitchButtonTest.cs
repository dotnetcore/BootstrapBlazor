// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Web;

namespace UnitTest.Components;

public class SwitchButtonTest : TestBase
{
    [Fact]
    public void Toggle_Ok()
    {
        var state = false;
        var clicked = false;
        var cut = Context.RenderComponent<SwitchButton>(pb =>
        {
            pb.Add(a => a.OnText, "Test_OnTest");
            pb.Add(a => a.OffText, "Test_OffTest");
            pb.Add(a => a.ToggleState, false);
            pb.Add(a => a.ToggleStateChanged, EventCallback.Factory.Create<bool>(this, v =>
            {
                state = v;
            }));
            pb.Add(a => a.OnClick, EventCallback.Factory.Create<MouseEventArgs>(this, e =>
            {
                clicked = true;
            }));
        });
        cut.Contains("Test_OffTest");

        // Click
        cut.InvokeAsync(() => cut.Find("a").Click());
        Assert.True(clicked);
    }
}
