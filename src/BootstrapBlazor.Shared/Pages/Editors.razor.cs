using BootstrapBlazor.Shared.Common;
using System.Collections.Generic;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Editors
    {
        private string EditorValue { get; set; } = "初始值 <b>Test</b>";

        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Placeholder",
                Description = "空值时的提示信息",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "点击后进行编辑"
            },
            new AttributeItem() {
                Name = "IsEditor",
                Description = "是否直接显示为富文本编辑框",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Height",
                Description = "组件高度",
                Type = "int",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };
    }
}
