// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 弹窗类配置项基类
/// </summary>
public abstract class PopupOptionBase
{
    /// <summary>
    /// 获得/设置 Toast Body 子组件
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// 获得/设置 是否自动隐藏 默认 true 自动关闭 <see cref="PopupOptionBase"/> 默认 true
    /// </summary>
    public bool IsAutoHide { get; set; } = true;

    /// <summary>
    /// 获得/设置 自动隐藏时间间隔 单位毫秒 默认 4000 可通过全局配置进行统一更改
    /// </summary>
    public int Delay { get; set; } = 4000;

    /// <summary>
    /// 获得/设置 是否强制使用本实例的延时时间，防止值被全局配置覆盖 默认 false
    /// <para>组件使用 <see cref="Delay"/> 值进行自动关闭，可通过 <see cref="BootstrapBlazorOptions"/> 类相关参数进行全局设置延时关闭时间，可通过本参数强制使用 <see cref="Delay"/> 值</para>
    /// </summary>
    public bool ForceDelay { get; set; }
}
