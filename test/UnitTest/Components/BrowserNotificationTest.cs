// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.JSInterop;

namespace UnitTest.Components;

public class BrowserNotificationTest : BootstrapBlazorTestBase
{
    [Fact]
    public void CheckPermission_Ok()
    {
        var cut = Context.RenderComponent<MockNotification>();
        cut.Instance.CheckPermission();
        var item = new NotificationItem()
        {
            Icon = "fa-solid fa-font-awesome",
            Message = "Test",
            OnClick = "",
            Silent = true,
            Sound = "test.wav",
            Title = "Title"
        };
        cut.Instance.Dispatch(item);
        Assert.Equal("fa-solid fa-font-awesome", item.Icon);
        Assert.Equal("Test", item.Message);
        Assert.Equal("", item.OnClick);
        Assert.True(item.Silent);
        Assert.Equal("test.wav", item.Sound);
        Assert.Equal("Title", item.Title);
    }

    private class MockNotification : ComponentBase
    {
        [Inject]
        [NotNull]
        private IJSRuntime? JSRuntime { get; set; }

        private JSInterop<MockNotification>? Interop { get; set; }

        public void CheckPermission()
        {
            Interop ??= new JSInterop<MockNotification>(JSRuntime);
            _ = BrowserNotification.CheckPermission(Interop, this);
        }

        public void Dispatch(NotificationItem item)
        {
            Interop ??= new JSInterop<MockNotification>(JSRuntime);
            _ = BrowserNotification.Dispatch<MockNotification>(Interop, this, item, "");
        }
    }
}
