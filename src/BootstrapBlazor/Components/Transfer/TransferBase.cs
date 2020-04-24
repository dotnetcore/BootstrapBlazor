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
        /// 获得/设置 按钮文本样式
        /// </summary>
        protected string? LeftButtonClassName => CssBuilder.Default()
            .AddClass("d-none", string.IsNullOrEmpty(LeftButtonText))
            .Build();

        /// <summary>
        /// 获得/设置 按钮文本样式
        /// </summary>
        protected string? RightButtonClassName => CssBuilder.Default("mr-1")
            .AddClass("d-none", string.IsNullOrEmpty(RightButtonText))
            .Build();

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

        /// <summary>
        /// 获得/设置 组件绑定数据项集合
        /// </summary>
        [Parameter] public IEnumerable<SelectedItem>? Items { get; set; }

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
        /// 获得/设置 左侧按钮显示文本
        /// </summary>
        [Parameter] public string LeftButtonText { get; set; } = "";

        /// <summary>
        /// 获得/设置 右侧按钮显示文本
        /// </summary>
        [Parameter] public string RightButtonText { get; set; } = "";

        /// <summary>
        /// 获得/设置 是否显示搜索框
        /// </summary>
        [Parameter] public bool ShowSearch { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (Items != null)
            {
                LeftItems.AddRange(Items.Where(i => !i.Active));
                RightItems.AddRange(Items.Where(i => i.Active));
            }
        }

        /// <summary>
        /// 选中数据移动方法
        /// </summary>
        protected void Transfer(List<SelectedItem> source, List<SelectedItem> target)
        {
            var items = source.Where(i => i.Active).ToList();
            source.RemoveAll(i => i.Active);
            items.ForEach(i => i.Active = false);
            target.AddRange(items);
            OnItemsChanged?.Invoke(RightItems);
        }

        /// <summary>
        /// 选项状态改变时回调此方法
        /// </summary>
        protected void OnSelectedItemsChanged() => StateHasChanged();

        /// <summary>
        /// 获得按钮是否可用
        /// </summary>
        /// <returns></returns>
        protected string? GetButtonState(IEnumerable<SelectedItem> source)
        {
            return (!source.Any() || !source.Any(i => i.Active)) ? "disabled" : null;
        }
    }
}
