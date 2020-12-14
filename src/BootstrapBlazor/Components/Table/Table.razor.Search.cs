// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    public partial class Table<TItem>
    {
        /// <summary>
        /// 获得 高级搜索样式
        /// </summary>
        protected string? AdvanceSearchClass => CssBuilder.Default("btn btn-secondary")
            .AddClass("btn-info", IsSearch)
            .Build();

        /// <summary>
        /// 获得/设置 是否搜索
        /// </summary>
        protected bool IsSearch { get; set; }

        /// <summary>
        /// 获得/设置 是否数据过滤
        /// </summary>
        protected bool IsFiltered { get; set; }

        /// <summary>
        /// 获得/设置 是否数据排序
        /// </summary>
        protected bool IsSorted { get; set; }

        /// <summary>
        /// 获得/设置 SearchTemplate 实例
        /// </summary>
        [Parameter]
        public RenderFragment<TItem>? SearchTemplate { get; set; }

        /// <summary>
        /// 获得/设置 SearchModel 实例
        /// </summary>
        [Parameter]
        public TItem SearchModel { get; set; } = new TItem();

        /// <summary>
        /// 获得/设置 是否显示搜索框 默认为 false 不显示搜索框
        /// </summary>
        [Parameter]
        public bool ShowSearch { get; set; }

        /// <summary>
        /// 获得/设置 是否显示高级搜索按钮 默认显示
        /// </summary>
        [Parameter]
        public bool ShowAdvancedSearch { get; set; } = true;

        /// <summary>
        /// 获得/设置 搜索关键字
        /// </summary>
        [Parameter]
        public string SearchText { get; set; } = "";

        /// <summary>
        /// 重置搜索按钮异步回调方法
        /// </summary>
        [Parameter]
        public Func<TItem, Task>? OnResetSearchAsync { get; set; }

        /// <summary>
        /// 重置查询方法
        /// </summary>
        protected async Task ResetSearchClick()
        {
            if (OnResetSearchAsync != null) await OnResetSearchAsync(SearchModel);
            else if (SearchTemplate == null) SearchModel.Reset();
            await SearchClick();
        }

        /// <summary>
        /// 查询方法
        /// </summary>
        protected async Task SearchClick()
        {
            PageIndex = 1;
            await QueryAsync();
        }

        /// <summary>
        /// 高级查询按钮点击时调用此方法
        /// </summary>
        protected Task ShowSearchDialog()
        {
            // 弹出高级查询弹窗
            DialogOption.IsScrolling = ScrollingDialogContent;
            DialogOption.Size = Size.ExtraLarge;
            DialogOption.Title = SearchModalTitle;
            DialogOption.ShowCloseButton = false;
            DialogOption.ShowFooter = false;
            DialogOption.OnCloseAsync = null;

            var columns = Columns.Where(i => i.Searchable).ToList();
            columns.ForEach(i => i.EditTemplate = i.SearchTemplate);
            var editorParameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>(nameof(TableSearchDialog<TItem>.Model), SearchModel),
                new KeyValuePair<string, object>(nameof(TableSearchDialog<TItem>.Columns), columns),
                new KeyValuePair<string, object>(nameof(TableSearchDialog<TItem>.ShowLabel), true),
                new KeyValuePair<string, object>(nameof(TableSearchDialog<TItem>.BodyTemplate), SearchTemplate!),
                new KeyValuePair<string, object>(nameof(TableSearchDialog<TItem>.OnResetSearchClick), new Func<Task>(ResetSearchClick)),
                new KeyValuePair<string, object>(nameof(TableSearchDialog<TItem>.OnSearchClick), new Func<Task>(SearchClick)),
            };
            DialogOption.Component = DynamicComponent.CreateComponent<TableSearchDialog<TItem>>(editorParameters);

            DialogService.Show(DialogOption);

            return Task.CompletedTask;
        }

        /// <summary>
        /// 重置搜索按钮调用此方法
        /// </summary>
        protected async Task ClearSearchClick()
        {
            SearchText = "";
            await ResetSearchClick();
        }

        /// <summary>
        /// 搜索文本框按键回调方法
        /// </summary>
        /// <param name="e"></param>
        protected async Task OnKeyUp(KeyboardEventArgs e)
        {
            // Enter Escape
            if (e.Key == "Enter")
            {
                await SearchClick();
            }
            else if (e.Key == "Escape")
            {
                await ClearSearchClick();
            }
        }
    }
}
