// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Reflection;

namespace BootstrapBlazor.Localization.Json;

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
    /// 获得/设置 回落默认文化 默认为 en 英文
    /// </summary>
    internal string FallbackCulture { get; set; } = "en";

    /// <summary>
    /// 获得/设置 是否回落到 UI 父文化 默认为 true
    /// </summary>
    internal bool FallBackToParentUICultures { get; set; } = true;

    /// <summary>
    /// 获得 组件内置本地化语言列表
    /// </summary>
    internal List<CultureInfo> SupportedCultures { get; } = new();

    /// <summary>
    /// 构造方法
    /// </summary>
    public JsonLocalizationOptions()
    {
        ResourcesPath = "Locales";
    }
}
