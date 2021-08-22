// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CheckboxLists
    {
        [NotNull]
        private IEnumerable<SelectedItem>? Items1 { get; set; }

        [NotNull]
        private IEnumerable<SelectedItem>? Items2 { get; set; }

        [NotNull]
        private IEnumerable<SelectedItem>? Items3 { get; set; }

        [NotNull]
        private IEnumerable<SelectedItem>? Items4 { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<Foo>? Localizer { get; set; }

        private IEnumerable<EnumEducation> SelectedEnumValues { get; set; } = new List<EnumEducation> { EnumEducation.Middel, EnumEducation.Primary };

        private string Value1 { get; set; } = "1,3";

        private IEnumerable<int> Value2 { get; set; } = new int[] { 9, 10 };

        private IEnumerable<string> Value3 { get; set; } = new string[] { "13", "15" };

        private BlockLogger? Trace { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        /// <returns></returns>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Items1 = new List<SelectedItem>(new List<SelectedItem> {
                new SelectedItem { Text = "Item 1", Value = "1" },
                new SelectedItem { Text = "Item 2", Value = "2" },
                new SelectedItem { Text = "Item 3", Value = "3" },
                new SelectedItem { Text = "Item 4", Value = "4" },
            });

            Items2 = new List<SelectedItem>(new List<SelectedItem>
            {
                new SelectedItem { Text = "张三", Value = "张三" },
                new SelectedItem { Text = "李四", Value = "李四" },
                new SelectedItem { Text = "王五", Value = "王五" },
                new SelectedItem { Text = "赵六", Value = "赵六" },
            });

            Items3 = new List<SelectedItem>(new List<SelectedItem>
            {
                new SelectedItem { Text = "Item 9", Value = "9" },
                new SelectedItem { Text = "Item 10", Value = "10" },
                new SelectedItem { Text = "Item 11", Value = "11" },
                new SelectedItem { Text = "Item 12", Value = "12" },
            });

            Items4 = new List<SelectedItem>(new List<SelectedItem>
            {
                new SelectedItem { Text = "Item 13", Value = "13" },
                new SelectedItem { Text = "Item 14", Value = "14" },
                new SelectedItem { Text = "Item 15", Value = "15" },
                new SelectedItem { Text = "Item 16", Value = "16" },
            });
        }

        private Task OnSelectedChanged(IEnumerable<SelectedItem> items, string value)
        {
            Trace?.Log($"共 {items.Where(i => i.Active).Count()} 项被选中 组件绑定值 value：{value}");
            return Task.CompletedTask;
        }

        private Foo Dummy { get; set; } = new Foo() { Name = "张三,李四" };

        private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            new AttributeItem() {
                Name = "Items",
                Description = "数据源",
                Type = "IEnumerable<SelectedItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "IsDisabled",
                Description = "是否禁用",
                Type = "boolean",
                ValueList = " — ",
                DefaultValue = "false"
            },
            new AttributeItem(){
                Name = "Value",
                Description = "组件值用于双向绑定",
                Type = "TValue",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem(){
                Name = "IsVertical",
                Description = "是否竖向排列",
                Type = "boolean",
                ValueList = " true / false ",
                DefaultValue = " false "
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
