// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">Repeat 组件</para>
///  <para lang="en">Repeater Component</para>
/// </summary>
/// <typeparam name="TItem"></typeparam>
public partial class Repeater<TItem>
{
    private string? RepeaterClassString => CssBuilder.Default("repeater")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    ///  <para lang="zh">获得/设置 数据源</para>
    ///  <para lang="en">Get/Set Items</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public IEnumerable<TItem>? Items { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示正在加载信息 默认 true 显示</para>
    ///  <para lang="en">Get/Set Whether to show loading info. Default true</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowLoading { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 正在加载模板</para>
    ///  <para lang="en">Get/Set Loading Template</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? LoadingTemplate { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示无数据信息 默认 true 显示</para>
    ///  <para lang="en">Get/Set Whether to show empty info. Default true</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowEmpty { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 无数据时提示信息 默认 null</para>
    ///  <para lang="en">Get/Set Empty Text. Default null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? EmptyText { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 正在加载模板</para>
    ///  <para lang="en">Get/Set Empty Template</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? EmptyTemplate { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 容器模板</para>
    ///  <para lang="en">Get/Set Container Template</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<RenderFragment>? ContainerTemplate { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 模板</para>
    ///  <para lang="en">Get/Set Item Template</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? ItemTemplate { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Repeater<TItem>>? Localizer { get; set; }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
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
