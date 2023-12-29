// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Services;

public class ClipboardServiceTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task ClipboardService_Ok()
    {
        var service = Context.Services.GetRequiredService<ClipboardService>();
        var cut = Context.RenderComponent<Clipboard>();
        var copied = false;
        await service.Copy("Test", () =>
        {
            copied = true;
            return Task.CompletedTask;
        });
        Assert.True(copied);

        await cut.Instance.DisposeAsync();
    }
}
