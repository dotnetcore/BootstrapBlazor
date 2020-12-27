// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// TransferPanelBase 穿梭框面板组件
    /// </summary>
    public class TransferPanelBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得/设置 头部复选框
        /// </summary>
        protected Checkbox<SelectedItem>? HeaderCheckbox { get; set; }

        /// <summary>
        /// 获得/设置 搜索关键字
        /// </summary>
        protected string? SearchText { get; set; }

        /// <summary>
        /// 获得 搜索图标样式
        /// </summary>
        protected string? SearchClass => CssBuilder.Default("input-prefix")
            .AddClass("is-on", !string.IsNullOrEmpty(SearchText))
            .Build();

        /// <summary>
        /// 获得 Panel 样式
        /// </summary>
        protected string? PanelListClassString => CssBuilder.Default("checkbox-group transfer-panel-list")
            .AddClass("disabled", IsDisabled)
            .Build();

        /// <summary>
        /// 获得 组件是否被禁用属性值
        /// </summary>
        protected string? DisabledString => IsDisabled ? "disabled" : null;

        /// <summary>
        /// 获得/设置 数据集合
        /// </summary>
        [Parameter]
        public IEnumerable<SelectedItem>? Items { get; set; }

        /// <summary>
        /// 获得/设置 面板显示文字
        /// </summary>
        [Parameter]
        public string Text { get; set; } = "列表";

        /// <summary>
        /// 获得/设置 是否显示搜索框
        /// </summary>
        [Parameter]
        public bool ShowSearch { get; set; }

        /// <summary>
        /// 获得/设置 选项状态变化时回调方法
        /// </summary>
        [Parameter]
        public Func<Task>? OnSelectedItemsChanged { get; set; }

        /// <summary>
        /// 获得/设置 搜索框的 placeholder 字符串
        /// </summary>
        [Parameter]
        public string? SearchPlaceHolderString { get; set; } = "请输入 ...";

        /// <summary>
        /// 获得/设置 是否禁用 默认为 false
        /// </summary>
        [Parameter]
        public bool IsDisabled { get; set; }

        /// <summary>
        /// 头部复选框初始化值方法
        /// </summary>
        protected CheckboxState HeaderCheckState()
        {
            var ret = CheckboxState.Mixed;
            if (Items != null && Items.Any() && Items.All(i => i.Active)) ret = CheckboxState.Checked;
            else if (Items != null && !Items.Any(i => i.Active)) ret = CheckboxState.UnChecked;
            return ret;
        }

        /// <summary>
        /// 左侧头部复选框初始化值方法
        /// </summary>
        protected async Task OnHeaderCheck(CheckboxState state, SelectedItem item)
        {
            if (Items != null)
            {
                if (state == CheckboxState.Checked) Items.ToList().ForEach(i => i.Active = true);
                else Items.ToList().ForEach(i => i.Active = false);
                if (OnSelectedItemsChanged != null) await OnSelectedItemsChanged.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected async Task OnStateChanged(CheckboxState state, SelectedItem item)
        {
            // trigger when transfer item clicked
            item.Active = state == CheckboxState.Checked;

            // set header
            if (OnSelectedItemsChanged != null) await OnSelectedItemsChanged.Invoke();
        }

        /// <summary>
        /// 搜索框文本改变时回调此方法
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSearch(ChangeEventArgs e) => SearchText = e.Value?.ToString();

        /// <summary>
        /// 搜索文本框按键回调方法
        /// </summary>
        /// <param name="e"></param>
        protected void OnKeyUp(KeyboardEventArgs e)
        {
            // Escape
            if (e.Key == "Escape") ClearSearch();
        }

        /// <summary>
        /// 清空搜索条件方法
        /// </summary>
        protected void ClearSearch()
        {
            SearchText = "";
        }
    }
}
