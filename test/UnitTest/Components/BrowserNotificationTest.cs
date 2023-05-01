// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace UnitTest.Components;

public class BrowserNotificationTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task CheckPermission_Ok()
    {
        var service = Context.Services.GetRequiredService<NotificationService>();
        Context.JSInterop.Setup<bool>("check", v => true).SetResult(true);
        var result = await service.CheckPermission();
        Assert.True(result);

        var item = new NotificationItem()
        {
            Icon = "fa-solid fa-font-awesome",
            Message = "Test",
            Silent = true,
            Sound = "test.wav",
            Title = "Title"
        };
        ResetModule(service);
        await service.Dispatch(item);
        Assert.Equal("fa-solid fa-font-awesome", item.Icon);
        Assert.Equal("Test", item.Message);
        Assert.True(item.Silent);
        Assert.Equal("test.wav", item.Sound);
        Assert.Equal("Title", item.Title);

        var callback = false;
        item.OnClick = () =>
        {
            callback = true;
            return Task.CompletedTask;
        };
        await service.DispatchCallback($"noti_item_{item.GetHashCode()}");
        Assert.True(callback);
    }

    private static void ResetModule(NotificationService service)
    {
        var pi = service.GetType().GetProperty("Module", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!;
        pi.SetValue(service, null);
    }
}
