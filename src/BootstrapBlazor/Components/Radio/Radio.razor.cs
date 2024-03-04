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

    private string? ClassString => CssBuilder.Default("form-check")
        .AddClass("is-checked", State == CheckboxState.Checked)
        .AddClass($"form-check-{Size.ToDescriptionString()}", Size != Size.None)
        .AddClass($"form-check-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass("disabled", IsDisabled)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private async Task OnClickHandler()
    {
        if (OnClick != null)
        {
            await OnClick(Value);
        }
    }
}
