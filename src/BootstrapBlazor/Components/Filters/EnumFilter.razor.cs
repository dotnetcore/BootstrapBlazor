// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">枚举过滤组件</para>
/// <para lang="en">Enum filter component</para>
/// </summary>
public partial class EnumFilter
{
    [NotNull]
    private Type? EnumType { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 相关枚举类型</para>
    /// <para lang="en">Gets or sets related enum type</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public Type? Type { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 过滤候选项，建议使用静态数据以避免性能损失</para>
    /// <para lang="en">Gets or sets the filter candidate items. It is recommended to use static data to avoid performance loss</para>
    /// </summary>
    [Parameter]
    public IEnumerable<SelectedItem>? Items { get; set; }

    private string? _value1 = "";
    private string? _value2 = "";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Type ??= TableColumnFilter?.Column.PropertyType;
        if (Type == null) throw new InvalidOperationException("the Parameter Type must be set.");

        EnumType = Nullable.GetUnderlyingType(Type) ?? Type;
        Items ??= EnumType.ToSelectList(new SelectedItem("", Localizer["EnumFilter.AllText"].Value));
    }

    /// <summary>
    /// <inheritdoc cref="IFilterAction.Reset"/>
    /// </summary>
    public override void Reset()
    {
        _value1 = "";
        _value2 = "";
        Count = 0;
        StateHasChanged();
    }

    /// <summary>
    /// <inheritdoc cref="IFilterAction.GetFilterConditions"/>
    /// </summary>
    public override FilterKeyValueAction GetFilterConditions()
    {
        var filter = new FilterKeyValueAction();
        if (!string.IsNullOrEmpty(_value1) && Enum.TryParse(EnumType, _value1, out var val))
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
    /// <inheritdoc cref="IFilterAction.SetFilterConditionsAsync(FilterKeyValueAction)"/>
    /// </summary>
    public override async Task SetFilterConditionsAsync(FilterKeyValueAction filter)
    {
        var first = filter.Filters.FirstOrDefault() ?? filter;
        var type = Nullable.GetUnderlyingType(Type) ?? Type;
        if (first.FieldValue != null && first.FieldValue.GetType() == type)
        {
            _value1 = first.FieldValue.ToString();
        }
        else
        {
            _value1 = "";
        }

        if (filter.Filters.Count > 1)
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
