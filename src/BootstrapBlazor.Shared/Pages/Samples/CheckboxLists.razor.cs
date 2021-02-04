// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CheckboxLists
    {
        private class Foo
        {
            public string Text { get; set; } = "";

            public int Value { get; set; }

            public bool Checked { get; set; }
        }

        private IEnumerable<Foo> Items1 { get; set; } = Enumerable.Empty<Foo>();

        private IEnumerable<Foo> Items2 { get; set; } = Enumerable.Empty<Foo>();

        private IEnumerable<Foo> Items3 { get; set; } = Enumerable.Empty<Foo>();

        private IEnumerable<Foo> Items4 { get; set; } = Enumerable.Empty<Foo>();

        private string Value1 { get; set; } = "1,3";

        private IEnumerable<int> Value2 { get; set; } = new int[] { 9, 10 };

        private IEnumerable<string> Value3 { get; set; } = new string[] { "Item 13", "Item 15" };

        private Logger? Trace { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Items1 = new List<Foo>(new List<Foo> {
                new Foo { Text = "Item 1", Value = 1, Checked = false },
                new Foo { Text = "Item 2", Value = 2, Checked = false },
                new Foo { Text = "Item 3", Value = 3, Checked = false },
                new Foo { Text = "Item 4", Value = 4, Checked = false },
            });

            Items2 = new List<Foo>(new List<Foo>
            {
                new Foo { Text = "Item 5", Value = 5, Checked = false },
                new Foo { Text = "Item 6", Value = 6, Checked = false },
                new Foo { Text = "Item 7", Value = 7, Checked = false },
                new Foo { Text = "Item 8", Value = 8, Checked = false },
            });

            Items3 = new List<Foo>(new List<Foo>
            {
                new Foo { Text = "Item 9", Value = 9, Checked = false },
                new Foo { Text = "Item 10", Value = 10, Checked = false },
                new Foo { Text = "Item 11", Value = 11, Checked = false },
                new Foo { Text = "Item 12", Value = 12, Checked = false },
            });

            Items4 = new List<Foo>(new List<Foo>
            {
                new Foo { Text = "Item 13", Value = 13, Checked = false },
                new Foo { Text = "Item 14", Value = 14, Checked = false },
                new Foo { Text = "Item 15", Value = 15, Checked = false },
                new Foo { Text = "Item 16", Value = 16, Checked = false },
            });
        }

        private Task OnSelectedChanged(IEnumerable<Foo> items, Foo foo, string value)
        {
            Trace?.Log($"{foo.Text} - {foo.Checked} Value: {foo.Value} 共 {items.Where(i => i.Checked).Count()} 项被选中 组件绑定值 value：{value}");
            Trace?.Log($"组件绑定值 Value1：{Value1}");

            return Task.CompletedTask;
        }

        private class Model
        {
            public string Value { get; set; } = "5,8";
        }

        private Model FooModel { get; set; } = new Model();

        private IEnumerable<AttributeItem> GetAttributes()
        {
            return new AttributeItem[]
            {
                new AttributeItem() {
                    Name = "Items",
                    Description = "数据源",
                    Type = "IEnumerable<TModel>",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem(){
                    Name = "Value",
                    Description = "组件值用于双向绑定",
                    Type = "TValue",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem() {
                    Name = "TextField",
                    Description = "显示列字段名称",
                    Type = "string",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem() {
                    Name = "ValueField",
                    Description = "值字段名称",
                    Type = "string",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem(){
                    Name = "CheckedField",
                    Description = "是否选中列字段名称",
                    Type = "string",
                    ValueList = " — ",
                    DefaultValue = " — "
                }
            };
        }

        /// <summary>
        /// 获得事件方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<EventItem> GetEvents() => new EventItem[]
        {
            new EventItem()
            {
                Name = "OnSelectedChanged",
                Description="复选框状态改变时回调此方法",
                Type ="EventCallback<IEnumerable<TModel>, TModel, TValue, Task>"
            }
        };
    }
}
