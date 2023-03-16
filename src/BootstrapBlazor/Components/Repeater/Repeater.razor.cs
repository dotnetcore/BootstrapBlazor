// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Repeat 组件
/// </summary>
/// <typeparam name="TItem"></typeparam>
public partial class Repeater<TItem>
{
    private string? RepeaterClassString => CssBuilder.Default("repeater")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 数据源
    /// </summary>
    [Parameter]
    public IEnumerable<TItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 是否显示正在加载信息 默认 true 显示
    /// </summary>
    [Parameter]
    public bool ShowLoading { get; set; } = true;

    /// <summary>
    /// 获得/设置 正在加载模板
    /// </summary>
    [Parameter]
    public RenderFragment? LoadingTemplate { get; set; }

    /// <summary>
    /// 获得/设置 是否显示无数据信息 默认 true 显示
    /// </summary>
    [Parameter]
    public bool ShowEmpty { get; set; } = true;

    /// <summary>
    /// 获得/设置 无数据时提示信息 默认 null
    /// </summary>
    [Parameter]
    public string? EmptyText { get; set; }

    /// <summary>
    /// 获得/设置 正在加载模板
    /// </summary>
    [Parameter]
    public RenderFragment? EmptyTemplate { get; set; }

    /// <summary>
    /// 获得/设置 模板
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? ItemTemplate { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Repeater<TItem>>? Localizer { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        EmptyText ??= Localizer[nameof(EmptyText)];
    }
}
