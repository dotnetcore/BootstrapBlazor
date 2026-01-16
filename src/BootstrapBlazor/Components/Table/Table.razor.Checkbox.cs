// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

public partial class Table<TItem>
{
    /// <summary>
    /// <para lang="zh">获得 选择列显示文字</para>
    /// <para lang="en">Get Checkbox Column Display Text</para>
    /// </summary>
    protected string? CheckboxDisplayTextString => ShowCheckboxText ? CheckboxDisplayText : null;

    /// <summary>
    /// <para lang="zh">获得 thead 样式表集合</para>
    /// <para lang="en">Get thead style sheet collection</para>
    /// </summary>
    protected string? HeaderClass => CssBuilder.Default()
        .AddClass(HeaderStyle.ToDescriptionString(), HeaderStyle != TableHeaderStyle.None)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 是否保持选择行，默认为 false 不保持</para>
    /// <para lang="en">Get/Set Whether to keep selected rows. Default false</para>
    /// </summary>
    [Parameter]
    public bool IsKeepSelectedRows { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 新建数据是否保持原选择行，默认为 false 不保持</para>
    /// <para lang="en">Get/Set Keep selected rows after adding data. Default false</para>
    /// </summary>
    [Parameter]
    public bool IsKeepSelectedRowAfterAdd { get; set; }

    /// <summary>
    /// <para lang="zh">获得 表头行是否选中状态</para>
    /// <para lang="en">Get Header Row Check State</para>
    /// </summary>
    /// <returns></returns>
    protected CheckboxState HeaderCheckState()
    {
        var ret = CheckboxState.UnChecked;
        // <para lang="zh">过滤掉不可选择的记录</para>
        // <para lang="en">Filter out unselectable records</para>
        var filterRows = ShowRowCheckboxCallback == null ? Rows : Rows.Where(ShowRowCheckboxCallback);
        if (filterRows.Any())
        {
            if (!filterRows.Except(SelectedRows).Any())
            {
                // <para lang="zh">所有行被选中</para>
                // <para lang="en">All rows are selected</para>
                // all rows are selected
                ret = CheckboxState.Checked;
            }
            else if (filterRows.Any(row => SelectedRows.Any(i => Equals(i, row))))
            {
                // <para lang="zh">任意一行被选中</para>
                // <para lang="en">Any one row is selected</para>
                // any one row is selected
                ret = CheckboxState.Indeterminate;
            }
        }
        return ret;
    }

    /// <summary>
    /// <para lang="zh">获得 当前行是否被选中</para>
    /// <para lang="en">Get whether current row is selected</para>
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected CheckboxState RowCheckState(TItem item) => SelectedRows.Any(i => Equals(i, item)) ? CheckboxState.Checked : CheckboxState.UnChecked;

    /// <summary>
    /// <para lang="zh">获得/设置 是否为多选模式 默认为 false</para>
    /// <para lang="en">Get/Set Multiple Selection Mode. Default false</para>
    /// </summary>
    /// <remarks>此参数在 <see cref="IsExcel"/> 模式下为 true</remarks>
    [Parameter]
    public bool IsMultipleSelect { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示选择框文字 默认为 false</para>
    /// <para lang="en">Get/Set Show Checkbox Text. Default false</para>
    /// </summary>
    /// <value></value>
    [Parameter]
    public bool ShowCheckboxText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 显示选择框文字 默认为 选择</para>
    /// <para lang="en">Get/Set Checkbox Display Text. Default "Select"</para>
    /// </summary>
    /// <value></value>
    [Parameter]
    [NotNull]
    public string? CheckboxDisplayText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 表格行是否显示选择框 默认全部显示 此属性在 <see cref="IsMultipleSelect"/> 参数为 true 时生效</para>
    /// <para lang="en">Get/Set Whether to show row checkbox. Default show all. This property is effective when <see cref="IsMultipleSelect"/> is true</para>
    /// </summary>
    [Parameter]
    public Func<TItem, bool>? ShowRowCheckboxCallback { get; set; }

    private bool GetShowRowCheckbox(TItem item) => ShowRowCheckboxCallback == null || ShowRowCheckboxCallback(item);

    /// <summary>
    /// <para lang="zh">点击 Header 选择复选框时触发此方法</para>
    /// <para lang="en">Header Checkbox Click Method</para>
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
    /// <para lang="zh">点击选择复选框时触发此方法</para>
    /// <para lang="en">Checkbox Click Method</para>
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
    /// <para lang="zh">是否重置列变量 <see cref="OnAfterRenderAsync(bool)"/> 方法中重置为 false</para>
    /// <para lang="en">Whether to reset column variables. Reset to false in <see cref="OnAfterRenderAsync(bool)"/> method</para>
    /// </summary>
    private bool _resetColumns;

    /// <summary>
    /// <para lang="zh">是否重置列拖拽事件 <see cref="OnAfterRenderAsync(bool)"/> 方法中重置为 false</para>
    /// <para lang="en">Whether to reset column drag listener. Reset to false in <see cref="OnAfterRenderAsync(bool)"/> method</para>
    /// </summary>
    private bool _resetColDragListener;

    /// <summary>
    /// <para lang="zh">获得/设置 列改变显示状态回调方法</para>
    /// <para lang="en">Get/Set Column Visible Changed Callback</para>
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

    private void TriggerSelectAllColumnList()
    {
        foreach (var column in _visibleColumns)
        {
            column.Visible = true;
        }
    }

    private void TriggerSelectInvertColumnList()
    {
        foreach (var column in _visibleColumns)
        {
            column.Visible = !column.Visible;
        }

        if (_visibleColumns.All(i => i.Visible == false))
        {
            _visibleColumns.First().Visible = true;
        }
    }
}
