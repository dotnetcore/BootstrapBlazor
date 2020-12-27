// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        [Parameter]
        public IEnumerable<SelectedItem>? Items { get; set; }

        /// <summary>
        /// 获得/设置 组件绑定数据项集合选项变化时回调方法
        /// </summary>
        [Parameter]
        public EventCallback<IEnumerable<SelectedItem>?> ItemsChanged { get; set; }

        /// <summary>
        /// 获得/设置 左侧面板 Header 显示文本
        /// </summary>
        [Parameter]
        public string? LeftPanelText { get; set; }

        /// <summary>
        /// 获得/设置 右侧面板 Header 显示文本
        /// </summary>
        [Parameter]
        public string? RightPanelText { get; set; }

        /// <summary>
        /// 获得/设置 左侧按钮显示文本
        /// </summary>
        [Parameter]
        public string? LeftButtonText { get; set; }

        /// <summary>
        /// 获得/设置 右侧按钮显示文本
        /// </summary>
        [Parameter]
        public string? RightButtonText { get; set; }

        /// <summary>
        /// 获得/设置 是否显示搜索框
        /// </summary>
        [Parameter]
        public bool ShowSearch { get; set; }

        /// <summary>
        /// 获得/设置 左侧面板搜索框 placeholder 文字
        /// </summary>
        [Parameter]
        public string? LeftPannelSearchPlaceHolderString { get; set; }

        /// <summary>
        /// 获得/设置 右侧面板搜索框 placeholder 文字
        /// </summary>
        [Parameter]
        public string? RightPannelSearchPlaceHolderString { get; set; }

        /// <summary>
        /// 获得/设置 是否禁用 默认为 false
        /// </summary>
        [Parameter]
        public bool IsDisabled { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (Items != null) LeftItems.AddRange(Items.Where(i => !i.Active));
            if (Items != null) RightItems.AddRange(Items.Where(i => i.Active));
        }

        /// <summary>
        /// 选中数据移动方法
        /// </summary>
        protected async Task Transfer(List<SelectedItem> source, List<SelectedItem> target)
        {
            if (!IsDisabled)
            {
                var items = source.Where(i => i.Active).ToList();
                source.RemoveAll(i => i.Active);
                items.ForEach(i => i.Active = false);
                target.AddRange(items);

                // 回调
                if (ItemsChanged.HasDelegate && Items != null)
                {
                    var s = Items.ToList();
                    LeftItems.ToList().ForEach(i =>
                    {
                        var index = s.FindIndex(item => item.Value == i.Value && item.Text == i.Text && item.GroupName == i.GroupName);
                        s[index].Active = false;
                    });
                    RightItems.ToList().ForEach(i =>
                    {
                        var index = s.FindIndex(item => item.Value == i.Value && item.Text == i.Text && item.GroupName == i.GroupName);
                        s[index].Active = true;
                    });
                    await ItemsChanged.InvokeAsync(s);
                }
            }
        }

        /// <summary>
        /// 选项状态改变时回调此方法
        /// </summary>
        protected Task OnSelectedItemsChanged()
        {
            StateHasChanged();
            return Task.CompletedTask;
        }

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
