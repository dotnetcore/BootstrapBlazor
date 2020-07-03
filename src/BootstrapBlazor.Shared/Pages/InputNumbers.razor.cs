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

        private string Formatter(double val) => val.ToString("0.0");

        private IEnumerable<AttributeItem> GetAttributes()
        {
            return new AttributeItem[]
            {
                new AttributeItem() {
                    Name = "Value",
                    Description = "当前值",
                    Type = "int|long|short|float|double|decimal",
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
            };
        }
    }
}
