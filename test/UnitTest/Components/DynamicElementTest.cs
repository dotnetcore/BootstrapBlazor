// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web;

namespace UnitTest.Components;

public class DynamicElementTest
{
    [Fact]
    public void Key_OK()
    {
        var context = new BunitContext();
        var cut = context.Render<DynamicElement>(pb =>
        {
            pb.Add(s => s.Key, Guid.NewGuid());
        });

        Assert.Equal("<div></div>", cut.Markup);
    }

    [Fact]
    public void TouchEvents_Ok()
    {
        var context = new BunitContext();
        TouchEventArgs? touchStartArgs = null;
        TouchEventArgs? touchEndArgs = null;
        var touchStartEventArgs = new TouchEventArgs
        {
            Touches = [new TouchPoint { ClientX = 10, ClientY = 20, ScreenX = 30, ScreenY = 40 }]
        };
        var touchEndEventArgs = new TouchEventArgs
        {
            ChangedTouches = [new TouchPoint { ClientX = 11, ClientY = 21, ScreenX = 31, ScreenY = 41 }]
        };

        var cut = context.Render<DynamicElement>(pb =>
        {
            pb.Add(a => a.TagName, "span");
            pb.Add(a => a.TriggerTouchStart, true);
            pb.Add(a => a.OnTouchStart, e =>
            {
                touchStartArgs = e;
                return Task.CompletedTask;
            });
            pb.Add(a => a.TriggerTouchEnd, true);
            pb.Add(a => a.OnTouchEnd, e =>
            {
                touchEndArgs = e;
                return Task.CompletedTask;
            });
            pb.AddChildContent("Touch");
        });

        var element = cut.Find("span");
        element.TriggerEvent("ontouchstart", touchStartEventArgs);
        element.TriggerEvent("ontouchend", touchEndEventArgs);

        Assert.Same(touchStartEventArgs, touchStartArgs);
        Assert.Same(touchEndEventArgs, touchEndArgs);
    }
}
