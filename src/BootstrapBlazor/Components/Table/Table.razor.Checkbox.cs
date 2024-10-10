﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

public partial class Table<TItem>
{
    /// <summary>
    /// 获得 选择列显示文字
    /// </summary>
    protected string? CheckboxDisplayTextString => ShowCheckboxText ? CheckboxDisplayText : null;

    /// <summary>
    /// 获得 thead 样式表集合
    /// </summary>
    protected string? HeaderClass => CssBuilder.Default()
        .AddClass(HeaderStyle.ToDescriptionString(), HeaderStyle != TableHeaderStyle.None)
        .Build();

    /// <summary>
    /// 获得/设置 是否保持选择行，默认为 false 不保持
    /// </summary>
    [Parameter]
    public bool IsKeepSelectedRows { get; set; }

    /// <summary>
    /// 获得/设置 新建数据是否保持原选择行，默认为 false 不保持
    /// </summary>
    [Parameter]
    public bool IsKeepSelectedRowAfterAdd { get; set; }

    /// <summary>
    /// 获得 表头行是否选中状态
    /// </summary>
    /// <returns></returns>
    protected CheckboxState HeaderCheckState()
    {
        var ret = CheckboxState.UnChecked;
        //过滤掉不可选择的记录
        var filterRows = ShowRowCheckboxCallback == null ? Rows : Rows.Where(ShowRowCheckboxCallback);
        if (filterRows.Any())
        {
            if (filterRows.All(AnyRow))
            {
                // 所有行被选中
                // all rows are selected
                ret = CheckboxState.Checked;
            }
            else if (filterRows.Any(AnyRow))
            {
                // 任意一行被选中
                // any one row is selected
                ret = CheckboxState.Indeterminate;
            }
        }
        return ret;

        bool AnyRow(TItem row) => SelectedRows.Any(i => Equals(i, row));
    }

    /// <summary>
    /// 获得 当前行是否被选中
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected CheckboxState RowCheckState(TItem item) => SelectedRows.Any(i => Equals(i, item)) ? CheckboxState.Checked : CheckboxState.UnChecked;

    /// <summary>
    /// 获得/设置 是否为多选模式 默认为 false
    /// </summary>
    /// <remarks>此参数在 <see cref="IsExcel"/> 模式下为 true</remarks>
    [Parameter]
    public bool IsMultipleSelect { get; set; }

    /// <summary>
    /// 获得/设置 是否显示选择框文字 默认为 false
    /// </summary>
    /// <value></value>
    [Parameter]
    public bool ShowCheckboxText { get; set; }

    /// <summary>
    /// 获得/设置 显示选择框文字 默认为 选择
    /// </summary>
    /// <value></value>
    [Parameter]
    [NotNull]
    public string? CheckboxDisplayText { get; set; }

    /// <summary>
    /// 获得/设置 表格行是否显示选择框 默认全部显示 此属性在 <see cref="IsMultipleSelect"/> 参数为 true 时生效
    /// </summary>
    [Parameter]
    public Func<TItem, bool>? ShowRowCheckboxCallback { get; set; }

    private bool GetShowRowCheckbox(TItem item) => ShowRowCheckboxCallback == null || ShowRowCheckboxCallback(item);

    /// <summary>
    /// 点击 Header 选择复选框时触发此方法
    /// </summary>
    /// <param name="state"></param>
    /// <param name="val"></param>
    protected virtual async Task OnHeaderCheck(CheckboxState state, TItem val)
    {
        SelectedRows.RemoveAll(x => Rows.Any(a => Equals(a, x)));
        if (state == CheckboxState.Checked)
        {
            SelectedRows.AddRange(ShowRowCheckboxCallback == null ? Rows : Rows.Where(ShowRowCheckboxCallback));
        }
        await OnSelectedRowsChanged();
    }

    /// <summary>
    /// 点击选择复选框时触发此方法
    /// </summary>
    protected async Task OnCheck(CheckboxState state, TItem val)
    {
        if (state == CheckboxState.Checked)
        {
            SelectedRows.Add(val);
        }
        else
        {
            var item = SelectedRows.FirstOrDefault(i => Equals(i, val));
            if (item != null)
            {
                SelectedRows.Remove(item);
            }
        }

        // auto quit edit in cell mode
        AddInCell = false;
        EditInCell = false;

        ShowAddForm = false;
        ShowEditForm = false;

        await OnSelectedRowsChanged();
    }

    /// <summary>
    /// 是否重置列变量 <see cref="OnAfterRenderAsync(bool)"/> 方法中重置为 false
    /// </summary>
    private bool _resetColumns;

    /// <summary>
    /// 获得/设置 列改变显示状态回调方法
    /// </summary>
    [Parameter]
    public Func<string, bool, Task>? OnColumnVisibleChanged { get; set; }

    private async Task OnToggleColumnVisible(string columnName, bool visible)
    {
        if (AllowResizing)
        {
            _resetColumns = true;
        }

        if (OnColumnVisibleChanged != null)
        {
            await OnColumnVisibleChanged(columnName, visible);
        }
    }
}
