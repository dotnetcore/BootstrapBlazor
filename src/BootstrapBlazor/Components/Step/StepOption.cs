// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Step 组件项类
/// </summary>
public class StepOption
{
    /// <summary>
    /// 获得/设置 步骤显示文字
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 步骤显示图标
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 步骤完成显示图标
    /// </summary>
    public string? FinishedIcon { get; set; }

    /// <summary>
    /// 获得/设置 步骤显示标题
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// 获得/设置 描述信息
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 获得/设置 Header 模板
    /// </summary>
    public RenderFragment<StepOption>? HeaderTemplate { get; set; }

    /// <summary>
    /// 获得/设置 Title 模板
    /// </summary>
    public RenderFragment<StepOption>? TitleTemplate { get; set; }

    /// <summary>
    /// 获得/设置 每个 step 内容模板
    /// </summary>
    public RenderFragment? Template { get; set; }
}
