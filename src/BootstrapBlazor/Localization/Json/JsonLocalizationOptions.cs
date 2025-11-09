// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;
using System.Reflection;

namespace BootstrapBlazor.Components;

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
    internal bool EnableFallbackCulture { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否忽略丢失文化日志信息 默认 false 不忽略
    /// </summary>
    public bool IgnoreLocalizerMissing { get; set; }

    /// <summary>
    /// 获得/设置 如果 Value 值为 null 时使用 Key 代替 默认 false
    /// </summary>
    public bool UseKeyWhenValueIsNull { get; set; }

    /// <summary>
    /// 获得/设置 是否禁用从服务中获取本地化资源 默认 false 未禁用
    /// </summary>
    public bool DisableGetLocalizerFromService { get; set; }

    /// <summary>
    /// 获得/设置 是否禁用获取 <see cref="ResourceManagerStringLocalizer"/> 类型本地化资源 默认 false 未禁用
    /// </summary>
    public bool DisableGetLocalizerFromResourceManager { get; set; }

    /// <summary>
    /// 获得/设置 资源文件是否热加载 默认 false
    /// </summary>
    [Obsolete("已弃用 Deprecated")]
    [ExcludeFromCodeCoverage]
    public bool ReloadOnChange { get; set; }

    /// <summary>
    /// 构造方法
    /// </summary>
    public JsonLocalizationOptions()
    {
        ResourcesPath = "Locales";
    }
}
