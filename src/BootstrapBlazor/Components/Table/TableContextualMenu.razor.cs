// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 表格过滤菜单组件
/// </summary>
public partial class TableContextualMenu
{
    /// <summary>
    /// 获得/设置 绑定类字段名称
    /// </summary>
    [Parameter]
    public string? FieldName { get; set; }

    /// <summary>
    /// 获得 相关联 ITableColumn 实例
    /// </summary>
    [Parameter]
    [NotNull]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public ITableColumn? Column { get; set; }

    private string Value = string.Empty;

    private bool checkAll = false;

    private List<TableContextualMenuItem> FilterItems { get; set; } = [];

    private List<SelectedItem>? SelectedItems { get; set; }

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
    protected override async void OnInitialized()
    {
        base.OnInitialized();

        FieldKey = Column.GetFieldName();
        if (Column.CustomFilter != null)
        {
            SelectedItems = await Column.CustomFilter();
            if (SelectedItems != null)
            {
                FilterItems = SelectedItems.Select(item => new TableContextualMenuItem() { Value = item.Value, Text = item.Text }).ToList();
            }
        }

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
    /// 过滤内容搜索
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    private Task OnSearchValueChanged(string val)
    {
        if (SelectedItems != null)
        {
            FilterItems = SelectedItems
                .Where(x => x.Text.Contains(val))
                .Select(item => new TableContextualMenuItem() { Value = item.Value, Text = item.Text })
                .ToList();
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// 生成过滤条件方法
    /// </summary>
    /// <returns></returns>
    public override FilterKeyValueAction GetFilterConditions()
    {
        var filter = new FilterKeyValueAction() { Filters = [], FilterLogic = FilterLogic.Or };

        foreach (var item in FilterItems.Where(x => x.Checked))
        {
            filter.Filters.Add(new FilterKeyValueAction()
            {
                FieldKey = FieldKey,
                FieldValue = item.Value,
                FilterAction = FilterAction.Equal
            });
        }

        return filter;
    }

    record TableContextualMenuItem
    {
        public bool Checked { get; set; }

        public string? Value { get; set; }

        public string? Text { get; set; }
    }
}
