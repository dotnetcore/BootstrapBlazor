using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class PopoverConfirms
    {
        /// <summary>
        /// 
        /// </summary>
        private Logger? Trace { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private void OnClose()
        {
            // 点击确认按钮后此方法被回调，点击取消按钮时此方法不会被调用
            Trace?.Log("OnClose Trigger");
        }

        /// <summary>
        /// 
        /// </summary>
        private Task OnConfirm()
        {
            // 点击确认按钮后此方法被回调，点击取消按钮时此方法不会被调用
            Trace?.Log("OnConfirm Trigger");
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
                Name = "Text",
                Description = "显示标题",
                Type = "string",
                ValueList = "",
                DefaultValue = "删除"
            },
            new AttributeItem() {
                Name = "Icon",
                Description = "按钮图标",
                Type = "string",
                ValueList = "",
                DefaultValue = "fa fa-remove"
            },
            new AttributeItem() {
                Name = "CloseButtonText",
                Description = "关闭按钮显示文字",
                Type = "string",
                ValueList = "",
                DefaultValue = "关闭"
            },
            new AttributeItem() {
                Name = "CloseButtonColor",
                Description = "确认按钮颜色",
                Type = "Color",
                ValueList = "None / Active / Primary / Secondary / Success / Danger / Warning / Info / Light / Dark / Link",
                DefaultValue = "Secondary"
            },
            new AttributeItem() {
                Name = "Color",
                Description = "颜色",
                Type = "Color",
                ValueList = "None / Active / Primary / Secondary / Success / Danger / Warning / Info / Light / Dark / Link",
                DefaultValue = "None"
            },
            new AttributeItem() {
                Name = "ConfirmButtonText",
                Description = "确认按钮显示文字",
                Type = "string",
                ValueList = "",
                DefaultValue = "确定"
            },
            new AttributeItem() {
                Name = "ConfirmButtonColor",
                Description = "确认按钮颜色",
                Type = "None / Active / Primary / Secondary / Success / Danger / Warning / Info / Light / Dark / Link",
                ValueList = "",
                DefaultValue = "Primary"
            },
            new AttributeItem() {
                Name = "ConfirmIcon",
                Description = "确认框图标",
                Type = "string",
                ValueList = "",
                DefaultValue = "fa fa-exclamation-circle text-info"
            },
            new AttributeItem() {
                Name = "Cotent",
                Description = "显示文字",
                Type = "string",
                ValueList = "",
                DefaultValue = "确认删除吗？"
            },
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
                DefaultValue = " "
            },
        };

        /// <summary>
        /// 获得事件方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<EventItem> GetEvents() => new EventItem[]
        {
            new EventItem()
            {
                Name = "OnConfirm",
                Description="点击确认时回调方法",
                Type ="Action"
            },
            new EventItem()
            {
                Name = "OnClose",
                Description="点击关闭时回调方法",
                Type ="Action"
            },
            new EventItem()
            {
                Name = "OnBeforeClick",
                Description="点击确认弹窗前回调方法 返回真时弹出弹窗 返回假时不弹出",
                Type ="Func<bool>"
            },
        };
    }
}
