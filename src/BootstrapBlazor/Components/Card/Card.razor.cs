// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Card组件基类
/// </summary>
public partial class Card
{
    /// <summary>
    /// Card 组件样式
    /// </summary>
    protected string? ClassString => CssBuilder.Default("card")
        .AddClass("text-center", IsCenter)
        .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass("card-shadow", IsShadow)
        .AddClass("is-collapsable", IsCollapsible)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 收缩图标样式
    /// </summary>
    protected string? ArrowClassString => CssBuilder.Default("card-collapse-arrow")
        .AddClass(CollapseIcon)
        .Build();

    /// <summary>
    ///  设置 Body Class 组件样式
    /// </summary>
    protected string? BodyClassName => CssBuilder.Default("card-body")
        .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass("collapse", IsCollapsible && Collapsed)
        .AddClass("collapse show", IsCollapsible && !Collapsed)
        .Build();

    /// <summary>
    /// 节点是否展开 aria Label
    /// </summary>
    protected string? ExpandedString => Collapsed ? "false" : "true";

    /// <summary>
    /// 设置 Footer Class 样式
    /// </summary>
    protected string? FooterClassName => CssBuilder.Default("card-footer")
        .AddClass("text-muted", IsCenter)
        .Build();

    /// <summary>
    /// 获得/设置 收缩展开箭头图标 默认 fa-solid fa-circle-chevron-right
    /// </summary>
    [Parameter]
    public string? CollapseIcon { get; set; }

    /// <summary>
    /// 获得/设置 HeaderTemplate 显示文本
    /// </summary>
    [Parameter]
    public string? HeaderText { get; set; }

    /// <summary>
    /// 获得/设置 CardHeard 模板
    /// </summary>
    [Parameter]
    public RenderFragment? HeaderTemplate { get; set; }

    /// <summary>
    /// 获得/设置 BodyTemplate 模板
    /// </summary>
    [Parameter]
    public RenderFragment? BodyTemplate { get; set; }

    /// <summary>
    /// 获得/设置 FooterTemplate 模板
    /// </summary>
    [Parameter]
    public RenderFragment? FooterTemplate { get; set; }

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
    /// 获得/设置 是否收缩 默认 false 展开
    /// </summary>
    [Parameter]
    public bool Collapsed { get; set; }

    /// <summary>
    /// 获得/设置 是否显示阴影 默认 false
    /// </summary>
    [Parameter]
    public bool IsShadow { get; set; }

    [Inject]
    [NotNull]
    private IOptions<IconMapperOptions>? IconOptions { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        CollapseIcon ??= IconOptions.Value.GetIcon(ComponentIcons.CollapseIcon, Constants.CollapseIcon);
    }
}
