using Microsoft.AspNetCore.Components;
using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// DateTimePickerCell 组件
    /// </summary>
    partial class DateTimePickerCell
    {
        /// <summary>
        /// 获得/设置 日期
        /// </summary>
        [Parameter] public DateTime Value { get; set; }

        /// <summary>
        /// 获得/设置 日期
        /// </summary>
        [Parameter] public string? Text { get; set; }

        /// <summary>
        /// 获得/设置 按钮点击回调方法
        /// </summary>
        [Parameter] public Action<DateTime>? OnClick { get; set; }
    }
}
