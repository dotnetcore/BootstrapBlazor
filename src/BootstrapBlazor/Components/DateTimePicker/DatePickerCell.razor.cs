// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using System;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// DateTimePickerCell 组件
    /// </summary>
    public sealed partial class DatePickerCell
    {
        /// <summary>
        /// 获得/设置 日期
        /// </summary>
        [Parameter]
        public DateTime Value { get; set; }

        /// <summary>
        /// 获得/设置 日期
        /// </summary>
        [Parameter]
        [NotNull]
        public string? Text { get; set; }

        /// <summary>
        /// 获得/设置 按钮点击回调方法
        /// </summary>
        [Parameter]
        [NotNull]
        public Action<DateTime>? OnClick { get; set; }
    }
}
