// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 枚举类型过滤组件
/// </summary>
public partial class TableColumnEnumFilter
{
    /// <summary>
    /// 内部使用
    /// </summary>
    [NotNull]
    private Type? EnumType { get; set; }

    /// <summary>
    /// 获得/设置 相关枚举类型
    /// </summary>
    [Parameter]
    [NotNull]
    public Type? Type { get; set; }

    /// <summary>
    /// 获得/设置 条件候选项 请尽量使用静态数据 避免组件性能损失
    /// </summary>
    [Parameter]
    public IEnumerable<SelectedItem>? Items { get; set; }

    private string? _value;
    private string? _value2;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Type ??= TableFilter?.Column.PropertyType;
        if (Type == null) throw new InvalidOperationException("the Parameter Type must be set.");

        EnumType = Nullable.GetUnderlyingType(Type) ?? Type;
        Items ??= EnumType.ToSelectList(new SelectedItem("", Localizer["EnumFilter.AllText"].Value));
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override void Reset()
    {
        _value = null;
        _value2 = null;
        Count = 0;
        StateHasChanged();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public override FilterKeyValueAction GetFilterConditions()
    {
        var filter = new FilterKeyValueAction() { Filters = [] };
        if (!string.IsNullOrEmpty(_value) && Enum.TryParse(EnumType, _value, out var val))
        {
            filter.Filters.Add(new FilterKeyValueAction()
            {
                FieldKey = FieldKey,
                FieldValue = val,
                FilterAction = FilterAction.Equal
            });
        }

        if (Count > 0 && Enum.TryParse(EnumType, _value2, out var val2))
        {
            filter.Filters.Add(new FilterKeyValueAction()
            {
                FieldKey = FieldKey,
                FieldValue = val2,
                FilterAction = FilterAction.Equal
            });
            filter.FilterLogic = Logic;
        }
        return filter;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override async Task SetFilterConditionsAsync(FilterKeyValueAction filter)
    {
        var first = filter.Filters?.FirstOrDefault() ?? filter;
        var type = Nullable.GetUnderlyingType(Type) ?? Type;
        if (first.FieldValue != null && first.FieldValue.GetType() == type)
        {
            _value = first.FieldValue.ToString();
        }
        else
        {
            _value = "";
        }

        if (filter.Filters != null && filter.Filters.Count == 2)
        {
            Count = 1;
            FilterKeyValueAction second = filter.Filters[1];
            if (second.FieldValue != null && second.FieldValue.GetType() == type)
            {
                _value2 = second.FieldValue.ToString();
            }
            else
            {
                _value2 = "";
            }
            Logic = filter.FilterLogic;
        }
        await base.SetFilterConditionsAsync(filter);
    }
}
