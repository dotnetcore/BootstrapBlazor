// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">LocalizationOptions 配置类</para>
/// <para lang="en">LocalizationOptions configuration class</para>
/// </summary>
public class JsonLocalizationOptions : LocalizationOptions
{
    /// <summary>
    /// <para lang="zh">获得/设置 微软 resx 格式指定类型</para>
    /// <para lang="en">Get/Set Microsoft resx format specified type</para>
    /// </summary>
    public Type? ResourceManagerStringLocalizerType { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 外置资源文件程序集集合</para>
    /// <para lang="en">Get/Set external resource file assembly collection</para>
    /// </summary>
    public IEnumerable<Assembly>? AdditionalJsonAssemblies { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 外置资源文件路径集合</para>
    /// <para lang="en">Get/Set external resource file path collection</para>
    /// </summary>
    public IEnumerable<string>? AdditionalJsonFiles { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 回落默认文化 默认为 en 英文</para>
    /// <para lang="en">Get/Set fallback default culture default is en English</para>
    /// </summary>
    internal string FallbackCulture { get; set; } = "en";

    /// <summary>
    /// <para lang="zh">获得/设置 是否回落到 UI 父文化 默认为 true</para>
    /// <para lang="en">Get/Set whether to fallback to UI parent culture default is true</para>
    /// </summary>
    internal bool EnableFallbackCulture { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否忽略丢失文化日志信息 默认 false 不忽略</para>
    /// <para lang="en">Get/Set whether to ignore missing culture log info default false not ignore</para>
    /// </summary>
    public bool IgnoreLocalizerMissing { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 如果 Value 值为 null 时使用 Key 代替 默认 false</para>
    /// <para lang="en">Get/Set whether to use Key when Value is null default false</para>
    /// </summary>
    public bool UseKeyWhenValueIsNull { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否禁用从服务中获取本地化资源 默认 false 未禁用</para>
    /// <para lang="en">Get/Set whether to disable getting localized resources from service default false not disabled</para>
    /// </summary>
    public bool DisableGetLocalizerFromService { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否禁用获取 <see cref="ResourceManagerStringLocalizer"/> 类型本地化资源 默认 false 未禁用</para>
    /// <para lang="en">Get/Set whether to disable getting <see cref="ResourceManagerStringLocalizer"/> type localized resources default false not disabled</para>
    /// </summary>
    public bool DisableGetLocalizerFromResourceManager { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 资源文件是否热加载 默认 false</para>
    /// <para lang="en">Get/Set whether resource file is hot reloaded default false</para>
    /// </summary>
    [Obsolete("已弃用 Deprecated")]
    [ExcludeFromCodeCoverage]
    public bool ReloadOnChange { get; set; }

    /// <summary>
    /// <para lang="zh">构造方法</para>
    /// <para lang="en">Constructor</para>
    /// </summary>
    public JsonLocalizationOptions()
    {
        ResourcesPath = "Locales";
    }
}
