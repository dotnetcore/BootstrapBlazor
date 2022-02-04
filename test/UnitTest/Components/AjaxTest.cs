// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Components;

public class AjaxTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Ajax_Test()
    {
        var option = new AjaxOption
        {
            Url = "/api/Login",
            Method = "POST",
            Data = new { UserName = "admin", Password = "1234567" }
        };
        var service = Context.Services.GetRequiredService<AjaxService>();
        _ = service.GetMessage(option);

        _ = Context.RenderComponent<Ajax>();
        _ = service.GetMessage(option);
        _ = service.Goto("http://www.blazor.zone");
    }
}
