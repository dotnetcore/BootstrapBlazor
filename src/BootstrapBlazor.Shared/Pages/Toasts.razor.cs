using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Toasts
    {
        /// <summary>
        /// 
        /// </summary>
        [Inject] public ToastService? ToastService { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Inject] public IJSRuntime? JSRuntime { get; set; }

        private Toast? Toast { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && JSRuntime != null)
            {
                await JSRuntime.InvokeVoidAsync("$._showToast");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnSuccessClick()
        {
            Toast?.SetPlacement(Placement.BottomEnd);
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
        private void OnErrorClick()
        {
            Toast?.SetPlacement(Placement.BottomEnd);
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
        private void OnInfoClick()
        {
            Toast?.SetPlacement(Placement.BottomEnd);
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
        private void OnNotAutoHideClick()
        {
            Toast?.SetPlacement(Placement.BottomEnd);
            ToastService?.Show(new ToastOption()
            {
                Category = ToastCategory.Information,
                IsAutoHide = false,
                Title = "消息通知",
                Content = "我不会自动关闭哦，请点击右上角关闭按钮"
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="placement"></param>
        private void OnPlacementClick(Placement placement)
        {
            Toast?.SetPlacement(placement);
            ToastService?.Show(new ToastOption()
            {
                Category = ToastCategory.Information,
                Title = "消息通知",
                Content = "<b>Toast</b> 组件更改位置啦，4 秒后自动关闭"
            });
        }

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Category",
                Description = "弹出框类型",
                Type = "ToastCategory",
                ValueList = "Success/Information/Error",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Cotent",
                Description = "Popover 弹窗内容",
                Type = "string",
                ValueList = "—",
                DefaultValue = "Popover"
            },
            new AttributeItem() {
                Name = "Delay",
                Description = "自动隐藏时间间隔",
                Type = "int",
                ValueList = "—",
                DefaultValue = "4000"
            },
            new AttributeItem() {
                Name = "IsAutoHide",
                Description = "是否自动隐藏",
                Type = "boolean",
                ValueList = "",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "IsHtml",
                Description = "内容中是否包含 Html 代码",
                Type = "boolean",
                ValueList = "",
                DefaultValue = "false"
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
                ValueList = "—",
                DefaultValue = "Popover"
            },
        };
    }
}
