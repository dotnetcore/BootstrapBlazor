// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace UnitTest.Core;

[Collection("EFCoreTableTestContext")]
public class EFCoreTableTestBase
{
    protected TestContext Context { get; }

    public EFCoreTableTestBase()
    {
        Context = EFCoreTableTestHost.Instance;
    }
}

[CollectionDefinition("EFCoreTableTestContext")]
public class EFCoreTableTestCollection : ICollectionFixture<EFCoreTableTestHost>
{

}

public class EFCoreTableTestHost : IDisposable
{
    [NotNull]
    internal static TestContext? Instance { get; private set; }

    public EFCoreTableTestHost()
    {
        Instance = new TestContext();

        // Mock 脚本
        Instance.JSInterop.Mode = JSRuntimeMode.Loose;

        ConfigureServices(Instance.Services);

        ConfigureConfiguration(Instance.Services);

        // 渲染 BootstrapBlazorRoot 组件 激活 ICacheManager 接口
        Instance.Services.GetRequiredService<ICacheManager>();
    }

    protected virtual void ConfigureServices(IServiceCollection services)
    {
        services.AddBootstrapBlazor(op => op.ToastDelay = 2000);
        services.AddDbContextFactory<FooContext>(option =>
        {
            option.UseSqlite("Data Source=FooTest.db;");
        });
    }

    protected virtual void ConfigureConfiguration(IServiceCollection services)
    {
        // 增加单元测试 appsettings.json 配置文件
        services.AddConfiguration();
    }

    public void Dispose()
    {
        Instance.Dispose();
        GC.SuppressFinalize(this);
    }
}
