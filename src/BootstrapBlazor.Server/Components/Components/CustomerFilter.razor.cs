﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// 
/// </summary>
public partial class CustomerFilter
{
    private int? Value;

    private readonly IEnumerable<SelectedItem> _items = new SelectedItem[]
    {
        new() { Value = "", Text = "请选择 ..." },
        new() { Value = "10", Text = "大于 10" },
        new() { Value = "50", Text = "大于 50" },
        new() { Value = "80", Text = "大于 80" }
    };

    /// <summary>
    /// 重置过滤条件方法
    /// </summary>
    public override void Reset()
    {
        Value = null;
        StateHasChanged();
    }

    /// <summary>
    /// 生成过滤条件方法
    /// </summary>
    /// <returns></returns>
    public override FilterKeyValueAction GetFilterConditions()
    {
        var filter = new FilterKeyValueAction();
        if (Value != null)
        {
            filter.Filters.Add(new FilterKeyValueAction()
            {
                FieldKey = FieldKey,
                FieldValue = Value.Value,
                FilterAction = FilterAction.GreaterThan
            });
        }
        return filter;
    }
}
