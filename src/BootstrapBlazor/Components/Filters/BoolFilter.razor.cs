// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">BoolFilter 组件用于表格列的布尔值过滤</para>
/// <para lang="en">BoolFilter component is used for boolean value filtering in table column</para>
/// </summary>
public partial class BoolFilter
{
    /// <summary>
    /// <para lang="zh">获得/设置过滤候选项，建议使用静态数据以避免性能损失</para>
    /// <para lang="en">Gets or sets the filter candidate items. It is recommended to use static data to avoid performance loss</para>
    /// </summary>
    [Parameter]
    public IEnumerable<SelectedItem>? Items { get; set; }

    private string _value = "";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items ??=
        [
            new SelectedItem("", Localizer["BoolFilter.AllText"].Value),
            new SelectedItem("true", Localizer["BoolFilter.TrueText"].Value),
            new SelectedItem("false", Localizer["BoolFilter.FalseText"].Value)
        ];
    }

    /// <summary>
    /// <inheritdoc cref="IFilterAction.Reset"/>
    /// </summary>
    public override void Reset()
    {
        _value = "";
        StateHasChanged();
    }

    /// <summary>
    /// <inheritdoc cref="IFilterAction.GetFilterConditions"/>
    /// </summary>
    public override FilterKeyValueAction GetFilterConditions()
    {
        var filter = new FilterKeyValueAction();
        if (!string.IsNullOrEmpty(_value))
        {
            filter.Filters.Add(new FilterKeyValueAction
            {
                FieldKey = FieldKey,
                FieldValue = _value == "true",
                FilterAction = FilterAction.Equal
            });
        }
        return filter;
    }

    /// <summary>
    /// <inheritdoc cref="IFilterAction.SetFilterConditionsAsync(FilterKeyValueAction)"/>
    /// </summary>
    public override async Task SetFilterConditionsAsync(FilterKeyValueAction filter)
    {
        var first = filter.Filters.FirstOrDefault() ?? filter;
        if (first.FieldValue is bool value)
        {
            _value = value ? "true" : "false";
        }
        await base.SetFilterConditionsAsync(filter);
    }
}
