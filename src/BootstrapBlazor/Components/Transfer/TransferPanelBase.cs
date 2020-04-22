using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;

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
        /// 获得/设置 数据集合
        /// </summary>
        [Parameter] public IEnumerable<SelectedItem>? Items { get; set; }

        /// <summary>
        /// 获得/设置 面板显示文字
        /// </summary>
        [Parameter] public string Text { get; set; } = "列表";

        /// <summary>
        /// 获得/设置 是否显示搜索框
        /// </summary>
        [Parameter] public bool ShowSearch { get; set; }

        /// <summary>
        /// 获得/设置 选项状态变化时回调方法
        /// </summary>
        [Parameter] public Action? OnSelectedItemsChanged { get; set; }

        /// <summary>
        /// 头部复选框初始化值方法
        /// </summary>
        protected CheckboxState HeaderCheckState()
        {
            var ret = CheckboxState.Mixed;
            if (Items != null && Items.Any() && Items.All(i => i.Active)) ret = CheckboxState.Checked;
            else if (!Items.Any(i => i.Active)) ret = CheckboxState.UnChecked;
            return ret;
        }

        /// <summary>
        /// 左侧头部复选框初始化值方法
        /// </summary>
        protected void OnHeaderCheck(CheckboxState state, SelectedItem item)
        {
            if (Items != null)
            {
                if (state == CheckboxState.Checked) Items.ToList().ForEach(i => i.Active = true);
                else Items.ToList().ForEach(i => i.Active = false);
                OnSelectedItemsChanged?.Invoke();
            }
        }

        /// <summary>
        /// RenderItem 方法
        /// </summary>
        /// <returns></returns>
        protected virtual RenderFragment RenderItem() => new RenderFragment(builder =>
        {
            var output = string.IsNullOrEmpty(SearchText) ? Items : Items?.Where(i => i.Text.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            foreach (var item in (output ?? new SelectedItem[0]))
            {
                var index = 0;
                builder.OpenComponent<Checkbox<SelectedItem>>(index++);
                builder.AddAttribute(index++, "class", "transfer-panel-item");
                builder.AddAttribute(index++, nameof(Checkbox<SelectedItem>.Value), item);
                builder.AddAttribute(index++, nameof(Checkbox<SelectedItem>.DisplayText), item.Text);
                builder.AddAttribute(index++, nameof(Checkbox<SelectedItem>.State), item.Active ? CheckboxState.Checked : CheckboxState.UnChecked);
                builder.AddAttribute(index++, nameof(Checkbox<SelectedItem>.OnStateChanged), new Action<CheckboxState, SelectedItem>((state, i) =>
                {
                    // trigger when transfer item clicked
                    i.Active = state == CheckboxState.Checked;

                    // set header
                    OnSelectedItemsChanged?.Invoke();
                }));
                builder.CloseComponent();
            }
        });

        /// <summary>
        /// 搜索框文本改变时回调此方法
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSearch(ChangeEventArgs e) => SearchText = e.Value.ToString();

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
