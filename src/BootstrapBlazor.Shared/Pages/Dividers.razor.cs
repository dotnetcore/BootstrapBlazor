using BootstrapBlazor.Shared.Common;
using System.Collections.Generic;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// Dividers 组件示例文档
    /// </summary>
    sealed partial class Dividers
    {
        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Text",
                Description = "设置分割线显示文字",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Icon",
                Description = "设置分割线显示图标",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Alignment",
                Description = "设置分割线显示文字对齐方式",
                Type = "Aligment",
                ValueList = "Left|Center|Right|Top|Bottom",
                DefaultValue = "Center"
            },
            new AttributeItem() {
                Name = "IsVertical",
                Description = "设置分割线是否为垂直分割",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ChildContent",
                Description = "ChildContent 模板",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };
    }
}
