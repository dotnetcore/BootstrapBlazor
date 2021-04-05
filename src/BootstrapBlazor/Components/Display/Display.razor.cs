// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Display 组件
    /// </summary>
    public partial class Display<TValue>
    {
        /// <summary>
        /// 获得 显示文本
        /// </summary>
        protected string? CurrentTextAsString { get; set; }

        /// <summary>
        /// 获得/设置 异步格式化字符串
        /// </summary>
        [Parameter]
        public Func<TValue, Task<string>>? FormatterAsync { get; set; }

        /// <summary>
        /// 获得/设置 格式化字符串 如时间类型设置 yyyy-MM-dd
        /// </summary>
        [Parameter]
        public string? FormatString { get; set; }

        /// <summary>
        /// OnParametersSetAsync 方法
        /// </summary>
        /// <returns></returns>
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            CurrentTextAsString = await FormatTextAsString(Value);
        }

        /// <summary>
        /// 数值格式化委托方法
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual async Task<string?> FormatTextAsString(TValue value) => FormatterAsync != null
            ? await FormatterAsync(value)
            : (!string.IsNullOrEmpty(FormatString) && value != null
                ? Utility.Format((object)value, FormatString)
                : FormatText(value));

        private string? FormatText(TValue value)
        {
            var ret = "";
            if (value != null)
            {
                var type = NullableUnderlyingType ?? typeof(TValue);
                if (type.IsEnum())
                {
                    ret = type.ToEnumDisplayName(value.ToString());
                }
                else if (type.IsArray)
                {
                    ret = ConvertArrayToString(value);
                }
                else if (type.IsGenericType && type.IsAssignableTo(typeof(IEnumerable)))
                {
                    // 泛型集合 IEnumerable<TValue>
                    ret = ConvertEnumerableToString(value);
                }
                else
                {
                    ret = value.ToString();
                }
            }
            return ret;
        }

        private static Func<TValue, string>? _converterArray;
        /// <summary>
        /// 获取属性方法 Lambda 表达式
        /// </summary>
        /// <returns></returns>
        private static string ConvertArrayToString(TValue value)
        {
            return (_converterArray ??= ConvertArrayToStringLambda())(value);

            static Func<TValue, string> ConvertArrayToStringLambda()
            {
                Func<TValue, string> ret = _ => "";
                var param_p1 = Expression.Parameter(typeof(Array));
                var target_type = typeof(TValue).UnderlyingSystemType;
                var methodType = Type.GetType(target_type.FullName!.Replace("[]", ""));
                if (methodType != null)
                {
                    var method = typeof(string).GetMethods().Where(m => m.Name == "Join" && m.IsGenericMethod && m.GetParameters()[0].ParameterType == typeof(string)).FirstOrDefault()?.MakeGenericMethod(methodType);
                    if (method != null)
                    {
                        var body = Expression.Call(method, Expression.Constant(","), Expression.Convert(param_p1, target_type));
                        ret = Expression.Lambda<Func<TValue, string>>(body, param_p1).Compile();
                    }
                }
                return ret;
            }
        }

        private static Func<TValue, string>? _converterEnumerable;
        /// <summary>
        /// 获取属性方法 Lambda 表达式
        /// </summary>
        /// <returns></returns>
        private static string ConvertEnumerableToString(TValue value)
        {
            return (_converterEnumerable ??= ConvertArrayToStringLambda())(value);

            static Func<TValue, string> ConvertArrayToStringLambda()
            {
                Func<TValue, string> ret = _ => "";
                var typeArguments = typeof(TValue).GenericTypeArguments;
                var param_p1 = Expression.Parameter(typeof(IEnumerable<>).MakeGenericType(typeArguments));
                var method = typeof(string).GetMethods().Where(m => m.Name == "Join" && m.IsGenericMethod && m.GetParameters()[0].ParameterType == typeof(string)).FirstOrDefault()?.MakeGenericMethod(typeArguments);
                if (method != null)
                {
                    var body = Expression.Call(method, Expression.Constant(","), param_p1);
                    ret = Expression.Lambda<Func<TValue, string>>(body, param_p1).Compile();
                }
                return ret;
            }
        }
    }
}
