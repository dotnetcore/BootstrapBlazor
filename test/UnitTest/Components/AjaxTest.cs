// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class AjaxTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task Ajax_Test()
    {
        var option = new AjaxOption
        {
            Url = "/api/Login",
            Method = "POST",
            Data = new { UserName = "admin", Password = "1234567" }
        };
        Assert.Equal("/api/Login", option.Url);
        Assert.Equal("POST", option.Method);
        Assert.True(option.ToJson);
        Assert.NotNull(option.Data);

        option.ToJson = false;
        Assert.False(option.ToJson);

        var service = Context.Services.GetRequiredService<AjaxService>();
        await service.InvokeAsync(option);
    }

    [Fact]
    public async Task Goto_Test()
    {
        var service = Context.Services.GetRequiredService<AjaxService>();
        await service.Goto("http://www.blazor.zone");
    }
}
