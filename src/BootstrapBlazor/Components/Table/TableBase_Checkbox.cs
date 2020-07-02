using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    public partial class TableBase<TItem>
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
            if (SelectedItems.Count == PageItems || (Items != null && SelectedItems.Count == Items.Count() && Items.Count() > 0)) ret = CheckboxState.Checked;
            else if (SelectedItems.Count == 0) ret = CheckboxState.UnChecked;
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
        protected CheckboxBase<TItem>? HeaderCheckbox { get; set; }

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
            if (Items != null)
            {
                switch (state)
                {
                    case CheckboxState.Checked:
                        // select all
                        SelectedItems.Clear();
                        SelectedItems.AddRange(Items);
                        StateHasChanged();
                        break;
                    case CheckboxState.UnChecked:
                        // unselect all
                        SelectedItems.Clear();
                        StateHasChanged();
                        break;
                    default:
                        break;
                }
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
                var headerCheckboxState = SelectedItems.Count == 0 && Items.Count() > 0
                    ? CheckboxState.UnChecked
                    : (SelectedItems.Count == Items.Count() ? CheckboxState.Checked : CheckboxState.Mixed);
                await HeaderCheckbox.SetState(headerCheckboxState);
            }
        }
    }
}
