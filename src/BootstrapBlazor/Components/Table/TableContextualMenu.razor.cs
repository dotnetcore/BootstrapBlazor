// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

public partial class TableContextualMenu
{
    private string Value = string.Empty;
    private bool checkAll = false;
    private List<TableContextualMenuData> FilterItems { get; set; } = new List<TableContextualMenuData>()
    {
        new(){Value = "1"},
        new(){Value = "2"},
        new(){Value = "3"},
        new(){Value = "4"},
        new(){Value = "5"}
    };

    private Task OnStateChanged(CheckboxState state, bool val)
    {
        if (state == CheckboxState.Checked)
        {
            foreach (var item in FilterItems)
            {
                item.Checked = true;
            }
        }
        else
        {
            foreach (var item in FilterItems)
            {
                item.Checked = false;
            }
        }
        return Task.CompletedTask;
    }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        if (TableFilter != null) TableFilter.ShowMoreButton = false;
    }

    /// <summary>
    /// 重置过滤条件方法
    /// </summary>
    public override void Reset()
    {
        Value = string.Empty;
        checkAll = false;
        foreach (var item in FilterItems)
        {
            item.Checked = false;
        }
        StateHasChanged();
    }

    /// <summary>
    /// 生成过滤条件方法
    /// </summary>
    /// <returns></returns>
    public override FilterKeyValueAction GetFilterConditions()
    {
        var filter = new FilterKeyValueAction() { Filters = new() };
        filter.Filters.Add(new FilterKeyValueAction()
        {
            FieldKey = FieldKey,
            FieldValue = Value,
            FilterAction = FilterAction.GreaterThan
        });
        return filter;
    }

    class TableContextualMenuData
    {
        public bool Checked { get; set; }
        public string? Value { get; set; }

    }
}
