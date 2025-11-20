// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Toast 弹出窗参数配置类
/// </summary>
public class ToastOption : PopupOptionBase
{
    /// <summary>
    /// 获得/设置 弹窗载体
    /// </summary>
    internal Toast? Toast { get; set; }

    /// <summary>
    /// 获得/设置 弹出框类型
    /// </summary>
    public ToastCategory Category { get; set; }

    /// <summary>
    /// 获得/设置 成功图标
    /// </summary>
    public string? SuccessIcon { get; set; }

    /// <summary>
    /// 获得/设置 提示图标
    /// </summary>
    public string? InformationIcon { get; set; }

    /// <summary>
    /// 获得/设置 错误图标
    /// </summary>
    public string? ErrorIcon { get; set; }

    /// <summary>
    /// 获得/设置 警告图标
    /// </summary>
    public string? WarningIcon { get; set; }

    /// <summary>
    /// 获得/设置 显示标题
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 是否显示关闭按钮 默认 true
    /// </summary>
    public bool ShowClose { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示 Header 默认 true
    /// </summary>
    public bool ShowHeader { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否阻止重复消息 默认 false
    /// </summary>
    public bool PreventDuplicates { get; set; }

    /// <summary>
    /// 获得/设置 Header 模板 默认为 null
    /// </summary>
    public RenderFragment? HeaderTemplate { get; set; }

    /// <summary>
    /// 获得/设置 是否开启动画 默认 true
    /// </summary>
    public bool Animation { get; set; } = true;

    /// <summary>
    /// 获得/设置 关闭当前 Toast 回调委托 默认 null
    /// </summary>
    public Func<Task>? OnCloseAsync { get; set; }

    /// <summary>
    /// 获得/设置 附加 style 字符串到 <see cref="Toast"/> 元素上
    /// </summary>
    public string? StyleString { get; set; }

    /// <summary>
    /// 获得/设置 附加 class 字符串到 <see cref="Toast"/> 元素上
    /// </summary>
    public string? ClassString { get; set; }

    /// <summary>
    /// 关闭当前弹窗方法
    /// </summary>
    public async Task Close()
    {
        if (Toast != null)
        {
            await Toast.Close();
        }
    }
}
