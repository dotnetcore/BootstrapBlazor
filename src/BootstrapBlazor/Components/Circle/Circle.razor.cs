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

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Circle
    {
        /// <summary>
        /// 获得/设置 当前值
        /// </summary>
        [Parameter]
        public int Value { get; set; }

        /// <summary>
        /// 获得/设置 当前进度值
        /// </summary>
        private string? ValueString => $"{Math.Round(((1 - Value * 1.0 / 100) * CircleLength), 2)}";

        /// <summary>
        /// 获得/设置 Title 字符串
        /// </summary>
        private string ValueTitleString => $"{Value}%";
    }
}
