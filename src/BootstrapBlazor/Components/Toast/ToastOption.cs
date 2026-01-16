// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">Toast 弹出窗参数配置类</para>
///  <para lang="en">Toast 弹出窗参数配置类</para>
/// </summary>
public class ToastOption : PopupOptionBase
{
    /// <summary>
    ///  <para lang="zh">获得/设置 弹窗载体</para>
    ///  <para lang="en">Gets or sets 弹窗载体</para>
    /// </summary>
    internal Toast? Toast { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 弹出框类型</para>
    ///  <para lang="en">Gets or sets 弹出框type</para>
    /// </summary>
    public ToastCategory Category { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 成功图标</para>
    ///  <para lang="en">Gets or sets 成功icon</para>
    /// </summary>
    public string? SuccessIcon { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 提示图标</para>
    ///  <para lang="en">Gets or sets 提示icon</para>
    /// </summary>
    public string? InformationIcon { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 错误图标</para>
    ///  <para lang="en">Gets or sets 错误icon</para>
    /// </summary>
    public string? ErrorIcon { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 警告图标</para>
    ///  <para lang="en">Gets or sets 警告icon</para>
    /// </summary>
    public string? WarningIcon { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 显示标题</para>
    ///  <para lang="en">Gets or sets display标题</para>
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 子组件</para>
    ///  <para lang="en">Gets or sets 子component</para>
    /// </summary>
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示关闭按钮 默认 true</para>
    ///  <para lang="en">Gets or sets whetherdisplay关闭button Default is true</para>
    /// </summary>
    public bool ShowClose { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示 Header 默认 true</para>
    ///  <para lang="en">Gets or sets whetherdisplay Header Default is true</para>
    /// </summary>
    public bool ShowHeader { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 是否阻止重复消息 默认 false</para>
    ///  <para lang="en">Gets or sets whether阻止重复消息 Default is false</para>
    /// </summary>
    public bool PreventDuplicates { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 Header 模板 默认为 null</para>
    ///  <para lang="en">Gets or sets Header template Default is为 null</para>
    /// </summary>
    public RenderFragment? HeaderTemplate { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否开启动画 默认 true</para>
    ///  <para lang="en">Gets or sets whether开启动画 Default is true</para>
    /// </summary>
    public bool Animation { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 关闭当前 Toast 回调委托 默认 null</para>
    ///  <para lang="en">Gets or sets 关闭当前 Toast 回调delegate Default is null</para>
    /// </summary>
    public Func<Task>? OnCloseAsync { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 附加 style 字符串到 <see cref="Toast"/> 元素上</para>
    ///  <para lang="en">Gets or sets 附加 style 字符串到 <see cref="Toast"/> 元素上</para>
    /// </summary>
    public string? StyleString { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 附加 class 字符串到 <see cref="Toast"/> 元素上</para>
    ///  <para lang="en">Gets or sets 附加 class 字符串到 <see cref="Toast"/> 元素上</para>
    /// </summary>
    public string? ClassString { get; set; }

    /// <summary>
    ///  <para lang="zh">关闭当前弹窗方法</para>
    ///  <para lang="en">关闭当前弹窗方法</para>
    /// </summary>
    public async Task Close()
    {
        if (Toast != null)
        {
            await Toast.Close();
        }
    }
}
