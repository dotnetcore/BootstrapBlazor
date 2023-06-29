// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Dialog 对话框
/// </summary>
public class L2DwidgetDialog
{
    /// <summary>
    /// Display dialog 显示人物对话框
    /// </summary>
    public bool Enable { get; set; } = false;

    /// <summary>
    /// Enable hitokoto 使用一言API
    /// </summary>
    public bool Hitokoto { get; set; } = false;

    /// <summary>
    /// Script 动作脚本
    /// </summary>
    public Dictionary<string, string> Script { get; set; } = new();
}
