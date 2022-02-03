// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// Card组件基类
/// </summary>
public abstract class CardBase : BootstrapComponentBase
{
    /// <summary>
    /// Card 组件样式
    /// </summary>
    protected virtual string? ClassName => CssBuilder.Default("card")
        .AddClass("text-center", IsCenter)
        .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass("card-shadow", IsShadow)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    ///  设置 Body Class 组件样式
    /// </summary>
    protected virtual string? BodyClassName => CssBuilder.Default("card-body")
        .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None)
        .Build();

    /// <summary>
    /// 设置 Footer Class 样式
    /// </summary>
    protected virtual string? FooterClassName => CssBuilder.Default("card-footer")
        .AddClass("text-muted", IsCenter)
        .Build();

    /// <summary>
    /// 获得/设置 CardHeader 显示文本
    /// </summary>
    [Parameter]
    public string? HeaderText { get; set; }

    /// <summary>
    /// 获得/设置 CardHeard 模板
    /// </summary>
    [Parameter]
    public RenderFragment? CardHeader { get; set; }

    /// <summary>
    /// 获得/设置 CardBody 模板
    /// </summary>
    [Parameter]
    public RenderFragment? CardBody { get; set; }

    /// <summary>
    /// 获得/设置 CardFooter 模板
    /// </summary>
    [Parameter]
    public RenderFragment? CardFooter { get; set; }

    /// <summary>
    /// 获得/设置 Card 颜色
    /// </summary>
    [Parameter]
    public Color Color { get; set; }

    /// <summary>
    /// 获得/设置 是否居中 默认 false
    /// </summary>
    [Parameter]
    public bool IsCenter { get; set; }

    /// <summary>
    /// 获得/设置 是否可收缩 默认 false
    /// </summary>
    [Parameter]
    public bool IsCollapsible { get; set; }

    /// <summary>
    /// 获得/设置 是否显示阴影 默认 false
    /// </summary>
    [Parameter]
    public bool IsShadow { get; set; }

    /// <summary>
    /// 
    /// </summary>
    protected ElementReference CardEelement { get; set; }

    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            if (IsCollapsible)
            {
                await JSRuntime.InvokeVoidAsync(CardEelement, "bb_card_collapse");
            }
        }
    }
}
