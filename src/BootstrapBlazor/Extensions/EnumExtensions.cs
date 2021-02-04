// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 枚举类型扩展方法
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取 DescriptionAttribute 标签方法
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToDescriptionString<TEnum>(this TEnum val) where TEnum : Enum => typeof(TEnum).ToDescriptionString(val.ToString());

        /// <summary>
        /// 通过字段名称获取 DescriptionAttribute 标签值
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static string ToDescriptionString(this Type? type, string? fieldName)
        {
            var ret = string.Empty;
            if (type != null && !string.IsNullOrEmpty(fieldName))
            {
                var t = Nullable.GetUnderlyingType(type) ?? type;
                var attributes = t.GetField(fieldName)?.GetCustomAttribute<DescriptionAttribute>();
                ret = attributes?.Description ?? fieldName;
            }
            return ret;
        }

        /// <summary>
        /// 获取指定枚举类型的枚举值集合，默认通过 Description 标签显示 DisplayName 未设置 Description 标签时显示字段名称
        /// </summary>
        /// <param name="type"></param>
        /// <param name="addtionalItem"></param>
        /// <returns></returns>
        public static IEnumerable<SelectedItem> ToSelectList(this Type type, SelectedItem? addtionalItem = null)
        {
            var ret = new List<SelectedItem>();
            if (addtionalItem != null) ret.Add(addtionalItem);

            if (type.IsEnum())
            {
                var t = Nullable.GetUnderlyingType(type) ?? type;
                foreach (var field in Enum.GetNames(t))
                {
                    var desc = t.ToDescriptionString(field);
                    if (string.IsNullOrEmpty(desc)) desc = field;
                    ret.Add(new SelectedItem(field, desc));
                }
            }
            return ret;
        }

        /// <summary>
        /// 判断类型是否为枚举类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsEnum(this Type? type)
        {
            var ret = false;
            if (type != null)
            {
                var t = Nullable.GetUnderlyingType(type) ?? type;
                ret = t.IsEnum;
            }
            return ret;
        }
    }
}
