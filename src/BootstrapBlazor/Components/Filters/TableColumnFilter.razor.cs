// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// TableColumnFilter 组件
/// </summary>
public partial class TableColumnFilter<TFilter> where TFilter : IComponent
{
    /// <summary>
    /// 获得/设置 过滤器组件参数集合 Default is null
    /// </summary>
    [Parameter]
    public IDictionary<string, object>? FilterParameters { get; set; }

    /// <summary>
    /// 获得/设置 重置按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ClearButtonText { get; set; }

    /// <summary>
    /// 获得/设置 过滤按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? FilterButtonText { get; set; }

    /// <summary>
    /// 获得/设置 Header 显示文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Title { get; set; }

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

    /// <summary>
    /// Gets or sets whether show the more button. Default is false.
    /// </summary>
    [Parameter]
    public bool ShowMoreButton { get; set; }

    [CascadingParameter]
    private TableFilter? TableFilter { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<TableFilter>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private int _count;
    private string? _fieldKey;
    private bool _isHeaderRow = false;

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

        _isHeaderRow = TableFilter.IsHeaderRow();
        _fieldKey = TableFilter.GetFieldKey();
    }

    /// <summary>
    /// 点击重置按钮时回调此方法
    /// </summary>
    /// <returns></returns>
    protected void OnClickReset()
    {
        TableFilter?.Reset();
    }

    /// <summary>
    /// 点击确认时回调此方法
    /// </summary>
    /// <returns></returns>
    protected async Task OnClickConfirm()
    {
        if (TableFilter != null)
        {
            await TableFilter.OnFilterAsync();
        }
    }

    /// <summary>
    /// 点击增加按钮时回调此方法
    /// </summary>
    /// <returns></returns>
    private void OnClickPlus()
    {
        if (_count == 0)
        {
            _count++;
        }
    }

    /// <summary>
    /// 点击减少按钮时回调此方法
    /// </summary>
    /// <returns></returns>
    private void OnClickMinus()
    {
        if (_count == 1)
        {
            _count--;
        }
    }

    /// <summary>
    /// 渲染自定义过滤器方法
    /// </summary>
    /// <returns></returns>
    protected virtual RenderFragment RenderFilter() => builder =>
    {
        var filterType = typeof(TFilter);
        builder.OpenComponent<TFilter>(0);
        if (filterType.IsSubclassOf(typeof(FilterBase)))
        {
            builder.AddAttribute(1, nameof(FilterBase.FieldKey), _fieldKey);
            builder.AddAttribute(2, nameof(FilterBase.IsHeaderRow), _isHeaderRow);
        }
        if (filterType.IsSubclassOf(typeof(MultipleFilterBase)))
        {
            builder.AddAttribute(10, nameof(MultipleFilterBase.Count), _count);
        }

        if (FilterParameters != null)
        {
            builder.AddMultipleAttributes(100, FilterParameters);
        }
        builder.CloseComponent();
    };
}
