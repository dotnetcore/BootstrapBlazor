// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 字符串类型过滤条件
/// </summary>
public partial class StringFilter
{
    private string Value1 { get; set; } = "";

    private FilterAction Action1 { get; set; } = FilterAction.Contains;

    private string Value2 { get; set; } = "";

    private FilterAction Action2 { get; set; } = FilterAction.Equal;

    [Inject]
    [NotNull]
    private IStringLocalizer<TableFilter>? Localizer { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override FilterLogic Logic { get; set; } = FilterLogic.Or;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items ??= new SelectedItem[]
        {
            new SelectedItem("Contains", Localizer["Contains"].Value),
            new SelectedItem("Equal", Localizer["Equal"].Value),
            new SelectedItem("NotEqual", Localizer["NotEqual"].Value),
            new SelectedItem("NotContains", Localizer["NotContains"].Value)
        };
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override void Reset()
    {
        Value1 = "";
        Value2 = "";
        Action1 = FilterAction.Contains;
        Action2 = FilterAction.Contains;
        Logic = FilterLogic.Or;
        Count = 0;
        StateHasChanged();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public override FilterKeyValueAction GetFilterConditions()
    {
        var filter = new FilterKeyValueAction() { Filters = new() };
        if (!string.IsNullOrEmpty(Value1))
        {
            filter.Filters.Add(new FilterKeyValueAction()
            {
                FieldKey = FieldKey,
                FieldValue = Value1,
                FilterAction = Action1
            });
        }

        if (Count > 0 && !string.IsNullOrEmpty(Value2))
        {
            filter.Filters.Add(new FilterKeyValueAction()
            {
                FieldKey = FieldKey,
                FieldValue = Value2,
                FilterAction = Action2,
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
        FilterKeyValueAction first = filter.Filters?.FirstOrDefault() ?? filter;
        if (first.FieldValue is string value)
        {
            Value1 = value;
        }
        else
        {
            Value1 = "";
        }
        Action1 = first.FilterAction;

        if (filter.Filters != null && filter.Filters.Count == 2)
        {
            Count = 1;
            FilterKeyValueAction second = filter.Filters[1];
            if (second.FieldValue is string value2)
            {
                Value2 = value2;
            }
            else
            {
                Value2 = "";
            }
            Action2 = second.FilterAction;
            Logic = second.FilterLogic;
        }
        await base.SetFilterConditionsAsync(filter);
    }
}
