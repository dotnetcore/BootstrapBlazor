// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

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
