// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Searchs
    {
        private IEnumerable<string> Items => new string[] { "1", "12", "123", "1234" };

        [NotNull]
        private Logger? Trace { get; set; }

        [NotNull]
        private Logger? Trace2 { get; set; }

        private Task OnSearch(string searchText)
        {
            Trace.Log($"SearchText: {searchText}");
            return Task.CompletedTask;
        }

        private Task OnSearch2(string searchText)
        {
            Trace2.Log($"SearchText: {searchText}");
            return Task.CompletedTask;
        }

        private Task OnClear(string searchText)
        {
            Trace2.Log($"OnClear: {searchText}");
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
                Name = "ClearButtonIcon",
                Description = "清空按钮图标",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "fa fa-trash"
            },
            new AttributeItem() {
                Name = "ClearButtonText",
                Description = "清空按钮文本",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ClearButtonColor",
                Description = "清空按钮颜色",
                Type = "Color",
                ValueList = " — ",
                DefaultValue = "Secondary"
            },
            new AttributeItem() {
                Name = "SearchButtonColor",
                Description = "搜索按钮颜色",
                Type = "Color",
                ValueList = " — ",
                DefaultValue = "Primary"
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
            new AttributeItem()
            {
                Name = "ShowClearButton",
                Description = "是否显示清除按钮",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem()
            {
                Name="OnSearch",
                Description = "点击搜索时回调此委托",
                Type = "Func<string, Task>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem()
            {
                Name="OnClear",
                Description = "点击清空时回调此委托",
                Type = "Func<string, Task>",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };
    }
}
