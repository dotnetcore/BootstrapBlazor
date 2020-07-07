using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// TableFilter 基类
    /// </summary>
    public abstract class TableFilterBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得/设置 DOM 实例
        /// </summary>
        protected ElementReference FilterElement { get; set; }

        private JSInterop<TableFilterBase>? Interop { get; set; }

        /// <summary>
        /// 获得/设置 是否显示
        /// </summary>
        protected bool IsShow { get; set; }

        /// <summary>
        /// 获得/设置 条件数量
        /// </summary>
        protected int Count = 0;

        /// <summary>
        /// 获得 样式
        /// </summary>
        protected string? ClassString => CssBuilder.Default("card table-filter-item")
            .AddClass("show", IsShow)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得/设置 Header 显示文字
        /// </summary>
        [Parameter]
        public string HeaderText { get; set; } = "过滤";

        /// <summary>
        /// 获得/设置 点击确认过滤按钮时回调委托
        /// </summary>
        [Parameter]
        public Func<IEnumerable<ITableFilter>, Task>? OnFilter { get; set; }

        /// <summary>
        /// 获得/设置 相关 Field 字段名称
        /// </summary>
        [Parameter]
        public string FieldKey { get; set; } = "";

        /// <summary>
        /// 获得/设置 是否显示增加减少条件按钮
        /// </summary>
        [Parameter]
        public bool ShowMoreButton { get; set; }

        /// <summary>
        /// 获得 Body 模板
        /// </summary>
        [Parameter]
        public RenderFragment? BodyTemplate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public Func<TableFilterBase, Task>? OnReset { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public Func<TableFilterBase, Task>? OnConfirm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public Func<TableFilterBase, Task>? OnPlus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public Func<TableFilterBase, Task>? OnMinus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public TableFilterContent? Filters { get; set; }

        /// <summary>
        /// 获得/设置 Table Header 实例
        /// </summary>
        [CascadingParameter]
        protected TableColumnCollection? Columns { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Columns?.AddFilter(FieldKey, this);
        }

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender && JSRuntime != null)
            {
                Interop = new JSInterop<TableFilterBase>(JSRuntime);
                await Interop.Invoke(this, FilterElement, "bb_filter", nameof(Close));
            }
        }

        /// <summary>
        /// 客户端 JS 调用关闭 TableFilter 弹窗
        /// </summary>
        [JSInvokable]
        public void Close()
        {
            if (IsShow)
            {
                IsShow = false;
                StateHasChanged();
            }
        }

        /// <summary>
        /// Table 表头过滤按钮点击时调用此方法 弹出 TableFilter 弹窗
        /// </summary>
        internal void ShowFilter()
        {
            IsShow = true;
            StateHasChanged();
        }

        /// <summary>
        /// 重置过滤条件方法
        /// </summary>
        internal void ResetFilter()
        {
            Columns?.ResetFilter(FieldKey);
        }

        /// <summary>
        /// 添加过滤条件方法
        /// </summary>
        /// <param name="filter"></param>
        internal void AddFilters(ITableFilter filter)
        {
            Columns?.AddFilters(FieldKey, filter);
        }

        /// <summary>
        /// 点击重置按钮时回调此方法
        /// </summary>
        /// <returns></returns>
        protected async Task OnClickReset()
        {
            IsShow = false;
            if (OnReset != null)
            {
                Count = 0;
                await OnReset(this);
            }
        }

        /// <summary>
        /// 点击确认时回调此方法
        /// </summary>
        /// <returns></returns>
        protected async Task OnClickConfirm()
        {
            IsShow = false;
            if (OnConfirm != null)
            {
                await OnConfirm(this);
            }
        }

        /// <summary>
        /// 点击增加按钮时回调此方法
        /// </summary>
        /// <returns></returns>
        protected async Task OnClickPlus()
        {
            if (OnPlus != null)
            {
                await OnPlus(this);
                if (Count == 0) Count++;
                StateHasChanged();
            }
        }

        /// <summary>
        /// 点击减少按钮时回调此方法
        /// </summary>
        /// <returns></returns>
        protected async Task OnClickMinus()
        {
            if (OnMinus != null)
            {
                await OnMinus(this);
                if (Count == 1) Count--;
                StateHasChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Interop?.Dispose();
                Interop = null;
            }
            base.Dispose(disposing);
        }
    }
}
