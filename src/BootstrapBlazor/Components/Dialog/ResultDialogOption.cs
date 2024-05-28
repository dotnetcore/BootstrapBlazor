// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 结果对话框配置类
/// </summary>
public class ResultDialogOption : DialogOption
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public ResultDialogOption()
    {
        ShowCloseButton = false;
        ResultTask = new();
    }

    /// <summary>
    /// 获得/设置 显示确认按钮
    /// </summary>
    public bool ShowYesButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 确认按钮文本
    /// </summary>
    public string? ButtonYesText { get; set; }

    /// <summary>
    /// 获得/设置 确认按钮图标
    /// </summary>
    public string? ButtonYesIcon { get; set; }

    /// <summary>
    /// 获得/设置 确认按钮颜色
    /// </summary>
    public Color ButtonYesColor { get; set; } = Color.Primary;

    /// <summary>
    /// 获得/设置 显示取消按钮
    /// </summary>
    public bool ShowNoButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 取消按钮文本
    /// </summary>
    public string? ButtonNoText { get; set; }

    /// <summary>
    /// 获得/设置 取消按钮图标
    /// </summary>
    public string? ButtonNoIcon { get; set; }

    /// <summary>
    /// 获得/设置 取消按钮颜色
    /// </summary>
    public Color ButtonNoColor { get; set; } = Color.Danger;

    /// <summary>
    /// 获得/设置 关闭按钮文本
    /// </summary>
    [Obsolete("已弃用，删除即可; Deprecated. Just delete it.")]
    [ExcludeFromCodeCoverage]
    public string? ButtonCloseText { get; set; }

    /// <summary>
    /// 获得/设置 关闭按钮图标
    /// </summary>
    [Obsolete("已弃用，删除即可; Deprecated. Just delete it.")]
    [ExcludeFromCodeCoverage]
    public string? ButtonCloseIcon { get; set; }

    /// <summary>
    /// 获得/设置 关闭按钮颜色
    /// </summary>
    [Obsolete("已弃用，删除即可; Deprecated. Just delete it.")]
    [ExcludeFromCodeCoverage]
    public Color ButtonCloseColor { get; set; } = Color.Secondary;

    /// <summary>
    /// 获得/设置 组件参数集合
    /// </summary>
    [Obsolete("已过期，单词拼写错误。请使用 ComponentParameters 代替 Please use ComponentParameters")]
    [ExcludeFromCodeCoverage]
    public Dictionary<string, object>? ComponentParamters
    {
        get => ComponentParameters;
        set => ComponentParameters = value;
    }

    /// <summary>
    /// 获得/设置 组件参数集合
    /// </summary>
    public Dictionary<string, object>? ComponentParameters { get; set; }

    /// <summary>
    /// 获得/设置 模态弹窗返回值任务实例
    /// </summary>
    internal TaskCompletionSource<DialogResult> ResultTask { get; set; }

    /// <summary>
    /// 获得 模态框接口方法
    /// </summary>
    internal Func<IResultDialog?>? GetDialog { get; set; }
}
