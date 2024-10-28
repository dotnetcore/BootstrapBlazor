﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Popover 弹出窗组件
/// </summary>
public partial class Popover
{
    /// <summary>
    /// 获得/设置 显示文字，复杂内容可通过 <see cref="Template"/> 自定义
    /// </summary>
    [Parameter]
    public string? Content { get; set; }

    /// <summary>
    /// 获得/设置 是否显示阴影 默认 true
    /// </summary>
    [Parameter]
    public bool ShowShadow { get; set; } = true;

    /// <summary>
    /// 获得/设置 内容模板 默认 null 设置值后 <see cref="Content"/> 参数失效
    /// </summary>
    [Parameter]
    public RenderFragment? Template { get; set; }

    private string? _lastContent;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override string? CustomClassString => CssBuilder.Default(CustomClass)
        .AddClass("shadow", ShowShadow)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, new { Content, Template = Template != null });

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _lastContent = Content;
        }
        else if (_lastContent != Content)
        {
            _lastContent = Content;
            await InvokeInitAsync();
        }
    }
}
