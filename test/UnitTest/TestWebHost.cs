// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Server;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Xunit;

namespace UnitTest
{
    /// <summary>
    /// 
    /// </summary>
    [CollectionDefinition("Blazor")]
    public class TestWebContext : ICollectionFixture<TestWebHost>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class TestWebHost : WebApplicationFactory<Startup>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            // 增加单元测试本身的配置文件
            var dirSeparator = Path.DirectorySeparatorChar;
            string settingsFile = Path.Combine(AppContext.BaseDirectory, $"..{dirSeparator}..{dirSeparator}..{dirSeparator}appsettings.json");
            if (File.Exists(settingsFile))
            {
                builder.ConfigureAppConfiguration((context, app) => app.AddJsonFile(settingsFile));
            }
        }
    }
}
