// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// PulseButton 按钮组件
/// </summary>
public partial class PulseButton
{
    /// <summary>
    /// 获得/设置 显示图片地址 默认为 null
    /// </summary>
    [Parameter]
    public string? ImageUrl { get; set; }

    /// <summary>
    /// 获得/设置 心跳环颜色 默认 <see cref="Color.Warning"/>
    /// </summary>
    [Parameter]
    public Color PulseColor { get; set; } = Color.Warning;

    private string? ButtonClassName => CssBuilder.Default(ClassName)
        .AddClass("btn-pulse")
        .Build();

    private string? PulseColorString => CssBuilder.Default("pulse-ring border")
        .AddClass($"border-{PulseColor.ToDescriptionString()}", PulseColor != Color.None)
        .Build();

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (ButtonStyle == ButtonStyle.None)
        {
            ButtonStyle = ButtonStyle.Circle;
        }
    }
}
