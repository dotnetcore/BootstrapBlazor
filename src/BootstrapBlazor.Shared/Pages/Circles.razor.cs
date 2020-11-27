// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using BootstrapBlazor.Shared.Common;
using System;
using System.Collections.Generic;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Circles
    {
        private int CircleValue = 0;

        private void Add(int interval)
        {
            CircleValue += interval;
            CircleValue = Math.Min(100, Math.Max(0, CircleValue));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes()
        {
            return new AttributeItem[]
            {
                new AttributeItem(){
                    Name = "Width",
                    Description = "组件宽度",
                    Type = "int",
                    ValueList = "",
                    DefaultValue = "120"
                },
                new AttributeItem(){
                    Name = "StrokeWidth",
                    Description = "进度条宽度",
                    Type = "int",
                    ValueList = "",
                    DefaultValue = "2"
                },
                new AttributeItem()
                {
                    Name = "Value",
                    Description = "当前进度",
                    Type = "int",
                    ValueList = "0-100",
                    DefaultValue = "0"
                },
                new AttributeItem(){
                    Name = "Color",
                    Description = "进度条颜色",
                    Type = "Color",
                    ValueList = "Primary / Secondary / Success / Danger / Warning / Info / Dark",
                    DefaultValue = "Primary"
                },
                new AttributeItem()
                {
                    Name = "ShowProgress",
                    Description = "是否显示进度条信息",
                    Type = "bool",
                    ValueList = "true / false",
                    DefaultValue = "true"
                },
                new AttributeItem()
                {
                    Name = "ChildContent",
                    Description = "子组件",
                    Type = "RenderFragment",
                    ValueList = "",
                    DefaultValue = ""
                }
            };
        }
    }
}

