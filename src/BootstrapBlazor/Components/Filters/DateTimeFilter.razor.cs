// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 时间类型过滤条件
/// </summary>
public partial class DateTimeFilter : FilterBase
{
    private DateTime? Value1 { get; set; }

    private FilterAction Action1 { get; set; } = FilterAction.GreaterThanOrEqual;

    private DateTime? Value2 { get; set; }

    private FilterAction Action2 { get; set; } = FilterAction.LessThanOrEqual;

    /// <summary>
    /// 获得/设置 条件候选项
    /// </summary>
    [Parameter]
    public IEnumerable<SelectedItem>? Items { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<TableFilter>? Localizer { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items ??= new SelectedItem[]
        {
            new SelectedItem("GreaterThanOrEqual", Localizer["GreaterThanOrEqual"].Value),
            new SelectedItem("LessThanOrEqual", Localizer["LessThanOrEqual"].Value),
            new SelectedItem("GreaterThan", Localizer["GreaterThan"].Value),
            new SelectedItem("LessThan", Localizer["LessThan"].Value),
            new SelectedItem("Equal", Localizer["Equal"].Value),
            new SelectedItem("NotEqual", Localizer["NotEqual"].Value )
        };
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Reset()
    {
        Value1 = null;
        Value2 = null;
        Action1 = FilterAction.GreaterThanOrEqual;
        Action2 = FilterAction.LessThanOrEqual;
        Count = 0;
        Logic = FilterLogic.And;
        StateHasChanged();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public override IEnumerable<FilterKeyValueAction> GetFilterConditions()
    {
        var filters = new List<FilterKeyValueAction>();
        if (Value1 != null)
        {
            filters.Add(new FilterKeyValueAction()
            {
                FieldKey = FieldKey,
                FieldValue = Value1,
                FilterAction = Action1
            });
        }

        if (Count > 0 && Value2 != null)
        {
            filters.Add(new FilterKeyValueAction()
            {
                FieldKey = FieldKey,
                FieldValue = Value2,
                FilterAction = Action2,
                FilterLogic = Logic
            });
        }
        return filters;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override async Task SetFilterConditionsAsync(IEnumerable<FilterKeyValueAction> conditions)
    {
        if (conditions.Any())
        {
            FilterKeyValueAction first = conditions.First();
            if (first.FieldValue is DateTime value)
            {
                Value1 = value;
            }
            else
            {
                Value1 = null;
            }
            Action1 = first.FilterAction;

            if (conditions.Count() == 2)
            {
                Count = 1;
                FilterKeyValueAction second = conditions.ElementAt(1);
                if (second.FieldValue is DateTime value2)
                {
                    Value2 = value2;
                }
                else
                {
                    Value2 = null;
                }
                Action1 = second.FilterAction;
                Logic = second.FilterLogic;
            }
        }
        await base.SetFilterConditionsAsync(conditions);
    }
}
