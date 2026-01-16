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
    /// <para lang="en">Get/Set Reset Callback Delegate</para>
    /// </summary>
    /// <returns></returns>
    [Parameter]
    [NotNull]
    public Func<Task>? OnResetSearchClick { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 搜索回调委托</para>
    /// <para lang="en">Get/Set Search Callback Delegate</para>
    /// </summary>
    /// <returns></returns>
    [Parameter]
    [NotNull]
    public Func<Task>? OnSearchClick { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 重置按钮文本</para>
    /// <para lang="en">Get/Set Reset Button Text</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ResetButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 查询按钮文本</para>
    /// <para lang="en">Get/Set Query Button Text</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? QueryButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 清空按钮图标</para>
    /// <para lang="en">Get/Set Clear Button Icon</para>
    /// </summary>
    [Parameter]
    public string? ClearIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 搜索按钮图标</para>
    /// <para lang="en">Get/Set Search Button Icon</para>
    /// </summary>
    [Parameter]
    public string? SearchIcon { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<SearchDialog<TModel>>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <para lang="zh">OnParametersSet 方法</para>
    /// <para lang="en">OnParametersSet Method</para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ResetButtonText ??= Localizer[nameof(ResetButtonText)];
        QueryButtonText ??= Localizer[nameof(QueryButtonText)];

        ClearIcon ??= IconTheme.GetIconByKey(ComponentIcons.SearchDialogClearIcon);
        SearchIcon ??= IconTheme.GetIconByKey(ComponentIcons.SearchDialogSearchIcon);
    }
}
