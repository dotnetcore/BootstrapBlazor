// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
