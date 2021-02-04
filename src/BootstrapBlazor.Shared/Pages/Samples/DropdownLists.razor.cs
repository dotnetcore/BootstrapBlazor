// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class DropdownLists
    {
        private int SelectedItem { get; set; } = 2;

        private List<SelectedItem> Items { get; set; } = new List<SelectedItem>
        {
            new SelectedItem{ Text = "北京", Value = "0" },
            new SelectedItem{ Text = "上海", Value = "1" },
            new SelectedItem{ Text = "广州", Value = "2" },
        };

        private List<string> EnumItems { get; set; } = Enum.GetNames(typeof(SortOrder)).ToList();

        private string SelectedEnumItem { get; set; } = "UnSet";

        private class Foo
        {
            [DisplayName("姓名")]
            public string Name { get; set; } = "";

            public int Age { get; set; }
        }

        private IEnumerable<Foo> Foos { get; } = new List<Foo>()
        {
            new Foo { Name = "张三", Age = 10 },
            new Foo { Name = "张四", Age = 20 },
            new Foo { Name = "张五", Age = 30 }
        };

        private string SelectedFooName { get; set; } = "";

        private Foo Model { get; set; } = new Foo();

        /// <summary>
        /// 获得事件方法
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<EventItem> GetEvents() => new EventItem[]
        {
            new EventItem()
            {
                Name = "OnSelectedItemChanged",
                Description="下拉框选项改变时触发此事件",
                Type ="EventCallback<TValue>"
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
                Name = "ShowLabel",
                Description = "是否显示前置标签",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "DisplayText",
                Description = "前置标签显示文本",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Class",
                Description = "样式",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Color",
                Description = "颜色",
                Type = "Color",
                ValueList = "Primary / Secondary / Success / Danger / Warning / Info / Dark",
                DefaultValue = "Primary"
            },
            new AttributeItem() {
                Name = "IsDisabled",
                Description = "是否禁用",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Items",
                Description = "数据集合",
                Type = "IEnumerable<SelectedItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "SelectItems",
                Description = "静态数据模板",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ChildContent",
                Description = "数据模板",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnSelectedItemChanged",
                Description = "下拉框选择项改变时回调此委托",
                Type = "Func<TValue, Task>",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };
    }
}
