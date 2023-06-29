// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

//https://l2dwidget.js.org/docs/class/src/index.js~L2Dwidget.html

/// <summary>
/// User's custom config 用户自定义设置
/// </summary>
public class L2DwidgetOptions
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public L2DwidgetModel? Model { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public L2DwidgetDisplay? Display { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public L2DwidgetMobile? Mobile { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public L2DwidgetName? Name { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public L2DwidgetDialog? Dialog { get; set; }
}
