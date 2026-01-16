// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Message 组件配置类</para>
/// <para lang="en">Message Component Configuration Class</para>
/// </summary>
public class MessageOption : PopupOptionBase
{
    /// <summary>
    /// <para lang="zh">获得/设置 颜色 默认 Primary</para>
    /// <para lang="en">Get/Set Color. Default Primary</para>
    /// </summary>
    public Color Color { get; set; } = Color.Primary;

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示关闭按钮 默认 false</para>
    /// <para lang="en">Get/Set Whether to show dismiss button. Default false</para>
    /// </summary>
    public bool ShowDismiss { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 显示图标 默认 null</para>
    /// <para lang="en">Get/Set Display Icon. Default null</para>
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示左侧 Bar 默认 false</para>
    /// <para lang="en">Get/Set Whether to show left Bar. Default false</para>
    /// </summary>
    public bool ShowBar { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示边框 默认 false 不显示</para>
    /// <para lang="en">Get/Set Whether to show border. Default false (Not shown)</para>
    /// </summary>
    public bool ShowBorder { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示阴影 默认 false 不显示</para>
    /// <para lang="en">Get/Set Whether to show shadow. Default false (Not shown)</para>
    /// </summary>
    public bool ShowShadow { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关闭当前 MessageItem 回调委托 默认 null</para>
    /// <para lang="en">Get/Set Callback delegate for closing current MessageItem. Default null</para>
    /// </summary>
    public Func<Task>? OnDismiss { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 内容模板 默认 null 设置此参数后 <see cref="PopupOptionBase.Content"/> 将失效</para>
    /// <para lang="en">Get/Set Content Template. Default null. <see cref="PopupOptionBase.Content"/> will be invalid if this parameter is set</para>
    /// </summary>
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 消息显示模式，默认为 <see cref="MessageShowMode.Multiple"/></para>
    /// <para lang="en">Get/Set Message show mode. Default <see cref="MessageShowMode.Multiple"/></para>
    /// </summary>
    public MessageShowMode ShowMode { get; set; } = MessageShowMode.Multiple;

    /// <summary>
    /// <para lang="zh">获得/设置 附加 style 字符串到 <see cref="Message"/> 元素上</para>
    /// <para lang="en">Get/Set Attach style string to <see cref="Message"/> element</para>
    /// </summary>
    public string? StyleString { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 附加 class 字符串到 <see cref="Message"/> 元素上</para>
    /// <para lang="en">Get/Set Attach class string to <see cref="Message"/> element</para>
    /// </summary>
    public string? ClassString { get; set; }
}
