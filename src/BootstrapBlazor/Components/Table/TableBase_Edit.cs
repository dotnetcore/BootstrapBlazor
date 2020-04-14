using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    partial class TableBase<TItem>
    {
        /// <summary>
        /// 获得/设置 删除按钮提示弹框实例
        /// </summary>
        protected PopoverConfirm? DeleteConfirm { get; set; }

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
        /// 编辑数据弹窗
        /// </summary>
        protected Modal? EditModal { get; set; }

        /// <summary>
        /// 编辑数据弹窗 Title
        /// </summary>
        [Parameter] public string? EditModalTitle { get; set; }

        /// <summary>
        /// 获得/设置 EditTemplate 实例
        /// </summary>
        [Parameter] public RenderFragment<TItem?>? EditTemplate { get; set; }

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
                //SelectedItems.Clear();
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
    }
}
