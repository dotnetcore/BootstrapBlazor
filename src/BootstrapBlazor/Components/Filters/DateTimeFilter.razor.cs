// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">DateTime 过滤组件</para>
/// <para lang="en">DateTime filter component</para>
/// </summary>
public partial class DateTimeFilter
{
    private DateTime? _value1;
    private FilterAction _action1 = FilterAction.GreaterThanOrEqual;
    private DateTime? _value2;
    private FilterAction _action2 = FilterAction.LessThanOrEqual;

    private string? FilterRowClassString => CssBuilder.Default("filter-row")
        .AddClass("active", TableColumnFilter.HasFilter())
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置过滤候选项，建议使用静态数据以避免性能损失</para>
    /// <para lang="en">Gets or sets the filter candidate items. It is recommended to use static data to avoid performance loss</para>
    /// </summary>
    [Parameter]
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items ??=
        [
            new SelectedItem("GreaterThanOrEqual", Localizer["GreaterThanOrEqual"].Value),
            new SelectedItem("LessThanOrEqual", Localizer["LessThanOrEqual"].Value),
            new SelectedItem("GreaterThan", Localizer["GreaterThan"].Value),
            new SelectedItem("LessThan", Localizer["LessThan"].Value),
            new SelectedItem("Equal", Localizer["Equal"].Value),
            new SelectedItem("NotEqual", Localizer["NotEqual"].Value)
        ];
    }

    /// <summary>
    /// <inheritdoc cref="IFilterAction.Reset"/>
    /// </summary>
    public override void Reset()
    {
        _value1 = null;
        _value2 = null;
        _action1 = FilterAction.GreaterThanOrEqual;
        _action2 = FilterAction.LessThanOrEqual;
        Count = 0;
        Logic = FilterLogic.And;
        StateHasChanged();
    }

    /// <summary>
    /// <inheritdoc cref="IFilterAction.GetFilterConditions"/>
    /// </summary>
    public override FilterKeyValueAction GetFilterConditions()
    {
        var filter = new FilterKeyValueAction();
        if (_value1 != null)
        {
            filter.Filters.Add(new FilterKeyValueAction()
            {
                FieldKey = FieldKey,
                FieldValue = _value1,
                FilterAction = _action1
            });
        }

        if (Count > 0 && _value2 != null)
        {
            filter.Filters.Add(new FilterKeyValueAction
            {
                FieldKey = FieldKey,
                FieldValue = _value2,
                FilterAction = _action2,
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
        if (first.FieldValue is DateTime value)
        {
            _value1 = value;
        }
        else
        {
            _value1 = null;
        }
        _action1 = first.FilterAction;

        if (filter.Filters.Count > 1)
        {
            Count = 1;
            FilterKeyValueAction second = filter.Filters[1];
            if (second.FieldValue is DateTime value2)
            {
                _value2 = value2;
            }
            else
            {
                _value2 = null;
            }
            _action2 = second.FilterAction;
            Logic = filter.FilterLogic;
        }
        await base.SetFilterConditionsAsync(filter);
    }
}
