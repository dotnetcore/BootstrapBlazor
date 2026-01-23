// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">加载更多组件</para>
/// <para lang="en">加载更多component</para>
/// </summary>
public partial class LoadMore
{
    /// <summary>
    /// <para lang="zh">获得/设置 触底元素触发 <see cref="OnLoadMoreAsync"/> 阈值 默认为 1</para>
    /// <para lang="en">Gets or sets 触底元素触发 <see cref="OnLoadMoreAsync"/> 阈值 Default is为 1</para>
    /// </summary>
    [Parameter]
    public string Threshold { get; set; } = "1";

    /// <summary>
    /// <para lang="zh">获得/设置 触底回调方法 <see cref="CanLoading"/> 为 true 时才触发此回调方法</para>
    /// <para lang="en">Gets or sets 触底callback method <see cref="CanLoading"/> 为 true 时才触发此callback method</para>
    /// </summary>
    [Parameter] public Func<Task>? OnLoadMoreAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否可以加载更多数据 默认为 true</para>
    /// <para lang="en">Gets or sets whether可以加载更多data Default is为 true</para>
    /// </summary>
    [Parameter]
    public bool CanLoading { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 加载更多模板 默认 null</para>
    /// <para lang="en">Gets or sets 加载更多template Default is null</para>
    /// </summary>
    [Parameter]
    public RenderFragment? LoadingTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 没有更多数据提示信息 默认为 null 读取资源文件中的预设值</para>
    /// <para lang="en">Gets or sets 没有更多data提示信息 Default is为 null 读取资源文件中的预设值</para>
    /// </summary>
    [Parameter]
    public string? NoMoreText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 没有更多数据时显示的模板 默认为 null</para>
    /// <para lang="en">Gets or sets 没有更多data时display的template Default is为 null</para>
    /// </summary>
    [Parameter]
    public RenderFragment? NoMoreTemplate { get; set; }

    [Inject, NotNull]
    private IStringLocalizer<LoadMore>? Localizer { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        NoMoreText ??= Localizer[nameof(NoMoreText)];
    }

    private async Task OnIntersecting(IntersectionObserverEntry entry)
    {
        if (entry.IsIntersecting && CanLoading && OnLoadMoreAsync != null)
        {
            await OnLoadMoreAsync();
        }
    }
}
