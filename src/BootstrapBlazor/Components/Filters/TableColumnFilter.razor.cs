// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// TableColumnFilter 组件
/// </summary>
public partial class TableColumnFilter
{
    /// <summary>
    /// 获得/设置 过滤组件类型
    /// </summary>
    [Parameter, NotNull, EditorRequired]
    public Type? Filter { get; set; }

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
    /// 获得/设置 是否为 HeaderRow 模式 默认 false
    /// </summary>
    [Parameter]
    public bool? IsHeaderRow { get; set; }

    /// <summary>
    /// 获得/设置 <see cref="ITable"/> 实例
    /// </summary>
    [Parameter]
    [NotNull]
    public ITable? Table { get; set; }

    /// <summary>
    /// 获得/设置 <see cref="ITableColumn"/> 实例
    /// </summary>
    [Parameter]
    [NotNull]
    public ITableColumn? Column { get; set; }

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

        Table ??= TableFilter?.Table;
        Column ??= TableFilter?.Column;

        if (Column != null)
        {
            Title = Column.GetDisplayName();
            _fieldKey = Column.GetFieldName();
        }
    }

    /// <summary>
    /// 点击重置按钮时回调此方法
    /// </summary>
    /// <returns></returns>
    protected async Task OnClickReset()
    {
        if (Table != null)
        {
            TableFilter.FilterAction.Reset();
            table.Filters.Remove(FieldKey);

            if (table.OnFilterAsync != null)
            {
                await table.OnFilterAsync();
            }
        }
    }

    /// <summary>
    /// 点击确认时回调此方法
    /// </summary>
    /// <returns></returns>
    protected async Task OnClickConfirm()
    {
        var table = TableFilter.Table;
        if (table != null)
        {
            var f = GetFilterConditions();
            if (f.Filters is { Count: > 0 })
            {
                table.Filters[FieldKey] = TableFilter.FilterAction;
            }
            else
            {
                table.Filters.Remove(FieldKey);
            }
            if (table.OnFilterAsync != null)
            {
                await table.OnFilterAsync();
            }
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

    private RenderFragment RenderFilter() => builder =>
    {
        if (Filter != null)
        {
            builder.OpenComponent(0, Filter);
            builder.AddAttribute(1, nameof(IFilter.FieldKey), FieldKey);
            builder.AddAttribute(2, nameof(IFilter.Table), Table);
            builder.AddAttribute(3, nameof(IFilter.IsActive), IsActive);
            builder.AddAttribute(4, nameof(IFilter.IsHeaderRow), IsHeaderRow);
            builder.AddAttribute(5, nameof(IFilter.NotSupportedMessage), NotSupportedMessage);
            builder.CloseComponent();
        }
    };
}
