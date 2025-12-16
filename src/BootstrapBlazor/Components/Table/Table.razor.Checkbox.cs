// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
            if (!filterRows.Except(SelectedRows).Any())
            {
                // 所有行被选中
                // all rows are selected
                ret = CheckboxState.Checked;
            }
            else if (filterRows.Any(row => SelectedRows.Any(i => Equals(i, row))))
            {
                // 任意一行被选中
                // any one row is selected
                ret = CheckboxState.Indeterminate;
            }
        }
        return ret;
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
        SelectedRows.RemoveAll(Rows.Intersect(SelectedRows).Contains);
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
    /// 是否重置列拖拽事件 <see cref="OnAfterRenderAsync(bool)"/> 方法中重置为 false
    /// </summary>
    private bool _resetColDragListener;

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
        if (AllowDragColumn && visible)
        {
            _resetColDragListener = true;
        }
        if (!string.IsNullOrEmpty(ClientTableName))
        {
            await InvokeVoidAsync("saveColumnList", ClientTableName, _visibleColumns);
        }
        if (OnColumnVisibleChanged != null)
        {
            await OnColumnVisibleChanged(columnName, visible);
        }
    }

    /// <summary>
    /// 当前所有列的是否全都显示/全都不显示/部分显示状态计算
    /// </summary>
    private CheckboxState VisibleColumnsCurrentSelectedResult
        => _visibleColumns.All(r => r.Visible)
            ? CheckboxState.Checked
            : _visibleColumns.All(r => !r.Visible)
                ? CheckboxState.UnChecked
                : CheckboxState.Indeterminate;

    private string _visibleColumnsSearchKey = "";

    /// <summary>
    /// 获得/设置 各列是否显示状态集合
    /// </summary>
    private List<ColumnVisibleItem> VisibleColumnsSearchResult
        => string.IsNullOrWhiteSpace(_visibleColumnsSearchKey)
            ? _visibleColumns : _visibleColumnsSearchResult;

    /// <summary>
    /// 获得/设置 各列是否显示状态集合
    /// </summary>
    private List<ColumnVisibleItem> _visibleColumnsSearchResult = [];

    private async Task SearchVisibleColumns(string searchKey)
    {
        _visibleColumnsSearchKey = searchKey;
        _visibleColumnsSearchResult = _visibleColumns
            .Where(r =>
                string.IsNullOrWhiteSpace(_visibleColumnsSearchKey) ||
                (r.DisplayName ?? r.Name).Contains(_visibleColumnsSearchKey))
            .ToList();
        await InvokeAsync(StateHasChanged);
    }

    private async Task InverseSelected()
    {
        foreach (var column in _visibleColumns)
        {
            column.Visible = !column.Visible;
            await OnToggleColumnVisible(column.Name, column.Visible);
        }

        if (VisibleColumnsCurrentSelectedResult == CheckboxState.UnChecked && _visibleColumns.Any())
        {
            await ShowToastAsync(
                ColumnGroupSelectButtonWarnToastTitle,
                ColumnGroupSelectButtonWarnToastContent, ToastCategory.Warning);
            _visibleColumns[0].Visible = true;
            await OnToggleColumnVisible(_visibleColumns[0].Name, true);
        }
        await InvokeAsync(StateHasChanged);
    }

    private async Task OnToggleAllColumnsVisibleState(CheckboxState state, string _)
    {
        if (state == CheckboxState.Checked)
            foreach (var column in _visibleColumns)
            {
                column.Visible = true;
                await OnToggleColumnVisible(column.Name, true);
            }
        else if (state == CheckboxState.UnChecked)
        {
            await ShowToastAsync(
                ColumnGroupSelectButtonWarnToastTitle,
                ColumnGroupSelectButtonWarnToastContent, ToastCategory.Warning);
            foreach (var column in _visibleColumns.Skip(1).ToList())
            {
                column.Visible = false;
                await OnToggleColumnVisible(column.Name, false);
            }
        }
        await InvokeAsync(StateHasChanged);
    }
}
