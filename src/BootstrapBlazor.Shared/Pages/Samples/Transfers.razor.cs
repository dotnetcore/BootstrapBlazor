// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Transfers : ComponentBase
    {
        /// <summary>
        /// 
        /// </summary>
        [NotNull]
        private IEnumerable<SelectedItem>? Items { get; set; }

        [NotNull]
        private IEnumerable<SelectedItem>? Items1 { get; set; }

        [NotNull]
        private IEnumerable<SelectedItem>? Items2 { get; set; }

        [NotNull]
        private List<SelectedItem>? Items3 { get; set; }

        [NotNull]
        private IEnumerable<SelectedItem>? Items4 { get; set; }

        [NotNull]
        private IEnumerable<SelectedItem>? Items5 { get; set; }

        [NotNull]
        private BlockLogger? Trace { get; set; }

        private IEnumerable<SelectedItem> SelectedValue { get; set; } = Enumerable.Empty<SelectedItem>();

        private Foo Model { get; set; } = new();

        /// <summary>
        /// OnInitializedAsync 方法
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            // 模拟异步加载数据源
            await Task.Delay(100);

            Items = Enumerable.Range(1, 5).Select(i => new SelectedItem()
            {
                Text = $"备选 {i:d2}",
                Value = i.ToString()
            });

            Items1 = Enumerable.Range(1, 5).Select(i => new SelectedItem()
            {
                Text = $"数据 {i:d2}",
                Value = i.ToString()
            });

            Items2 = Enumerable.Range(1, 5).Select(i => new SelectedItem()
            {
                Text = $"数据 {i:d2}",
                Value = i.ToString()
            });

            Items3 = Enumerable.Range(1, 5).Select(i => new SelectedItem()
            {
                Text = $"备选 {i:d2}",
                Value = i.ToString()
            }).ToList();

            SelectedValue = Items3.Take(2);

            Items4 = Enumerable.Range(1, 5).Select(i => new SelectedItem()
            {
                Text = $"数据 {i:d2}",
                Value = i.ToString()
            });

            Items5 = Enumerable.Range(1, 5).Select(i => new SelectedItem()
            {
                Text = $"数据 {i:d2}",
                Value = i.ToString()
            });
        }

        private void OnAddItem()
        {
            var count = Items3.Count + 1;
            Items3.Add(new SelectedItem(count.ToString(), $"备选 {count:d2}"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        private Task OnSelectedItemsChanged(IEnumerable<SelectedItem> items)
        {
            Trace?.Log(string.Join(" ", items.Where(i => i.Active).Select(i => i.Text)));
            return Task.CompletedTask;
        }

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Items",
                Description = "组件绑定数据项集合",
                Type = "IEnumerable<SelectedItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "LeftButtonText",
                Description = "左侧按钮显示文本",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "LeftPanelText",
                Description = "左侧面板 Header 显示文本",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "列表 1"
            },
            new AttributeItem() {
                Name = "RightButtonText",
                Description = "右侧按钮显示文本",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "RightPanelText",
                Description = "右侧面板 Header 显示文本",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "列表 2"
            },
            new AttributeItem() {
                Name = "ShowSearch",
                Description = "是否显示搜索框",
                Type = "boolean",
                ValueList = " — ",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "LeftPannelSearchPlaceHolderString",
                Description = "左侧面板中的搜索框 placeholder 字符串",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "RightPannelSearchPlaceHolderString",
                Description = "右侧面板中的搜索框 placeholder 字符串",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "IsDisabled",
                Description = "是否禁用",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "false"
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
                Name = "OnItemsChanged",
                Description="组件绑定数据项集合选项变化时回调方法",
                Type ="Action<IEnumerable<SelectedItem>>"
            }
        };
    }
}
