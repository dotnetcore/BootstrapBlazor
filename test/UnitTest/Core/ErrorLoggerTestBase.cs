// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Core;

[Collection("ErrorLoggerTestContext")]
public class ErrorLoggerTestBase
{
    protected TestContext Context { get; }

    public ErrorLoggerTestBase()
    {
        Context = ErrorLoggerTestHost.Instance;
    }
}

[CollectionDefinition("ErrorLoggerTestContext")]
public class ErrorLoggerTestCollection : ICollectionFixture<ErrorLoggerTestHost>
{

}

public class ErrorLoggerTestHost : IDisposable
{
    [NotNull]
    internal static TestContext? Instance { get; private set; }

    public ErrorLoggerTestHost()
    {
        Instance = new TestContext();

        // Mock 脚本
        Instance.JSInterop.Mode = JSRuntimeMode.Loose;

        ConfigureServices(Instance.Services);

        ConfigureConfiguration(Instance.Services);
    }

    protected virtual void ConfigureServices(IServiceCollection services)
    {
        services.AddBootstrapBlazor();
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
