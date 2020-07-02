using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    public partial class TableBase<TItem>
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
        /// 高级查询弹窗
        /// </summary>
        protected Modal? SearchModal { get; set; }

        /// <summary>
        /// 获得/设置 搜索框提示文字
        /// </summary>
        protected string SearchTooltip { get; set; } = "<div class='search-input-tooltip'>输入任意字符串全局搜索</br><kbd>Enter</kbd> 搜索 <kbd>ESC</kbd> 清除搜索</div>";

        /// <summary>
        /// 获得 搜索条件集合
        /// </summary>
        protected List<FilterKeyValueAction> Searchs { get; } = new List<FilterKeyValueAction>(10);

        /// <summary>
        /// 获得/设置 SearchTemplate 实例
        /// </summary>
        [Parameter] public RenderFragment<TItem>? SearchTemplate { get; set; }

        /// <summary>
        /// 获得/设置 SearchModel 实例
        /// </summary>
        [Parameter] public TItem SearchModel { get; set; } = new TItem();

        /// <summary>
        /// 获得/设置 是否显示搜索框 默认为 false 不显示搜索框
        /// </summary>
        [Parameter] public bool ShowSearch { get; set; }

        /// <summary>
        /// 获得/设置 是否显示高级搜索按钮 默认显示
        /// </summary>
        [Parameter]
        public bool ShowAdvancedSearch { get; set; } = true;

        /// <summary>
        /// 获得/设置 搜索关键字
        /// </summary>
        [Parameter] public string SearchText { get; set; } = "";

        /// <summary>
        /// 获得/设置 搜索关键字改变事件
        /// </summary>
        [Parameter] public EventCallback<string> SearchTextChanged { get; set; }

        /// <summary>
        /// 重置搜索按钮异步回调方法
        /// </summary>
        [Parameter] public Func<TItem, Task>? OnResetSearchAsync { get; set; }

        /// <summary>
        /// 重置查询方法
        /// </summary>
        protected async Task ResetSearchClick()
        {
            if (OnResetSearchAsync != null) await OnResetSearchAsync(SearchModel);
            await SearchClick();
        }

        /// <summary>
        /// 查询方法
        /// </summary>
        protected async Task SearchClick()
        {
            // 查询控件按钮触发此事件
            // 拼接 Searchs 通过 SearchModel 遍历属性值获取搜索条件 未完待续
            Searchs.Clear();
            
            PageIndex = 1;
            await QueryAsync();
        }

        /// <summary>
        /// 高级查询按钮点击时调用此方法
        /// </summary>
        protected void AdvancedSearchClick()
        {
            // 弹出高级查询弹窗
            SearchModal?.Toggle();
        }

        /// <summary>
        /// 重置搜索按钮调用此方法
        /// </summary>
        protected async Task ClearSearchClick()
        {
            SearchText = "";
            PageIndex = 1;
            if (OnResetSearchAsync != null) await OnResetSearchAsync(SearchModel);
            await QueryAsync();
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
