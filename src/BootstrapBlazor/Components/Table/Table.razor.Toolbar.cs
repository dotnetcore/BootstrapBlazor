using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    partial class Table<TItem>
    {
        /// <summary>
        /// 获得/设置 是否显示工具栏 默认 false 不显示
        /// </summary>
        [Parameter]
        public bool ShowToolbar { get; set; }

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
        /// 获得/设置 是否显示编辑按钮 默认为 true 显示
        /// </summary>
        [Parameter]
        public bool ShowEditButton { get; set; } = true;

        /// <summary>
        /// 获得/设置 是否显示删除按钮 默认为 true 显示
        /// </summary>
        [Parameter]
        public bool ShowDeleteButton { get; set; } = true;

        /// <summary>
        /// 获得/设置 是否显示扩展按钮 默认为 false
        /// </summary>
        [Parameter]
        public bool ShowExtendButtons { get; set; }

        /// <summary>
        /// 获得/设置 行内操作列宽度 默认为 130
        /// </summary>
        [Parameter]
        public int ExtendButtonColumnWidth { get; set; } = 130;

        /// <summary>
        /// 获得/设置 是否显示刷新按钮 默认为 true
        /// </summary>
        [Parameter]
        public bool ShowRefresh { get; set; } = true;

        /// <summary>
        /// 获得/设置 按钮列 Header 文本 默认为 操作
        /// </summary>
        [Parameter]
        public string ButtonTemplateHeaderText { get; set; } = "操作";

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
        /// 获得/设置 保存按钮异步回调方法
        /// </summary>
        [Parameter]
        public Func<TItem, Task<bool>>? OnSaveAsync { get; set; }

        /// <summary>
        /// 获得/设置 删除按钮异步回调方法
        /// </summary>
        [Parameter]
        public Func<IEnumerable<TItem>, Task<bool>>? OnDeleteAsync { get; set; }

#nullable disable
        /// <summary>
        /// ToastService 服务实例
        /// </summary>
        [Inject]
        protected ToastService Toast { get; set; }

        /// <summary>
        /// DialogService 服务实例
        /// </summary>
        [Inject]
        protected DialogService DialogService { get; set; }
#nullable restore

        /// <summary>
        /// 新建按钮方法
        /// </summary>
        public async Task AddAsync()
        {
            if (OnSaveAsync != null)
            {
                if (OnAddAsync != null) EditModel = await OnAddAsync();
                else EditModel = new TItem();

                SelectedItems.Clear();
                EditModalTitleString = AddModalTitle;

                ShowEditorDialog();
            }
            else
            {
                var option = new ToastOption
                {
                    Category = ToastCategory.Error,
                    Title = "新建数据",
                    Content = "未提供保存数据方法，无法新建数据"
                };
                Toast.Show(option);
            }
        }

        /// <summary>
        /// 编辑按钮方法
        /// </summary>
        public Task EditAsync()
        {
            if (OnSaveAsync != null)
            {
                if (SelectedItems.Count == 1)
                {
                    EditModel = SelectedItems[0].Clone();
                    EditModalTitleString = EditModalTitle;

                    ShowEditorDialog();
                }
                else
                {
                    var option = new ToastOption
                    {
                        Category = ToastCategory.Information,
                        Title = "编辑数据",
                        Content = SelectedItems.Count == 0 ? "请选择要编辑的数据" : "只能选择一项要编辑的数据"
                    };
                    Toast.Show(option);
                }
            }
            else
            {
                var option = new ToastOption
                {
                    Category = ToastCategory.Error,
                    Title = "编辑数据",
                    Content = "未提供保存数据方法，无法编辑数据"
                };
                Toast.Show(option);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="context"></param>
        protected async Task Save(EditContext context)
        {
            var valid = false;
            if (OnSaveAsync != null)
            {
                valid = await OnSaveAsync((TItem)context.Model);
                var option = new ToastOption
                {
                    Category = valid ? ToastCategory.Success : ToastCategory.Error,
                    Title = "保存数据"
                };
                option.Content = $"保存数据{(valid ? "成功" : "失败")}, {Math.Ceiling(option.Delay / 1000.0)} 秒后自动关闭";
                Toast.Show(option);
                if (valid)
                {
                    DialogOption.Dialog?.Toggle();
                    await QueryAsync();
                }
            }
            else
            {
                var option = new ToastOption
                {
                    Category = ToastCategory.Error,
                    Title = "编辑数据",
                    Content = "未提供保存数据方法，无法保存数据"
                };
                Toast.Show(option);
            }
        }

        private readonly DialogOption DialogOption = new DialogOption();

        /// <summary>
        /// 
        /// </summary>
        protected void ShowEditorDialog()
        {
            DialogOption.IsScrolling = ScrollingDialogContent;
            DialogOption.ShowFooter = false;
            DialogOption.Size = Size.ExtraLarge;
            DialogOption.Title = EditModalTitleString ?? "未设置";

            var editorParameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>(nameof(TableEditorDialog<TItem>.Model), EditModel),
                new KeyValuePair<string, object>(nameof(TableEditorDialog<TItem>.Columns), Columns),
                new KeyValuePair<string, object>(nameof(TableEditorDialog<TItem>.OnSaveAsync), new Func<EditContext, Task>(Save)),
                new KeyValuePair<string, object>(nameof(TableEditorDialog<TItem>.ShowLabel), false),
                new KeyValuePair<string, object>(nameof(TableEditorDialog<TItem>.BodyTemplate), EditTemplate)
            };
            DialogOption.Component = DynamicComponent.CreateComponent<TableEditorDialog<TItem>>(editorParameters);

            DialogService.Show(DialogOption);
        }

        /// <summary>
        /// 确认删除按钮方法
        /// </summary>
        protected Task<bool> ConfirmDelete()
        {
            var ret = false;
            if (SelectedItems.Count == 0)
            {
                var option = new ToastOption
                {
                    Category = ToastCategory.Information,
                    Title = "删除数据"
                };
                option.Content = $"请选择要删除的数据, {Math.Ceiling(option.Delay / 1000.0)} 秒后自动关闭";
                Toast.Show(option);
            }
            else
            {
                ret = true;
            }
            return Task.FromResult(ret);
        }

        /// <summary>
        /// 删除数据方法
        /// </summary>
        protected async Task DeleteAsync()
        {
            var ret = false;
            if (OnDeleteAsync != null) ret = await OnDeleteAsync(SelectedItems);
            var option = new ToastOption()
            {
                Title = "删除数据"
            };
            option.Category = ret ? ToastCategory.Success : ToastCategory.Error;
            option.Content = $"删除数据{(ret ? "成功" : "失败")}, {Math.Ceiling(option.Delay / 1000.0)} 秒后自动关闭";

            if (ret)
            {
                // 删除成功 重新查询
                // 由于数据删除导致页码会改变，尤其是最后一页
                // 重新计算页码
                // https://gitee.com/LongbowEnterprise/BootstrapBlazor/issues/I1UJSL
                PageIndex = Math.Max(1, Math.Min(PageIndex, int.Parse(Math.Ceiling((TotalCount - SelectedItems.Count) * 1d / PageItems).ToString())));
                var items = PageItemsSource.Where(item => item >= (TotalCount - SelectedItems.Count));
                PageItems = Math.Min(PageItems, items.Any() ? items.Min() : PageItems);

                SelectedItems.Clear();
                await QueryAsync();
            }
            Toast.Show(option);
        }

        /// <summary>
        /// 获取当前 Table 选中的所有行数据
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<TItem> GetSelectedRows() => SelectedItems;
    }
}
