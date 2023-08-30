// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// QueryBuilder 组件
/// </summary>
#if NET6_0_OR_GREATER
[CascadingTypeParameter(nameof(TModel))]
#endif
public partial class QueryBuilder<TModel> where TModel : notnull, new()
{
    private string? ClassString => CssBuilder.Default()
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 过滤模型 <see cref="FilterKeyValueAction"/> 实例值
    /// </summary>
    [Parameter]
    [NotNull]
    public FilterKeyValueAction? Filter { get; set; }

    /// <summary>
    /// 获得/设置 Filter 回调方法 支持双向绑定
    /// </summary>
    [Parameter]
    public EventCallback<FilterKeyValueAction>? FilterChanged { get; set; }

    /// <summary>
    /// 获得/设置 逻辑运算符
    /// </summary>
    [Parameter]
    public FilterLogic logic { get; set; }

    /// <summary>
    /// 获得/设置 模板
    /// </summary>
    [Parameter]
    public RenderFragment<TModel>? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 首次加载是否显示加载骨架屏 默认 false 不显示 使用 <see cref="ShowLoadingInFirstRender" /> 参数值
    /// </summary>
    [Parameter]
    public bool ShowSkeleton { get; set; }

    /// <summary>
    /// 获得/设置 首次加载是否显示加载动画 默认 true 显示 设置 <see cref="ShowSkeleton"/> 值覆盖此参数
    /// </summary>
    [Parameter]
    public bool ShowLoadingInFirstRender { get; set; } = true;

    /// <summary>
    /// 获得/设置 增加过滤条件图标
    /// </summary>
    [Parameter]
    public string? PlusIcon { get; set; }

    /// <summary>
    /// 获得/设置 减少过滤条件图标
    /// </summary>
    [Parameter]
    public string? MinusIcon { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<QueryBuilder<TModel>>? Localizer { get; set; }

    private List<FilterKeyValueAction> _filters = new();

    private bool _inited = false;

    private List<SelectedItem>? Operations { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        PlusIcon ??= IconTheme.GetIconByKey(ComponentIcons.QueryBuilderPlusIcon);
        MinusIcon ??= IconTheme.GetIconByKey(ComponentIcons.QueryBuilderMinusIcon);
        Filter ??= new();

        Operations ??= new()
        {
            new SelectedItem("GreaterThanOrEqual", Localizer["GreaterThanOrEqual"].Value),
            new SelectedItem("LessThanOrEqual", Localizer["LessThanOrEqual"].Value),
            new SelectedItem("GreaterThan", Localizer["GreaterThan"].Value),
            new SelectedItem("LessThan", Localizer["LessThan"].Value),
            new SelectedItem("Equal", Localizer["Equal"].Value),
            new SelectedItem("NotEqual", Localizer["NotEqual"].Value ),
            new SelectedItem("Contains", Localizer["Contains"].Value ),
            new SelectedItem("NotContains", Localizer["NotContains"].Value )
        };
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (firstRender)
        {
            _inited = true;
            StateHasChanged();
        }
    }
}
