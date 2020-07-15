using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Lights
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes()
        {
            return new AttributeItem[]
            {
                new AttributeItem() {
                    Name = "Color",
                    Description = "颜色",
                    Type = "Color",
                    ValueList = "None / Active / Primary / Secondary / Success / Danger / Warning / Info / Light / Dark / Link",
                    DefaultValue = "Success"
                },
                new AttributeItem() {
                    Name = "IsFlash",
                    Description = "是否闪烁",
                    Type = "boolean",
                    ValueList = " — ",
                    DefaultValue = "false"
                }
            };
        }
    }
}
