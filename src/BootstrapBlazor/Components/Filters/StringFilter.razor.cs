// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 字符串类型过滤条件
/// </summary>
public partial class StringFilter : FilterBase
{
    private string Value1 { get; set; } = "";

    private FilterAction Action1 { get; set; } = FilterAction.Contains;

    private string Value2 { get; set; } = "";

    private FilterAction Action2 { get; set; } = FilterAction.Equal;

    [Inject]
    [NotNull]
    private IStringLocalizer<TableFilter>? Localizer { get; set; }

    /// <summary>
    /// 
    /// </summary>
    protected override FilterLogic Logic { get; set; } = FilterLogic.Or;

    [NotNull]
    private IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items = new SelectedItem[]
        {
            new SelectedItem("Contains", Localizer["Contains"].Value),
            new SelectedItem("Equal", Localizer["Equal"].Value),
            new SelectedItem("NotEqual", Localizer["NotEqual"].Value),
            new SelectedItem("NotContains", Localizer["NotContains"].Value)
        };
    }

    /// <summary>
    /// 
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
    /// 
    /// </summary>
    /// <returns></returns>
    public override IEnumerable<FilterKeyValueAction> GetFilterConditions()
    {
        var filters = new List<FilterKeyValueAction>();
        if (!string.IsNullOrEmpty(Value1))
        {
            filters.Add(new FilterKeyValueAction()
            {
                FieldKey = FieldKey,
                FieldValue = Value1,
                FilterAction = Action1
            });
        }

        if (Count > 0 && !string.IsNullOrEmpty(Value2))
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
    /// Override existing filter conditions
    /// </summary>
    public override async Task SetFilterConditionsAsync(IEnumerable<FilterKeyValueAction> conditions)
    {
        if (conditions.Any())
        {
            FilterKeyValueAction first = conditions.First();
            if (first.FieldValue is string value)
            {
                Value1 = value;
            }
            else
            {
                Value1 = "";
            }
            Action1 = first.FilterAction;

            if (conditions.Count() == 2)
            {
                Count = 1;

                FilterKeyValueAction second = conditions.ElementAt(1);
                if (second.FieldValue is string value2)
                {
                    Value2 = value2;
                }
                else
                {
                    Value2 = "";
                }
                Action1 = second.FilterAction;
                Logic = second.FilterLogic;
            }
        }
        await base.SetFilterConditionsAsync(conditions);
    }
}
