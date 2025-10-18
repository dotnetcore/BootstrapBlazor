// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// PopoverSelectBase 基类
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
    /// <see cref="BootstrapBlazorOptions"/> 配置类实例
    /// </summary>
    [Inject]
    [NotNull]
    protected IOptions<BootstrapBlazorOptions>? BootstrapBlazorOptions { get; set; }

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
        IsPopover |= BootstrapBlazorOptions.Value.IsPopover;
    }
}
