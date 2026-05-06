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
    /// <para lang="zh">获得 表头行样式表集合</para>
    /// <para lang="en">Get thead style sheet collection</para>
    /// </summary>
    protected string? HeaderClass => CssBuilder.Default()
        .AddClass(HeaderStyle.ToDescriptionString(), HeaderStyle != TableHeaderStyle.None)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 是否保持选择行，默认值为 false</para>
    /// <para lang="en">Gets or sets Whether to keep selected rows. Default false</para>
    /// </summary>
    [Parameter]
    public bool IsKeepSelectedRows { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 新建数据是否保持原选择行，默认值为 false</para>
    /// <para lang="en">Gets or sets Keep selected rows after adding data. Default false</para>
    /// </summary>
    [Parameter]
    public bool IsKeepSelectedRowAfterAdd { get; set; }

    /// <summary>
    /// <para lang="zh">获得 表头行是否选中状态</para>
    /// <para lang="en">Get Header Row Check State</para>
    /// </summary>
    protected CheckboxState HeaderCheckState()
    {
        var ret = CheckboxState.UnChecked;
        var filterRows = ShowRowCheckboxCallback == null ? Rows : Rows.Where(ShowRowCheckboxCallback);
        if (filterRows.Any())
        {
            if (!filterRows.Except(SelectedRows).Any())
            {
                ret = CheckboxState.Checked;
            }
            else if (filterRows.Any(row => SelectedRows.Any(i => Equals(i, row))))
            {
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
    protected CheckboxState RowCheckState(TItem item) => SelectedRows.Any(i => Equals(i, item)) ? CheckboxState.Checked : CheckboxState.UnChecked;

    /// <summary>
    /// <para lang="zh">获得/设置 是否为多选模式，默认值为 false</para>
    /// <para lang="en">Gets or sets Multiple Selection Mode. Default false</para>
    /// </summary>
    /// <remarks>此参数在 <see cref="IsExcel"/> 模式下为 true</remarks>
    [Parameter]
    public bool IsMultipleSelect { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示选择框文字，默认值为 false</para>
    /// <para lang="en">Gets or sets Show Checkbox Text. Default false</para>
    /// </summary>
    [Parameter]
    public bool ShowCheckboxText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 显示选择框文字 默认为 选择</para>
    /// <para lang="en">Gets or sets Checkbox Display Text. Default "Select"</para>
    /// </summary>
    /// <value></value>
    [Parameter]
    [NotNull]
    public string? CheckboxDisplayText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 表格行是否显示选择框 默认全部显示 此属性在 <see cref="IsMultipleSelect"/> 参数为 true 时生效</para>
    /// <para lang="en">Gets or sets Whether to show row checkbox. Default show all. This property is effective when <see cref="IsMultipleSelect"/> is true</para>
    /// </summary>
    [Parameter]
    public Func<TItem, bool>? ShowRowCheckboxCallback { get; set; }

    private bool GetShowRowCheckbox(TItem item) => ShowRowCheckboxCallback == null || ShowRowCheckboxCallback(item);

    /// <summary>
    /// <para lang="zh">点击表头选择复选框时触发此方法</para>
    /// <para lang="en">Header Checkbox Click Method</para>
    /// </summary>
    /// <param name="state"></param>
    /// <param name="val"></param>
    protected virtual async Task OnHeaderCheck(CheckboxState state, TItem val)
    {
        var items = Rows.Intersect(SelectedRows);
        SelectedRows.RemoveAll(i => items.Any(item => Equals(item, i)));
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
    /// <para lang="zh">获得/设置 列改变显示状态回调方法</para>
    /// <para lang="en">Gets or sets Column Visible Changed Callback</para>
    /// </summary>
    [Parameter]
    public Func<string, bool, Task>? OnColumnVisibleChanged { get; set; }

    private async Task OnToggleColumnVisible(TableColumnState item, bool visible)
    {
        // 设置可见性
        item.Visible = visible;

        // 设置列状态缓存中可见状态
        if (!string.IsNullOrEmpty(ClientTableName))
        {
            var tableWidth = 0;
            var useTableWidth = true;
            for (var index = 0; index < _tableColumnStateCache.Columns.Count; index++)
            {
                var column = _tableColumnStateCache.Columns[index];
                if (column.Name == item.Name)
                {
                    column.Visible = visible;
                }

                if (column.Visible)
                {
                    // 重新计算表格宽度
                    if (column.Width.HasValue)
                    {
                        tableWidth += column.Visible ? column.Width.Value : 0;
                    }
                    else
                    {
                        useTableWidth = false;
                    }
                }
            }

            _tableColumnStateCache.TableWidth = useTableWidth ? tableWidth : 0;
        }

        // 触发 OnColumnVisibleChanged 回调
        if (OnColumnVisibleChanged != null)
        {
            await OnColumnVisibleChanged(item.Name, visible);
        }

        _resetColumns = true;
        _invoke = true;

        StateHasChanged();
    }

    private void TriggerSelectAllColumnList()
    {
        foreach (var column in _columnVisibleItems)
        {
            column.Visible = true;
        }
    }

    private void TriggerSelectInvertColumnList()
    {
        foreach (var column in _columnVisibleItems)
        {
            column.Visible = !column.Visible;
        }

        // 如果全部列都不可见了，则至少显示第一列
        if (_columnVisibleItems.All(i => i.Visible == false))
        {
            _columnVisibleItems.First().Visible = true;
        }
    }
}
