// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Inputs
    {
        private byte[] ByteArray { get; set; } = new byte[] { 0x01, 0x12, 0x34, 0x56 };

        private string ByteArrayFormatter(byte[] source) => Convert.ToBase64String(source);

        private string InputValue => "数据值";

        [System.ComponentModel.DisplayName("标签值")]
        private string BindValue { get; set; } = "绑定值";

        private double BindDoubleValue { get; set; } = 1.0;

        private FooModel Model { get; set; } = new FooModel();

        private string DateTimeFormatter(DateTime source) => source.ToString("yyyy-MM-dd");

        private class FooModel
        {
            [System.ComponentModel.DisplayName("姓名")]
            public string Name { get; set; } = "张三";
        }

        private IEnumerable<AttributeItem> GetAttributes()
        {
            return new AttributeItem[]
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
                new AttributeItem()
                {
                    Name = "IsDisabled",
                    Description = "是否禁用 默认为 fasle",
                    Type = "bool",
                    ValueList = "true|false",
                    DefaultValue = "false"
                }
            };
        }
    }
}
