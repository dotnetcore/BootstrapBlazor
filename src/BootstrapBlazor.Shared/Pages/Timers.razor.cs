// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Timers
    {
        private Logger? Trace { get; set; }

        private Task OnTimeout()
        {
            Trace?.Log("计时器时间到");
            return Task.CompletedTask;
        }

        private Task OnCancel()
        {
            Trace?.Log("计时器取消");
            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes()
        {
            return new AttributeItem[]
            {
                new AttributeItem()
                {
                    Name = "Width",
                    Description = "组件宽度",
                    Type = "int",
                    ValueList = " - ",
                    DefaultValue = "300"
                },
                new AttributeItem()
                {
                    Name = "StrokeWidth",
                    Description = "进度条宽度",
                    Type = "int",
                    ValueList = " - ",
                    DefaultValue = "6"
                },
                new AttributeItem()
                {
                    Name = "IsVibrate",
                    Description = "倒计时结束时设备震动",
                    Type = "bool",
                    ValueList = "true/false",
                    DefaultValue = "true"
                },
                new AttributeItem()
                {
                    Name = "Value",
                    Description = "倒计时时间",
                    Type = "Timespan",
                    ValueList = " - ",
                    DefaultValue = " - "
                },
                new AttributeItem()
                {
                    Name = "Color",
                    Description = "进度条颜色",
                    Type = "Color",
                    ValueList = "Primary / Secondary / Success / Danger / Warning / Info / Dark",
                    DefaultValue = "Primary"
                }
            };
        }
    }
}
