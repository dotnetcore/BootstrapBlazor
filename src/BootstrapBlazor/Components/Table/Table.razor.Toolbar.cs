// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
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
    /// 获得/设置 是否显示加载骨架屏 默认 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowSkeleton { get; set; }

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
    /// 获得/设置 是否显示编辑按钮 默认为 true 显示 <see cref="ShowEditButtonCallback" />
    /// </summary>
    [Parameter]
    public bool ShowEditButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示编辑按钮 设置此参数时 <see cref="ShowEditButton" /> 参数不起作用 默认为 null 
    /// </summary>
    [Parameter]
    public Func<TItem, bool>? ShowEditButtonCallback { get; set; }

    /// <summary>
    /// 获得/设置 是否显示删除按钮 默认为 true 显示 <see cref="ShowDeleteButtonCallback" />
    /// </summary>
    [Parameter]
    public bool ShowDeleteButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示删除按钮  设置此参数时 <see cref="ShowDeleteButton" /> 参数不起作用 默认为 null 
    /// </summary>
    [Parameter]
    public Func<TItem, bool>? ShowDeleteButtonCallback { get; set; }

    /// <summary>
    /// 获得/设置 是否显示导出按钮 默认为 false 显示
    /// </summary>
    [Parameter]
    public bool ShowExportButton { get; set; }

    /// <summary>
    /// 获得/设置 是否显示扩展按钮 默认为 false
    /// </summary>
    [Parameter]
    public bool ShowExtendButtons { get; set; }

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
    /// 获得/设置 是否固定扩展按钮列 默认为 false 不固定
    /// </summary>
    [Parameter]
    public bool FixedExtendButtonsColumn { get; set; }

    /// <summary>
    /// 获得/设置 是否显示刷新按钮 默认为 true
    /// </summary>
    /// <remarks><see cref="IsExcel"/> 模式下此设置无效</remarks>
    [Parameter]
    public bool ShowRefresh { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示视图按钮 默认为 false
    /// </summary>
    /// <remarks><see cref="IsExcel"/> 模式下此设置无效</remarks>
    [Parameter]
    public bool ShowCardView { get; set; }

    /// <summary>
    /// 获得/设置 是否显示列选择下拉框 默认为 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowColumnList { get; set; }

    /// <summary>
    /// 获得/设置 保存、删除失败后是否显示 Toast 提示框 默认为 true 显示
    /// </summary>
    /// <remarks>已过期请使用 <see cref="ShowToastAfterSaveOrDeleteModel"/></remarks>
    [Parameter]
    [Obsolete("请使用 ShowToastAfterSaveOrDeleteModel")]
    public bool ShowErrorToast
    {
        get => ShowToastAfterSaveOrDeleteModel;
        set => ShowToastAfterSaveOrDeleteModel = value;
    }

    /// <summary>
    /// 获得/设置 保存、删除失败后是否显示 Toast 提示框 默认为 true 显示
    /// </summary>
    [Parameter]
    public bool ShowToastAfterSaveOrDeleteModel { get; set; } = true;

    /// <summary>
    /// 获得/设置 表格 Toolbar 按钮模板
    /// </summary>
    [Parameter]
    public RenderFragment? TableToolbarTemplate { get; set; }

    /// <summary>
    /// 获得/设置 新建按钮回调方法
    /// </summary>
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
    public Func<IEnumerable<TItem>, Task<bool>>? OnExportAsync { get; set; }

    /// <summary>
    /// 获得/设置 保存弹窗中的保存按钮显示文本 默认为资源文件中的 保存
    /// </summary>
    [Parameter]
    public string? EditDialogSaveButtonText { get; set; }

    /// <summary>
    /// 获得/设置 保存弹窗中的关闭按钮显示文本 默认为资源文件中的 关闭
    /// </summary>
    [Parameter]
    public string? EditDialogCloseButtonText { get; set; }

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

    [Inject]
    [NotNull]
    private ITableExcelExport? ExcelExport { get; set; }

    /// <summary>
    /// 获得/设置 各列是否显示状态集合
    /// </summary>
    [NotNull]
    private IEnumerable<ColumnVisibleItem>? ColumnVisibles { get; set; }

    private class ColumnVisibleItem
    {
        [NotNull]
        public string? FieldName { get; set; }

        public bool Visible { get; set; }
    }

    private IEnumerable<ITableColumn> GetColumns()
    {
        // https://gitee.com/LongbowEnterprise/BootstrapBlazor/issues/I2LBM8
        var items = ColumnVisibles?.Where(i => i.Visible);
        return Columns.Where(i => items?.Any(v => v.FieldName == i.GetFieldName()) ?? true);
    }

    private bool GetColumnsListState(ITableColumn col)
    {
        return ColumnVisibles.First(i => i.FieldName == col.GetFieldName()).Visible && ColumnVisibles.Count(i => i.Visible) == 1;
    }

    private bool ShowAddForm { get; set; }

    private bool EditInCell { get; set; }

    private bool AddInCell { get; set; }

    /// <summary>
    /// 新建按钮方法
    /// </summary>
    public async Task AddAsync()
    {
        if (IsExcel || DynamicContext != null)
        {
            await AddDynamicOjbectExcelModelAsync();
        }
        else if (IsTracking || CanSave)
        {
            await AddItemAsync();
        }
        else
        {
            await ShowAddToastAsync(SaveButtonToastContent);
        }

        async Task ShowAddToastAsync(string content)
        {
            var option = new ToastOption
            {
                Category = ToastCategory.Error,
                Title = AddButtonToastTitle,
                Content = content
            };
            await Toast.Show(option);
        }

        async Task AddItemAsync()
        {
            await ToggleLoading(true);

            await InternalOnAddAsync();

            SelectedRows.Clear();

            EditModalTitleString = AddModalTitle;
            if (IsTracking)
            {
                RowItems.Insert(0, EditModel);
            }
            if (EditMode == EditMode.Popup)
            {
                await ShowEditDialog(ItemChangedType.Add);
            }
            else if (EditMode == EditMode.EditForm)
            {
                ShowAddForm = true;
                ShowEditForm = false;

                await UpdateAsync();
            }
            else if (EditMode == EditMode.InCell)
            {
                AddInCell = true;
                EditInCell = true;
                SelectedRows.Add(EditModel);

                await UpdateAsync();
            }
            await OnSelectedRowsChanged();
            await ToggleLoading(false);
        }

        async Task AddDynamicOjbectExcelModelAsync()
        {
            if (DynamicContext != null)
            {
                // 数据源为 DataTable 新建后重建行与列
                await DynamicContext.AddAsync(SelectedRows.OfType<IDynamicObject>());
                ResetDynamicContext();
                StateHasChanged();
            }
            else
            {
                await InternalOnAddAsync();
                SelectedRows.Clear();
                RowItemsCache = null;
                await OnSelectedRowsChanged();
                await QueryAsync();
            }
        }
    }

    private bool ShowEditForm { get; set; }

    /// <summary>
    /// 编辑按钮方法
    /// </summary>
    public async Task EditAsync()
    {
        if (IsTracking || CanSave || DynamicContext != null)
        {
            if (SelectedRows.Count == 1)
            {
                // 检查是否选中了不可编辑行（行内无编辑按钮）
                if (ShowEditButtonCallback != null && !ShowEditButtonCallback(SelectedRows[0]))
                {
                    // 提示不可编辑
                    await ShowToastAsync(ToastCategory.Information, EditButtonToastReadonlyContent);
                }
                else
                {
                    await ToggleLoading(true);
                    await InternalOnEditAsync();
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
                        await UpdateAsync();

                    }
                    else if (EditMode == EditMode.InCell)
                    {
                        AddInCell = false;
                        EditInCell = true;
                        await UpdateAsync();

                    }
                    await ToggleLoading(false);
                }
            }
            else
            {
                var content = SelectedRows.Count == 0 ? EditButtonToastNotSelectContent : EditButtonToastMoreSelectContent;
                await ShowToastAsync(ToastCategory.Information, content);
            }
        }
        else
        {
            await ShowToastAsync(ToastCategory.Error, EditButtonToastNoSaveMethodContent);
        }

        async Task ShowToastAsync(ToastCategory category, string content)
        {
            var option = new ToastOption
            {
                Category = category,
                Title = EditButtonToastTitle,
                Content = content
            };
            await Toast.Show(option);
        }
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
            RowItemsCache = null;
            valid = true;
        }
        else
        {
            valid = await InternalOnSaveAsync((TItem)context.Model, changedType);
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
        if (DynamicContext != null || CanSave)
        {
            await ToggleLoading(true);
            if (await SaveModelAsync(context, changedType))
            {
                if (EditMode == EditMode.Popup)
                {
                    await QueryAsync();
                }
                else if (EditMode == EditMode.EditForm)
                {
                    if (ShowAddForm)
                    {
                        await QueryData();
                        ShowAddForm = false;
                    }
                    ShowEditForm = false;
                    StateHasChanged();
                }
                else if (EditMode == EditMode.InCell)
                {
                    SelectedRows.Clear();
                    EditInCell = false;
                    if (AddInCell)
                    {
                        AddInCell = false;
                        await QueryAsync();
                    }
                }
            }
            await ToggleLoading(false);
        }
        else
        {
            var option = new ToastOption
            {
                Category = ToastCategory.Error,
                Title = SaveButtonToastTitle,
                Content = SaveButtonToastContent
            };
            await Toast.Show(option);
        }
    }

    /// <summary>
    /// 编辑框的大小
    /// </summary>
    [Parameter]
    public Size EditDialogSize { get; set; } = Size.Large;

    /// <summary>
    /// 获得/设置 编辑框是否可以拖拽 默认 false 不可以拖拽
    /// </summary>
    [Parameter]
    public bool EditDialogIsDraggable { get; set; }

    /// <summary>
    /// 获得/设置 编辑框是否显示最大化按钮 默认 false 不显示
    /// </summary>
    [Parameter]
    public bool EditDialogShowMaximizeButton { get; set; }

    /// <summary>
    /// 弹出编辑对话框方法
    /// </summary>
    protected async Task ShowEditDialog(ItemChangedType changedType)
    {
        var option = new EditDialogOption<TItem>()
        {
            IsTracking = IsTracking,
            IsScrolling = ScrollingDialogContent,
            IsKeyboard = IsKeyboard,
            ShowLoading = ShowLoading,
            Title = EditModalTitleString,
            Model = EditModel,
            Items = Columns.Where(i => i.Editable),
            SaveButtonText = EditDialogSaveButtonText,
            CloseButtonText = EditDialogCloseButtonText,
            DialogBodyTemplate = EditTemplate,
            RowType = EditDialogRowType,
            ItemsPerRow = EditDialogItemsPerRow,
            LabelAlign = EditDialogLabelAlign,
            ItemChangedType = changedType,
            Size = EditDialogSize,
            IsDraggable = EditDialogIsDraggable,
            ShowMaximizeButton = EditDialogShowMaximizeButton,
            OnCloseAsync = async () =>
            {
                var d = DataService ?? InjectDataService;
                if (d is IEntityFrameworkCoreDataService ef)
                {
                    // EFCore
                    await ToggleLoading(true);
                    await ef.CancelAsync();
                    await ToggleLoading(false);
                }
                await UpdateAsync();
            },
            OnEditAsync = async context =>
            {
                await ToggleLoading(true);
                var valid = await SaveModelAsync(context, changedType);
                if (valid)
                {
                    if (DynamicContext != null)
                    {
                        SelectedRows.Clear();
                    }
                    await QueryAsync();
                }
                await ToggleLoading(false);
                return valid;
            }
        };
        await DialogService.ShowEditDialog(option);
    }

    private async Task UpdateAsync()
    {
        if (ItemsChanged.HasDelegate)
        {
            await ItemsChanged.InvokeAsync(RowItems);
        }
        else
        {
            StateHasChanged();
        }
    }

    /// <summary>
    /// 确认删除按钮方法
    /// </summary>
    protected async Task<bool> ConfirmDelete()
    {
        var ret = false;
        if (SelectedRows.Count == 0)
        {
            await ShowToastAsync(DeleteButtonToastContent);
        }
        else
        {
            if (ShowDeleteButtonCallback != null && SelectedRows.Any(i => !ShowDeleteButtonCallback(i)))
            {
                await ShowToastAsync(DeleteButtonToastCanNotDeleteContent);
            }
            else
            {
                ret = true;
            }
        }
        return ret;

        async Task ShowToastAsync(string content)
        {
            var option = new ToastOption
            {
                Category = ToastCategory.Information,
                Title = DeleteButtonToastTitle
            };
            option.Content = string.Format(content, Math.Ceiling(option.Delay / 1000.0));
            await Toast.Show(option);
        }
    }

    /// <summary>
    /// 删除数据方法
    /// </summary>
    protected async Task DeleteAsync()
    {
        if (IsExcel || DynamicContext != null)
        {
            await DeleteDynamicObjectExcelModelAsync();
        }
        else if (IsTracking)
        {
            RowItems.RemoveAll(i => SelectedRows.Contains(i));
            SelectedRows.Clear();
            await UpdateAsync();
        }
        else
        {
            await ToggleLoading(true);
            var ret = await DelteItemsAsync();

            if (ret && ShowToastAfterSaveOrDeleteModel && !IsTracking)
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

        async Task<bool> DelteItemsAsync()
        {
            var ret = await InternalOnDeleteAsync();
            if (ret)
            {
                // 删除成功 重新查询
                // 由于数据删除导致页码会改变，尤其是最后一页
                // 重新计算页码
                // https://gitee.com/LongbowEnterprise/BootstrapBlazor/issues/I1UJSL
                PageIndex = Math.Max(1, Math.Min(PageIndex, int.Parse(Math.Ceiling((TotalCount - SelectedRows.Count) * 1d / PageItems).ToString())));
                var items = PageItemsSource.Where(item => item >= (TotalCount - SelectedRows.Count));
                PageItems = Math.Min(PageItems, items.Any() ? items.Min() : PageItems);
                SelectedRows.Clear();
                await QueryAsync();
            }
            return ret;
        }

        async Task DeleteDynamicObjectExcelModelAsync()
        {
            if (DynamicContext != null)
            {
                await DynamicContext.DeleteAsync(SelectedRows.AsEnumerable().OfType<IDynamicObject>());
                ResetDynamicContext();
                StateHasChanged();
            }
            else
            {
                if (CanDelete)
                {
                    await InternalOnDeleteAsync();
                    await QueryAsync();
                }
            }
        }
    }

    private void ResetDynamicContext()
    {
        if (DynamicContext != null && typeof(TItem).IsAssignableTo(typeof(IDynamicObject)))
        {
            AutoGenerateColumns = false;

            var cols = DynamicContext.GetColumns();
            Columns.Clear();
            Columns.AddRange(cols);

            QueryItems = DynamicContext.GetItems().Cast<TItem>();
            RowItemsCache = null;
        }
    }

    /// <summary>
    /// 确认导出按钮方法
    /// </summary>
    protected async Task<bool> ConfirmExport()
    {
        var ret = false;
        if (!RowItems.Any())
        {
            var option = new ToastOption
            {
                Category = ToastCategory.Information,
                Title = "导出数据"
            };
            option.Content = $"没有需要导出的数据, {Math.Ceiling(option.Delay / 1000.0)} 秒后自动关闭";
            await Toast.Show(option);
        }
        else
        {
            ret = true;
        }
        return ret;
    }

    /// <summary>
    /// 导出数据方法
    /// </summary>
    protected async Task ExportAsync()
    {
        var ret = false;

        _ = Task.Run(async () =>
        {
            if (OnExportAsync != null)
            {
                ret = await OnExportAsync(RowItems);
            }
            else
            {
                // 如果未提供 OnExportAsync 回调委托使用注入服务来尝试解析
                // TODO: 这里将本页数据作为参数传递给导出服务，服务本身可以利用自身优势获取全部所需数据，如果获取全部数据呢？
                ret = await ExcelExport.ExportAsync(RowItems, Columns, JSRuntime);
            }

            var option = new ToastOption()
            {
                Title = "导出数据"
            };
            option.Category = ret ? ToastCategory.Success : ToastCategory.Error;
            option.Content = $"导出数据{(ret ? "成功" : "失败")}, {Math.Ceiling(option.Delay / 1000.0)} 秒后自动关闭";

            await Toast.Show(option);
        });

        var option = new ToastOption()
        {
            Title = "导出数据"
        };
        option.Category = ToastCategory.Information;
        option.Content = $"正在导出数据，请稍候, {Math.Ceiling(option.Delay / 1000.0)} 秒后自动关闭";

        await Toast.Show(option);

        await Task.CompletedTask;
    }

    /// <summary>
    /// 获取当前 Table 选中的所有行数据
    /// </summary>
    /// <returns></returns>
    protected IEnumerable<TItem> GetSelectedRows() => SelectedRows;

    /// <summary>
    /// 是否显示行内编辑按钮
    /// </summary>
    /// <returns></returns>
    protected bool GetShowEditButton(TItem item) => ShowToolbar && ShowDefaultButtons && (ShowEditButtonCallback == null ? ShowEditButton : ShowEditButtonCallback(item));

    /// <summary>
    /// 是否显示行内删除按钮
    /// </summary>
    /// <returns></returns>
    protected bool GetShowDeleteButton(TItem item) => ShowToolbar && ShowDefaultButtons && (ShowDeleteButtonCallback == null ? ShowDeleteButton : ShowDeleteButtonCallback(item));
}
