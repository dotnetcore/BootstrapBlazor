// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using System.Collections.Generic;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Rates
    {
        private int BindValue { get; set; } = 3;

        private int BindValue1 { get; set; } = 2;

        private bool IsDisable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private Logger? Trace { get; set; }

        private void OnValueChanged(int val)
        {
            BindValue = val;
            Trace?.Log($"评星: {val}");
        }

        /// <summary>
        /// 获得事件方法
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<EventItem> GetEvents() => new EventItem[]
        {
            new EventItem()
            {
                Name = "ValueChanged",
                Description="值改变时回调委托",
                Type ="EventCallback<int>"
            }
        };

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Value",
                Description = "组件值",
                Type = "int",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem()
            {
                Name = "IsDisabled",
                Description = "是否禁用 默认为 fasle",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            }
        };
    }
}
