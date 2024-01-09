// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TValue"></typeparam>
public abstract class PopoverDropdownBase<TValue> : ValidateBase<TValue>
{
    /// <summary>
    /// 获得/设置 弹窗位置 默认为 Bottom
    /// </summary>
    [Parameter]
    public Placement Placement { get; set; } = Placement.Bottom;

    /// <summary>
    /// 获得/设置 自定义样式 参数 默认 null
    /// </summary>
    /// <remarks>由 data-bs-custom-class 实现</remarks>
    [Parameter]
    public string? CustomClass { get; set; }

    /// <summary>
    /// 获得/设置 是否显示阴影 默认 true
    /// </summary>
    [Parameter]
    public bool ShowShadow { get; set; } = true;

    /// <summary>
    /// 弹窗位置字符串
    /// </summary>
    protected string? PlacementString => Placement == Placement.Auto ? null : Placement.ToDescriptionString();

    /// <summary>
    /// 获得 CustomClass 字符串
    /// </summary>
    protected virtual string? CustomClassString => CssBuilder.Default(CustomClass)
        .AddClass("shadow", ShowShadow)
        .Build();
}
