using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using System.Collections.Generic;
using System.Linq;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    sealed partial class Stepss
    {
        private IEnumerable<StepItem> Items { get; set; } = new StepItem[3]
        {
            new StepItem() { Title = "步骤一" },
            new StepItem() { Title = "步骤二" },
            new StepItem() { Title = "步骤三" }
        };

        private void NextStep()
        {
            var item = Items.FirstOrDefault(i => i.Status == StepStatus.Process);
            if (item != null)
            {
                item.Status = StepStatus.Success;
                var index = Items.ToList().IndexOf(item) + 1;
                if (index < Items.Count())
                {
                    Items.ElementAt(index).Status = StepStatus.Process;
                }
            }
            else
            {
                ResetStep();
                Items.ElementAt(0).Status = StepStatus.Process;
            }
        }

        private void ResetStep()
        {
            Items.ToList().ForEach(i =>
            {
                i.Status = StepStatus.Wait;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        private Logger? Trace { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        private void OnStatusChanged(StepStatus status)
        {
            Trace?.Log($"Steps Status: {status}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes()
        {
            return new AttributeItem[]
            {
                new AttributeItem() {
                    Name = "Items",
                    Description = "设置步骤数据集合",
                    Type = "IEnumerable<StepItem>",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem() {
                    Name = "IsVertical",
                    Description = "显示方向",
                    Type = "bool",
                    ValueList = "true|false",
                    DefaultValue = "false"
                },
                new AttributeItem() {
                    Name = "IsCenter",
                    Description = "进行居中对齐",
                    Type = "bool",
                    ValueList = "true|false",
                    DefaultValue = "false"
                },
                new AttributeItem() {
                    Name = "Status",
                    Description = "设置当前步骤的状态",
                    Type = "StepStatus",
                    ValueList = "Wait|Process|Finish|Error|Success",
                    DefaultValue = "Wait"
                }
            };
        }

        private IEnumerable<AttributeItem> GetStepItemAttributes()
        {
            return new AttributeItem[]
            {
                new AttributeItem() {
                    Name = "IsCenter",
                    Description = "进行居中对齐",
                    Type = "bool",
                    ValueList = "true|false",
                    DefaultValue = "false"
                },
                new AttributeItem() {
                    Name = "IsIcon",
                    Description = "进行使用图标进行步骤显示",
                    Type = "bool",
                    ValueList = "true|false",
                    DefaultValue = "false"
                },
                new AttributeItem() {
                    Name = "IsLast",
                    Description = "是否为最后一个步骤",
                    Type = "bool",
                    ValueList = "true|false",
                    DefaultValue = "false"
                },
                new AttributeItem() {
                    Name = "StepIndex",
                    Description = "步骤顺序号",
                    Type = "int",
                    ValueList = " — ",
                    DefaultValue = "0"
                },
                new AttributeItem() {
                    Name = "Space",
                    Description = "间距不填写将自适应间距支持百分比",
                    Type = "string",
                    ValueList = " — ",
                    DefaultValue = "—"
                },
                new AttributeItem() {
                    Name = "Title",
                    Description = "步骤显示文字",
                    Type = "string",
                    ValueList = " — ",
                    DefaultValue = "—"
                },
                new AttributeItem() {
                    Name = "Icon",
                    Description = "步骤显示图标",
                    Type = "string",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem() {
                    Name = "Description",
                    Description = "描述信息",
                    Type = "string",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem() {
                    Name = "Status",
                    Description = "设置当前步骤的状态",
                    Type = "StepStatus",
                    ValueList = "Wait|Process|Finish|Error|Success",
                    DefaultValue = "Wait"
                }
            };
        }

        private IEnumerable<EventItem> GetEvents()
        {
            return new List<EventItem>()
            {
                new EventItem()
                {
                    Name = "OnStatusChanged",
                    Description="组件状态改变时回调委托",
                    Type ="Action<StepStatus>"
                }
            };
        }
    }
}
