// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// CheckboxList 组件基类
    /// </summary>
    public abstract class CheckboxListBase<TModel, TValue> : ValidateBase<TValue>
    {
        /// <summary>
        /// 获得 组件样式
        /// </summary>
        protected string? ClassString => CssBuilder.Default("checkbox-list form-control")
            .AddClass(CssClass).AddClass(ValidCss)
            .Build();
        /// <summary>
        /// 获得 组件内部 Checkbox 项目样式
        /// </summary>
        protected string? CheckboxItemClassString => CssBuilder.Default("checkbox-item")
            .AddClass(CheckboxItemClass)
            .Build();

        /// <summary>
        /// 获得/设置 数据源
        /// </summary>
        [Parameter]
        public IEnumerable<TModel> Items { get; set; } = Enumerable.Empty<TModel>();

        /// <summary>
        /// 获得/设置 Checkbox 组件布局样式
        /// </summary>
        [Parameter]
        public string? CheckboxItemClass { get; set; }

        /// <summary>
        /// 获得/设置 显示列字段名称
        /// </summary>
        [Parameter]
        public string? TextField { get; set; }

        /// <summary>
        /// 获得/设置 数值列字段名称
        /// </summary>
        [Parameter]
        public string? ValueField { get; set; }

        /// <summary>
        /// 获得/设置 是否选中列字段名称
        /// </summary>
        [Parameter]
        public string? CheckedField { get; set; }

        /// <summary>
        /// 获得/设置 SelectedItemChanged 方法
        /// </summary>
        [Parameter]
        public Func<IEnumerable<TModel>, TModel, TValue, Task>? OnSelectedChanged { get; set; }

        /// <summary>
        /// 获得/设置 所有选中的子项以逗号分隔的字符串 如 "Value1,Value2"
        /// </summary>
        [Parameter]
        public string ValueAsString { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public EventCallback<string> ValueAsStringChanged { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // 通过 Value 对集合进行赋值
            if (Value != null)
            {
                var typeValue = typeof(TValue);
                IList? list = null;
                if (typeValue == typeof(string))
                {
                    list = CurrentValueAsString.Split(',', StringSplitOptions.RemoveEmptyEntries);
                }
                else if (typeValue.IsGenericType)
                {
                    var t = typeValue.GenericTypeArguments;
                    var instance = Activator.CreateInstance(typeof(List<>).MakeGenericType(t));
                    if (instance != null)
                    {
                        var mi = instance.GetType().GetMethod("AddRange");
                        if (mi != null)
                        {
                            mi.Invoke(instance, new object[] { Value });
                        }
                        list = instance as IList;
                    }
                }
                if (list != null)
                {
                    foreach (var model in Items)
                    {
                        SetValue(model, false);
                        var v = GetValue<object>(model)?.ToString() ?? "";
                        if (!string.IsNullOrEmpty(v))
                        {
                            foreach (var item in list)
                            {
                                if (v == item.ToString())
                                {
                                    SetValue(model, true);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        #region SetProperty Methods
        private void SetValue(TModel item, bool v)
        {
            if (!string.IsNullOrEmpty(CheckedField))
            {
                var invoker = SetPropertyValueLambdaCache.GetOrAdd((typeof(TModel), CheckedField), key => item.SetPropertyValueLambda<TModel, bool>(key.FieldName).Compile());
                invoker.Invoke(item, v);
            }
        }
        /// <summary>
        /// Checkbox 组件选项状态改变时触发此方法
        /// </summary>
        /// <param name="item"></param>
        /// <param name="v"></param>
        protected async Task OnStateChanged(TModel item, bool v)
        {
            SetValue(item, v);

            var typeValue = typeof(TValue);
            if (typeValue == typeof(string))
            {
                CurrentValueAsString = string.Join(",", Items.Where(i => GetChecked(i)).Select(i => GetValue<object>(i)?.ToString() ?? ""));
            }
            else if (typeValue.IsGenericType)
            {
                var t = typeValue.GenericTypeArguments;

                if (Activator.CreateInstance(typeof(List<>).MakeGenericType(t)) is IList instance)
                {
                    foreach (var model in Items.Where(i => GetChecked(i)))
                    {
                        var val = GetValue<object>(model);
                        instance.Add(val);
                    }
                    CurrentValue = (TValue)instance;
                }
            }

            if (OnSelectedChanged != null) await OnSelectedChanged.Invoke(Items, item, Value);
        }

        private static ConcurrentDictionary<(Type ModelType, string FieldName), Action<TModel, bool>> SetPropertyValueLambdaCache { get; } = new ConcurrentDictionary<(Type, string), Action<TModel, bool>>();
        #endregion

        #region GetProperty Methods
        /// <summary>
        /// 获得 组件数值
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected TResult? GetValue<TResult>(TModel item) where TResult : class
        {
            TResult? ret = default;
            if (!string.IsNullOrEmpty(ValueField))
            {
                var invoker = GetPropertyValueLambdaCache.GetOrAdd((typeof(TModel), ValueField), key => item.GetPropertyValueLambda<TModel, object>(key.FieldName).Compile());
                ret = invoker.Invoke(item) as TResult;
            }
            return ret;
        }

        /// <summary>
        /// 获得 Checkbox 组件显示名称方法
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string GetDisplayText(TModel item)
        {
            var ret = "未设置";
            if (!string.IsNullOrEmpty(TextField))
            {
                var invoker = GetPropertyValueLambdaCache.GetOrAdd((typeof(TModel), TextField), key => item.GetPropertyValueLambda<TModel, object>(key.FieldName).Compile());
                var v = invoker.Invoke(item);
                ret = v?.ToString() ?? "";
            }
            return ret;
        }

        /// <summary>
        /// 获得 Checkbox 组件状态方法
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected bool GetChecked(TModel item)
        {
            var ret = false;
            if (!string.IsNullOrEmpty(CheckedField))
            {
                var invoker = GetPropertyValueLambdaCache.GetOrAdd((typeof(TModel), CheckedField), key => item.GetPropertyValueLambda<TModel, object>(key.FieldName).Compile());
                var v = invoker.Invoke(item);
                if (v is bool b)
                {
                    ret = b;
                }
            }
            return ret;
        }

        private static ConcurrentDictionary<(Type ModelType, string FieldName), Func<TModel, object>> GetPropertyValueLambdaCache { get; set; } = new ConcurrentDictionary<(Type, string), Func<TModel, object>>();
        #endregion
    }
}
