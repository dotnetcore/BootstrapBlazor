// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// DateTimePicker 按钮组件
/// </summary>
public partial class DateTimePickerButton
{
    /// <summary>
    /// 获得/设置 OnClick 事件
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    /// <summary>
    /// 获得/设置 OnClick 事件不刷新父组件
    /// </summary>
    [Parameter]
    public Func<Task>? OnClickWithoutRender { get; set; }

    /// <summary>
    /// 获得/设置 按钮颜色 默认 <see cref="Color.Primary"/>
    /// </summary>
    [Parameter]
    public virtual Color Color { get; set; } = Color.Primary;

    /// <summary>
    /// 获得/设置 显示图标
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 显示文字
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 Outline 样式 默认 false
    /// </summary>
    [Parameter]
    public bool IsOutline { get; set; }

    /// <summary>
    /// 获得/设置 Size 大小
    /// </summary>
    [Parameter]
    public Size Size { get; set; }

    /// <summary>
    /// 获得/设置 Block 模式
    /// </summary>
    [Parameter]
    public bool IsBlock { get; set; }

    /// <summary>
    /// 获得/设置 是否禁用 默认为 false
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (Tooltip != null && !string.IsNullOrEmpty(TooltipText))
        {
            Tooltip.SetParameters(TooltipText, TooltipPlacement, TooltipTrigger);
        }
    }
}
