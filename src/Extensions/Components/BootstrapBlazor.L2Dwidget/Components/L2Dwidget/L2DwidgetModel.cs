// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Model 模型
/// </summary>
public class L2DwidgetModel
{
    /// <summary>
    /// Path to Live2D model's main json eg. model主文件路径
    /// </summary>
    public string JsonPath { get; set; } = string.Empty;

    /// <summary>
    /// Scale between the model and the canvas 模型与canvas的缩放
    /// </summary>
    public double Scale { get; set; } = 1;
}
