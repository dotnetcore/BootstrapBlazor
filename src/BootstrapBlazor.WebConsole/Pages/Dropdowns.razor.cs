using BootstrapBlazor.Components;
using BootstrapBlazor.WebConsole.Common;
using BootstrapBlazor.WebConsole.Pages.Components;
using System.Collections.Generic;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Dropdowns
    {
        readonly List<SelectedItem> items = new List<SelectedItem>
        {
            new SelectedItem{ Text="北京",Value="0"},
            new SelectedItem{ Text="上海",Value="1"},
            new SelectedItem{ Text="广州",Value="2"},
        };

        /// <summary>
        /// 
        /// </summary>
        protected Logger? Trace { get; set; }

        private void ShowMessage(SelectedItem e)
        {
            Trace?.Log($"Dropdown Item Clicked: Value={e.Value} Text={e.Text}");
        }

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
                ValueList = "Primary / Secondary / Info / Warning / Danger ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Class",
                Description = "样式",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "TagName",
                Description = "标签",
                Type = "string",
                ValueList = " a / button ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Items",
                Description = "下拉框值",
                Type = "list",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "@bind-Value",
                Description = "当前选中的值",
                Type = " — ",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ShowSplit",
                Description = "分裂式按钮下拉菜单(使用分裂式组件时需要加上MenuType='MenuType.Btngroup')",
                Type = "bool",
                ValueList = "true / false ",
                DefaultValue = " false "
            },
            new AttributeItem() {
                Name = "Size",
                Description = "尺寸大小定义",
                Type = "Size",
                ValueList = "true / false ",
                DefaultValue = " false "
            },
            new AttributeItem() {
                Name = "Direction",
                Description = "下拉框弹出方向",
                Type = "Direction",
                ValueList = "Dropup / Dropright /  Dropleft",
                DefaultValue = " None "
            },
            new AttributeItem() {
                Name = "MenuItem",
                Description = "菜单项渲染标签",
                Type = "string",
                ValueList = "button / a ",
                DefaultValue = " a "
            },
            new AttributeItem() {
                Name = "Responsive",
                Description = "菜单对齐",
                Type = "string",
                ValueList = "dropdown-menu-right / dropdown-menu-right / dropdown-menu-{lg | md | sm }-{right | left}",
                DefaultValue = " - "
            },
            new AttributeItem() {
                Name = "OnValueChanged",
                Description = "下拉框值发生改变时触发",
                Type = " - ",
                ValueList = " - ",
                DefaultValue = " - "
            },
        };
    }
}

