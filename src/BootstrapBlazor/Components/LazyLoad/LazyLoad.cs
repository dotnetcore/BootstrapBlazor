﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components;

/// <summary>
/// 懒加载组件
/// </summary>
public partial class LazyLoad : ComponentBase
{
    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 组件加载条件回调方法 默认 null 未设置 一旦返回 true 后此回调将不再调用
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<Task<bool>>? OnLoadConditionCheckAsync { get; set; }

    /// <summary>
    /// 获得/设置 首次显示时回调方法 可用于组件初始化数据 仅触发一次
    /// </summary>
    [Parameter]
    public Func<Task>? OnFirstLoadCallbackAsync { get; set; }

    private bool _init;

    private bool _show;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        OnLoadConditionCheckAsync ??= () => Task.FromResult(true);

        _show = _show || await OnLoadConditionCheckAsync();

        if (_show)
        {
            await BeforeLoadCallbackAsync();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (_show)
        {
            builder.AddContent(0, ChildContent);
        }
    }

    private async Task BeforeLoadCallbackAsync()
    {
        if (!_init)
        {
            _init = true;
            if (OnFirstLoadCallbackAsync != null)
            {
                await OnFirstLoadCallbackAsync();
            }
        }
    }
}
