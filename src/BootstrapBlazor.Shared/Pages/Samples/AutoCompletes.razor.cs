// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class AutoCompletes
    {
        private readonly List<string> _items = new List<string>();

        private IEnumerable<string> Items => _items;

        /// <summary>
        /// 
        /// </summary>
        private Foo Model { get; set; } = new Foo() { Name = "" };

        private IEnumerable<string> StaticItems => new List<string> { "1", "12", "123", "1234", "12345", "123456", "abc", "abcdef", "ABC", "aBcDeFg", "ABCDEFG" };


        private Task OnValueChanged(string val)
        {
            _items.Clear();
            _items.Add($"{val}@163.com");
            _items.Add($"{val}@126.com");
            _items.Add($"{val}@sina.com");
            _items.Add($"{val}@hotmail.com");
            return Task.CompletedTask;
        }

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
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
                Name = "ChildContent",
                Description = "内容",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Items",
                Description = "内容",
                Type = "IEnumerable<string>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "NoDataTip",
                Description = "自动完成数据无匹配项时提示信息",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "无匹配数据"
            },
            new AttributeItem() {
                Name = "ValueChanged",
                Description = "文本框值变化时回调委托方法",
                Type = "Action<string>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "IsLikeMatch",
                Description = "是否开启模糊匹配",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem()
            {
                Name = "IgnoreCase",
                Description = "匹配时是否忽略大小写",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "true"
            },
        };
    }
}
