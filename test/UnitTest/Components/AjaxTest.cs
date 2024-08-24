﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class AjaxTest : TestBase
{
    public AjaxTest()
    {
        Context.Services.AddBootstrapBlazor();
    }

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
        Assert.NotNull(option.Data);

        var service = Context.Services.GetRequiredService<AjaxService>();
        await service.InvokeAsync(option);

        Context.RenderComponent<Ajax>();
        await service.InvokeAsync(option);
        await service.Goto("http://www.blazor.zone");
    }
}
