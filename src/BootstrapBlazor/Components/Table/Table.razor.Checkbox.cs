// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    public partial class Table<TItem>
    {
        /// <summary>
        /// 获得 选择列显示文字
        /// </summary>
        protected string? CheckboxDisplayTextString => ShowCheckboxText ? CheckboxDisplayText : null;

        /// <summary>
        /// 获得 Checkbox 样式表集合
        /// </summary>
        /// <returns></returns>
        protected string? CheckboxColumnClass => CssBuilder.Default("table-th-checkbox")
            .AddClass("show-text", ShowCheckboxText)
            .Build();

        /// <summary>
        /// 获得 thead 样式表集合
        /// </summary>
        protected string? HeaderClass => CssBuilder.Default()
            .AddClass(HeaderStyle.ToDescriptionString(), HeaderStyle != TableHeaderStyle.None)
            .Build();

        /// <summary>
        /// 获得 表头行是否选中状态
        /// </summary>
        /// <returns></returns>
        protected CheckboxState HeaderCheckState()
        {
            var ret = CheckboxState.UnChecked;
            if (Items.Any() && Items.All(i => SelectedItems.Contains(i))) ret = CheckboxState.Checked;
            else if (Items.Any(i => SelectedItems.Contains(i))) ret = CheckboxState.Mixed;
            return ret;
        }

        /// <summary>
        /// 获得 当前行是否被选中
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected CheckboxState RowCheckState(TItem item) => SelectedItems.Contains(item) ? CheckboxState.Checked : CheckboxState.UnChecked;

        /// <summary>
        /// 获得/设置 表头上的复选框
        /// </summary>
        protected Checkbox<TItem>? HeaderCheckbox { get; set; }

        /// <summary>
        /// 获得/设置 是否为多选模式 默认为 false
        /// </summary>
        [Parameter] public bool IsMultipleSelect { get; set; }

        /// <summary>
        /// 获得/设置 是否显示选择框文字
        /// </summary>
        /// <value></value>
        [Parameter] public bool ShowCheckboxText { get; set; }

        /// <summary>
        /// 获得/设置 显示选择框文字 默认为 选择
        /// </summary>
        /// <value></value>
        [Parameter]
        [NotNull]
        public string? CheckboxDisplayText { get; set; }

        /// <summary>
        /// 点击 Header 选择复选框时触发此方法
        /// </summary>
        /// <param name="state"></param>
        /// <param name="val"></param>
        protected virtual async Task OnHeaderCheck(CheckboxState state, TItem val)
        {
            switch (state)
            {
                case CheckboxState.Checked:
                    // select all
                    SelectedItems.Clear();
                    SelectedItems.AddRange(Items);

                    // callback
                    await OnSelectedRowsChanged();
                    StateHasChanged();
                    break;
                case CheckboxState.UnChecked:
                    // unselect all
                    SelectedItems.Clear();

                    // callback
                    await OnSelectedRowsChanged();
                    StateHasChanged();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 点击选择复选框时触发此方法
        /// </summary>
        protected Func<CheckboxState, TItem, Task> OnCheck() => async (state, val) =>
        {
            if (state == CheckboxState.Checked) SelectedItems.Add(val);
            else SelectedItems.Remove(val);

            await OnSelectedRowsChanged();

            if (EditInCell)
            {
                // auto quit edit in cell mode
                EditInCell = false;
            }

            // https://gitee.com/LongbowEnterprise/BootstrapBlazor/issues/I1UYQG
            StateHasChanged();
        };
    }
}
