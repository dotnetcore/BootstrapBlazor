using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;

namespace BootstrapBlazor.Components
{
    partial class TableBase<TItem>
    {
        /// <summary>
        /// 获得 高级搜索样式
        /// </summary>
        protected string? AdvanceSearchClass => CssBuilder.Default("btn btn-secondary")
            .AddClass("btn-info", IsFiltered)
            .Build();

        /// <summary>
        /// 获得/设置 查询条件是否不为空
        /// </summary>
        protected bool IsFiltered { get; set; }

        /// <summary>
        /// 高级查询弹窗
        /// </summary>
        protected Modal? SearchModal { get; set; }

        /// <summary>
        /// 获得/设置 搜索框提示文字
        /// </summary>
        protected string SearchTooltip { get; set; } = "<div class='search-input-tooltip'>输入任意字符串全局搜索</br><kbd>Enter</kbd> 搜索 <kbd>ESC</kbd> 清除搜索</div>";

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
        /// 重置搜索按钮回调方法
        /// </summary>
        [Parameter] public Action<TItem>? OnResetSearch { get; set; }

        /// <summary>
        /// 重置查询方法
        /// </summary>
        protected void ResetSearchClick()
        {
            OnResetSearch?.Invoke(SearchModel);
            SearchClick();
        }

        /// <summary>
        /// 查询方法
        /// </summary>
        protected void SearchClick()
        {
            // 查询控件按钮触发此事件
            PageIndex = 1;
            Query();
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
        protected void ClearSearchClick()
        {
            SearchText = "";
            PageIndex = 1;
            Query();
        }

        /// <summary>
        /// 搜索文本框按键回调方法
        /// </summary>
        /// <param name="e"></param>
        protected void OnKeyUp(KeyboardEventArgs e)
        {
            // Enter Escape
            if (e.Key == "Enter")
            {
                SearchClick();
            }
            else if (e.Key == "Escape")
            {
                ClearSearchClick();
            }
        }
    }
}
