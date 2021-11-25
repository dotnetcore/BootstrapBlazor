// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace UnitTest.Core
{
    [Collection("BlazorTestContext")]
    public class BootstrapBlazorTestBase
    {
        protected TestContext Context { get; }

        public BootstrapBlazorTestBase()
        {
            Context = BootstrapBlazorTestHost.Instance;
        }
    }

    [CollectionDefinition("BlazorTestContext")]
    public class BootstrapBlazorTestCollection : ICollectionFixture<BootstrapBlazorTestHost>
    {

    }

    public class BootstrapBlazorTestHost : IDisposable
    {
        [NotNull]
        internal static TestContext? Instance { get; private set; }

        public BootstrapBlazorTestHost()
        {
            Instance = new TestContext();

            // Mock 脚本
            Instance.JSInterop.Mode = JSRuntimeMode.Loose;

            ConfigureServices(Instance.Services);

            ConfigureConfigration(Instance.Services);

            // 渲染 BootstrapBlazorRoot 组件 激活 ICacheManager 接口
            Instance.RenderComponent<BootstrapBlazorRoot>();
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
            // 支持 微软 resx 格式资源文件
            services.AddLocalization(option => option.ResourcesPath = "Resources");
            services.AddBootstrapBlazor(localizationAction: options =>
            {
                options.ResourceManagerStringLocalizerType = typeof(BootstrapBlazorTestHost);
            });
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

    #region 英文环境
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
        protected override void ConfigureConfigration(IServiceCollection services)
        {
            // 增加单元测试 appsettings.json 配置文件
            services.AddConfiguration("en-US");
        }
    }
    #endregion
}
