// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Core;

[Collection("TableSortDialogTestContext")]
public class TableSortDialogTestBase
{
    protected TestContext Context { get; }

    public TableSortDialogTestBase()
    {
        Context = TableSortDialogTestHost.Instance;
    }
}

[CollectionDefinition("TableSortDialogTestContext")]
public class TableSortDialogTestCollection : ICollectionFixture<TableSortDialogTestHost>
{

}

public class TableSortDialogTestHost : IDisposable
{
    [NotNull]
    internal static TestContext? Instance { get; private set; }

    public TableSortDialogTestHost()
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
        services.AddBootstrapBlazor();
    }

    protected virtual void ConfigureConfiguration(IServiceCollection services)
    {
        services.AddConfiguration();
    }

    public void Dispose()
    {
        Instance.Dispose();
        GC.SuppressFinalize(this);
    }
}
