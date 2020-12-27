// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
