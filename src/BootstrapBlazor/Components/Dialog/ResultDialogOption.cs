// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">结果对话框配置类</para>
/// <para lang="en">Result Dialog Option Class</para>
/// </summary>
public class ResultDialogOption : DialogOption
{
    /// <summary>
    /// <para lang="zh">构造函数</para>
    /// <para lang="en">Constructor</para>
    /// </summary>
    public ResultDialogOption()
    {
        ShowCloseButton = false;
        ResultTask = new();
    }

    /// <summary>
    /// <para lang="zh">获得/设置 显示确认按钮</para>
    /// <para lang="en">Get/Set Show Yes Button</para>
    /// </summary>
    public bool ShowYesButton { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 确认按钮文本</para>
    /// <para lang="en">Get/Set Yes Button Text</para>
    /// </summary>
    public string? ButtonYesText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 确认按钮图标</para>
    /// <para lang="en">Get/Set Yes Button Icon</para>
    /// </summary>
    public string? ButtonYesIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 确认按钮颜色</para>
    /// <para lang="en">Get/Set Yes Button Color</para>
    /// </summary>
    public Color ButtonYesColor { get; set; } = Color.Primary;

    /// <summary>
    /// <para lang="zh">获得/设置 显示取消按钮</para>
    /// <para lang="en">Get/Set Show No Button</para>
    /// </summary>
    public bool ShowNoButton { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 取消按钮文本</para>
    /// <para lang="en">Get/Set No Button Text</para>
    /// </summary>
    public string? ButtonNoText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 取消按钮图标</para>
    /// <para lang="en">Get/Set No Button Icon</para>
    /// </summary>
    public string? ButtonNoIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 取消按钮颜色</para>
    /// <para lang="en">Get/Set No Button Color</para>
    /// </summary>
    public Color ButtonNoColor { get; set; } = Color.Danger;

    /// <summary>
    /// <para lang="zh">获得/设置 关闭按钮文本</para>
    /// <para lang="en">Get/Set Close Button Text</para>
    /// </summary>
    [Obsolete("已弃用，删除即可; Deprecated. Just delete it.")]
    [ExcludeFromCodeCoverage]
    public string? ButtonCloseText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关闭按钮图标</para>
    /// <para lang="en">Get/Set Close Button Icon</para>
    /// </summary>
    [Obsolete("已弃用，删除即可; Deprecated. Just delete it.")]
    [ExcludeFromCodeCoverage]
    public string? ButtonCloseIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关闭按钮颜色</para>
    /// <para lang="en">Get/Set Close Button Color</para>
    /// </summary>
    [Obsolete("已弃用，删除即可; Deprecated. Just delete it.")]
    [ExcludeFromCodeCoverage]
    public Color ButtonCloseColor { get; set; } = Color.Secondary;

    /// <summary>
    /// <para lang="zh">获得/设置 组件参数集合</para>
    /// <para lang="en">Get/Set Component Parameters</para>
    /// </summary>
    [Obsolete("已过期，单词拼写错误。请使用 ComponentParameters 代替 Please use ComponentParameters")]
    [ExcludeFromCodeCoverage]
    public Dictionary<string, object>? ComponentParamters
    {
        get => ComponentParameters;
        set => ComponentParameters = value;
    }

    /// <summary>
    /// <para lang="zh">获得/设置 组件参数集合</para>
    /// <para lang="en">Get/Set Component Parameters</para>
    /// </summary>
    public Dictionary<string, object>? ComponentParameters { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 模态弹窗返回值任务实例</para>
    /// <para lang="en">Get/Set Modal Result Task Instance</para>
    /// </summary>
    internal TaskCompletionSource<DialogResult> ResultTask { get; set; }

    /// <summary>
    /// <para lang="zh">获得 模态框接口方法</para>
    /// <para lang="en">Get Modal Interface Method</para>
    /// </summary>
    internal Func<IResultDialog?>? GetDialog { get; set; }
}
