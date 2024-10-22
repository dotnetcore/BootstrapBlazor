// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
