// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using BootstrapBlazor.Server.Components.Samples.Table;

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// CustomerFilter 组件示例代码
/// </summary>
public partial class CustomerFilter
{
    [Inject]
    [NotNull]
    private IStringLocalizer<TablesFilter>? TableFilterLocalizer { get; set; }

    private int? _value;

    private List<SelectedItem> _items = [];

    /// <summary>
    ///     <inheritdoc />
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _items =
        [
            new SelectedItem { Value = "", Text = TableFilterLocalizer["CustomerFilterItem1"] },
            new SelectedItem { Value = "10", Text = TableFilterLocalizer["CustomerFilterItem2"] },
            new SelectedItem { Value = "50", Text = TableFilterLocalizer["CustomerFilterItem3"] },
            new SelectedItem { Value = "80", Text = TableFilterLocalizer["CustomerFilterItem4"] }
        ];
    }

    /// <summary>
    /// 重置过滤条件方法
    /// </summary>
    public override void Reset()
    {
        _value = null;
        StateHasChanged();
    }

    /// <summary>
    /// 生成过滤条件方法
    /// </summary>
    /// <returns></returns>
    public override FilterKeyValueAction GetFilterConditions()
    {
        var filter = new FilterKeyValueAction();
        if (_value != null)
        {
            filter.Filters.Add(new FilterKeyValueAction()
            {
                FieldKey = FieldKey,
                FieldValue = _value.Value,
                FilterAction = FilterAction.GreaterThan
            });
        }
        return filter;
    }
}
