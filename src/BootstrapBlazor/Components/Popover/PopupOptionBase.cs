// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">弹窗类配置项基类</para>
/// <para lang="en">Popup Configuration Base Class</para>
/// </summary>
public abstract class PopupOptionBase
{
    /// <summary>
    /// <para lang="zh">获得/设置 Toast Body 子组件</para>
    /// <para lang="en">Gets or sets the Toast Body child component</para>
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否自动隐藏，默认为 true</para>
    /// <para lang="en">Gets or sets whether to auto hide. Default is true</para>
    /// </summary>
    public bool IsAutoHide { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 自动隐藏时间间隔（单位毫秒），默认为 4000。可通过全局配置进行统一更改</para>
    /// <para lang="en">Gets or sets the auto hide interval in milliseconds. Default is 4000. Can be configured globally</para>
    /// </summary>
    public int Delay { get; set; } = 4000;

    /// <summary>
    /// <para lang="zh">获得/设置 是否强制使用本实例的延时时间，防止值被全局配置覆盖，默认为 false。组件使用 <see cref="Delay"/> 值进行自动关闭，可通过 <see cref="BootstrapBlazorOptions"/> 类相关参数进行全局设置延时关闭时间，可通过本参数强制使用 <see cref="Delay"/> 值</para>
    /// <para lang="en">Gets or sets whether to force use the delay time of this instance to prevent the value from being overwritten by global configuration. Default is false. Component uses <see cref="Delay"/> value for automatic closing, global setting of delay closing time can be done through related parameters of <see cref="BootstrapBlazorOptions"/> class, this parameter can force usage of <see cref="Delay"/> value</para>
    /// </summary>
    public bool ForceDelay { get; set; }
}
