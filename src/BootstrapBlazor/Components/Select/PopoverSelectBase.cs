// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">PopoverSelectBase 基类</para>
/// <para lang="en">PopoverSelectBase Base Class</para>
/// </summary>
/// <typeparam name="TValue"></typeparam>
public abstract class PopoverSelectBase<TValue> : PopoverDropdownBase<TValue>
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否使用 Popover 渲染下拉框 默认 false</para>
    /// <para lang="en">Gets or sets Whether to use Popover to render dropdown. Default false</para>
    /// </summary>
    [Parameter]
    public bool IsPopover { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 弹窗偏移量 默认 [0, 10]</para>
    /// <para lang="en">Gets or sets Popover Offset. Default [0, 10]</para>
    /// </summary>
    [Parameter]
    public string? Offset { get; set; }

    /// <summary>
    /// <para lang="zh"><see cref="BootstrapBlazorOptions"/> 配置类实例</para>
    /// <para lang="en"><see cref="BootstrapBlazorOptions"/> Config Instance</para>
    /// </summary>
    [Inject]
    [NotNull]
    protected IOptions<BootstrapBlazorOptions>? BootstrapBlazorOptions { get; set; }

    /// <summary>
    /// <para lang="zh">data-bs-toggle 值</para>
    /// <para lang="en">data-bs-toggle Value</para>
    /// </summary>
    protected string? ToggleString => IsPopover ? Constants.DropdownToggleString : "dropdown";

    /// <summary>
    /// <para lang="zh">偏移量字符串</para>
    /// <para lang="en">Offset String</para>
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
