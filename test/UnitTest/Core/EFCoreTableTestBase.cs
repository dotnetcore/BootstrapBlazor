// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.EntityFrameworkCore;

namespace UnitTest.Core;

public class EFCoreTableTestBase : BootstrapBlazorTestBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddBootstrapBlazor(op => op.ToastDelay = 2000);
        services.AddDbContextFactory<FooContext>(option =>
        {
            option.UseSqlite("Data Source=FooTest.db;");
        });
    }
}
