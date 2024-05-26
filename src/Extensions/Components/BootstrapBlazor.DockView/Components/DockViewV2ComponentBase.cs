// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public class DockViewV2ComponentBase : IdComponentBase
{
    /// <summary>
    /// 获得/设置 组件名称 默认 component golden-layout 渲染使用
    /// </summary>
    [Parameter]
    public string ComponentName { get; set; } = "component";

    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 父容器级联参数
    /// </summary>
    [CascadingParameter, NotNull]
    protected List<DockViewV2Panel>? Panels { get; set; }
}
