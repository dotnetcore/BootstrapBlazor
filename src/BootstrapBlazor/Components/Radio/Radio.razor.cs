// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Radio 单选框组件
/// </summary>
public partial class Radio<TValue> : Checkbox<TValue>
{
    /// <summary>
    /// 获得/设置 点击回调方法
    /// </summary>
    [Parameter]
    public Func<TValue, Task>? OnClick { get; set; }

    /// <summary>
    /// 获得/设置 子组件 RenderFragment 实例
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 Radio 组名称一般来讲需要设置 默认为 null 未设置
    /// </summary>
    [Parameter]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public string? GroupName { get; set; }

    /// <summary>
    /// 获得/设置 是否为按钮样式 默认 false
    /// </summary>
    [CascadingParameter]
    private CheckboxList<TValue>? Parent { get; set; }

    private bool IsButton => Parent?.IsButton ?? false;

    private async Task OnClickHandler()
    {
        if (OnClick != null)
        {
            await OnClick(Value);
        }
    }
}
