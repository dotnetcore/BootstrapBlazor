using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    partial class TableBase<TItem>
    {
        /// <summary>
        /// 获得 Checkbox 样式表集合
        /// </summary>
        /// <returns></returns>
        protected string? ButtonColumnClass => CssBuilder.Default("table-th-button")
            .Build();

        /// <summary>
        /// 获得/设置 删除按钮提示弹框实例
        /// </summary>
        protected PopoverConfirm? DeleteConfirm { get; set; }

        /// <summary>
        /// 获得/设置 删除按钮提示弹框实例
        /// </summary>
        protected PopoverConfirm? ButtonDeleteConfirm { get; set; }

        /// <summary>
        /// 获得/设置 编辑弹窗 Title 文字
        /// </summary>
        protected string? EditModalTitleString { get; set; }

        /// <summary>
        /// 获得/设置 被选中数据集合
        /// </summary>
        /// <value></value>
        protected List<TItem> SelectedItems { get; set; } = new List<TItem>();

        /// <summary>
        /// 获得/设置 编辑表单实例
        /// </summary>
        protected ValidateForm? ValidateForm { get; set; }

        /// <summary>
        /// 获得/设置 编辑数据弹窗实例
        /// </summary>
        protected Modal? EditModal { get; set; }

        /// <summary>
        /// 获得/设置 编辑数据弹窗 Title
        /// </summary>
        [Parameter] public string EditModalTitle { get; set; } = "编辑数据窗口";

        /// <summary>
        /// 获得/设置 新建数据弹窗 Title
        /// </summary>
        [Parameter] public string AddModalTitle { get; set; } = "新建数据窗口";

        /// <summary>
        /// 获得/设置 新建数据弹窗 Title
        /// </summary>
        [Parameter] public string ColumnButtonTemplateHeaderText { get; set; } = "操作";

        /// <summary>
        /// 获得/设置 EditTemplate 实例
        /// </summary>
        [Parameter] public RenderFragment<TItem?>? EditTemplate { get; set; }

        /// <summary>
        /// 获得/设置 EditTemplate 实例
        /// </summary>
        [Parameter] public RenderFragment<TItem?>? RowButtonTemplate { get; set; }

        /// <summary>
        /// 获得/设置 EditModel 实例
        /// </summary>
        [Parameter] public TItem? EditModel { get; set; }

        /// <summary>
        /// 查询按钮调用此方法
        /// </summary>
        public void Query()
        {
            QueryData();
            StateHasChanged();
        }

        /// <summary>
        /// 调用 OnQuery 回调方法获得数据源
        /// </summary>
        protected void QueryData()
        {
            SelectedItems.Clear();
            if (OnQuery != null)
            {
                var queryData = OnQuery(new QueryPageOptions()
                {
                    PageIndex = PageIndex,
                    PageItems = PageItems,
                    SearchText = SearchText,
                    SortOrder = SortOrder,
                    SortName = SortName
                });
                Items = queryData.Items;
                TotalCount = queryData.TotalCount;
                IsFiltered = queryData.IsFiltered;
            }
        }

        /// <summary>
        /// 行尾列按钮点击回调此方法
        /// </summary>
        /// <param name="method"></param>
        /// <param name="item"></param>
        protected void ClickButton(string method, TItem item)
        {
            SelectedItems.Clear();
            SelectedItems.Add(item);
            if (method == "edit")
            {
                // 更新行选中状态
                Edit();
            }
            else if (method == "delete")
            {
                var content = "确定要删除本条数据吗？";
                //ButtonDeleteConfirm?.Show(content: content);
            }
            StateHasChanged();
        }
    }
}
