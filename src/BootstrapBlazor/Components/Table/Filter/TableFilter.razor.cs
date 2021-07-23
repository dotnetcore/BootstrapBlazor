// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// TableFilter 基类
    /// </summary>
    public partial class TableFilter : IFilter, IDisposable
    {
        private JSInterop<TableFilter>? Interop { get; set; }

        /// <summary>
        /// 获得 样式
        /// </summary>
        private string? ClassString => CssBuilder.Default("card table-filter-item")
            .AddClass("show", IsShow)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得/设置 DOM 实例
        /// </summary>
        private ElementReference FilterElement { get; set; }

        /// <summary>
        /// 获得/设置 Header 显示文字
        /// </summary>
        [NotNull]
        private string? Title { get; set; }

        /// <summary>
        /// 获得/设置 相关 Field 字段名称
        /// </summary>
        internal string FieldKey { get; set; } = "";

        /// <summary>
        /// 获得/设置 是否显示
        /// </summary>
        private bool IsShow { get; set; }

        /// <summary>
        /// 获得/设置 条件数量
        /// </summary>
        private int Count { get; set; }

        /// <summary>
        /// 获得/设置 是否显示增加减少条件按钮
        /// </summary>
        public bool ShowMoreButton { get; set; } = true;

        /// <summary>
        /// 获得/设置 过滤条件 IFilterAction 接口
        /// </summary>
        public IFilterAction? FilterAction { get; set; }

        /// <summary>
        /// 获得 当前过滤条件是否激活
        /// </summary>
        internal bool HasFilter => (Table != null && Column != null) ? Table.Filters.ContainsKey(Column.GetFieldName()) : false;

        /// <summary>
        /// 获得 相关联 ITableColumn 实例
        /// </summary>
        [Parameter]
        public ITableColumn? Column { get; set; }

        /// <summary>
        /// 重置按钮文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? ClearButtonText { get; set; }

        /// <summary>
        /// 获得/设置 是否为 HeaderRow 模式 默认 false
        /// </summary>
        [Parameter]
        public bool IsHeaderRow { get; set; }

        /// <summary>
        /// 过滤按钮文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? FilterButtonText { get; set; }

        /// <summary>
        /// 获得/设置 Table Header 实例
        /// </summary>
        [CascadingParameter]
        private ITable? Table { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<TableFilter>? Localizer { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Title ??= Localizer[nameof(Title)];
            FilterButtonText ??= Localizer[nameof(FilterButtonText)];
            ClearButtonText ??= Localizer[nameof(ClearButtonText)];

            if (Column != null)
            {
                Title = Column.GetDisplayName();
                FieldKey = Column.GetFieldName();
                Column.Filter = this;
            }
        }

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                Interop = new JSInterop<TableFilter>(JSRuntime);
                await Interop.InvokeVoidAsync(this, FilterElement, "bb_filter", nameof(Close));
            }
        }

        /// <summary>
        /// 显示弹窗方法
        /// </summary>
        /// <returns></returns>
        public void Show()
        {
            if (!IsShow)
            {
                IsShow = true;
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
        /// 客户端 JS 回车按键事件调用
        /// </summary>
        [JSInvokable]
        public async Task ConfirmByKey()
        {
            if (IsShow)
            {
                await OnClickConfirm();
            }
        }

        /// <summary>
        /// 客户端 JS ESC 按键事件调用
        /// </summary>
        [JSInvokable]
        public async Task EscByKey()
        {
            if (IsShow)
            {
                await OnClickReset();
            }
        }

        /// <summary>
        /// 点击重置按钮时回调此方法
        /// </summary>
        /// <returns></returns>
        private async Task OnClickReset()
        {
            if (IsShow)
            {
                IsShow = false;
                Count = 0;

                if (Table != null)
                {
                    Table.Filters.Remove(FieldKey);
                    FilterAction?.Reset();
                    if (Table.OnFilterAsync != null) await Table.OnFilterAsync();
                }
            }
        }

        /// <summary>
        /// 点击确认时回调此方法
        /// </summary>
        /// <returns></returns>
        private async Task OnClickConfirm()
        {
            if (IsShow)
            {
                IsShow = false;

                await OnFilterAsync();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal async Task OnFilterAsync()
        {
            if (Table != null)
            {
                if (FilterAction?.GetFilterConditions().Any() ?? false)
                {
                    Table.Filters[FieldKey] = FilterAction;
                }
                else
                {
                    Table.Filters.Remove(FieldKey);
                }

                if (Table.OnFilterAsync != null)
                {
                    await Table.OnFilterAsync();
                }
            }
        }

        /// <summary>
        /// 点击增加按钮时回调此方法
        /// </summary>
        /// <returns></returns>
        private void OnClickPlus()
        {
            if (Count == 0) Count++;
        }

        /// <summary>
        /// 点击减少按钮时回调此方法
        /// </summary>
        /// <returns></returns>
        private void OnClickMinus()
        {
            if (Count == 1) Count--;
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {

            if (disposing && Interop != null)
            {
                Interop.Dispose();
                Interop = null;
            }
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
