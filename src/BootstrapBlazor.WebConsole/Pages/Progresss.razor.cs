using BootstrapBlazor.WebConsole.Common;
using BootstrapBlazor.WebConsole.Pages.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Progresss
    {
        /// <summary>
        /// 
        /// </summary>
        protected Logger? Trace { get; set; }

       

       

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Color",
                Description = "颜色",
                Type = "Color",
                ValueList = "Primary / Secondary / Success / Danger / Warning / Info / Dark",
                DefaultValue = "Primary"
            },
            new AttributeItem() {
                Name = "Class",
                Description = "样式",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
              new AttributeItem() {
                Name = "Height",
                Description = "进度条高度",
                Type = "int",
                ValueList = " — ",
                DefaultValue = "15"
            },
           new AttributeItem() {
                Name = "IsShowValue",
                Description = "是否显示值",
                Type = "boolean",
                ValueList = " — ",
                DefaultValue = "false"
            },
           new AttributeItem() {
                Name = "IsStriped",
                Description = "是否显示条纹",
                Type = "boolean",
                ValueList = " — ",
                DefaultValue = "false"
            },
           new AttributeItem() {
                Name = "IsAnimated",
                Description = "是否动态显示",
                Type = "boolean",
                ValueList = " — ",
                DefaultValue = "false"
            },

        };
    }
}
