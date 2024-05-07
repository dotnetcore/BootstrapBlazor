// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Localization.Json;
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
    /// 获得/设置 默认文化信息
    /// </summary>
    /// <remarks>开启多文化时此参数无效</remarks>
    public string? DefaultCultureInfo { get; set; }

    /// <summary>
    /// 获得/设置 表格设置实例
    /// </summary>
    public TableSettings TableSettings { get; set; } = new();

    /// <summary>
    /// 获得/设置 Step 配置实例
    /// </summary>
    public StepSettings StepSettings { get; set; } = new();

    /// <summary>
    /// 获得/设置 是否禁用表单内回车自动提交功能 默认 null 未设置
    /// </summary>
    public bool? DisableAutoSubmitFormByEnter { get; set; }

    /// <summary>
    /// 获得/设置 JavaScript 模块脚本版本号 默认为 null
    /// </summary>
    public string? JSModuleVersion { get; set; }

    /// <summary>
    /// 获得/设置 ConnectionHubOptions 配置 默认为不为空
    /// </summary>
    public ConnectionHubOptions ConnectionHubOptions { get; set; } = new();

    /// <summary>
    /// 获得/设置 IpLocatorOptions 配置 默认为不为空
    /// </summary>
    public IpLocatorOptions IpLocatorOptions { get; set; } = new();

    /// <summary>
    /// 获得/设置 ScrollOptions 配置 默认为不为空
    /// </summary>
    public ScrollOptions ScrollOptions { get; set; } = new();

    BootstrapBlazorOptions IOptions<BootstrapBlazorOptions>.Value => this;

    /// <summary>
    /// 获得支持多语言集合
    /// </summary>
    /// <returns></returns>
    public IList<CultureInfo> GetSupportedCultures() => SupportedCultures?.Select(name => new CultureInfo(name)).ToList()
        ?? [new("en"), new("zh")];
}
