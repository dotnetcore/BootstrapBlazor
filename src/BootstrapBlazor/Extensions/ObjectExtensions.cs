// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Object 扩展方法
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 转化为带单位的字符串 [% px] => [% px] [int] => [int]px
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ConvertToPercentString(this string? val)
        {
            var ret = "";
            if (!string.IsNullOrEmpty(val))
            {
                if (val.EndsWith('%'))
                {
                    ret = val;
                }
                else if (val.EndsWith("px", StringComparison.OrdinalIgnoreCase))
                {
                    ret = val;
                }
                else if (int.TryParse(val, out var d))
                {
                    ret = $"{d}px";
                }
            }
            return ret;
        }

        /// <summary>
        /// 检查是否为 Number 数据类型
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsNumber(this Type t)
        {
            var targetType = Nullable.GetUnderlyingType(t) ?? t;
            var check =
                targetType == typeof(int) ||
                targetType == typeof(long) ||
                targetType == typeof(short) ||
                targetType == typeof(float) ||
                targetType == typeof(double) ||
                targetType == typeof(decimal);
            return check;
        }

        /// <summary>
        /// 检查是否为 DateTime 数据类型
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsDateTime(this Type t)
        {
            var targetType = Nullable.GetUnderlyingType(t) ?? t;
            var check = targetType == typeof(DateTime) ||
               targetType == typeof(DateTimeOffset);
            return check;
        }

        /// <summary>
        /// 通过类型获取类型描述文字
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string GetTypeDesc(this Type t)
        {
            string? ret;
            if (t.IsEnum) ret = "枚举";
            else if (t.IsNumber()) ret = "数字";
            else if (t.IsDateTime()) ret = "日期";
            else ret = "字符串";
            return ret;
        }

        /// <summary>
        /// 格式化为 文件大小与单位格式 字符串
        /// </summary>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        internal static string ToFileSizeString(this long fileSize) => fileSize switch
        {
            >= 1024 and < 1024 * 1024 => $"{Math.Round(fileSize / 1024D, 0, MidpointRounding.AwayFromZero)} KB",
            >= 1024 * 1024 and < 1024 * 1024 * 1024 => $"{Math.Round(fileSize / 1024 / 1024D, 0, MidpointRounding.AwayFromZero)} MB",
            >= 1024 * 1024 * 1024 => $"{Math.Round(fileSize / 1024 / 1024 / 1024D, 0, MidpointRounding.AwayFromZero)} GB",
            _ => $"{fileSize} B"
        };
    }
}
