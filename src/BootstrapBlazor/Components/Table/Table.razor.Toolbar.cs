﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Components;

public partial class Table<TItem>
{
    /// <summary>
    /// 获得/设置 是否显示工具栏 默认 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowToolbar { get; set; }

    /// <summary>
    /// 获得/设置 首次加载是否显示加载骨架屏 默认 false 不显示 使用 <see cref="ShowLoadingInFirstRender" /> 参数值
    /// </summary>
    [Parameter]
    public bool ShowSkeleton { get; set; }

    /// <summary>
    /// 获得/设置 首次加载是否显示加载动画 默认 true 显示 设置 <see cref="ShowSkeleton"/> 值覆盖此参数
    /// </summary>
    [Parameter]
    public bool ShowLoadingInFirstRender { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示按钮列 默认为 true
    /// </summary>
    /// <remarks>本属性设置为 true 新建编辑删除按钮设置为 false 可单独控制每个按钮是否显示</remarks>
    [Parameter]
    public bool ShowDefaultButtons { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示新建按钮 默认为 true 显示
    /// </summary>
    [Parameter]
    public bool ShowAddButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示编辑按钮 默认为 true 行内是否显示请使用 <see cref="ShowExtendEditButton"/> 与 <see cref="ShowExtendEditButtonCallback" />
    /// </summary>
    [Parameter]
    public bool ShowEditButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示删除按钮 默认为 true 行内是否显示请使用 <see cref="ShowExtendDeleteButton"/> 与 <see cref="ShowExtendDeleteButtonCallback" />
    /// </summary>
    [Parameter]
    public bool ShowDeleteButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示导出按钮 默认为 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowExportButton { get; set; }

    /// <summary>
    /// 获得/设置 是否显示 Excel 导出按钮 默认为 true 显示
    /// </summary>
    [Parameter]
    public bool ShowExportExcelButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示 Csv 导出按钮 默认为 false 显示
    /// </summary>
    [Parameter]
    public bool ShowExportCsvButton { get; set; }

    /// <summary>
    /// 获得/设置 是否显示 Pdf 导出按钮 默认为 false 显示
    /// </summary>
    [Parameter]
    public bool ShowExportPdfButton { get; set; }

    /// <summary>
    /// 获得/设置 导出按钮图标
    /// </summary>
    [Parameter]
    public string? ExportButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 内置导出 Csv 按钮图标
    /// </summary>
    [Parameter]
    public string? CsvExportIcon { get; set; }

    /// <summary>
    /// 获得/设置 内置导出 Excel 按钮图标
    /// </summary>
    [Parameter]
    public string? ExcelExportIcon { get; set; }

    /// <summary>
    /// 获得/设置 内置导出 Pdf 按钮图标
    /// </summary>
    [Parameter]
    public string? PdfExportIcon { get; set; }

    /// <summary>
    /// 获得/设置 导出数据前是否弹出 Toast 提示框 默认 true
    /// </summary>
    [Parameter]
    public bool ShowToastBeforeExport { get; set; } = true;

    /// <summary>
    /// 获得/设置 导出数据后是否弹出 Toast 提示框 默认 true
    /// </summary>
    [Parameter]
    public bool ShowToastAfterExport { get; set; } = true;

    /// <summary>
    /// 获得/设置 导出数据前回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<Task>? BeforeExportCallback { get; set; }

    /// <summary>
    /// 获得/设置 导出数据后回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<bool, Task>? AfterExportCallback { get; set; }

    /// <summary>
    /// 获得/设置 导出按钮下拉菜单模板 默认 null
    /// </summary>
    [Parameter]
    public RenderFragment<ITableExportContext<TItem>>? ExportButtonDropdownTemplate { get; set; }

    /// <summary>
    /// 获得/设置 内置导出微软 Csv 按钮文本 默认 null 读取资源文件
    /// </summary>
    [Parameter]
    public string? ExportCsvDropdownItemText { get; set; }

    /// <summary>
    /// 获得/设置 内置导出微软 Excel 按钮文本 默认 null 读取资源文件
    /// </summary>
    [Parameter]
    public string? ExportExcelDropdownItemText { get; set; }

    /// <summary>
    /// 获得/设置 内置导出 Pdf 按钮文本 默认 null 读取资源文件
    /// </summary>
    [Parameter]
    public string? ExportPdfDropdownItemText { get; set; }

    /// <summary>
    /// 获得/设置 是否显示扩展按钮 默认为 false
    /// </summary>
    [Parameter]
    public bool ShowExtendButtons { get; set; }

    /// <summary>
    /// 获得/设置 是否自动收缩工具栏按钮 默认 true
    /// </summary>
    [Parameter]
    public bool IsAutoCollapsedToolbarButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 工具栏移动端按钮图标
    /// </summary>
    [Parameter]
    public string? GearIcon { get; set; }

    /// <summary>
    /// 获得/设置 扩展按钮是否在前面 默认 false 在行尾
    /// </summary>
    [Parameter]
    public bool IsExtendButtonsInRowHeader { get; set; }

    /// <summary>
    /// 获得/设置 行内操作列宽度 默认为 130
    /// </summary>
    [Parameter]
    public int ExtendButtonColumnWidth { get; set; } = 130;

    /// <summary>
    /// 获得/设置 行内操作列对齐方式 默认 center
    /// </summary>
    [Parameter]
    public Alignment ExtendButtonColumnAlignment { get; set; }

    /// <summary>
    /// 获得/设置 是否显示行内扩展编辑按钮 默认 true 显示
    /// </summary>
    [Parameter]
    public bool ShowExtendEditButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示行内扩展编辑按钮 默认为 null 未设置时使用 <see cref="ShowExtendEditButton"/> 值
    /// </summary>
    [Parameter]
    public Func<TItem, bool>? ShowExtendEditButtonCallback { get; set; }

    /// <summary>
    /// 获得/设置 是否禁用行内扩展编辑按钮 默认 false 不禁用
    /// </summary>
    [Parameter]
    public bool DisableExtendEditButton { get; set; }

    /// <summary>
    /// 获得/设置 是否禁用行内扩展编辑按钮 默认为 null 未设置时使用 <see cref="DisableExtendEditButton"/> 值
    /// </summary>
    [Parameter]
    public Func<TItem, bool>? DisableExtendEditButtonCallback { get; set; }

    /// <summary>
    /// 获得/设置 是否禁用行内扩展删除按钮 默认 false 不禁用
    /// </summary>
    [Parameter]
    public bool DisableExtendDeleteButton { get; set; }

    /// <summary>
    /// 获得/设置 是否禁用行内扩展删除按钮 默认为 null 未设置时使用 <see cref="DisableExtendDeleteButton"/> 值
    /// </summary>
    [Parameter]
    public Func<TItem, bool>? DisableExtendDeleteButtonCallback { get; set; }

    /// <summary>
    /// 获得/设置 是否显示行内扩展编辑按钮 默认为 null 未设置时使用 <see cref="ShowExtendEditButton"/> 值
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请使用 ShowExtendEditButtonCallback 参数. Deprecated Use ShowExtendEditButtonCallback instead.")]
    [ExcludeFromCodeCoverage]
    public Func<TItem, bool>? ShowEditButtonCallback
    {
        get => ShowExtendEditButtonCallback;
        set => ShowExtendEditButtonCallback = value;
    }

    /// <summary>
    /// 获得/设置 是否显示行内扩展删除按钮 默认 true 显示
    /// </summary>
    [Parameter]
    public bool ShowExtendDeleteButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示行内扩展删除按钮 默认为 null 未设置时使用 <see cref="ShowExtendDeleteButton"/> 值
    /// </summary>
    [Parameter]
    public Func<TItem, bool>? ShowExtendDeleteButtonCallback { get; set; }

    /// <summary>
    /// 获得/设置 是否显示行内扩展删除按钮 默认为 null 未设置时使用 <see cref="ShowExtendDeleteButton"/> 值
    /// </summary>
    [Parameter]
    [Obsolete(" 已过期，请使用 ShowExtendDeleteButtonCallback 参数. Deprecated Use ShowExtendDeleteButtonCallback instead.")]
    [ExcludeFromCodeCoverage]
    public Func<TItem, bool>? ShowDeleteButtonCallback
    {
        get => ShowExtendDeleteButtonCallback;
        set => ShowExtendDeleteButtonCallback = value;
    }

    /// <summary>
    /// 获得/设置 是否固定扩展按钮列 默认为 false 不固定
    /// </summary>
    [Parameter]
    public bool FixedExtendButtonsColumn { get; set; }

    /// <summary>
    /// 获得/设置 是否固定多选列 默认为 false 不固定
    /// </summary>
    [Parameter]
    public bool FixedMultipleColumn { get; set; }

    /// <summary>
    /// 获得/设置 是否固定明细行 Header 列 默认为 false 不固定
    /// </summary>
    [Parameter]
    public bool FixedDetailRowHeaderColumn { get; set; }

    /// <summary>
    /// 获得/设置 是否固定 LineNo 列 默认为 false 不固定
    /// </summary>
    [Parameter]
    public bool FixedLineNoColumn { get; set; }

    /// <summary>
    /// 获得/设置 是否显示刷新按钮 默认为 true
    /// </summary>
    [Parameter]
    public bool ShowRefresh { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示视图按钮 默认为 false <see cref="IsExcel"/> 模式下此设置无效
    /// </summary>
    [Parameter]
    public bool ShowCardView { get; set; }

    /// <summary>
    /// 获得/设置 是否显示列选择下拉框 默认为 false 不显示 点击下拉框内列控制是否显示后触发 <see cref="OnColumnVisibleChanged"/> 回调方法
    /// </summary>
    [Parameter]
    public bool ShowColumnList { get; set; }

    /// <summary>
    /// 获得/设置 列选择下拉框图标
    /// </summary>
    [Parameter]
    public string? ColumnListButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 保存、删除失败后是否显示 Toast 提示框 默认为 true 显示
    /// </summary>
    [Parameter]
    public bool ShowToastAfterSaveOrDeleteModel { get; set; } = true;

    /// <summary>
    /// 获得/设置 表格 Toolbar 按钮模板
    /// <para>表格工具栏左侧按钮模板，模板中内容出现在默认按钮前面</para>
    /// </summary>
    [Parameter]
    public RenderFragment? TableToolbarBeforeTemplate { get; set; }

    /// <summary>
    /// 获得/设置 表格 Toolbar 按钮模板
    /// <para>表格工具栏左侧按钮模板，模板中内容出现在默认按钮后面</para>
    /// </summary>
    [Parameter]
    public RenderFragment? TableToolbarTemplate { get; set; }

    /// <summary>
    /// 获得/设置 表格 Toolbar 按钮模板
    /// <para>表格工具栏右侧按钮模板，模板中内容出现在默认按钮前面</para>
    /// </summary>
    [Parameter]
    public RenderFragment? TableExtensionToolbarBeforeTemplate { get; set; }

    /// <summary>
    /// 获得/设置 表格 Toolbar 按钮模板
    /// <para>表格工具栏右侧按钮模板，模板中内容出现在默认按钮后面</para>
    /// </summary>
    [Parameter]
    public RenderFragment? TableExtensionToolbarTemplate { get; set; }

    /// <summary>
    /// 获得/设置 新建按钮回调方法 用于补充模型字段值
    /// </summary>
    /// <remarks>有些场景下新建模型有些属性字段默认值需要更改为默认业务值，或者该属性数据库中设置不可为空，新建模型默认值为空时，可通过此回调进行属性值补充更新</remarks>
    [Parameter]
    public Func<Task<TItem>>? OnAddAsync { get; set; }

    /// <summary>
    /// 获得/设置 编辑按钮回调方法
    /// </summary>
    [Parameter]
    public Func<TItem, Task>? OnEditAsync { get; set; }

    /// <summary>
    /// 获得/设置 保存按钮异步回调方法
    /// </summary>
    [Parameter]
    public Func<TItem, ItemChangedType, Task<bool>>? OnSaveAsync { get; set; }

    /// <summary>
    /// 获得/设置 删除按钮异步回调方法
    /// </summary>
    [Parameter]
    public Func<IEnumerable<TItem>, Task<bool>>? OnDeleteAsync { get; set; }

    /// <summary>
    /// 获得/设置 导出按钮异步回调方法
    /// </summary>
    [Parameter]
    public Func<ITableExportDataContext<TItem>, Task<bool>>? OnExportAsync { get; set; }

    /// <summary>
    /// 获得/设置 保存弹窗中的保存按钮显示文本 默认为资源文件中的 保存
    /// </summary>
    [Parameter]
    public string? EditDialogSaveButtonText { get; set; }

    /// <summary>
    /// 获得/设置 保存弹窗中的保存按钮图标 默认 null 使用当前主题图标
    /// </summary>
    [Parameter]
    public string? EditDialogSaveButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 保存弹窗中的关闭按钮显示文本 默认为资源文件中的 关闭
    /// </summary>
    [Parameter]
    public string? EditDialogCloseButtonText { get; set; }

    /// <summary>
    /// 获得/设置 保存弹窗中的关闭按钮图标 默认 null 使用当前主题图标
    /// </summary>
    [Parameter]
    public string? EditDialogCloseButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 导出数据弹窗 Title 默认为资源文件 导出数据
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ExportToastTitle { get; set; }

    /// <summary>
    /// 获得/设置 导出数据提示内容 默认为资源文件
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ExportToastContent { get; set; }

    /// <summary>
    /// 获得/设置 正在导出数据提示内容 默认为资源文件
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ExportToastInProgressContent { get; set; }

    /// <summary>
    /// 获得/设置 编辑弹窗配置类扩展回调方法 新建/编辑弹窗弹出前回调此方法用于设置弹窗配置信息
    /// </summary>
    [Parameter]
    public Action<ITableEditDialogOption<TItem>>? BeforeShowEditDialogCallback { get; set; }

    /// <summary>
    /// ToastService 服务实例
    /// </summary>
    [Inject]
    [NotNull]
    protected ToastService? Toast { get; set; }

    /// <summary>
    /// DialogService 服务实例
    /// </summary>
    [Inject]
    [NotNull]
    protected DialogService? DialogService { get; set; }

    /// <summary>
    /// DrawerService 服务实例
    /// </summary>
    [Inject]
    [NotNull]
    protected DrawerService? DrawerService { get; set; }

    /// <summary>
    /// 获得/设置 抽屉打开之前回调方法 用于设置 <see cref="DrawerOption"/> 抽屉配置信息
    /// </summary>
    [Parameter]
    public Func<DrawerOption, Task>? OnBeforeShowDrawer { get; set; }

    [Inject]
    [NotNull]
    private ITableExport? TableExport { get; set; }

    /// <summary>
    /// 获得/设置 各列是否显示状态集合
    /// </summary>
    private List<ColumnVisibleItem> VisibleColumns { get; } = [];

    /// <summary>
    /// 获得当前可见列集合
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ITableColumn> GetVisibleColumns()
    {
        // 不可见列
        var items = VisibleColumns.Where(i => i.Visible);
        return Columns.Where(i => !i.GetIgnore() && items.Any(v => v.Name == i.GetFieldName()));
    }

    private bool GetColumnsListState(ColumnVisibleItem item) => VisibleColumns.Find(i => i.Name == item.Name) is { Visible: true } && VisibleColumns.Where(i => i.Visible).DistinctBy(i => i.Name).Count(i => i.Visible) == 1;

    private bool ShowAddForm { get; set; }

    private bool EditInCell { get; set; }

    private bool AddInCell { get; set; }

    private bool InCellMode => AddInCell || EditInCell;

    /// <summary>
    /// 新建按钮方法
    /// </summary>
    public async Task AddAsync()
    {
        if (DynamicContext != null)
        {
            // 数据源为 DataTable 新建后重建行与列
            await DynamicContext.AddAsync(SelectedRows.OfType<IDynamicObject>());
            ResetDynamicContext();

            if (!IsKeepSelectedRowAfterAdd)
            {
                SelectedRows.Clear();
                await OnSelectedRowsChanged();
            }
        }
        else if (IsExcel)
        {
            await InternalOnAddAsync();
            await QueryAsync(false);
            await OnSelectedRowsChanged();
        }
        else
        {
            await ToggleLoading(true);
            await InternalOnAddAsync();
            EditModalTitleString = AddModalTitle;
            if (EditMode == EditMode.Popup)
            {
                await ShowEditDialog(ItemChangedType.Add);
            }
            else if (EditMode == EditMode.EditForm)
            {
                ShowAddForm = true;
                ShowEditForm = false;
            }
            else if (EditMode == EditMode.InCell)
            {
                AddInCell = true;
                EditInCell = false;
                SelectedRows.Add(EditModel);
            }
            else if (EditMode == EditMode.Drawer)
            {
                await ShowEditDrawer(ItemChangedType.Add);
            }
            await OnSelectedRowsChanged();
            await ToggleLoading(false);
        }
    }

    private bool ShowEditForm { get; set; }

    /// <summary>
    /// 编辑按钮方法
    /// </summary>
    public async Task EditAsync()
    {
        if (SelectedRows.Count == 1)
        {
            // 检查是否选中了不可编辑行（行内无编辑按钮）
            if (ShowExtendEditButtonCallback != null && !ShowExtendEditButtonCallback(SelectedRows[0]))
            {
                // 提示不可编辑
                await ShowToastAsync(EditButtonToastTitle, EditButtonToastReadonlyContent);
            }
            else
            {
                await ToggleLoading(true);

                // 跟踪模式与动态类型时使用原始数据，否则使用克隆数据
                EditModel = (IsTracking || DynamicContext != null) ? SelectedRows[0] : Utility.Clone(SelectedRows[0]);
                if (OnEditAsync != null)
                {
                    await OnEditAsync(EditModel);
                }
                else
                {
                    var d = DataService ?? InjectDataService;
                    if (d is IEntityFrameworkCoreDataService ef)
                    {
                        await ef.EditAsync(EditModel);
                    }
                }
                EditModalTitleString = EditModalTitle;

                // 显示编辑框
                if (EditMode == EditMode.Popup)
                {
                    await ShowEditDialog(ItemChangedType.Update);
                }
                else if (EditMode == EditMode.EditForm)
                {
                    ShowEditForm = true;
                    ShowAddForm = false;
                    StateHasChanged();
                }
                else if (EditMode == EditMode.InCell)
                {
                    AddInCell = false;
                    EditInCell = true;
                    StateHasChanged();
                }
                else if (EditMode == EditMode.Drawer)
                {
                    await ShowEditDrawer(ItemChangedType.Update);
                }
                await ToggleLoading(false);
            }
        }
        else
        {
            // 不选或者多选弹窗提示
            var content = SelectedRows.Count == 0 ? EditButtonToastNotSelectContent : EditButtonToastMoreSelectContent;
            await ShowToastAsync(EditButtonToastTitle, content);
        }
    }

    private async Task ShowToastAsync(string title, string content, ToastCategory category = ToastCategory.Information)
    {
        var option = new ToastOption
        {
            Category = category,
            Title = title,
            Content = content
        };
        await Toast.Show(option);
    }

    private async Task ShowDeleteToastAsync(string title, string content, ToastCategory category = ToastCategory.Information)
    {
        var option = new ToastOption
        {
            Category = category,
            Title = title
        };
        option.Content = string.Format(content, Math.Ceiling(option.Delay / 1000.0));
        await Toast.Show(option);
    }

    /// <summary>
    /// 取消保存方法
    /// </summary>
    /// <returns></returns>
    protected void CancelSave()
    {
        if (EditMode == EditMode.EditForm)
        {
            ShowAddForm = false;
            ShowEditForm = false;
        }
        else if (EditMode == EditMode.InCell)
        {
            SelectedRows.Clear();
            AddInCell = false;
            EditInCell = false;
        }
    }

    /// <summary>
    /// 保存数据方法
    /// </summary>
    /// <param name="context"></param>
    /// <param name="changedType"></param>
    /// <returns></returns>
    protected async Task<bool> SaveModelAsync(EditContext context, ItemChangedType changedType)
    {
        bool valid;
        if (DynamicContext != null)
        {
            await DynamicContext.SetValue(context.Model);
            RowsCache = null;
            valid = true;
        }
        else
        {
            valid = await InternalOnSaveAsync((TItem)context.Model, changedType);
        }

        // 回调外部自定义方法
        if (OnAfterSaveAsync != null)
        {
            await OnAfterSaveAsync((TItem)context.Model);
        }
        if (OnAfterModifyAsync != null)
        {
            await OnAfterModifyAsync();
        }
        if (ShowToastAfterSaveOrDeleteModel)
        {
            var option = new ToastOption
            {
                Category = valid ? ToastCategory.Success : ToastCategory.Error,
                Title = SaveButtonToastTitle
            };
            option.Content = string.Format(SaveButtonToastResultContent, valid ? SuccessText : FailText, Math.Ceiling(option.Delay / 1000.0));
            await Toast.Show(option);
        }
        return valid;
    }

    /// <summary>
    /// 保存数据
    /// </summary>
    /// <param name="context"></param>
    /// <param name="changedType"></param>
    protected async Task SaveAsync(EditContext context, ItemChangedType changedType)
    {
        await ToggleLoading(true);
        if (await SaveModelAsync(context, changedType))
        {
            if (EditMode == EditMode.EditForm)
            {
                ShowEditForm = false;
                if (ShowAddForm)
                {
                    ShowAddForm = false;
                    if (IsTracking)
                    {
                        var index = InsertRowMode == InsertRowMode.First ? 0 : Rows.Count;
                        Rows.Insert(index, EditModel);
                        await InvokeItemsChanged();
                    }
                    else
                    {
                        await QueryData();
                    }
                }

                // TODO: 如果有双绑时（bind-SelectedRow）其实这里不需要手动更新，多刷新一次
                StateHasChanged();
            }
            else if (EditMode == EditMode.InCell)
            {
                EditInCell = false;
                AddInCell = false;

                // 通过 EditModel 恢复 编辑数据
                if (changedType == ItemChangedType.Add)
                {
                    var index = InsertRowMode == InsertRowMode.First ? 0 : Rows.Count;
                    Rows.Insert(index, EditModel);
                }
                else
                {
                    var index = Rows.IndexOf(SelectedRows[0]);
                    Rows.RemoveAt(index);
                    Rows.Insert(index, EditModel);
                }
                SelectedRows.Clear();
                if (ItemsChanged.HasDelegate)
                {
                    await ItemsChanged.InvokeAsync(Rows);
                }
                else
                {
                    await QueryAsync();
                }
            }
        }
        await ToggleLoading(false);
    }

    /// <summary>
    /// 编辑框的大小
    /// </summary>
    [Parameter]
    public Size EditDialogSize { get; set; } = Size.ExtraExtraLarge;

    /// <summary>
    /// 获得/设置 编辑框是否可以拖拽 默认 false 不可以拖拽
    /// </summary>
    [Parameter]
    public bool EditDialogIsDraggable { get; set; }

    /// <summary>
    /// 获得/设置 编辑框 FullScreenSize 参数 默认 none
    /// </summary>
    [Parameter]
    public FullScreenSize EditDialogFullScreenSize { get; set; }

    /// <summary>
    /// 获得/设置 编辑框是否显示最大化按钮 默认 true 显示
    /// </summary>
    [Parameter]
    public bool EditDialogShowMaximizeButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 未分组编辑项布局位置 默认 false 在尾部
    /// </summary>
    [Parameter]
    public bool ShowUnsetGroupItemsOnTop { get; set; }

    /// <summary>
    /// 获得/设置 弹窗 Footer
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? EditFooterTemplate { get; set; }

    /// <summary>
    /// 获得/设置 编辑弹窗关闭前回调方法
    /// </summary>
    [Parameter]
    public Func<TItem, bool, Task>? EditDialogCloseAsync { get; set; }

    /// <summary>
    /// 获得/设置 编辑弹窗 Dialog, 可避免弹窗中 Table 再次弹窗时隐藏原表格问题
    /// </summary>
    [Parameter]
    public Dialog? EditDialog { get; set; }

    private async Task AddItem(EditContext context)
    {
        var index = InsertRowMode == InsertRowMode.First ? 0 : Rows.Count;
        Rows.Insert(index, (TItem)context.Model);
        await UpdateRow();
    }

    private async Task EditItem(EditContext context)
    {
        // 使用 Comparer 确保能找到集合中的编辑项
        // 解决可能使用 Clone 副本导致编辑数据与 Items 中数据不一致
        var entity = Rows.FirstOrDefault(i => this.Equals<TItem>(i, (TItem)context.Model));
        if (entity != null)
        {
            var index = Rows.IndexOf(entity);
            Rows.RemoveAt(index);
            Rows.Insert(index, (TItem)context.Model);
            await UpdateRow();
        }
    }

    private async Task UpdateRow()
    {
        if (ItemsChanged.HasDelegate)
        {
            await InvokeItemsChanged();
        }
        else
        {
            Items = Rows;
        }
    }

    private void AppendOptions(ITableEditDialogOption<TItem> option, ItemChangedType changedType)
    {
        option.ShowLoading = ShowLoading;
        option.Model = EditModel;
        option.Items = Columns.Where(i => !i.GetIgnore() && !string.IsNullOrEmpty(i.GetFieldName()));
        option.SaveButtonIcon = EditDialogSaveButtonIcon;
        option.SaveButtonText = EditDialogSaveButtonText;
        option.CloseButtonIcon = EditDialogCloseButtonIcon;
        option.CloseButtonText = EditDialogCloseButtonText;
        option.DialogBodyTemplate = EditTemplate;
        option.RowType = EditDialogRowType;
        option.ItemsPerRow = EditDialogItemsPerRow;
        option.LabelAlign = EditDialogLabelAlign;
        option.ItemChangedType = changedType;
        option.ShowUnsetGroupItemsOnTop = ShowUnsetGroupItemsOnTop;
        option.DisableAutoSubmitFormByEnter = DisableAutoSubmitFormByEnter;
        option.IsTracking = IsTracking;
        option.DialogFooterTemplate = EditFooterTemplate;

        BeforeShowEditDialogCallback?.Invoke(option);
    }

    /// <summary>
    /// 弹出编辑对话框方法
    /// </summary>
    protected async Task ShowEditDialog(ItemChangedType changedType)
    {
        var saved = false;
        var option = new EditDialogOption<TItem>()
        {
            Class = "modal-dialog-table",
            IsScrolling = ScrollingDialogContent,
            IsKeyboard = IsKeyboard,
            Title = EditModalTitleString,
            Size = EditDialogSize,
            IsDraggable = EditDialogIsDraggable,
            ShowMaximizeButton = EditDialogShowMaximizeButton,
            FullScreenSize = EditDialogFullScreenSize,
            OnCloseAsync = () => OnCloseEditDialogCallbackAsync(saved),
            OnEditAsync = async context =>
            {
                saved = await OnSaveEditCallbackAsync(context, changedType);
                return saved;
            }
        };
        AppendOptions(option, changedType);
        await DialogService.ShowEditDialog(option, EditDialog);
    }

    /// <summary>
    /// 弹出编辑抽屉方法
    /// </summary>
    protected async Task ShowEditDrawer(ItemChangedType changedType)
    {
        var saved = false;
        var editOption = new TableEditDrawerOption<TItem>()
        {
            OnCloseAsync = () => OnCloseEditDialogCallbackAsync(saved),
            OnEditAsync = async context =>
            {
                saved = await OnSaveEditCallbackAsync(context, changedType);
                return saved;
            }
        };
        AppendOptions(editOption, changedType);

        var option = new DrawerOption() { Class = "drawer-table-edit", Placement = Placement.Right, AllowResize = true, IsBackdrop = true, Width = "600px" };
        if (OnBeforeShowDrawer != null)
        {
            await OnBeforeShowDrawer(option);
        }
        await DrawerService.ShowEditDrawer(editOption, option);
    }

    private async Task OnCloseEditDialogCallbackAsync(bool saved)
    {
        if (EditDialogCloseAsync != null)
        {
            await EditDialogCloseAsync(EditModel, saved);
        }

        if (!saved)
        {
            // EFCore 模式保存失败后调用 CancelAsync 回调
            var d = DataService ?? InjectDataService;
            if (d is IEntityFrameworkCoreDataService ef)
            {
                // EFCore
                await ToggleLoading(true);
                await ef.CancelAsync();
                await ToggleLoading(false);
            }
        }
    }

    private async Task<bool> OnSaveEditCallbackAsync(EditContext context, ItemChangedType changedType)
    {
        bool saved;
        await ToggleLoading(true);
        if (IsTracking)
        {
            saved = true;
            if (changedType == ItemChangedType.Add)
            {
                var index = InsertRowMode == InsertRowMode.First ? 0 : Rows.Count;
                Rows.Insert(index, EditModel);
            }
            await InvokeItemsChanged();
        }
        else
        {
            saved = await SaveModelAsync(context, changedType);
            if (saved)
            {
                if (Items != null)
                {
                    if (changedType == ItemChangedType.Add)
                    {
                        await AddItem(context);
                    }
                    else if (changedType == ItemChangedType.Update)
                    {
                        await EditItem(context);
                    }
                }
                else
                {
                    await QueryAsync();
                }
            }
        }
        await ToggleLoading(false);
        return saved;
    }

    /// <summary>
    /// 确认删除按钮方法
    /// </summary>
    protected async Task<bool> ConfirmDelete()
    {
        var ret = false;
        if (SelectedRows.Count == 0)
        {
            await ShowDeleteToastAsync(DeleteButtonToastTitle, DeleteButtonToastContent);
        }
        else if (ShowExtendDeleteButtonCallback != null && SelectedRows.Any(i => !ShowExtendDeleteButtonCallback(i)))
        {
            await ShowDeleteToastAsync(DeleteButtonToastTitle, DeleteButtonToastCanNotDeleteContent);
        }
        else
        {
            ret = true;
        }
        return ret;
    }

    /// <summary>
    /// 删除数据方法
    /// </summary>
    protected async Task DeleteAsync()
    {
        if (DynamicContext != null)
        {
            await DynamicContext.DeleteAsync(SelectedRows.OfType<IDynamicObject>());
            ResetDynamicContext();
            SelectedRows.Clear();
            await OnSelectedRowsChanged();
        }
        else if (IsExcel)
        {
            await InternalOnDeleteAsync();
            await QueryAsync();
        }
        else
        {
            await ToggleLoading(true);
            var ret = await DeleteItemsAsync();

            if (ShowToastAfterSaveOrDeleteModel)
            {
                var option = new ToastOption()
                {
                    Title = DeleteButtonToastTitle,
                    Category = ret ? ToastCategory.Success : ToastCategory.Error
                };
                option.Content = string.Format(DeleteButtonToastResultContent, ret ? SuccessText : FailText, Math.Ceiling(option.Delay / 1000.0));
                await Toast.Show(option);
            }
            await ToggleLoading(false);
        }

        async Task<bool> DeleteItemsAsync()
        {
            var ret = await InternalOnDeleteAsync();
            if (ret)
            {
                if (Items != null)
                {
                    SelectedRows.ForEach(i => Rows.Remove(i));
                    if (ItemsChanged.HasDelegate)
                    {
                        await InvokeItemsChanged();
                    }
                }
                else
                {
                    if (IsPagination)
                    {
                        // 删除成功 重新查询
                        // 由于数据删除导致页码会改变，尤其是最后一页
                        // 重新计算页码
                        // https://gitee.com/LongbowEnterprise/BootstrapBlazor/issues/I1UJSL
                        PageIndex = Math.Max(1, Math.Min(PageIndex, int.Parse(Math.Ceiling((TotalCount - SelectedRows.Count) * 1d / _pageItems).ToString())));
                        var items = PageItemsSource.Where(item => item >= (TotalCount - SelectedRows.Count));
                        if (items.Any())
                        {
                            _pageItems = Math.Min(_pageItems, items.Min());
                        }
                    }
                }
                if (OnAfterDeleteAsync != null)
                {
                    await OnAfterDeleteAsync(SelectedRows);
                }
                if (OnAfterModifyAsync != null)
                {
                    await OnAfterModifyAsync();
                }
                SelectedRows.Clear();
                await QueryAsync();
            }
            return ret;
        }
    }

    private void ResetDynamicContext()
    {
        if (DynamicContext != null)
        {
            AutoGenerateColumns = false;

            var cols = DynamicContext.GetColumns();
            Columns.Clear();
            Columns.AddRange(cols);

            // Columns 重构 清空缓存
            FirstFixedColumnCache.Clear();
            LastFixedColumnCache.Clear();

            InternalResetVisibleColumns();

            var queryOption = BuildQueryPageOptions();
            // 设置是否为首次查询
            queryOption.IsFirstQuery = _firstQuery;

            QueryDynamicItems(queryOption, DynamicContext);

            // 重新绑定列拖拽
            _bindResizeColumn = true;
        }
    }

    private void QueryDynamicItems(QueryPageOptions queryOption, IDynamicObjectContext? context)
    {
        RowsCache = null;
        if (context != null)
        {
            var items = context.GetItems();
            if (context.OnFilterCallback != null)
            {
                items = context.OnFilterCallback(queryOption, items);
            }
            if (IsPagination)
            {
                TotalCount = items.Count();
                PageCount = (int)Math.Ceiling(TotalCount * 1.0 / Math.Max(1, _pageItems));
                PageIndex = Math.Max(1, Math.Min(PageIndex, int.Parse(Math.Ceiling((TotalCount - SelectedRows.Count) * 1d / _pageItems).ToString())));
                items = items.Skip((PageIndex - 1) * _pageItems).Take(_pageItems);
            }
            QueryItems = items.Cast<TItem>();

            // 重置选中行
            ResetSelectedRows(QueryItems);
        }
    }

    private async Task ExecuteExportAsync(Func<Task<bool>> callback)
    {
        if (BeforeExportCallback != null)
        {
            await BeforeExportCallback();
        }
        else if (ShowToastBeforeExport)
        {
            var option = new ToastOption
            {
                Title = ExportToastTitle,
                Category = ToastCategory.Information
            };
            option.Content = string.Format(ExportToastInProgressContent, Math.Ceiling(option.Delay / 1000.0));
            await Toast.Show(option);
        }

        var ret = await callback();

        if (AfterExportCallback != null)
        {
            await AfterExportCallback(ret);
        }
        else if (ShowToastAfterExport)
        {
            var option = new ToastOption
            {
                Title = ExportToastTitle,
                Category = ret ? ToastCategory.Success : ToastCategory.Error
            };
            option.Content = string.Format(ExportToastContent, ret ? SuccessText : FailText, Math.Ceiling(option.Delay / 1000.0));
            await Toast.Show(option);
        }
    }

    private Task ExportAsync() => ExecuteExportAsync(() => OnExportAsync != null
        ? OnExportAsync(new TableExportDataContext<TItem>(TableExportType.Unknown, Rows, GetVisibleColumns(), BuildQueryPageOptions()))
        : TableExport.ExportAsync(Rows, GetVisibleColumns()));

    private Task ExportCsvAsync() => ExecuteExportAsync(() => OnExportAsync != null
        ? OnExportAsync(new TableExportDataContext<TItem>(TableExportType.Csv, Rows, GetVisibleColumns(), BuildQueryPageOptions()))
        : TableExport.ExportCsvAsync(Rows, GetVisibleColumns()));

    private Task ExportPdfAsync() => ExecuteExportAsync(() => OnExportAsync != null
        ? OnExportAsync(new TableExportDataContext<TItem>(TableExportType.Pdf, Rows, GetVisibleColumns(), BuildQueryPageOptions()))
        : TableExport.ExportPdfAsync(Rows, GetVisibleColumns()));

    private Task ExportExcelAsync() => ExecuteExportAsync(() => OnExportAsync != null
        ? OnExportAsync(new TableExportDataContext<TItem>(TableExportType.Excel, Rows, GetVisibleColumns(), BuildQueryPageOptions()))
        : TableExport.ExportExcelAsync(Rows, GetVisibleColumns()));

    /// <summary>
    /// 获取当前 Table 选中的所有行数据
    /// </summary>
    /// <returns></returns>
    protected IEnumerable<TItem> GetSelectedRows() => SelectedRows;

    /// <summary>
    /// 是否显示行内编辑按钮
    /// </summary>
    /// <returns></returns>
    protected bool GetShowExtendEditButton(TItem item) => ShowExtendEditButtonCallback?.Invoke(item) ?? ShowExtendEditButton;

    /// <summary>
    /// 是否显示行内删除按钮
    /// </summary>
    /// <returns></returns>
    protected bool GetShowExtendDeleteButton(TItem item) => ShowExtendDeleteButtonCallback?.Invoke(item) ?? ShowExtendDeleteButton;
}
