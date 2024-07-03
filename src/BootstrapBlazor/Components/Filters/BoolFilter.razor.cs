// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 布尔类型过滤条件
/// </summary>
public partial class BoolFilter
{
    private string Value { get; set; } = "";

    [Inject]
    [NotNull]
    private IStringLocalizer<TableFilter>? Localizer { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (TableFilter != null)
        {
            TableFilter.ShowMoreButton = false;
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items ??= new SelectedItem[]
        {
            new("", Localizer["BoolFilter.AllText"].Value),
            new("true", Localizer["BoolFilter.TrueText"].Value),
            new("false", Localizer["BoolFilter.FalseText"].Value)
        };
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override void Reset()
    {
        Value = "";
        StateHasChanged();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public override FilterKeyValueAction GetFilterConditions()
    {
        var filter = new FilterKeyValueAction() { Filters = [] };
        if (!string.IsNullOrEmpty(Value))
        {
            filter.Filters.Add(new FilterKeyValueAction()
            {
                FieldKey = FieldKey,
                FieldValue = Value == "true",
                FilterAction = FilterAction.Equal
            });
        }
        return filter;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override async Task SetFilterConditionsAsync(FilterKeyValueAction filter)
    {
        var first = filter.Filters?.FirstOrDefault() ?? filter;
        if (first.FieldValue is bool value)
        {
            Value = value ? "true" : "false";
        }
        else if (first.FieldValue is null)
        {
            Value = "";
        }
        await base.SetFilterConditionsAsync(filter);
    }
}
