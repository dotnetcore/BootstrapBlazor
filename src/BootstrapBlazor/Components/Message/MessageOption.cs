// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Message 组件配置类
/// </summary>
public class MessageOption : PopupOptionBase
{
    /// <summary>
    /// 获得/设置 颜色 默认 Primary
    /// </summary>
    public Color Color { get; set; } = Color.Primary;

    /// <summary>
    /// 获得/设置 是否显示关闭按钮 默认 false
    /// </summary>
    public bool ShowDismiss { get; set; }

    /// <summary>
    /// 获得/设置 显示图标 默认 null
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 是否显示左侧 Bar 默认 false
    /// </summary>
    public bool ShowBar { get; set; }

    /// <summary>
    /// 获得/设置 是否显示边框 默认 false 不显示
    /// </summary>
    public bool ShowBorder { get; set; }

    /// <summary>
    /// 获得/设置 是否显示阴影 默认 false 不显示
    /// </summary>
    public bool ShowShadow { get; set; }

    /// <summary>
    /// 获得/设置 关闭当前 MessageItem 回调委托 默认 null
    /// </summary>
    public Func<Task>? OnDismiss { get; set; }

    /// <summary>
    /// 获得/设置 内容模板 默认 null 设置此参数后 <see cref="PopupOptionBase.Content"/> 将失效
    /// </summary>
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 消息显示模式，默认为 <see cref="MessageShowMode.Multiple"/>
    /// </summary>
    public MessageShowMode ShowMode { get; set; } = MessageShowMode.Multiple;

    /// <summary>
    /// 获得/设置 附加 style 字符串到 <see cref="Message"/> 元素上
    /// </summary>
    public string? StyleString { get; set; }

    /// <summary>
    /// 获得/设置 附加 class 字符串到 <see cref="Message"/> 元素上
    /// </summary>
    public string? ClassString { get; set; }
}
