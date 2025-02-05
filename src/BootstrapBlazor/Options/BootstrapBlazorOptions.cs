// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;
using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 组件全局配置类
/// </summary>
public class BootstrapBlazorOptions : IOptions<BootstrapBlazorOptions>
{
    /// <summary>
    /// 获得/设置 Toast 组件 Delay 默认值 默认为 0
    /// </summary>
    public int ToastDelay { get; set; }

    /// <summary>
    /// 获得/设置 Message 组件 Delay 默认值 默认为 0
    /// </summary>
    public int MessageDelay { get; set; }

    /// <summary>
    /// 获得/设置 Swal 组件 Delay 默认值 默认为 0
    /// </summary>
    public int SwalDelay { get; set; }

    /// <summary>
    /// 获得/设置 回落默认语言文化 默认为 en 英文
    /// </summary>
    public string FallbackCulture { get; set; } = "en";

    /// <summary>
    /// 获得/设置 Toast 组件全局弹窗默认位置 默认为 null 当设置值后覆盖整站设置
    /// </summary>
    public Placement? ToastPlacement { get; set; }

    /// <summary>
    /// 获得/设置 组件内置本地化语言列表 默认为 null
    /// </summary>
    public List<string>? SupportedCultures { get; set; }

    /// <summary>
    /// 获得/设置 是否开启全局异常捕获功能 默认为 true
    /// </summary>
    public bool EnableErrorLogger { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否回落到 Fallback 文化 默认为 true
    /// </summary>
    public bool EnableFallbackCulture { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否忽略丢失文化日志信息 默认 null 未设置
    /// </summary>
    /// <remarks>使用 <see cref="JsonLocalizationOptions.IgnoreLocalizerMissing"/> 默认值</remarks>
    public bool? IgnoreLocalizerMissing { get; set; }

    /// <summary>
    /// 获得/设置 是否禁用从服务中获取本地化资源 默认 false 未禁用
    /// </summary>
    public bool? DisableGetLocalizerFromService { get; set; }

    /// <summary>
    /// 获得/设置 是否禁用获取 <see cref="ResourceManagerStringLocalizer"/> 类型本地化资源 默认 false 未禁用
    /// </summary>
    public bool? DisableGetLocalizerFromResourceManager { get; set; }

    /// <summary>
    /// 获得/设置 默认文化信息
    /// </summary>
    /// <remarks>开启多文化时此参数无效</remarks>
    public string? DefaultCultureInfo { get; set; }

    /// <summary>
    /// 获得/设置 是否禁用表单内回车自动提交功能 默认 null 未设置
    /// </summary>
    public bool? DisableAutoSubmitFormByEnter { get; set; }

    /// <summary>
    /// 获得/设置 JavaScript 模块脚本版本号 默认为 null
    /// </summary>
    public string? JSModuleVersion { get; set; }

    /// <summary>
    /// 获得/设置 表格设置实例
    /// </summary>
    public TableSettings TableSettings { get; set; } = new();

    /// <summary>
    /// 获得/设置 <see cref="StepSettings"/> 配置实例
    /// </summary>
    public StepSettings StepSettings { get; set; } = new();

    /// <summary>
    /// 获得/设置 <see cref="ConnectionHubOptions"/> 配置 默认不为空
    /// </summary>
    public ConnectionHubOptions ConnectionHubOptions { get; set; } = new();

    /// <summary>
    /// 获得/设置 <see cref="WebClientOptions"/> 配置 默认不为空
    /// </summary>
    public WebClientOptions WebClientOptions { get; set; } = new();

    /// <summary>
    /// 获得/设置 <see cref="IpLocatorOptions"/> 配置 默认不为空
    /// </summary>
    public IpLocatorOptions IpLocatorOptions { get; set; } = new();

    /// <summary>
    /// 获得/设置 <see cref="ScrollOptions"/> 配置 默认不为空
    /// </summary>
    public ScrollOptions ScrollOptions { get; set; } = new();

    /// <summary>
    /// 获得/设置 <see cref="ContextMenuOptions"/> 配置 默认不为空
    /// </summary>
    public ContextMenuOptions ContextMenuOptions { get; set; } = new();

    /// <summary>
    /// 获得/设置 CacheManagerOptions 配置 默认不为空
    /// </summary>
    public CacheManagerOptions CacheManagerOptions { get; set; } = new();

    BootstrapBlazorOptions IOptions<BootstrapBlazorOptions>.Value => this;

    /// <summary>
    /// 获得支持多语言集合
    /// </summary>
    /// <returns></returns>
    public IList<CultureInfo> GetSupportedCultures() => SupportedCultures?.Select(name => new CultureInfo(name)).ToList()
        ?? [new("en"), new("zh")];
}
