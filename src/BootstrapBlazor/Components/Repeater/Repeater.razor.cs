// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Repeat 组件</para>
/// <para lang="en">Repeater Component</para>
/// </summary>
/// <typeparam name="TItem"></typeparam>
public partial class Repeater<TItem>
{
    private string? RepeaterClassString => CssBuilder.Default("repeater")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 数据源</para>
    /// <para lang="en">Gets or sets the items</para>
    /// </summary>
    [Parameter]
    public IEnumerable<TItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示正在加载信息，默认为 true</para>
    /// <para lang="en">Gets or sets whether to show loading info. Default is true</para>
    /// </summary>
    [Parameter]
    public bool ShowLoading { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 正在加载模板</para>
    /// <para lang="en">Gets or sets the loading template</para>
    /// </summary>
    [Parameter]
    public RenderFragment? LoadingTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示无数据信息，默认为 true</para>
    /// <para lang="en">Gets or sets whether to show empty info. Default is true</para>
    /// </summary>
    [Parameter]
    public bool ShowEmpty { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 无数据时提示信息，默认为 null</para>
    /// <para lang="en">Gets or sets the empty text. Default is null</para>
    /// </summary>
    [Parameter]
    public string? EmptyText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 无数据时的模板</para>
    /// <para lang="en">Gets or sets the empty template</para>
    /// </summary>
    [Parameter]
    public RenderFragment? EmptyTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 容器模板</para>
    /// <para lang="en">Gets or sets the container template</para>
    /// </summary>
    [Parameter]
    public RenderFragment<RenderFragment>? ContainerTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 项模板</para>
    /// <para lang="en">Gets or sets the item template</para>
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

    private RenderFragment RenderItemTemplate(IEnumerable<TItem> items) => builder =>
    {
        if (ItemTemplate != null)
        {
            foreach (var item in items)
            {
                builder.AddContent(0, ItemTemplate(item));
            }
        }
    };
}
