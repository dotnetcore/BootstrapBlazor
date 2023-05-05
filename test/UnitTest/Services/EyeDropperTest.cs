// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Services;

public class EyeDropperTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task EyeDropperService_Ok()
    {
        Context.JSInterop.Setup<string?>("open").SetResult("Ok");
        var service = Context.Services.GetRequiredService<EyeDropperService>();
        var cut = Context.RenderComponent<EyeDropper>();
        var expected = await service.PickAsync();
        Assert.Equal("Ok", expected);
    }
}
