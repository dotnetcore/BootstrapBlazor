// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Toast 弹出窗参数配置类</para>
/// <para lang="en">Toast Popup Window Option Class</para>
/// </summary>
public class ToastOption : PopupOptionBase
{
    /// <summary>
    /// <para lang="zh">获得/设置 弹窗载体</para>
    /// <para lang="en">Gets or sets the toast popup carrier</para>
    /// </summary>
    internal Toast? Toast { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 弹出框类型</para>
    /// <para lang="en">Gets or sets the popup type</para>
    /// </summary>
    public ToastCategory Category { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 成功图标</para>
    /// <para lang="en">Gets or sets the success icon</para>
    /// </summary>
    public string? SuccessIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 提示图标</para>
    /// <para lang="en">Gets or sets the information icon</para>
    /// </summary>
    public string? InformationIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 错误图标</para>
    /// <para lang="en">Gets or sets the error icon</para>
    /// </summary>
    public string? ErrorIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 警告图标</para>
    /// <para lang="en">Gets or sets the warning icon</para>
    /// </summary>
    public string? WarningIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 显示标题</para>
    /// <para lang="en">Gets or sets the display title</para>
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 子组件</para>
    /// <para lang="en">Gets or sets the child component</para>
    /// </summary>
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示关闭按钮，默认 true</para>
    /// <para lang="en">Gets or sets whether to show the close button. Default is true.</para>
    /// </summary>
    public bool ShowClose { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 Header，默认 true</para>
    /// <para lang="en">Gets or sets whether to show the header. Default is true.</para>
    /// </summary>
    public bool ShowHeader { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否阻止重复消息，默认 false</para>
    /// <para lang="en">Gets or sets whether to prevent duplicate messages. Default is false.</para>
    /// </summary>
    public bool PreventDuplicates { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Header 模板，默认为 null</para>
    /// <para lang="en">Gets or sets the header template. Default is null.</para>
    /// </summary>
    public RenderFragment? HeaderTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否开启动画，默认 true</para>
    /// <para lang="en">Gets or sets whether to enable animation. Default is true.</para>
    /// </summary>
    public bool Animation { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 关闭当前 Toast 回调委托，默认 null</para>
    /// <para lang="en">Gets or sets the callback delegate to close the current Toast. Default is null.</para>
    /// </summary>
    public Func<Task>? OnCloseAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 附加 style 字符串到 Toast 元素上</para>
    /// <para lang="en">Gets or sets the additional style string to append to the Toast element</para>
    /// </summary>
    public string? StyleString { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 附加 class 字符串到 Toast 元素上</para>
    /// <para lang="en">Gets or sets the additional class string to append to the Toast element</para>
    /// </summary>
    public string? ClassString { get; set; }

    /// <summary>
    /// <para lang="zh">关闭当前弹窗方法</para>
    /// <para lang="en">Closes the current popup window</para>
    /// </summary>
    public async Task Close()
    {
        if (Toast != null)
        {
            await Toast.Close();
        }
    }
}
