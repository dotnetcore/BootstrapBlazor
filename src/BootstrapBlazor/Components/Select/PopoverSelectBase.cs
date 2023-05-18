// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TValue"></typeparam>
public abstract class PopoverSelectBase<TValue> : PopoverDropdownBase<TValue>
{
    /// <summary>
    /// 获得/设置 是否使用 Popover 渲染下拉框 默认 false
    /// </summary>
    [Parameter]
    public bool IsPopover { get; set; }

    /// <summary>
    /// 获得/设置 弹窗偏移量 默认 [0, 10]
    /// </summary>
    [Parameter]
    public string? Offset { get; set; }

    /// <summary>
    /// data-bs-toggle 值
    /// </summary>
    protected string? ToggleString => IsPopover ? Constants.DropdownToggleString : "dropdown";

    /// <summary>
    /// 偏移量字符串
    /// </summary>
    protected string? OffsetString => IsPopover ? null : Offset;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Offset ??= "[0, 10]";
    }
}
