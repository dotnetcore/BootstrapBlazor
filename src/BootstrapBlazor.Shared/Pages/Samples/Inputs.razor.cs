// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Inputs
    {
        private string? PlaceHolderText { get; set; }

        private byte[] ByteArray { get; set; } = new byte[] { 0x01, 0x12, 0x34, 0x56 };

        private static string ByteArrayFormatter(byte[] source) => Convert.ToBase64String(source);

        private Foo Model { get; set; } = new Foo() { Name = "张三" };

        private static string DateTimeFormatter(DateTime source) => source.ToString("yyyy-MM-dd");

        [NotNull]
        private BlockLogger? Trace { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            PlaceHolderText = Localizer["PlaceHolder"];
        }

        private Task OnEnterAsync(string val)
        {
            Trace.Log($"Enter 按键触发 当前文本框值: {val}");
            return Task.CompletedTask;
        }

        private Task OnEscAsync(string val)
        {
            Trace.Log($"Esc 按键触发 当前文本框值: {val}");
            return Task.CompletedTask;
        }

        private static IEnumerable<AttributeItem> GetAttributes() => new[]
        {
            new AttributeItem() {
                Name = "ChildContent",
                Description = "验证控件",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ShowLabel",
                Description = "是否显示前置标签",
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
                Name = "Color",
                Description = "颜色",
                Type = "Color",
                ValueList = "Primary / Secondary / Success / Danger / Warning / Info / Dark",
                DefaultValue = "Primary"
            },
            new AttributeItem() {
                Name = "FormatString",
                Description = "数值格式化字符串",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Formatter",
                Description = "TableHeader 实例",
                Type = "RenderFragment<TItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem()
            {
                Name = "type",
                Description = "控件类型",
                Type = "string",
                ValueList = "text / number / email / url / password",
                DefaultValue = "text"
            },
            new AttributeItem() {
                Name = "OnEnterAsync",
                Description = "用户按下 Enter 键回调委托",
                Type = "Func<TValue, Task>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnEscAsync",
                Description = "用户按下 Esc 键回调委托",
                Type = "Func<TValue, Task>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem()
            {
                Name = "IsDisabled",
                Description = "是否禁用 默认为 fasle",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem()
            {
                Name = "IsAutoFocus",
                Description = "是否自动获取焦点 默认为 fasle",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            }
        };
    }
}
