// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Core;

[Collection("TabTestContext")]
public class TabTestBase
{
    protected TestContext Context { get; }

    public TabTestBase()
    {
        Context = TabTestHost.Instance;
    }
}

[CollectionDefinition("TabTestContext")]
public class TabTestCollection : ICollectionFixture<TabTestHost>
{

}

public class TabTestHost : IDisposable
{
    [NotNull]
    internal static TestContext? Instance { get; private set; }

    public TabTestHost()
    {
        Instance = new TestContext();

        // Mock 脚本
        Instance.JSInterop.Mode = JSRuntimeMode.Loose;

        ConfigureServices(Instance.Services);

        ConfigureConfigration(Instance.Services);

        // 渲染 BootstrapBlazorRoot 组件 激活 ICacheManager 接口
        Instance.Services.GetRequiredService<ICacheManager>();
    }

    protected virtual void ConfigureServices(IServiceCollection services)
    {
        services.AddBootstrapBlazor(op => op.ToastDelay = 2000);
        services.ConfigureJsonLocalizationOptions(op => op.AdditionalJsonAssemblies = new[] { typeof(Alert).Assembly });
    }

    protected virtual void ConfigureConfigration(IServiceCollection services)
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
