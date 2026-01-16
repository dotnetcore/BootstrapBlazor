// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// FilterProvider component
/// </summary>
public partial class FilterProvider
{
    /// <summary>
    /// <para lang="zh">获得/设置 重置按钮文本</para>
    /// <para lang="en">Get/Set Reset Button Text</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ClearButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 过滤按钮文本</para>
    /// <para lang="en">Get/Set Filter Button Text</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? FilterButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 增加过滤条件图标</para>
    /// <para lang="en">Get/Set Add Filter Condition Icon</para>
    /// </summary>
    [Parameter]
    public string? PlusIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 减少过滤条件图标</para>
    /// <para lang="en">Get/Set Remove Filter Condition Icon</para>
    /// </summary>
    [Parameter]
    public string? MinusIcon { get; set; }

    /// <summary>
    /// Gets or sets whether show the more button. Default is false.
    /// </summary>
    [Parameter]
    public bool ShowMoreButton { get; set; }

    /// <summary>
    /// Gets or sets the filter title. Default is null.
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the child content. Default is null.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="TableColumnFilter"/> instance from cascading parameter.
    /// </summary>
    [CascadingParameter]
    protected TableColumnFilter? TableColumnFilter { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<TableColumnFilter>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// Gets or sets the filter counter. Default is 0.
    /// </summary>
    protected int Count { get; set; }

    /// <summary>
    /// Gets or sets the column field key. Default is null.
    /// </summary>
    protected string? FieldKey { get; set; }

    /// <summary>
    /// Gets or sets whether the filter is header row. Default is false.
    /// </summary>
    protected bool IsHeaderRow { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        PlusIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableFilterPlusIcon);
        MinusIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableFilterMinusIcon);

        FilterButtonText ??= Localizer[nameof(FilterButtonText)];
        ClearButtonText ??= Localizer[nameof(ClearButtonText)];

        Title ??= TableColumnFilter.GetFilterTitle();
        FieldKey ??= TableColumnFilter.GetFieldKey();
        IsHeaderRow = TableColumnFilter.IsHeaderRow();
    }

    /// <summary>
    /// <para lang="zh">点击重置按钮时回调此方法</para>
    /// <para lang="en">Callback this method when clicking reset button</para>
    /// </summary>
    /// <returns></returns>
    private async Task OnClickReset()
    {
        Count = 0;
        if (TableColumnFilter != null)
        {
            await TableColumnFilter.Reset();
        }
        StateHasChanged();
    }

    /// <summary>
    /// <para lang="zh">点击确认时回调此方法</para>
    /// <para lang="en">Callback this method when clicking confirm</para>
    /// </summary>
    /// <returns></returns>
    protected async Task OnClickConfirm()
    {
        if (TableColumnFilter != null)
        {
            await TableColumnFilter.OnFilterAsync();
        }
    }

    /// <summary>
    /// <para lang="zh">点击增加按钮时回调此方法</para>
    /// <para lang="en">Callback this method when clicking add button</para>
    /// </summary>
    /// <returns></returns>
    private void OnClickPlus()
    {
        if (Count == 0)
        {
            Count++;
        }
    }

    /// <summary>
    /// <para lang="zh">点击减少按钮时回调此方法</para>
    /// <para lang="en">Callback this method when clicking remove button</para>
    /// </summary>
    /// <returns></returns>
    private void OnClickMinus()
    {
        if (Count == 1)
        {
            Count--;
        }
    }

    private FilterContext FilterContext => new()
    {
        Count = Count,
        FieldKey = FieldKey,
        IsHeaderRow = IsHeaderRow
    };
}
