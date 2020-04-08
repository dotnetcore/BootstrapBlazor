using BootstrapBlazor.Components;
using BootstrapBlazor.WebConsole.Common;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Toasts
    {
        /// <summary>
        /// 
        /// </summary>
        [Inject] public ToastService? ToastService { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected void OnSuccessClick()
        {
            ToastService?.Show(new ToastOption()
            {
                Category = ToastCategory.Success,
                Title = "保存成功",
                Content = "保存数据成功，4 秒后自动关闭"
            });
        }

        /// <summary>
        /// 
        /// </summary>
        protected void OnErrorClick()
        {
            ToastService?.Show(new ToastOption()
            {
                Category = ToastCategory.Error,
                Title = "保存失败",
                Content = "保存数据失败，4 秒后自动关闭"
            });
        }

        /// <summary>
        /// 
        /// </summary>
        protected void OnInfoClick()
        {
            ToastService?.Show(new ToastOption()
            {
                Category = ToastCategory.Information,
                Title = "消息通知",
                Content = "系统增加新组件啦，4 秒后自动关闭"
            });
        }

        /// <summary>
        /// 
        /// </summary>
        protected void OnNotAutoHideClick()
        {
            ToastService?.Show(new ToastOption()
            {
                Category = ToastCategory.Information,
                IsAutoHide = false,
                Title = "消息通知",
                Content = "我不会自动关闭哦，请点击右上角关闭按钮"
            });
        }

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Placement",
                Description = "位置",
                Type = "Placement",
                ValueList = "Auto / Top / Left / Bottom / Right",
                DefaultValue = "Auto"
            },
            new AttributeItem() {
                Name = "Title",
                Description = "Popover 弹窗标题",
                Type = "string",
                ValueList = "",
                DefaultValue = "Popover"
            },
            new AttributeItem() {
                Name = "Cotent",
                Description = "Popover 弹窗内容",
                Type = "string",
                ValueList = "",
                DefaultValue = "Popover"
            },
            new AttributeItem() {
                Name = "IsHtml",
                Description = "内容中是否包含 Html 代码",
                Type = "boolean",
                ValueList = "",
                DefaultValue = "false"
            }
        };
    }
}
