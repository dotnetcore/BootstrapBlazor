// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Select 组件实现类
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public partial class Select<TValue> : ISelect
    {
        private ElementReference SelectElement { get; set; }

        private JSInterop<Select<TValue>>? Interop { get; set; }

        /// <summary>
        /// 获得 样式集合
        /// </summary>
        private string? ClassName => CssBuilder.Default("form-select dropdown")
            .AddClass("is-disabled", IsDisabled)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 样式集合
        /// </summary>
        private string? InputClassName => CssBuilder.Default("form-control form-select-input")
            .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled)
            .AddClass(CssClass).AddClass(ValidCss)
            .Build();

        /// <summary>
        /// 获得 样式集合
        /// </summary>
        private string? AppendClassName => CssBuilder.Default("form-select-append")
            .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled)
            .Build();

        /// <summary>
        /// 设置当前项是否 Active 方法
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string? ActiveItem(SelectedItem item) => CssBuilder.Default("dropdown-item")
            .AddClass("active", () => item.Value == CurrentValueAsString)
            .AddClass("is-disabled", item.IsDisabled)
            .Build();

        /// <summary>
        /// Razor 文件中 Options 模板子项
        /// </summary>
        [NotNull]
        private List<SelectedItem>? Childs { get; set; }

        /// <summary>
        /// 获得/设置 搜索文本发生变化时回调此方法
        /// </summary>
        [Parameter]
        [NotNull]
        public Func<string, IEnumerable<SelectedItem>>? OnSearchTextChanged { get; set; }

        /// <summary>
        /// 获得/设置 是否显示搜索框 默认为 false 不显示
        /// </summary>
        [Parameter]
        public bool ShowSearch { get; set; }

        /// <summary>
        /// 获得 PlaceHolder 属性
        /// </summary>
        [Parameter]
        public string? PlaceHolder { get; set; }

        /// <summary>
        /// 获得/设置 选项模板支持静态数据
        /// </summary>
        [Parameter]
        public RenderFragment? Options { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<Select<TValue>>? Localizer { get; set; }

        [NotNull]
        private List<SelectedItem>? DataSource { get; set; }

        /// <summary>
        /// 获得 input 组件 Id 方法
        /// </summary>
        /// <returns></returns>
        protected override string? RetrieveId() => InputId;

        /// <summary>
        /// 获得/设置 Select 内部 Input 组件 Id
        /// </summary>
        private string? InputId => string.IsNullOrEmpty(Id) ? null : $"{Id}_input";

        /// <summary>
        /// 获得/设置 搜索文字
        /// </summary>
        private string SearchText { get; set; } = "";

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            PlaceHolder ??= Localizer[nameof(PlaceHolder)];

            if (OnSearchTextChanged == null)
            {
                OnSearchTextChanged = text => Items.Where(i => i.Text.Contains(text, StringComparison.OrdinalIgnoreCase));
            }

            Items ??= Enumerable.Empty<SelectedItem>();

            Childs = new List<SelectedItem>();

            // 内置对枚举类型的支持
            var t = typeof(TValue);
            if (!Items.Any() && t.IsEnum())
            {
                var item = "";
                // 如果可为空枚举增加 请选择 ...
                if (NullableUnderlyingType != null)
                {
                    // 优先查找 placeholder 字样 如果未设置使用资源文件中
                    if (AdditionalAttributes != null && AdditionalAttributes.TryGetValue("placeholder", out var pl))
                    {
                        item = pl.ToString();
                    }
                    else
                    {
                        item = Localizer["PlaceHolder"].Value;
                    }
                }
                Items = typeof(TValue).ToSelectList(string.IsNullOrEmpty(item) ? null : new SelectedItem("", item));
            }
        }

        private void ResetSelectedItem()
        {
            // 合并 Items 与 Options 集合
            if (!Items.Any() && typeof(TValue).IsEnum())
            {
                Items = typeof(TValue).ToSelectList();
            }
            DataSource = Items.ToList();
            DataSource.AddRange(Childs);

            SelectedItem = DataSource.FirstOrDefault(i => i.Value == CurrentValueAsString)
                ?? DataSource.FirstOrDefault(i => i.Active)
                ?? DataSource.FirstOrDefault();

            if (SelectedItem != null)
            {
                SelectedItem.Active = true;
                if (CurrentValueAsString != SelectedItem.Value)
                {
                    CurrentValueAsString = SelectedItem.Value;
                }
            }
        }

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                if (Interop == null)
                {
                    Interop = new JSInterop<Select<TValue>>(JSRuntime);
                }
                await Interop.InvokeVoidAsync(this, SelectElement, "bb_select", nameof(ConfirmSelectedItem));

                // 选项值不为 null 后者 string.Empty 时触发一次 OnSelectedItemChanged 回调
                if (SelectedItem != null && OnSelectedItemChanged != null && !string.IsNullOrEmpty(SelectedItem.Value))
                {
                    await OnSelectedItemChanged.Invoke(SelectedItem);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [JSInvokable]
        public async Task ConfirmSelectedItem(int index)
        {
            var item = GetShownItems().ElementAt(index);
            await OnItemClick(item);
            StateHasChanged();
        }

        /// <summary>
        /// 下拉框选项点击时调用此方法
        /// </summary>
        private async Task OnItemClick(SelectedItem item)
        {
            if (!item.IsDisabled)
            {
                var i = DataSource.FirstOrDefault(i => i.Active);
                if (i != null)
                {
                    i.Active = false;
                }
                item.Active = true;

                SelectedItem = item;
                CurrentValueAsString = item.Value;

                // 触发 SelectedItemChanged 事件
                if (OnSelectedItemChanged != null)
                {
                    await OnSelectedItemChanged.Invoke(SelectedItem);
                }
            }
        }

        /// <summary>
        /// 获取显示的候选项集合
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SelectedItem> GetShownItems() => string.IsNullOrEmpty(SearchText)
            ? DataSource
            : OnSearchTextChanged.Invoke(SearchText);

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
