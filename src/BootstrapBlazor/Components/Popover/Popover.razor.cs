﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
    protected override void OnInitialized()
    {
        base.OnInitialized();

        ToggleString = "popover";
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task InvokeInitAsync()
    {
        if (!string.IsNullOrEmpty(Content))
        {
            await InvokeVoidAsync("init", Id, Content);
        }
    }

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

        if (_lastContent != Content)
        {
            _lastContent = Content;
            await InvokeInitAsync();
        }
    }
}
