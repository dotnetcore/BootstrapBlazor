// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Localization.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// ColumnName 属性标签用于标示列头显示信息支持多语言
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnNameAttribute : Attribute
    {
        /// <summary>
        /// 获得/设置 列名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 获得/设置 资源文件键值名称 为空时使用当前属性名
        /// </summary>
        public string? ResourceName { get; set; }

        /// <summary>
        /// 获得/设置 资源文件所在程序集 为空时扫描当前程序集
        /// </summary>
        public Type? ResourceType { get; set; }

        /// <summary>
        /// 获得显示名称方法
        /// </summary>
        /// <returns></returns>
        public string? GetName()
        {
            var ret = Name;
            if (ResourceType != null && !string.IsNullOrEmpty(ResourceName))
            {
                var options = ServiceProviderHelper.ServiceProvider.GetRequiredService<IOptions<JsonLocalizationOptions>>();
                var loggerFactory = ServiceProviderHelper.ServiceProvider.GetRequiredService<ILoggerFactory>();
                var factory = new JsonStringLocalizerFactory(options, loggerFactory);
                var localizer = factory.Create(ResourceType);
                ret = localizer[ResourceName];
            }
            return ret;
        }
    }
}
