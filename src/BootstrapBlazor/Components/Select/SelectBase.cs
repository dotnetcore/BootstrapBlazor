// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Select 组件基类
    /// </summary>
    public abstract class SelectBase<TValue> : ValidateBase<TValue>, ISelect
    {
        /// <summary>
        /// 当前选择项实例
        /// </summary>
        protected SelectedItem? SelectedItem { get; set; }

        /// <summary>
        /// 获得/设置 Select 内部 Input 组件 Id
        /// </summary>
        protected string? InputId => string.IsNullOrEmpty(Id) ? null : $"{Id}_input";

        /// <summary>
        /// 获得 当前选项显示文本
        /// </summary>
        protected string CurrentTextAsString => SelectedItem?.Text ?? "";

        /// <summary>
        /// 获得/设置 是否初始化完成 默认为 false
        /// </summary>
        protected bool Initialized { get; set; }

        /// <summary>
        /// 获得/设置 搜索文字
        /// </summary>
        protected string SearchText { get; set; } = "";

        /// <summary>
        /// 获得/设置 是否显示搜索框 默认为 false 不显示
        /// </summary>
        [Parameter]
        public bool ShowSearch { get; set; }

        /// <summary>
        /// 获得/设置 按钮颜色
        /// </summary>
        [Parameter]
        public Color Color { get; set; } = Color.None;

        /// <summary>
        /// 获得 PlaceHolder 属性
        /// </summary>
        [Parameter]
        public string? PlaceHolder { get; set; }

        /// <summary>
        /// 获得/设置 绑定数据集
        /// </summary>
        [Parameter]
        public IEnumerable<SelectedItem> Items { get; set; } = Enumerable.Empty<SelectedItem>();

        /// <summary>
        /// 获得/设置 选项模板
        /// </summary>
        [Parameter]
        public RenderFragment<SelectedItem>? ItemTemplate { get; set; }

        /// <summary>
        /// SelectedItemChanged 方法
        /// </summary>
        [Parameter]
        public Func<SelectedItem, Task>? OnSelectedItemChanged { get; set; }

        /// <summary>
        /// 获得/设置 选项模板支持静态数据
        /// </summary>
        [Parameter]
        public RenderFragment? Options { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string? RetrieveId() => InputId;

        /// <summary>
        /// 下拉框选项点击时调用此方法
        /// </summary>
        protected async Task OnItemClick(SelectedItem item)
        {
            if (SelectedItem != null) SelectedItem.Active = false;
            SelectedItem = item;

            SelectedItem.Active = true;

            // ValueChanged
            CurrentValueAsString = SelectedItem.Value;

            // 触发 SelectedItemChanged 事件
            if (OnSelectedItemChanged != null) await OnSelectedItemChanged.Invoke(SelectedItem);
        }

        /// <summary>
        /// 获得 数据源
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<SelectedItem> GetItems()
        {
            var items = Items.ToList();
            items.AddRange(Childs);

            if (items.Any())
            {
                // 双向绑定其他组件更改了数据源值时
                if (SelectedItem != null && SelectedItem.Value != CurrentValueAsString)
                {
                    SelectedItem = items.FirstOrDefault(i => i.Value == CurrentValueAsString);
                }

                // 设置数据集合后 SelectedItem 设置默认值
                if (SelectedItem == null || !items.Any(i => i.Value == SelectedItem.Value && i.Text == SelectedItem.Text))
                {
                    var item = items.FirstOrDefault(i => i.Value == CurrentValueAsString)
                            ?? items.FirstOrDefault(i => i.Active)
                            ?? items.FirstOrDefault();
                    if (item != null)
                    {
                        SelectedItem = item;
                        if (Value != null && CurrentValueAsString != SelectedItem.Value)
                        {
                            item = items.FirstOrDefault(i => i.Text == CurrentValueAsString);
                            if (item != null) SelectedItem = item;
                        }
                        CurrentValueAsString = SelectedItem.Value;
                    }
                }
            }
            else
            {
                SelectedItem = null;
            }

            return items;
        }

        /// <summary>
        /// 更改组件数据源方法
        /// </summary>
        /// <param name="items"></param>
        public void SetItems(IEnumerable<SelectedItem> items)
        {
            Items = items;
            SelectedItem = GetItems().FirstOrDefault(i => i.Active);
            StateHasChanged();
        }

        private List<SelectedItem> Childs { get; set; } = new List<SelectedItem>();
        /// <summary>
        /// 添加静态下拉项方法
        /// </summary>
        /// <param name="item"></param>
        public void Add(SelectedItem item)
        {
            Childs.Add(item);
        }
    }
}
