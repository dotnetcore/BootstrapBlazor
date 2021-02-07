// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
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
    public partial class MultiSelects
    {
        /// <summary>
        /// 获得/设置 Logger 实例
        /// </summary>
        private Logger? Trace { get; set; }

        /// <summary>
        /// 获得 默认数据集合
        /// </summary>
        private readonly IEnumerable<SelectedItem> Items = new SelectedItem[]
        {
            new SelectedItem ("Beijing", "北京"),
            new SelectedItem ("Shanghai", "上海"),
            new SelectedItem ("Guangzhou", "广州"),
            new SelectedItem ("Shenzhen", "深圳"),
            new SelectedItem ("Chengdu", "成都"),
            new SelectedItem ("Wuhan", "武汉"),
            new SelectedItem ("Dalian", "大连"),
            new SelectedItem ("Hangzhou", "杭州"),
            new SelectedItem ("Lianyungang", "连云港"),
        };

        /// <summary>
        /// 获得 默认数据集合
        /// </summary>
        private readonly IEnumerable<SelectedItem> LongItems = new SelectedItem[]
        {
            new SelectedItem ("1", "特别甜的东瓜(特别甜的东瓜)"),
            new SelectedItem ("2", "特别甜的西瓜(特别甜的西瓜)"),
            new SelectedItem ("3", "特别甜的南瓜(特别甜的南瓜)"),
            new SelectedItem ("4", "特别甜的傻瓜(特别甜的傻瓜)"),
            new SelectedItem ("5", "特别甜的金瓜(特别甜的金瓜)"),
            new SelectedItem ("6", "特别甜的木瓜(特别甜的木瓜)"),
            new SelectedItem ("7", "特别甜的水瓜(特别甜的水瓜)"),
            new SelectedItem ("8", "特别甜的火瓜(特别甜的火瓜)"),
            new SelectedItem ("9", "特别甜的土瓜(特别甜的土瓜)"),
        };

        private string SelectedLongItemsValue { get; set; } = "";
        private string SelectedMaxItemsValue { get; set; } = "";
        private string SelectedMinItemsValue { get; set; } = "";

        private string SelectedItemsValue { get; set; } = "Beijing,Chengdu";

        private void AddItems()
        {
            SelectedItemsValue = "Beijing,Chengdu,Hangzhou,Lianyungang";
        }

        private void RemoveItems()
        {
            SelectedItemsValue = "Beijing,Chengdu";
        }

        private void AddListItems()
        {
            SelectedArrayValues = "Beijing,Chengdu,Hangzhou,Lianyungang".Split(',');
        }

        private void RemoveListItems()
        {
            SelectedArrayValues = "Beijing,Chengdu".Split(',');
        }

        private IEnumerable<string> SelectedArrayValues { get; set; } = Enumerable.Empty<string>();

        private IEnumerable<SelectedItem> OnSearch(string searchText)
        {
            return Items.Where(i => i.Text.Contains(searchText, System.StringComparison.OrdinalIgnoreCase));
        }

        private Task OnSelectedItemsChanged(IEnumerable<SelectedItem> items)
        {
            Trace?.Log($"选中项集合：{string.Join(",", items.Select(i => i.Value))}");
            return Task.CompletedTask;
        }

        private BindItem Model { get; set; } = new BindItem();

        private Foo Foo { get; set; } = new Foo();

        /// <summary>
        /// 获得事件方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<EventItem> GetEvents() => new EventItem[]
        {
            new EventItem()
            {
                Name = "OnSelectedItemChanged",
                Description="下拉框选项改变时触发此事件",
                Type ="Func<SelectedItem, Task>"
            },
            new EventItem()
            {
                Name = "OnSearchTextChanged",
                Description="搜索文本发生变化时回调此方法",
                Type ="Func<string, IEnumerable<SelectedItem>>"
            }
        };

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
                Name = "ShowCloseButton",
                Description = "是否显示前置标签关闭按钮",
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
                Name = "PlaceHolder",
                Description = "未选择时的占位显示文字",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "点击进行多选 ..."
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
            }
        };
    }
}
