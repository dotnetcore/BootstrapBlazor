// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PlaceHolderAttribute : Attribute
    {
        /// <summary>
        /// 获得 Order 属性
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="placeholder"></param>
        public PlaceHolderAttribute(string placeholder)
        {
            Text = placeholder;
        }
    }
}
