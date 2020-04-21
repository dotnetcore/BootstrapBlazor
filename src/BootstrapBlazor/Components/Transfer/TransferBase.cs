using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Transfer 穿梭框组件基类
    /// </summary>
    public abstract class TransferBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得/设置 左侧数据集合
        /// </summary>
        protected List<SelectedItem> LeftItems { get; set; } = new List<SelectedItem>();

        /// <summary>
        /// 获得/设置 右侧数据集合
        /// </summary>
        protected List<SelectedItem> RightItems { get; set; } = new List<SelectedItem>();

        /// <summary>
        /// 获得/设置 左侧选项集合头部复选框
        /// </summary>
        protected TransferPanel? LeftPanel { get; set; }

        /// <summary>
        /// 获得/设置 右侧选项集合头部复选框
        /// </summary>
        protected TransferPanel? RightPanel { get; set; }

        /// <summary>
        /// 获得/设置 左侧选项集合头部复选框
        /// </summary>
        protected Button? LeftButton { get; set; }

        /// <summary>
        /// 获得/设置 右侧选项集合头部复选框
        /// </summary>
        protected Button? RightButton { get; set; }

        private List<SelectedItem> _items = new List<SelectedItem>();
        /// <summary>
        /// 获得/设置 组件绑定数据项集合
        /// </summary>
        [Parameter]
        public IEnumerable<SelectedItem> Items
        {
            get
            {
                _items.Clear();
                _items.AddRange(LeftItems);
                _items.AddRange(RightItems.Select(i => { i.Active = true; return i; }));
                return _items;
            }
            set
            {
                LeftItems.Clear();
                RightItems.Clear();
                _items = value.ToList();
                LeftItems.AddRange(_items.Where(i => !i.Active));
                RightItems.AddRange(_items.Where(i => i.Active));
            }
        }

        /// <summary>
        /// 获得/设置 组件绑定数据项集合选项变化时回调方法
        /// </summary>
        [Parameter] public EventCallback<IEnumerable<SelectedItem>> ItemsChanged { get; set; }

        /// <summary>
        /// 获得/设置 组件绑定数据项集合选项变化时回调方法
        /// </summary>
        [Parameter] public Action<IEnumerable<SelectedItem>>? OnItemsChanged { get; set; }

        /// <summary>
        /// 获得/设置 左侧面板 Header 显示文本
        /// </summary>
        [Parameter] public string LeftPanelText { get; set; } = "列表 1";

        /// <summary>
        /// 获得/设置 右侧面板 Header 显示文本
        /// </summary>
        [Parameter] public string RightPanelText { get; set; } = "列表 2";

        /// <summary>
        /// 选中数据移动方法
        /// </summary>
        protected void Transfer(TransferPanel? source, TransferPanel? target)
        {
            if (source != null && source.SelectedItems.Any())
            {
                // remove selected items
                if (target != null)
                {
                    target.Add(source.SelectedItems);
                }
                source.Remove(source.SelectedItems);

                // callback
                if (ItemsChanged.HasDelegate) ItemsChanged.InvokeAsync(Items);
                OnItemsChanged?.Invoke(Items);
            }
        }

        /// <summary>
        /// 选项状态改变时回调此方法
        /// </summary>
        protected void OnLeftSelectedItemsChanged(IEnumerable<SelectedItem> items)
        {
            RightButton?.SetDisable(!items.Any(i => i.Active));
        }

        /// <summary>
        /// 选项状态改变时回调此方法
        /// </summary>
        protected void OnRightSelectedItemsChanged(IEnumerable<SelectedItem> items)
        {
            LeftButton?.SetDisable(!items.Any(i => i.Active));
        }
    }
}
