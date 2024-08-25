// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Core;

public class TabTestBase : BootstrapBlazorTestBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddBootstrapBlazor(op => op.ToastDelay = 2000);
        services.ConfigureTabItemMenuBindOptions(options =>
        {
            options.Binders = new()
            {
                { "/Binder", new() { Text = "Index_Binder_Test" } }
            };
        });
    }
}
