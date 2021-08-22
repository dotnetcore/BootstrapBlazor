// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Radios
    {
        [NotNull]
        private BlockLogger? Trace { get; set; }

        [NotNull]
        private BlockLogger? BinderLog { get; set; }

        private IEnumerable<SelectedItem> DemoValues { get; set; } = new List<SelectedItem>(2)
        {
            new SelectedItem("1", "选项一"),
            new SelectedItem("2", "选项二"),
        };

        private Task OnSelectedChanged(IEnumerable<SelectedItem> values, string val)
        {
            var value = values.FirstOrDefault();
            Trace.Log($"组件选中值: {value?.Value} 显示值: {value?.Text} 组件 Value 值: {val}");
            return Task.CompletedTask;
        }

        private Task OnItemChanged(IEnumerable<SelectedItem> values, SelectedItem val)
        {
            var value = values.FirstOrDefault();
            BinderLog.Log($"组件选中值: {value?.Value} 显示值: {value?.Text}");
            return Task.CompletedTask;
        }

        private IEnumerable<SelectedItem> Items { get; set; } = new SelectedItem[]
        {
            new SelectedItem("1", "北京") { Active = true },
            new SelectedItem("2", "上海")
        };

        private SelectedItem BindRadioItem { get; set; } = new SelectedItem();

        [NotNull]
        private EnumEducation? SelectedEnumItem { get; set; }

        private static IEnumerable<AttributeItem> GetAttributes() => new[]
        {
            new AttributeItem() {
                Name = "DisplayText",
                Description = "显示文字",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "—"
            },
            new AttributeItem() {
                Name = "IsDisabled",
                Description = "是否禁用",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsVertical",
                Description = "是否垂直分布",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Items",
                Description = "绑定数据源",
                Type = "IEnumerable<TItem>",
                ValueList = " — ",
                DefaultValue = "—"
            }
        };

        /// <summary>
        /// 获得事件方法
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<EventItem> GetEvents() => new EventItem[]
        {
            new EventItem()
            {
                Name = "OnSelectedChanged",
                Description="复选框状态改变时回调此方法",
                Type ="Func<IEnumerable<SelectedItem>, TValue, Task>"
            }
        };
    }
}
