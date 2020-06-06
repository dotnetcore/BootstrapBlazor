using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    partial class TableBase<TItem>
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
        /// 获得 表头行是否选中状态
        /// </summary>
        /// <returns></returns>
        protected CheckboxState HeaderCheckState()
        {
            var ret = CheckboxState.Mixed;
            if (SelectedItems.Count == PageItems) ret = CheckboxState.Checked;
            else if (SelectedItems.Count == 0) ret = CheckboxState.UnChecked;
            return ret;
        }

        /// <summary>
        /// 获得 当前行是否被选中
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected CheckboxState RowCheckState(TItem item) => SelectedItems.Contains(item) ? CheckboxState.Checked : CheckboxState.UnChecked;

        private List<CheckboxBase<TItem>>? ItemCheckboxs;
        private CheckboxBase<TItem>? HeaderCheckbox;

        /// <summary>
        /// 获得/设置 显示选择列
        /// </summary>
        [Parameter] public bool ShowCheckbox { get; set; }

        /// <summary>
        /// 获得/设置 是否显示选择框文字
        /// </summary>
        /// <value></value>
        [Parameter] public bool ShowCheckboxText { get; set; }

        /// <summary>
        /// 获得/设置 显示选择框文字 默认为 选择
        /// </summary>
        /// <value></value>
        [Parameter] public string CheckboxDisplayText { get; set; } = "选择";

        /// <summary>
        /// 点击 Header 选择复选框时触发此方法
        /// </summary>
        /// <param name="state"></param>
        /// <param name="val"></param>
        protected virtual Task OnHeaderCheck(CheckboxState state, TItem val)
        {
            if (state != CheckboxState.Mixed && Items != null)
            {
                ItemCheckboxs?.ForEach(async checkbox => await checkbox.SetState(state));
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 点击选择复选框时触发此方法
        /// </summary>
        /// <param name="state"></param>
        /// <param name="val"></param>
        protected virtual async Task OnCheck(CheckboxState state, TItem val)
        {
            if (state == CheckboxState.Checked) SelectedItems.Add(val);
            else SelectedItems.Remove(val);

            if (Items != null && HeaderCheckbox != null)
            {
                var headerCheckboxState = SelectedItems.Count == 0
                    ? CheckboxState.UnChecked
                    : (SelectedItems.Count == Items.Count() ? CheckboxState.Checked : CheckboxState.Mixed);
                await HeaderCheckbox.SetState(headerCheckboxState);
            }
        }

        /// <summary>
        /// 行内选择框初始化回调函数
        /// </summary>
        /// <param name="component"></param>
        protected void OnCheckboxInit(CheckboxBase<TItem> component)
        {
            HeaderCheckbox = component;
        }

        /// <summary>
        /// 表头选择框初始化回调函数
        /// </summary>
        /// <param name="component"></param>
        protected void OnItemCheckboxInit(CheckboxBase<TItem> component)
        {
            if (ItemCheckboxs == null) ItemCheckboxs = new List<CheckboxBase<TItem>>();

            ItemCheckboxs.Add(component);
        }
    }
}
