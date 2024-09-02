// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Configuration;

namespace UnitTest.Core;

public class BootstrapBlazorTestBase : TestBase, IDisposable
{
    protected ICacheManager Cache { get; }

    public BootstrapBlazorTestBase() : base()
    {
        ConfigureServices(Context.Services);

        ConfigureConfiguration(Context.Services);

        // 渲染 BootstrapBlazorRoot 组件 激活 ICacheManager 接口
        Cache = Context.Services.GetRequiredService<ICacheManager>();
    }

    protected virtual void ConfigureServices(IServiceCollection services)
    {
        services.AddBootstrapBlazor();
        services.ConfigureJsonLocalizationOptions(op =>
        {
            op.IgnoreLocalizerMissing = false;
        });
        services.AddSingleton<ILookupService, FooLookupService>();
    }

    protected virtual void ConfigureConfiguration(IServiceCollection services)
    {
        // 增加单元测试 appsettings.json 配置文件
        services.AddConfiguration();
    }

    public void Dispose()
    {
        Context.Dispose();
        GC.SuppressFinalize(this);
    }

    class FooLookupService : LookupServiceBase
    {
        public override IEnumerable<SelectedItem>? GetItemsByKey(string? key, object? data)
        {
            IEnumerable<SelectedItem>? ret = null;

            if (key == "FooLookup")
            {
                ret = new SelectedItem[]
                {
                    new("v1", "LookupService-Test-1"),
                    new("v2", "LookupService-Test-2")
                };
            }
            return ret;
        }
    }
}
