// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Localization;

public class BootstrapBlazorEnTestBase : BootstrapBlazorTestBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        // 支持 微软 resx 格式资源文件
        services.AddLocalization(option => option.ResourcesPath = "Resources");
        services.AddBootstrapBlazor(localizationConfigure: options =>
        {
            options.ResourceManagerStringLocalizerType = typeof(BootstrapBlazorEnTestBase);
        });
    }

    protected override void ConfigureConfiguration(IServiceCollection services)
    {
        // 增加单元测试 appsettings.json 配置文件
        services.AddConfiguration("en-US");
    }
}
