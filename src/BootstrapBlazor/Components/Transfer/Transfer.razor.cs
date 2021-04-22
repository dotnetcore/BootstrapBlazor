// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Transfer<TValue>
    {
        /// <summary>
        /// 
        /// </summary>
        [Inject]
        [NotNull]
        protected IStringLocalizer<Transfer<TValue>>? Localizer { get; set; }

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

        private string? ValidateClass => CssBuilder.Default()
            .AddClass(CssClass).AddClass(ValidCss)
            .Build();

        /// <summary>
        /// 获得/设置 左侧数据集合
        /// </summary>
        private List<SelectedItem> LeftItems { get; set; } = new List<SelectedItem>();

        /// <summary>
        /// 获得/设置 右侧数据集合
        /// </summary>
        private List<SelectedItem> RightItems { get; set; } = new List<SelectedItem>();

        /// <summary>
        /// 获得/设置 是否按钮点击转移 优化性能使用
        /// </summary>
        private bool IsTransfer { get; set; }

        /// <summary>
        /// 获得/设置 组件绑定数据项集合
        /// </summary>
        [Parameter]
        public IEnumerable<SelectedItem>? Items { get; set; }

        /// <summary>
        /// 获得/设置 选中项集合发生改变时回调委托方法
        /// </summary>
        [Parameter]
        public Func<IEnumerable<SelectedItem>, Task>? OnSelectedItemsChanged { get; set; }

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
        /// 获得/设置 IStringLocalizerFactory 注入服务实例 默认为 null
        /// </summary>
        [Inject]
        [NotNull]
        public IStringLocalizerFactory? LocalizerFactory { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            LeftPanelText ??= Localizer[nameof(LeftPanelText)];
            RightPanelText ??= Localizer[nameof(RightPanelText)];

            // 如果未设置 Value 取 Items Active 值
            if (Items != null && string.IsNullOrEmpty(CurrentValueAsString))
            {
                CurrentValueAsString = string.Join(",", Items.Where(i => i.Active));
            }

            // 处理 Required 标签
            if (EditContext != null && FieldIdentifier != null)
            {
                var pi = FieldIdentifier.Value.Model.GetType().GetProperty(FieldIdentifier.Value.FieldName);
                if (pi != null)
                {
                    var required = pi.GetCustomAttribute<RequiredAttribute>();
                    if (required != null)
                    {
                        Rules.Add(new RequiredValidator() { LocalizerFactory = LocalizerFactory, ErrorMessage = required.ErrorMessage, AllowEmptyString = required.AllowEmptyStrings });
                    }
                }
            }
        }

        /// <summary>
        /// OnParametersSet 方法
        /// </summary>
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            // 通过 Value 对集合进行赋值
            if (Items != null && !string.IsNullOrEmpty(CurrentValueAsString))
            {
                Items = Items.ToList();
                var list = CurrentValueAsString.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in Items)
                {
                    item.Active = list.Any(i => i.Contains(item.Value, StringComparison.OrdinalIgnoreCase));
                }
            }

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

                Value = default;
                CurrentValueAsString = string.Join(",", RightItems.Select(i => i.Value));

                if (OnSelectedItemsChanged != null)
                {
                    await OnSelectedItemsChanged.Invoke(RightItems);
                }
                if (!ValueChanged.HasDelegate)
                {
                    StateHasChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <param name="validationErrorMessage"></param>
        /// <returns></returns>
        protected override bool TryParseValueFromString(string value, out TValue result, out string? validationErrorMessage)
        {
            validationErrorMessage = null;
            if (typeof(TValue) == typeof(string))
            {
                result = (TValue)(object)value;
            }
            else if (typeof(TValue) == typeof(IEnumerable<string>))
            {
                var v = value.Split(",", StringSplitOptions.RemoveEmptyEntries);
                result = (TValue)(object)v;
            }
            else if (typeof(TValue) == typeof(IEnumerable<SelectedItem>))
            {
                result = (TValue)(object)RightItems;
            }
            else
            {
                result = default!;
            }
            return true;
        }

        /// <summary>
        /// FormatValueAsString 方法
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override string? FormatValueAsString(TValue value) => value == null
            ? null
            : ConvertValueToString(value);

        private string? ConvertValueToString(TValue value)
        {
            var ret = "";
            var typeValue = typeof(TValue);
            if (typeValue == typeof(string))
            {
                ret = value!.ToString();
            }
            else if (typeValue.IsGenericType || typeValue.IsArray)
            {
                var t = typeValue.IsGenericType ? typeValue.GenericTypeArguments[0] : typeValue.GetElementType()!;
                var instance = Activator.CreateInstance(typeof(List<>).MakeGenericType(t))!;
                var mi = instance.GetType().GetMethod("AddRange");
                if (mi != null)
                {
                    mi.Invoke(instance, new object[] { value! });
                }

                var invoker = ConverterCache.GetOrAdd(t, key => CreateConverterInvoker(key));
                var v = invoker.Invoke(instance);
                ret = string.Join(",", v);
            }
            return ret;
        }

        private static ConcurrentDictionary<Type, Func<object, IEnumerable<string?>>> ConverterCache { get; set; } = new();

        private Func<object, IEnumerable<string?>> CreateConverterInvoker(Type type)
        {
            var method = this.GetType()
                .GetMethod(nameof(ConvertToString), BindingFlags.NonPublic | BindingFlags.Static)!
                .MakeGenericMethod(type);

            var para_exp = Expression.Parameter(typeof(object));
            var convert = Expression.Convert(para_exp, typeof(List<>).MakeGenericType(type));
            var body = Expression.Call(method, convert);
            return Expression.Lambda<Func<object, IEnumerable<string?>>>(body, para_exp).Compile();
        }

        private static IEnumerable<string?> ConvertToString<TSource>(List<TSource> source) => typeof(TSource) == typeof(SelectedItem)
            ? source.Select(o => (o as SelectedItem)!.Value)
            : source.Select(o => o?.ToString());

        /// <summary>
        /// 选项状态改变时回调此方法
        /// </summary>
        private Task SelectedItemsChanged()
        {
            StateHasChanged();
            return Task.CompletedTask;
        }

        /// <summary>
        /// 更改组件数据源方法
        /// </summary>
        /// <param name="items"></param>
        [Obsolete("更改数据源 Items 参数即可")]
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
