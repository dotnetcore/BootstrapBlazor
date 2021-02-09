// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// MultiSelect 组件
    /// </summary>
    public partial class MultiSelect<TValue>
    {
        private string? _oldStringValue;

        private ElementReference SelectElement { get; set; }

        private List<SelectedItem> SelectedItems { get; set; } = new List<SelectedItem>();

        private bool IsShow { get; set; }

        private string? ClassString => CssBuilder.Default("multi-select")
            .AddClass("show", IsShow)
            .AddClass("disabled", IsDisabled)
            .Build();

        private string? ToggleClassString => CssBuilder.Default("multi-select-toggle")
            .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled)
            .AddClass("disabled", IsDisabled)
            .AddClass("selected", SelectedItems.Any())
            .AddClass(CssClass).AddClass(ValidCss)
            .Build();

        private string? GetItemClassString(SelectedItem item) => CssBuilder.Default("multi-select-menu-item")
            .AddClass("active", GetCheckedState(item))
            .Build();

        private string? PlaceHolderClassString => CssBuilder.Default("multi-select-ph")
            .AddClass("d-none", SelectedItems.Any())
            .Build();

        private JSInterop<MultiSelect<TValue>>? Interop { get; set; }

        /// <summary>
        /// 获得/设置 组件 PlaceHolder 文字 默认为 点击进行多选 ...
        /// </summary>
        [Parameter]
        [NotNull]
        public string? PlaceHolder { get; set; }

        /// <summary>
        /// 获得/设置 是否显示搜索框 默认为 false 不显示
        /// </summary>
        [Parameter]
        public bool ShowSearch { get; set; }

        /// <summary>
        /// 获得/设置 是否显示关闭按钮 默认为 true 显示
        /// </summary>
        [Parameter]
        public bool ShowCloseButton { get; set; } = true;

        /// <summary>
        /// 获得/设置 是否显示功能按钮 默认为 false 不显示
        /// </summary>
        [Parameter]
        public bool ShowToolbar { get; set; }

        /// <summary>
        /// 获得/设置 是否显示默认功能按钮 默认为 true 显示
        /// </summary>
        [Parameter]
        public bool ShowDefaultButtons { get; set; } = true;

        /// <summary>
        /// 获得/设置 扩展按钮模板
        /// </summary>
        [Parameter]
        public RenderFragment? ButtonTemplate { get; set; }

        /// <summary>
        /// 获得/设置 按钮颜色
        /// </summary>
        [Parameter]
        public Color Color { get; set; } = Color.None;

        /// <summary>
        /// 获得/设置 绑定数据集
        /// </summary>
        [Parameter]
        [NotNull]
        public IEnumerable<SelectedItem>? Items { get; set; }

        /// <summary>
        /// 获得/设置 搜索文本发生变化时回调此方法
        /// </summary>
        [Parameter]
        public Func<string, IEnumerable<SelectedItem>>? OnSearchTextChanged { get; set; }

        /// <summary>
        /// 获得/设置 选中项集合发生改变时回调委托方法
        /// </summary>
        [Parameter]
        public Func<IEnumerable<SelectedItem>, Task>? OnSelectedItemsChanged { get; set; }

        /// <summary>
        /// 获得/设置 全选按钮显示文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? SelectAllText { get; set; }

        /// <summary>
        /// 获得/设置 全选按钮显示文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? ReverseSelectText { get; set; }

        /// <summary>
        /// 获得/设置 全选按钮显示文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? ClearText { get; set; }

        /// <summary>
        /// 获得/设置 选项最大数 默认为 0 不限制
        /// </summary>
        [Parameter]
        public int Max { get; set; }

        /// <summary>
        /// 获得/设置 设置最大值时错误消息文字
        /// </summary>
        [Parameter]
        [NotNull]
        public string? MaxErrorMessage { get; set; }

        /// <summary>
        /// 获得/设置 选项最小数 默认为 0 不限制
        /// </summary>
        [Parameter]
        public int Min { get; set; }

        /// <summary>
        /// 获得/设置 设置最小值时错误消息文字
        /// </summary>
        [Parameter]
        [NotNull]
        public string? MinErrorMessage { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<MultiSelect<TValue>>? Localizer { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            PlaceHolder ??= Localizer[nameof(PlaceHolder)];
            SelectAllText ??= Localizer[nameof(SelectAllText)];
            ReverseSelectText ??= Localizer[nameof(ReverseSelectText)];
            ClearText ??= Localizer[nameof(ClearText)];
            MinErrorMessage ??= Localizer[nameof(MinErrorMessage)];
            MaxErrorMessage ??= Localizer[nameof(MaxErrorMessage)];

            if (Items == null) Items = Enumerable.Empty<SelectedItem>();

            if (OnSearchTextChanged == null)
            {
                OnSearchTextChanged = text => Items.Where(i => i.Text.Contains(text, StringComparison.OrdinalIgnoreCase));
            }

            if (Min > 0)
            {
                Rules.Add(new MinValidator() { Value = Min, ErrorMessage = MinErrorMessage });
            }
            if (Max > 0)
            {
                Rules.Add(new MaxValidator() { Value = Max, ErrorMessage = MaxErrorMessage });
            }
        }

        /// <summary>
        /// OnParametersSetAsync 方法
        /// </summary>
        /// <returns></returns>
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            // 通过 Value 对集合进行赋值
            if (Value != null)
            {
                if (_oldStringValue == CurrentValueAsString) return;

                var typeValue = typeof(TValue);
                IList? list = null;
                if (typeValue == typeof(string))
                {
                    list = CurrentValueAsString.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    _oldStringValue = CurrentValueAsString;
                }
                else if (typeValue.IsGenericType)
                {
                    var t = typeValue.GenericTypeArguments;
                    var instance = Activator.CreateInstance(typeof(List<>).MakeGenericType(t))!;
                    var mi = instance.GetType().GetMethod("AddRange");
                    if (mi != null)
                    {
                        mi.Invoke(instance, new object[] { Value });
                    }

                    list = instance as IList;
                    _oldStringValue = string.Join(",", list);
                }
                if (list != null)
                {
                    SelectedItems.Clear();
                    foreach (var item in Items)
                    {
                        var v = item.Value;
                        if (!string.IsNullOrEmpty(v))
                        {
                            foreach (var l in list)
                            {
                                if (v == l.ToString())
                                {
                                    SelectedItems.Add(item);
                                    break;
                                }
                            }
                        }
                    }
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
                Interop = new JSInterop<MultiSelect<TValue>>(JSRuntime);
                await Interop.Invoke(this, SelectElement, "bb_multi_select", nameof(Close));
            }
        }

        /// <summary>
        /// 客户端关闭下拉框方法
        /// </summary>
        [JSInvokable]
        public void Close()
        {
            SearchText = "";
            IsShow = false;
            StateHasChanged();
        }

        private void ToggleMenu()
        {
            if (!IsDisabled)
            {
                IsShow = !IsShow;
            }
        }

        private Task ToggleRow(SelectedItem item, bool force = false)
        {
            if (!IsDisabled)
            {
                if (SelectedItems.Contains(item))
                {
                    SelectedItems.Remove(item);
                }
                else
                {
                    SelectedItems.Add(item);
                }

                SetValue();

                if (Min > 0 || Max > 0)
                {
                    var validationContext = new ValidationContext(Value!) { MemberName = FieldIdentifier?.FieldName };
                    var validationResults = new List<ValidationResult>();

                    ValidateProperty(SelectedItems.Count, validationContext, validationResults);
                    ToggleMessage(validationResults, true);
                }

                _ = TriggerSelectedItemChanged();

                if (force)
                {
                    StateHasChanged();
                }
            }

            return Task.CompletedTask;
        }

        private async Task TriggerSelectedItemChanged()
        {
            if (OnSelectedItemsChanged != null) await OnSelectedItemsChanged.Invoke(SelectedItems);
        }

        private void SetValue()
        {
            var typeValue = typeof(TValue);
            if (typeValue == typeof(string))
            {
                CurrentValueAsString = string.Join(",", SelectedItems.Select(i => i.Value));
            }
            else if (typeValue.IsGenericType)
            {
                var t = typeValue.GenericTypeArguments;
                var instance = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(t))!;

                foreach (var item in SelectedItems)
                {
                    var val = item.Value;
                    instance.Add(val);
                }
                CurrentValue = (TValue)instance;
            }
        }

        private async Task Clear()
        {
            SelectedItems.Clear();

            await TriggerSelectedItemChanged();
        }

        private async Task SelectAll()
        {
            SelectedItems.AddRange(Items);

            await TriggerSelectedItemChanged();
        }

        private async Task InvertSelect()
        {
            var items = Items.Where(i => !SelectedItems.Any(item => item == i)).ToList();
            SelectedItems.Clear();
            SelectedItems.AddRange(items);

            await TriggerSelectedItemChanged();
        }

        private bool GetCheckedState(SelectedItem item) => SelectedItems.Contains(item);

        private bool CheckCanTrigger(SelectedItem item)
        {
            var ret = true;
            if (Max > 0)
            {
                ret = SelectedItems.Count < Max || GetCheckedState(item);
            }
            return ret;
        }

        private bool CheckCanSelect(SelectedItem item)
        {
            var ret = GetCheckedState(item);
            if (!ret)
            {
                ret = CheckCanTrigger(item);
            }
            return !ret;
        }

        private string SearchText { get; set; } = "";

        private IEnumerable<SelectedItem> GetData()
        {
            var data = Items;
            if (ShowSearch && !string.IsNullOrEmpty(SearchText) && OnSearchTextChanged != null)
            {
                data = OnSearchTextChanged.Invoke(SearchText);
            }
            return data;
        }

        /// <summary>
        /// 客户端检查完成时调用此方法
        /// </summary>
        /// <param name="valid"></param>
        protected override void OnValidate(bool valid)
        {
            Color = valid ? Color.Success : Color.Danger;
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                Interop?.Dispose();
            }
        }
    }
}
