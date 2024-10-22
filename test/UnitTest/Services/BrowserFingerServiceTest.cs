// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Services;

public class BrowserFingerServiceTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task GetFingerCodeAsync_Ok()
    {
        var service = Context.Services.GetRequiredService<IBrowserFingerService>();
        var cut = Context.RenderComponent<BrowserFinger>();
        var code = await service.GetFingerCodeAsync();
        cut.Instance.Dispose();
        Assert.Null(code);
    }
}
