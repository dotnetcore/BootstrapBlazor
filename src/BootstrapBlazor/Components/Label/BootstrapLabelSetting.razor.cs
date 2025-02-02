// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// BootstrapLabelSetting 组件类
/// </summary>
public partial class BootstrapLabelSetting
{
    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 标签宽度 默认 null 未设置使用全局设置 <code>--bb-row-label-width</code> 值
    /// </summary>
    [Parameter]
    public int? LabelWidth { get; set; }
}
