﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
