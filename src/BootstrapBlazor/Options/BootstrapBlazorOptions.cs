// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 组件全局配置类
/// </summary>
public class BootstrapBlazorOptions
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
    /// 获得/设置 是否回落到 UI 父文化 默认为 true
    /// </summary>
    public bool FallBackToParentUICultures { get; set; } = true;

    /// <summary>
    /// 获得/设置 默认文化信息
    /// </summary>
    /// <remarks>开启多文化时此参数无效</remarks>
    public string? DefaultCultureInfo { get; set; }

    /// <summary>
    /// 获得/设置 表格设置实例
    /// </summary>
    [NotNull]
    public TableSettings TableSettings { get; set; } = new();

    /// <summary>
    /// 获得/设置 网站主题集合
    /// </summary>
    public List<KeyValuePair<string, string>> Themes { get; } = new()
    {
        new("Bootstrap", "bootstrap.blazor.bundle.min.css"),
        new("Motronic", "motronic.min.css")
    };

    private Lazy<List<CultureInfo>>? CulturesCache { get; set; }
    /// <summary>
    /// 获得支持多语言集合
    /// </summary>
    /// <returns></returns>
    public IList<CultureInfo> GetSupportedCultures()
    {
        // 用户设置时使用用户设置，未设置时使用内置中英文文化
        CulturesCache ??= new Lazy<List<CultureInfo>>(() => SupportedCultures?.Select(name => new CultureInfo(name)).ToList()
            ?? new List<CultureInfo> { new("zh"), new("en") });
        return CulturesCache.Value;
    }
}
