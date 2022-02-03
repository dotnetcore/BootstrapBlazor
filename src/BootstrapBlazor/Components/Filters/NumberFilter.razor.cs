// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 数字类型过滤条件
/// </summary>
public partial class NumberFilter<TType>
{
    private TType? Value1 { get; set; }

    private FilterAction Action1 { get; set; } = FilterAction.GreaterThanOrEqual;

    private TType? Value2 { get; set; }

    private FilterAction Action2 { get; set; } = FilterAction.LessThanOrEqual;

    [Inject]
    [NotNull]
    private IStringLocalizer<TableFilter>? Localizer { get; set; }

    [NotNull]
    private IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 步长 默认 0.01
    /// </summary>
    [Parameter]
    public string Step { get; set; } = "0.01";

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items = new SelectedItem[]
        {
                new SelectedItem("GreaterThanOrEqual", Localizer["GreaterThanOrEqual"]?.Value ?? "GreaterThanOrEqual"),
                new SelectedItem("LessThanOrEqual", Localizer["LessThanOrEqual"]?.Value ?? "LessThanOrEqual"),
                new SelectedItem("GreaterThan", Localizer["GreaterThan"]?.Value ?? "GreaterThan"),
                new SelectedItem("LessThan", Localizer["LessThan"]?.Value ?? "LessThan"),
                new SelectedItem("Equal", Localizer["Equal"]?.Value ?? "Equal"),
                new SelectedItem("NotEqual", Localizer["NotEqual"]?.Value ?? "NotEqual")
        };
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Reset()
    {
        Value1 = default;
        Value2 = default;
        Action1 = FilterAction.GreaterThanOrEqual;
        Action2 = FilterAction.LessThanOrEqual;
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
        if (Value1 != null) filters.Add(new FilterKeyValueAction()
        {
            FieldKey = FieldKey,
            FieldValue = Value1,
            FilterAction = Action1
        });
        if (Count > 0 && Value2 != null) filters.Add(new FilterKeyValueAction()
        {
            FieldKey = FieldKey,
            FieldValue = Value2,
            FilterAction = Action2,
            FilterLogic = Logic
        });
        return filters;
    }
}
