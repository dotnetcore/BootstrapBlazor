// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
