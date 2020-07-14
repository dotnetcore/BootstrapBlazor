using BootstrapBlazor.Shared.Common;
using System.Collections.Generic;

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
                }
            };
        }
    }
}
