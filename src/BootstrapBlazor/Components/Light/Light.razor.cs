// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class Light
{
    /// <summary>
    /// 获得 组件样式
    /// </summary>
    protected string? ClassString => CssBuilder.Default("light")
        .AddClass("flash", IsFlash)
        .AddClass($"light-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 组件是否闪烁 默认为 false 不闪烁
    /// </summary>
    [Parameter]
    public bool IsFlash { get; set; }

    /// <summary>
    /// 获得/设置 指示灯 Tooltip 显示文字
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// 获得/设置 指示灯颜色 默认为 Success 绿色
    /// </summary>
    [Parameter]
    public Color Color { get; set; } = Color.Success;

    /// <summary>
    /// 获得/设置 位置 默认为底部 Bottom
    /// </summary>
    [Parameter]
    public Placement Placement { get; set; } = Placement.Bottom;

    /// <summary>
    /// 获得/设置 触发方式 可组合 click focus hover 默认为 focus hover
    /// </summary>
    [Parameter]
    public string Trigger { get; set; } = "focus hover";

    private bool Rendered { get; set; }

    /// <summary>
    /// OnParametersSetAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnParametersSetAsync()
    {
        if (Rendered)
        {
            if (string.IsNullOrEmpty(Title))
            {
                await ShowTooltip("", "dispose");
            }
            else
            {
                await ShowTooltip(Title, "");
            }
        }
    }

    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Rendered = true;
            if (!string.IsNullOrEmpty(Title))
            {
                await ShowTooltip(Title, "");
            }
        }
    }

    private async ValueTask ShowTooltip(string title, string method)
    {
        if (!string.IsNullOrEmpty(Id))
        {
            await JSRuntime.InvokeVoidAsync(null, "bb_tooltip", Id, method, title, Placement.ToDescriptionString(), false, Trigger);
        }
    }
}
