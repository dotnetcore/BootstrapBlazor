// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Transfer
    {
        /// <summary>
        /// 
        /// </summary>
        [Inject]
        [NotNull]
        protected IStringLocalizer<Transfer>? Localizer { get; set; }

        /// <summary>
        /// 获得/设置 按钮文本样式
        /// </summary>
        private string? LeftButtonClassName => CssBuilder.Default()
            .AddClass("d-none", string.IsNullOrEmpty(LeftButtonText))
            .Build();

        /// <summary>
        /// 获得/设置 按钮文本样式
        /// </summary>
        private string? RightButtonClassName => CssBuilder.Default("mr-1")
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
        /// 获得/设置 是否按钮点击转移 优化性能使用
        /// </summary>
        private bool IsTransfer { get; set; }

        /// <summary>
        /// 获得/设置 组件绑定数据项集合
        /// </summary>
        [Parameter]
        public List<SelectedItem>? Items { get; set; }

        /// <summary>
        /// 获得/设置 组件绑定数据项集合选项变化时回调方法
        /// </summary>
        [Parameter]
        public EventCallback<List<SelectedItem>?> ItemsChanged { get; set; }

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

            LeftPanelText ??= Localizer[nameof(LeftPanelText)];
            RightPanelText ??= Localizer[nameof(RightPanelText)];
        }

        /// <summary>
        /// OnParametersSet 方法
        /// </summary>
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (!IsTransfer)
            {
                ResetItems();
            }
        }

        /// <summary>
        /// OnAfterRender 方法
        /// </summary>
        /// <param name="firstRender"></param>
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            IsTransfer = false;
        }

        /// <summary>
        /// 选中数据移动方法
        /// </summary>
        private async Task TransferItems(List<SelectedItem> source, List<SelectedItem> target)
        {
            IsTransfer = true;
            if (!IsDisabled && Items != null)
            {
                var items = source.Where(i => i.Active).ToList();
                source.RemoveAll(i => items.Contains(i));
                target.AddRange(items);

                LeftItems.ForEach(i =>
                {
                    var item = Items.FirstOrDefault(item => item.Value == i.Value && item.Text == i.Text && item.GroupName == i.GroupName);
                    if (item != null)
                    {
                        item.Active = false;
                    }
                });
                RightItems.ForEach(i =>
                {
                    var item = Items.FirstOrDefault(item => item.Value == i.Value && item.Text == i.Text && item.GroupName == i.GroupName);
                    if (item != null)
                    {
                        item.Active = true;
                    }
                });

                // 回调
                if (ItemsChanged.HasDelegate)
                {
                    await ItemsChanged.InvokeAsync(Items);
                }
                else
                {
                    StateHasChanged();
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
        /// 更改组件数据源方法
        /// </summary>
        /// <param name="items"></param>
        [Obsolete("请使用双向绑定 @bind-Items 来获取 Items 集合变化，更改数据源只需更改 Items 参数即可")]
        public void SetItems(List<SelectedItem>? items)
        {
            Items = items;
            ResetItems();

            StateHasChanged();
        }

        private void ResetItems()
        {
            LeftItems.Clear();
            RightItems.Clear();
            if (Items != null)
            {
                LeftItems.AddRange(Items.Where(i => !i.Active));
            }

            if (Items != null)
            {
                RightItems.AddRange(Items.Where(i => i.Active));
            }
        }

        /// <summary>
        /// 获得按钮是否可用
        /// </summary>
        /// <returns></returns>
        private static bool GetButtonState(IEnumerable<SelectedItem> source) => !(source.Any(i => i.Active));
    }
}
