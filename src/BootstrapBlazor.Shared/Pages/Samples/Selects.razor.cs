// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 下拉框操作类
    /// </summary>
    public sealed partial class Selects
    {
        /// <summary>
        ///
        /// </summary>
        private Foo Model { get; set; } = new Foo();

        /// <summary>
        ///
        /// </summary>
        private Foo BindModel { get; set; } = new Foo() { Name = "" };

        /// <summary>
        /// 获得/设置 Logger 实例
        /// </summary>
        private BlockLogger? Trace { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<Selects>? Localizer { get; set; }

        /// <summary>
        /// 获得 默认数据集合
        /// </summary>
        private IEnumerable<SelectedItem> Items { get; set; } = new[]
        {
            new SelectedItem ("Beijing", "北京"),
            new SelectedItem ("Shanghai", "上海") { Active = true },
        };

        /// <summary>
        /// 获得 默认数据集合
        /// </summary>
        private readonly IEnumerable<SelectedItem> Items4 = new[]
        {
            new SelectedItem ("Beijing", "北京") { IsDisabled = true},
            new SelectedItem ("Shanghai", "上海") { Active = true },
            new SelectedItem ("Guangzhou", "广州")
        };

        /// <summary>
        /// 获得 默认数据集合
        /// </summary>
        private readonly IEnumerable<SelectedItem> StringItems = new[]
        {
            new SelectedItem ("1", "1"),
            new SelectedItem ("12", "12"),
            new SelectedItem ("123", "123"),
            new SelectedItem ("1234", "1234"),
            new SelectedItem ("a", "a"),
            new SelectedItem ("ab", "ab"),
            new SelectedItem ("abc", "abc"),
            new SelectedItem ("abcd", "abcd"),
            new SelectedItem ("abcde", "abcde")
        };

        /// <summary>
        /// 获得 默认数据集合
        /// </summary>
        private readonly IEnumerable<SelectedItem> Items3 = new SelectedItem[]
        {
            new SelectedItem ("", "请选择 ..."),
            new SelectedItem ("Beijing", "北京") { Active = true },
            new SelectedItem ("Shanghai", "上海"),
            new SelectedItem ("Hangzhou", "杭州")
        };

        private readonly IEnumerable<SelectedItem> GroupItems = new SelectedItem[]
        {
            new SelectedItem ("Jilin", "吉林") { GroupName = "东北"},
            new SelectedItem ("Liaoning", "辽宁") {GroupName = "东北", Active = true },
            new SelectedItem ("Beijing", "北京") { GroupName = "华中"},
            new SelectedItem ("Shijiazhuang", "石家庄") { GroupName = "华中"},
            new SelectedItem ("Shanghai", "上海") {GroupName = "华东", Active = true },
            new SelectedItem ("Ningbo", "宁波") {GroupName = "华东", Active = true }
        };

        private Guid CurrentGuid { get; set; }

        private readonly IEnumerable<SelectedItem> GuidItems = new SelectedItem[]
        {
            new SelectedItem(Guid.NewGuid().ToString(), "Guid1"),
            new SelectedItem(Guid.NewGuid().ToString(), "Guid2")
        };

        /// <summary>
        /// 下拉选项改变时调用此方法
        /// </summary>
        /// <param name="item"></param>
        private Task OnItemChanged(SelectedItem item)
        {
            Trace?.Log($"SelectedItem Text: {item.Text} Value: {item.Value} Selected");
            StateHasChanged();
            return Task.CompletedTask;
        }

        /// <summary>
        /// 级联绑定菜单
        /// </summary>
        /// <param name="item"></param>
        private async Task OnCascadeBindSelectClick(SelectedItem item)
        {
            // 模拟异步通讯切换线程
            await Task.Delay(10);
            if (item.Value == "Beijing")
            {
                Items2 = new SelectedItem[]
                {
                    new SelectedItem("1","朝阳区") { Active = true},
                    new SelectedItem("2","海淀区"),
                };
            }
            else if (item.Value == "Shanghai")
            {
                Items2 = new SelectedItem[]
                {
                    new SelectedItem("1","静安区"),
                    new SelectedItem("2","黄浦区") { Active = true } ,
                };
            }
            else
            {
                Items2 = Enumerable.Empty<SelectedItem>();
            }
            StateHasChanged();
        }

        private Task OnShowDialog() => Dialog.Show(new DialogOption()
        {
            Title = "弹窗中使用级联下拉框",
            Component = BootstrapDynamicComponent.CreateComponent<CustomerSelectDialog>()
        });

        private IEnumerable<SelectedItem>? Items2 { get; set; }

        private IEnumerable<SelectedItem> NullableIntItems { get; set; } = new SelectedItem[]
        {
            new SelectedItem() { Text = "Item 1", Value = "" },
            new SelectedItem() { Text = "Item 2", Value = "2" },
            new SelectedItem() { Text = "Item 3", Value = "3" }
        };

        private int? SelectedIntItem { get; set; }

        private string GetSelectedIntItemString()
        {
            return SelectedIntItem.HasValue ? SelectedIntItem.Value.ToString() : "null";
        }

        private IEnumerable<SelectedItem> NullableBoolItems { get; set; } = new SelectedItem[]
        {
            new SelectedItem() { Text = "空值", Value = "" },
            new SelectedItem() { Text = "True 值", Value = "true" },
            new SelectedItem() { Text = "False 值", Value = "false" }
        };

        private bool? SelectedBoolItem { get; set; }

        private string GetSelectedBoolItemString()
        {
            return SelectedBoolItem.HasValue ? SelectedBoolItem.Value.ToString() : "null";
        }

        private EnumEducation SelectedEnumItem { get; set; } = EnumEducation.Primary;

        private EnumEducation? SelectedEnumItem1 { get; set; }

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
                Type ="Func<SelectedItem, Task>"
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
                Name = "ShowSearch",
                Description = "是否显示搜索框",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
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
                Name = "ItemTemplate",
                Description = "数据选项模板",
                Type = "RenderFragment<SelectedItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ChildContent",
                Description = "数据模板",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };
    }
}
