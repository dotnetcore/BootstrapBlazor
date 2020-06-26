using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Carousels
    {
        /// <summary>
        /// 
        /// </summary>
        private Logger? Trace { get; set; }

        private IEnumerable<string> Images => new List<string>()
        {
            "_content/BootstrapBlazor.Shared/images/Pic0.jpg",
            "_content/BootstrapBlazor.Shared/images/Pic1.jpg",
            "_content/BootstrapBlazor.Shared/images/Pic2.jpg"
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <returns></returns>
        private Task OnClick(string imageUrl)
        {
            Trace?.Log($"Image Clicked: {imageUrl}");
            return Task.CompletedTask;
        }

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Images",
                Description = "Images 集合",
                Type = "IEnumerable<string>",
                ValueList = "—",
                DefaultValue = "—"
            },
            new AttributeItem() {
                Name = "IsFade",
                Description = "是否淡入淡出",
                Type = "boolean",
                ValueList = " — ",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Width",
                Description = "设置图片宽度",
                Type = "int",
                ValueList = " — ",
                DefaultValue = "—"
            },
            new AttributeItem() {
                Name = "OnClick",
                Description = "点击图片回调委托",
                Type = "Func<string, Task>",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };
    }
}
