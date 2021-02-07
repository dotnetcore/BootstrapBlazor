// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace BootstrapBlazor.Localization.Json
{
    /// <summary>
    /// LocalizationOptions 配置类
    /// </summary>
    public class JsonLocalizationOptions : LocalizationOptions
    {
        /// <summary>
        /// 获得/设置 微软 resx 格式指定类型
        /// </summary>
        public Type? ResourceManagerStringLocalizerType { get; set; }

        /// <summary>
        /// 获得/设置 外置资源文件程序集集合
        /// </summary>
        public IEnumerable<Assembly>? AdditionalJsonAssemblies { get; set; }

        /// <summary>
        /// 获得/设置 外置资源文件路径集合
        /// </summary>
        public IEnumerable<string>? AdditionalJsonFiles { get; set; }

        /// <summary>
        /// 获得/设置 回落默认文化
        /// </summary>
        internal string? FallbackCulture { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        public JsonLocalizationOptions()
        {
            ResourcesPath = "Locales";
        }
    }
}
