using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class TransferPanelBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得/设置 头部复选框
        /// </summary>
        protected Checkbox<SelectedItem>? HeaderCheckbox { get; set; }

        /// <summary>
        /// 获得/设置 选中的所有数据项集合
        /// </summary>
        public IEnumerable<SelectedItem> SelectedItems => Items.Where(i => i.Active);

        /// <summary>
        /// 获得/设置 数据集合
        /// </summary>
        [Parameter] public List<SelectedItem> Items { get; set; } = new List<SelectedItem>();

        /// <summary>
        /// 获得/设置 数据集合改变时回调方法
        /// </summary>
        [Parameter] public EventCallback<List<SelectedItem>> ItemsChanged { get; set; }

        /// <summary>
        /// 获得/设置 选项状态变化时回调方法
        /// </summary>
        [Parameter] public Action<IEnumerable<SelectedItem>>? OnSelectedItemsChanged { get; set; }

        /// <summary>
        /// 获得/设置 面板显示文字
        /// </summary>
        [Parameter] public string Text { get; set; } = "列表";

        /// <summary>
        /// 头部复选框初始化值方法
        /// </summary>
        protected CheckboxState HeaderCheckState()
        {
            var ret = CheckboxState.Mixed;
            if (Items.Count > 0 && Items.Where(i => i.Active).Count() == Items.Count) ret = CheckboxState.Checked;
            else if (Items.Where(i => i.Active).Count() == 0) ret = CheckboxState.UnChecked;
            return ret;
        }

        /// <summary>
        /// 左侧头部复选框初始化值方法
        /// </summary>
        protected void OnHeaderCheck(CheckboxState state, SelectedItem item)
        {
            if (state == CheckboxState.Checked) Items.ForEach(i => i.Active = true);
            else Items.ForEach(i => i.Active = false);
            OnSelectedItemsChanged?.Invoke(Items);
            StateHasChanged();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected virtual RenderFragment RenderItem(SelectedItem item) => new RenderFragment(builder =>
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
                HeaderCheckbox?.SetState(HeaderCheckState());
                OnSelectedItemsChanged?.Invoke(Items);
            }));
            builder.CloseComponent();
        });

        /// <summary>
        /// 数据源增加数据项方法
        /// </summary>
        /// <param name="items"></param>
        public void Add(IEnumerable<SelectedItem> items)
        {
            Items.AddRange(items);
            StateHasChanged();
        }

        /// <summary>
        /// 数据源移除数据项方法
        /// </summary>
        /// <param name="items"></param>
        public void Remove(IEnumerable<SelectedItem> items)
        {
            items.ToList().ForEach(i =>
            {
                i.Active = false;
                Items.Remove(i);
            });
            StateHasChanged();
        }
    }
}
