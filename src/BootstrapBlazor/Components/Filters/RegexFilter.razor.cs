// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.RegularExpressions;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">RegexFilter 组件用于表格列的正则表达式过滤</para>
/// <para lang="en">RegexFilter component is used for regular expression filtering in table column</para>
/// </summary>
public partial class RegexFilter
{
    /// <summary>
    /// <para lang="zh">获得/设置 过滤候选项集合 建议使用静态数据以避免性能损耗</para>
    /// <para lang="en">Gets or sets the filter candidate items. It is recommended to use static data to avoid performance loss</para>
    /// </summary>
    [Parameter]
    public IEnumerable<SelectedItem>? Items { get; set; }

    private string? _value1;
    private FilterAction _action1 = FilterAction.Match;
    private string? _value2;
    private FilterAction _action2 = FilterAction.Match;

    private string? FilterRowClassString => CssBuilder.Default("filter-row")
        .AddClass("active", TableColumnFilter.HasFilter())
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Logic = FilterLogic.Or;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items ??=
        [
            new SelectedItem("Match", Localizer["Match"].Value),
            new SelectedItem("NotMatch", Localizer["NotMatch"].Value)
        ];
    }

    /// <summary>
    /// <inheritdoc cref="IFilterAction.Reset"/>
    /// </summary>
    public override void Reset()
    {
        _value1 = null;
        _value2 = null;
        _action1 = FilterAction.Match;
        _action2 = FilterAction.Match;
        Logic = FilterLogic.Or;
        Count = 0;
        StateHasChanged();
    }

    /// <summary>
    /// <inheritdoc cref="IFilterAction.GetFilterConditions"/>
    /// </summary>
    public override FilterKeyValueAction GetFilterConditions()
    {
        var filter = new FilterKeyValueAction();
        if (!string.IsNullOrEmpty(_value1) && IsValidRegex(_value1))
        {
            filter.Filters.Add(new FilterKeyValueAction
            {
                FieldKey = FieldKey,
                FieldValue = _value1,
                FilterAction = _action1
            });
        }

        if (Count > 0 && !string.IsNullOrEmpty(_value2) && IsValidRegex(_value2))
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
        FilterKeyValueAction first = filter.Filters.FirstOrDefault() ?? filter;
        if (first.FieldValue is string value)
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
            if (second.FieldValue is string value2)
            {
                _value2 = value2;
            }
            else
            {
                _value2 = null;
            }
            _action2 = second.FilterAction;
            Logic = second.FilterLogic;
        }
        await base.SetFilterConditionsAsync(filter);
    }

    private async Task OnValueChanged(string? val)
    {
        _value1 = val;
        await OnFilterAsync();
    }

    /// <summary>
    /// <para lang="zh">校验正则表达式是否合法 防止用户输入过程中触发过滤导致异常</para>
    /// <para lang="en">Validates whether the regular expression is legal to prevent exceptions caused by filtering during user input</para>
    /// </summary>
    private static bool IsValidRegex(string pattern)
    {
        var ret = true;
        try
        {
            _ = new Regex(pattern);
        }
        catch (ArgumentException)
        {
            ret = false;
        }
        return ret;
    }
}
