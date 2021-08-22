// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    ///
    /// </summary>
    public partial class MultiSelects
    {
        [NotNull]
        private BlockLogger? Trace { get; set; }

        [NotNull]
        private BlockLogger? Trace2 { get; set; }

        /// <summary>
        /// 级联绑定菜单
        /// </summary>
        /// <param name="item"></param>
        private async Task OnCascadeBindSelectClick(SelectedItem item)
        {
            // 模拟异步获取数据源
            await Task.Delay(100);
            if (item.Value == "Beijing")
            {
                Items2 = new List<SelectedItem>(new[]
                {
                    new SelectedItem("1","朝阳区") { Active = true },
                    new SelectedItem("2","海淀区")
                });
            }
            else if (item.Value == "Shanghai")
            {
                Items2 = new List<SelectedItem>(new[]
                {
                    new SelectedItem("1","静安区"),
                    new SelectedItem("2","黄浦区") {Active = true },
                });
            }
            else
            {
                Items2 = new List<SelectedItem>();
            }
            StateHasChanged();
        }

        private void AddItems()
        {
            SelectedItemsValue = "Beijing,Shanghai,Guangzhou";
        }

        private void RemoveItems()
        {
            SelectedItemsValue = "Beijing";
        }

        private void ClearItems()
        {
            SelectedItemsValue = "";
        }

        private void AddListItems()
        {
            SelectedArrayValues = "Beijing,Shanghai".Split(',');
        }

        private void RemoveListItems()
        {
            SelectedArrayValues = new[] { "Beijing" };
        }

        private void ClearListItems()
        {
            SelectedArrayValues = Enumerable.Empty<string>();
        }

        private void AddArrayItems()
        {
            SelectedIntArrayValues = new[] { 1, 2, 3, 4 };
        }

        private void RemoveArrayItems()
        {
            SelectedIntArrayValues = new[] { 1, 2, };
        }

        private void ClearArrayItems()
        {
            SelectedIntArrayValues = new int[0];
        }

        private int[] SelectedIntArrayValues { get; set; } = Array.Empty<int>();
        private IEnumerable<string> SelectedArrayValues { get; set; } = Enumerable.Empty<string>();
        private IEnumerable<EnumEducation> SelectedEnumValues { get; set; } = new List<EnumEducation> { EnumEducation.Middel, EnumEducation.Primary };

        private IEnumerable<SelectedItem> OnSearch(string searchText)
        {
            Trace.Log($"搜索文字：{searchText}");
            return Items.Where(i => i.Text.Contains(searchText, System.StringComparison.OrdinalIgnoreCase));
        }

        private Task OnSelectedItemsChanged(IEnumerable<SelectedItem> items)
        {
            Trace.Log($"选中项集合：{string.Join(",", items.Select(i => i.Value))}");
            return Task.CompletedTask;
        }

        private Task OnSelectedItemsChanged8(IEnumerable<SelectedItem> items)
        {
            Trace2.Log($"选中项集合：{string.Join(",", items.Select(i => i.Value))}");
            return Task.CompletedTask;
        }

        private Foo Model { get; set; } = new Foo();

        private Foo Foo { get; set; } = new Foo();

        private void OnClickButton()
        {

        }

        private string SelectedLongItemsValue1 { get; set; } = "";
        private string SelectedLongItemsValue2 { get; set; } = "";
        private string SelectedLongItemsValue3 { get; set; } = "";
        private string SelectedMaxItemsValue { get; set; } = "";
        private string SelectedMinItemsValue { get; set; } = "";

        private string SelectedItemsValue { get; set; } = "Beijing";

        private string SelectedItemsValue6 { get; set; } = "Beijing";

        private string SelectedItemsValue7 { get; set; } = "Beijing";

        private string SelectedItemsValue8 { get; set; } = "Beijing";

        private IEnumerable<SelectedItem> Items { get; set; } = new[] {
            new SelectedItem ("Beijing", "北京"),
            new SelectedItem ("Shanghai", "上海"),
            new SelectedItem ("Guangzhou", "广州"),
            new SelectedItem ("Shenzhen", "深圳"),
            new SelectedItem ("Chengdu", "成都"),
            new SelectedItem ("Wuhan", "武汉"),
            new SelectedItem ("Dalian", "大连"),
            new SelectedItem ("Hangzhou", "杭州"),
            new SelectedItem ("Lianyungang", "连云港")
        };

        private List<SelectedItem> Items2 { get; set; } = new List<SelectedItem>();

        private readonly List<SelectedItem> Items3 = new SelectedItem[]
        {
            new SelectedItem ("", "请选择 ..."),
            new SelectedItem ("Beijing", "北京") { Active = true },
            new SelectedItem ("Shanghai", "上海"),
            new SelectedItem ("Hangzhou", "杭州")
        }.ToList();

        private IEnumerable<SelectedItem> Items4 { get; set; } = new[] {
            new SelectedItem ("Beijing", "北京"),
            new SelectedItem ("Shanghai", "上海"),
            new SelectedItem ("Guangzhou", "广州")
        };

        private IEnumerable<SelectedItem> Items5 { get; set; } = new[] {
            new SelectedItem ("Beijing", "北京"),
            new SelectedItem ("Shanghai", "上海"),
            new SelectedItem ("Guangzhou", "广州")
        };

        private IEnumerable<SelectedItem> Items6 { get; set; } = new[] {
            new SelectedItem ("Beijing", "北京"),
            new SelectedItem ("Shanghai", "上海"),
            new SelectedItem ("Guangzhou", "广州")
        };

        private IEnumerable<SelectedItem> Items7 { get; set; } = new[] {
            new SelectedItem ("Beijing", "北京"),
            new SelectedItem ("Shanghai", "上海"),
            new SelectedItem ("Guangzhou", "广州")
        };

        private IEnumerable<SelectedItem> Items8 { get; set; } = new[] {
            new SelectedItem ("Beijing", "北京"),
            new SelectedItem ("Shanghai", "上海"),
            new SelectedItem ("Guangzhou", "广州")
        };

        private IEnumerable<SelectedItem> Items9 { get; set; } = new[] {
            new SelectedItem ("Beijing", "北京"),
            new SelectedItem ("Shanghai", "上海"),
            new SelectedItem ("Guangzhou", "广州")
        };

        private IEnumerable<SelectedItem> Items10 { get; set; } = new[] {
            new SelectedItem ("Beijing", "北京"),
            new SelectedItem ("Shanghai", "上海"),
            new SelectedItem ("Guangzhou", "广州")
        };

        private IEnumerable<SelectedItem> Items11 { get; set; } = new[] {
            new SelectedItem ("Beijing", "北京"),
            new SelectedItem ("Shanghai", "上海"),
            new SelectedItem ("Guangzhou", "广州")
        };

        private IEnumerable<SelectedItem> LongItems { get; set; } = new[]
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

        private IEnumerable<SelectedItem> LongItems1 { get; set; } = new[]
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

        private IEnumerable<SelectedItem> LongItems2 { get; set; } = new[]
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

        private IEnumerable<SelectedItem> LongItems3 { get; set; } = new[]
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

        private IEnumerable<SelectedItem> LongItems4 { get; set; } = new[]
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

        private IEnumerable<SelectedItem> LongItems5 { get; set; } = new[]
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

        /// <summary>
        /// 获得事件方法
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<EventItem> GetEvents() => new[]
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
        private static IEnumerable<AttributeItem> GetAttributes() => new[]
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
                Name = "ShowToolbar",
                Description = "是否显示功能按钮",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowDefaultButtons",
                Description = "是否显示默认功能按钮",
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
            },
            new AttributeItem() {
                Name = "ButtonTemplate",
                Description = "扩展按钮模板",
                Type = "RenderFragment<IEnumerable<SelectedItem>>",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };
    }
}
