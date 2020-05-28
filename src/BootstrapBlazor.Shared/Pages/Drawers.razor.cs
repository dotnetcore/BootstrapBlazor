using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    sealed partial class Drawers
    {
        private IEnumerable<SelectedItem> DrawerDirection { get; } = new SelectedItem[] {
            new SelectedItem("left", "从左向右") { Active = true },
            new SelectedItem("right", "从右向左"),
            new SelectedItem("top", "从上到下"),
            new SelectedItem("bottom", "从下向上")
        };

        private Placement DrawerAlign { get; set; }

        private Task OnStateChanged(CheckboxState state, SelectedItem val)
        {
            DrawerAlign = val.Value switch
            {
                "right" => Placement.Right,
                "top" => Placement.Top,
                "bottom" => Placement.Bottom,
                _ => Placement.Left
            };
            IsOpen = false;
            StateHasChanged();
            return Task.CompletedTask;
        }

        private bool IsOpen { get; set; }

        private bool IsBackdropOpen { get; set; }

        private void OpenDrawer()
        {
            IsBackdropOpen = true;
        }

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Width",
                Description = "抽屉宽度",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "360px"
            },
            new AttributeItem() {
                Name = "Height",
                Description = "抽屉高度",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "290px"
            },
            new AttributeItem() {
                Name = "IsOpen",
                Description = "抽屉是否打开",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsBackdrop",
                Description = "点击遮罩是否关闭抽屉",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "OnClickBackdrop",
                Description = "点击背景遮罩时回调委托方法",
                Type = "Action",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Placement",
                Description = "组件出现位置",
                Type = "Placement",
                ValueList = "Left|Right|Top|Bottom",
                DefaultValue = "Left"
            },
            new AttributeItem() {
                Name = "ChildContent",
                Description = "子组件",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
        };
    }
}
