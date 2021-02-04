// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using System.Collections.Generic;
using System.ComponentModel;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class InputNumbers
    {
        /// <summary>
        /// 
        /// </summary>
        public int BindValue { get; set; } = 5;

        /// <summary>
        /// 
        /// </summary>
        public sbyte BindSByteValue { get; set; } = 10;

        /// <summary>
        /// 
        /// </summary>
        public byte BindByteValue { get; set; } = 10;

        /// <summary>
        /// 
        /// </summary>
        public long BindLongValue { get; set; } = 10;

        /// <summary>
        /// 
        /// </summary>
        public short BindShortValue { get; set; } = 10;

        /// <summary>
        /// 
        /// </summary>
        public double BindDoubleValue { get; set; } = 10;

        /// <summary>
        /// 
        /// </summary>
        public float BindFloatValue { get; set; } = 10;

        /// <summary>
        /// 
        /// </summary>
        public decimal BindDecimalValue { get; set; } = 10;

        /// <summary>
        /// 
        /// </summary>
        private InputModel Model { get; set; } = new InputModel() { Count = 10 };

        private class InputModel
        {
            /// <summary>
            /// 
            /// </summary>
            [DisplayName("数量")]
            public int Count { get; set; }
        }

        private string Formatter(double val) => val.ToString("0.0");

        private IEnumerable<AttributeItem> GetAttributes()
        {
            return new AttributeItem[]
            {
                new AttributeItem() {
                    Name = "Value",
                    Description = "当前值",
                    Type = "sbyte|byte|int|long|short|float|double|decimal",
                    ValueList = " — ",
                    DefaultValue = "0"
                },
                new AttributeItem() {
                    Name = "Max",
                    Description = "可允许最大值",
                    Type = "string",
                    ValueList = " - ",
                    DefaultValue = " - "
                },
                new AttributeItem()
                {
                    Name = "Min",
                    Description = "可允许最小值",
                    Type = "string",
                    ValueList = " - ",
                    DefaultValue = " - "
                },
                new AttributeItem()
                {
                    Name = "Step",
                    Description = "步长",
                    Type = "int|long|short|float|double|decimal",
                    ValueList = " - ",
                    DefaultValue = "1"
                },
                new AttributeItem()
                {
                    Name = "IsDisabled",
                    Description = "是否禁用 默认为 fasle",
                    Type = "bool",
                    ValueList = "true|false",
                    DefaultValue = "false"
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
                }
            };
        }
    }
}
