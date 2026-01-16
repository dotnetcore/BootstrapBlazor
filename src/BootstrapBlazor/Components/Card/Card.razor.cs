// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Card 组件</para>
/// <para lang="en">Card component</para>
/// </summary>
public partial class Card
{
    /// <summary>
    /// <para lang="zh">Card 组件样式</para>
    /// <para lang="en">Card component style</para>
    /// </summary>
    protected string? ClassString => CssBuilder.Default("card")
        .AddClass("text-center", IsCenter)
        .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass("card-shadow", IsShadow)
        .AddClass("is-collapsible", IsCollapsible)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 收缩图标样式</para>
    /// <para lang="en">Get the collapse icon style</para>
    /// </summary>
    protected string? ArrowClassString => CssBuilder.Default("card-collapse-arrow")
        .AddClass(CollapseIcon)
        .Build();

    /// <summary>
    /// <para lang="zh">设置 Body Class 组件样式</para>
    /// <para lang="en">Set Body Class component style</para>
    /// </summary>
    protected string? BodyClassName => CssBuilder.Default("card-body")
        .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass("collapse", IsCollapsible && Collapsed)
        .AddClass("collapse show", IsCollapsible && !Collapsed)
        .Build();

    /// <summary>
    /// <para lang="zh">节点是否展开 aria Label</para>
    /// <para lang="en">Node expansion status aria Label</para>
    /// </summary>
    protected string? ExpandedString => Collapsed ? "false" : "true";

    /// <summary>
    /// <para lang="zh">设置 Footer Class 样式</para>
    /// <para lang="en">Set Footer Class style</para>
    /// </summary>
    protected string? FooterClassName => CssBuilder.Default("card-footer")
        .AddClass("text-muted", IsCenter)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 Card Header 高度 padding Y轴值 默认 null</para>
    /// <para lang="en">Gets or sets the Card Header height padding Y-axis value. Default is null</para>
    /// <para lang="zh">单位需自行给定 如 0.25rem</para>
    /// <para lang="en">Units need to be given by yourself, such as 0.25rem</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? HeaderPaddingY { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 收缩展开箭头图标 默认 fa-solid fa-circle-chevron-right</para>
    /// <para lang="en">Gets or sets the collapse/expand arrow icon. Default is fa-solid fa-circle-chevron-right</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? CollapseIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 HeaderTemplate 显示文本</para>
    /// <para lang="en">Gets or sets the HeaderTemplate display text</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? HeaderText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 CardHeard 模板</para>
    /// <para lang="en">Gets or sets the CardHeader template</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? HeaderTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 BodyTemplate 模板</para>
    /// <para lang="en">Gets or sets the BodyTemplate template</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? BodyTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 FooterTemplate 模板</para>
    /// <para lang="en">Gets or sets the FooterTemplate template</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? FooterTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Card 颜色</para>
    /// <para lang="en">Gets or sets the Card color</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Color Color { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否居中 默认 false</para>
    /// <para lang="en">Gets or sets whether to center. Default is false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsCenter { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否可收缩 默认 false</para>
    /// <para lang="en">Gets or sets whether it is collapsible. Default is false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsCollapsible { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否收缩 默认 false 展开</para>
    /// <para lang="en">Gets or sets whether it is collapsed. Default is false (expanded)</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool Collapsed { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否收缩 默认 false 展开</para>
    /// <para lang="en">Gets or sets whether it is collapsed. Default is false (expanded)</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public EventCallback<bool> CollapsedChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示阴影 默认 false</para>
    /// <para lang="en">Gets or sets whether to show shadow. Default is false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsShadow { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private string? HeaderStyleString => CssBuilder.Default()
        .AddClass($"--bs-card-cap-padding-y: {HeaderPaddingY};", !string.IsNullOrEmpty(HeaderPaddingY))
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        CollapseIcon ??= IconTheme.GetIconByKey(ComponentIcons.CardCollapseIcon);
    }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(ToggleCollapse));

    private string? BodyId => $"{Id}_body";

    /// <summary>
    /// <para lang="zh">The callback click collapse button</para>
    /// <para lang="en">The callback click collapse button</para>
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task ToggleCollapse(bool collapsed)
    {
        Collapsed = collapsed;
        if (CollapsedChanged.HasDelegate)
        {
            await CollapsedChanged.InvokeAsync(Collapsed);
        }
    }
}
