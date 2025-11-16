// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Services;

public class BrowserFingerServiceTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task GetFingerCodeAsync_Ok()
    {
        Context.JSInterop.Setup<string?>("getFingerCode").SetResult("9527");
        var service = Context.Services.GetRequiredService<IBrowserFingerService>();
        var code = await service.GetFingerCodeAsync();
        Assert.Equal("9527", code);
    }

    [Fact]
    public async Task GetClientHubIdAsync_Ok()
    {
        Context.JSInterop.Setup<string?>("getClientHubId").SetResult("9528");
        var service = Context.Services.GetRequiredService<IBrowserFingerService>();
        var code = await service.GetClientHubIdAsync();
        Assert.Equal("9528", code);
    }
}
