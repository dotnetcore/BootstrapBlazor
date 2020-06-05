using BootstrapBlazor.Shared.Common;
using System.Collections.Generic;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Avatars
    {
        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Size",
                Description = "头像框大小",
                Type = "Size",
                ValueList = "ExtraSmall|Small|Medium|Large|ExtraLarge",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsBorder",
                Description = "是否显示边框",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsCircle",
                Description = "是否为圆形",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsIcon",
                Description = "是否为图标",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsText",
                Description = "是否为显示为文字",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Icon",
                Description = "头像框显示图标",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "fa fa-user"
            },
            new AttributeItem() {
                Name = "Text",
                Description = "头像框显示文字",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Url",
                Description = "Image 头像路径地址",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };
    }
}
