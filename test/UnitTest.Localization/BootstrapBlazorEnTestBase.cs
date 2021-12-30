// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Bunit;
using Microsoft.Extensions.DependencyInjection;
using UnitTest.Core;
using Xunit;

namespace UnitTest.Localization;

[Collection("BlazorEnTestContext")]
public class BootstrapBlazorEnTestBase
{
    protected TestContext Context { get; }

    public BootstrapBlazorEnTestBase()
    {
        Context = BootstrapBlazorEnTestHost.Instance;
    }
}

[CollectionDefinition("BlazorEnTestContext")]
public class BootstrapBlazorEnTestCollection : ICollectionFixture<BootstrapBlazorEnTestHost>
{

}

public class BootstrapBlazorEnTestHost : BootstrapBlazorTestHost
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        // 支持 微软 resx 格式资源文件
        services.AddLocalization(option => option.ResourcesPath = "Resources");
        services.AddBootstrapBlazor(localizationAction: options =>
        {
            options.ResourceManagerStringLocalizerType = typeof(BootstrapBlazorEnTestHost);
        });
    }

    protected override void ConfigureConfigration(IServiceCollection services)
    {
        // 增加单元测试 appsettings.json 配置文件
        services.AddConfiguration("en-US");
    }
}
