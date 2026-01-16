// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">懒加载组件
///</para>
/// <para lang="en">懒加载component
///</para>
/// </summary>
public partial class LazyLoad : ComponentBase
{
    /// <summary>
    /// <para lang="zh">获得/设置 子组件
    ///</para>
    /// <para lang="en">Gets or sets 子component
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件加载条件回调方法 默认 null 未设置 一旦返回 true 后此回调将不再调用
    ///</para>
    /// <para lang="en">Gets or sets component加载条件callback method Default is null 未Sets 一旦返回 true 后此回调将不再调用
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<Task<bool>>? OnLoadConditionCheckAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 首次显示时回调方法 可用于组件初始化数据 仅触发一次
    ///</para>
    /// <para lang="en">Gets or sets 首次display时callback method 可用于component初始化data 仅触发一次
    ///</para>
    /// <para><version>10.2.2</version></para>
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
