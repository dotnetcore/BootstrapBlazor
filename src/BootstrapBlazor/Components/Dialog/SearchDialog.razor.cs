// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">查询弹窗组件</para>
/// <para lang="en">Search Dialog Component</para>
/// </summary>
public partial class SearchDialog<TModel>
{
    /// <summary>
    /// <para lang="zh">获得/设置 重置回调委托</para>
    /// <para lang="en">Gets or sets Reset Callback Delegate</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<Task>? OnResetSearchClick { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 搜索回调委托</para>
    /// <para lang="en">Gets or sets Search Callback Delegate</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<Task>? OnSearchClick { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 重置按钮文本</para>
    /// <para lang="en">Gets or sets Reset Button Text</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ResetButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 查询按钮文本</para>
    /// <para lang="en">Gets or sets Query Button Text</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? QueryButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 清空按钮图标</para>
    /// <para lang="en">Gets or sets Clear Button Icon</para>
    /// </summary>
    [Parameter]
    public string? ClearIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 搜索按钮图标</para>
    /// <para lang="en">Gets or sets Search Button Icon</para>
    /// </summary>
    [Parameter]
    public string? SearchIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否使用 SearchForm 组件进行搜索条件编辑 默认 false 不使用</para>
    /// <para lang="en">Gets or sets Whether to use SearchForm component for editing search conditions. Default is false</para>
    /// </summary>
    [Parameter]
    public bool UseSearchForm { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 搜索表单项集合</para>
    /// <para lang="en">Gets or sets Search Form Items collection</para>
    /// </summary>
    [Parameter]
    public List<ISearchItem>? SearchItems { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 过滤器改变回调事件 Func 版本</para>
    /// <para lang="en">Gets or sets the filter changed callback event Func version</para>
    /// </summary>
    [Parameter]
    public Func<FilterKeyValueAction, Task>? OnChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 搜索表单本地化配置项</para>
    /// <para lang="en">Gets or sets Search Form Localization Options</para>
    /// </summary>
    [Parameter]
    public SearchFormLocalizerOptions? SearchFormLocalizerOptions { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<SearchDialog<TModel>>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ResetButtonText ??= Localizer[nameof(ResetButtonText)];
        QueryButtonText ??= Localizer[nameof(QueryButtonText)];

        ClearIcon ??= IconTheme.GetIconByKey(ComponentIcons.SearchDialogClearIcon);
        SearchIcon ??= IconTheme.GetIconByKey(ComponentIcons.SearchDialogSearchIcon);

        if (UseSearchForm)
        {
            return;
        }

        if (BodyTemplate != null)
        {
            return;
        }

        Items ??= GetItemsByColumns();
    }

    private async Task OnSearchFormFilterChanged(FilterKeyValueAction action)
    {
        // 通知父组件过滤器改变事件，此时并没有触发 OnSearchClick 搜索事件，父组件可以在 OnChanged 事件中获取当前过滤器状态并决定是否触发搜索事件
        if (OnChanged != null)
        {
            await OnChanged(action);
        }
    }

    private RenderFragment RenderButtons => builder =>
    {
        builder.OpenComponent<DialogCloseButton>(0);
        builder.AddAttribute(10, nameof(DialogCloseButton.Icon), ClearIcon);
        builder.AddAttribute(20, nameof(DialogCloseButton.Text), ResetButtonText);
        builder.AddAttribute(30, nameof(DialogCloseButton.OnClickWithoutRender), OnResetSearchClick);
        builder.CloseComponent();

        builder.OpenComponent<DialogCloseButton>(100);
        builder.AddAttribute(101, nameof(DialogCloseButton.Color), Color.Primary);
        builder.AddAttribute(110, nameof(DialogCloseButton.Icon), SearchIcon);
        builder.AddAttribute(120, nameof(DialogCloseButton.Text), QueryButtonText);
        builder.AddAttribute(130, nameof(DialogCloseButton.OnClickWithoutRender), OnSearchClick);
        builder.CloseComponent();
    };
}
