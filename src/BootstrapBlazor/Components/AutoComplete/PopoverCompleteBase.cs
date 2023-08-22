// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components;

/// <summary>
/// 弹窗可悬浮组件基类
/// </summary>
public abstract class PopoverCompleteBase<TValue> : BootstrapInputBase<TValue>, IPopoverBaseComponent
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public Placement Placement { get; set; } = Placement.Bottom;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public string? CustomClass { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool ShowShadow { get; set; } = true;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool IsPopover { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public string? Offset { get; set; }

    /// <summary>
    /// 获得/设置 防抖时间 默认为 0 即不开启
    /// </summary>
    [Parameter]
    public int Debounce { get; set; }

    /// <summary>
    /// 防抖时长字符串
    /// </summary>
    protected string? DurationString => Debounce > 0 ? $"{Debounce}" : null;

    /// <summary>
    /// data-bs-toggle 值
    /// </summary>
    protected string? ToggleString => IsPopover ? Constants.DropdownToggleString : null;

    /// <summary>
    /// 偏移量字符串
    /// </summary>
    protected string? OffsetString => IsPopover ? null : Offset;

    /// <summary>
    /// 输入框 Id
    /// </summary>
    protected string InputId => $"{Id}_input";

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

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Offset ??= "[0, 10]";
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override string? GetInputId() => InputId;
}
