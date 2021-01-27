// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;

namespace BootstrapBlazor.Localization.Json
{
    /// <summary>
    /// LocalizationOptions 配置类
    /// </summary>
    public class JsonLocalizationOptions : LocalizationOptions
    {
        /// <summary>
        /// 获得/设置 本地化资源文件流集合 默认为空
        /// </summary>
        public IEnumerable<Stream>? JsonLocalizationStreams { get; set; }

        /// <summary>
        /// 获得/设置 自定义 IStringLocalizer 接口 默认为空
        /// </summary>
        public IStringLocalizer? StringLocalizer { get; set; }

        /// <summary>
        /// 获得/设置 自定义 Json 格式资源流集合
        /// </summary>
        public Func<string, IConfiguration>? LocalizerConfigurationFactory { get; set; }

        /// <summary>
        /// 获得/设置 LoggerFactory 实例
        /// </summary>
        public ILoggerFactory LoggerFactory { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        public JsonLocalizationOptions()
        {
            ResourcesPath = "Resources";
            LoggerFactory = ServiceProviderHelper.ServiceProvider.GetRequiredService<ILoggerFactory>();
        }

        /// <summary>
        /// 创建 IOptions 示例方法
        /// </summary>
        /// <param name="resourcesPath"></param>
        /// <returns></returns>
        public IOptions<LocalizationOptions> CreateOptions(string? resourcesPath = null) => Options.Create(new LocalizationOptions() { ResourcesPath = resourcesPath ?? ResourcesPath });
    }
}
