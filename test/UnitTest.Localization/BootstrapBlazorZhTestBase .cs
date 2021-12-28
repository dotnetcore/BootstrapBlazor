// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Bunit;
using Microsoft.Extensions.DependencyInjection;
using UnitTest.Core;
using Xunit;

namespace UnitTest.Localization;

[Collection("BlazorZhTestContext")]
public class BootstrapBlazorZhTestBase
{
    protected TestContext Context { get; }

    public BootstrapBlazorZhTestBase()
    {
        Context = BootstrapBlazorZhTestHost.Instance;
    }
}

[CollectionDefinition("BlazorZhTestContext")]
public class BootstrapBlazorZhTestCollection : ICollectionFixture<BootstrapBlazorZhTestHost>
{

}

public class BootstrapBlazorZhTestHost : BootstrapBlazorTestHost
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        // 支持 微软 resx 格式资源文件
        services.AddLocalization(option => option.ResourcesPath = "Resources");
        services.AddBootstrapBlazor(localizationAction: options =>
        {
            options.ResourceManagerStringLocalizerType = typeof(BootstrapBlazorZhTestHost);
        });
    }

    protected override void ConfigureConfigration(IServiceCollection services)
    {
        // 增加单元测试 appsettings.json 配置文件
        services.AddConfiguration("zh-CN");
    }
}
