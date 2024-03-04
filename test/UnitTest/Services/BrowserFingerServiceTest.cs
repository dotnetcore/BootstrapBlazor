// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;

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
