using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Dropdowns
    {
        private List<SelectedItem> Items { get; set; } = new List<SelectedItem>
        {
            new SelectedItem{ Text="北京",Value="0"},
            new SelectedItem{ Text="上海",Value="1"},
            new SelectedItem{ Text="广州",Value="2"},
        };

        private List<SelectedItem> EmptyList => new List<SelectedItem> { };

        private List<SelectedItem> BindItems { get; set; } = new List<SelectedItem>
        {
            new SelectedItem{ Text="北京",Value="0"},
            new SelectedItem{ Text="上海",Value="1"},
            new SelectedItem{ Text="广州",Value="2"},
        };

        private List<SelectedItem> RadioItems { get; set; } = new List<SelectedItem>
        {
            new SelectedItem("1", "北京") { Active = true },
            new SelectedItem("2", "上海")
        };

        private List<SelectedItem> RadioDropDownItems { get; set; } = new List<SelectedItem>
        {
            new SelectedItem("1", "北京") { Active = true },
            new SelectedItem("2", "上海"),
            new SelectedItem("3", "广州")
        };

        private Logger? Trace { get; set; }

        private void ShowMessage(SelectedItem e)
        {
            Trace?.Log($"Dropdown Item Clicked: Value={e.Value} Text={e.Text}");
        }

        private void AddItem()
        {
            BindItems.Add(new SelectedItem($"{BindItems.Count()}", $"城市 {BindItems.Count()}"));
        }

        private Task OnRadioItemChanged(CheckboxState state, SelectedItem item)
        {
            RadioDropDownItems.Add(new SelectedItem($"{RadioDropDownItems.Count() + 1}", $"城市 {RadioDropDownItems.Count()}"));
            StateHasChanged();
            return Task.CompletedTask;
        }

        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "@bind-Value",
                Description = "当前选中的值",
                Type = " — ",
                ValueList = " — ",
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
                Name = "Color",
                Description = "颜色",
                Type = "Color",
                ValueList = "Primary / Secondary / Info / Warning / Danger ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Direction",
                Description = "下拉框弹出方向",
                Type = "Direction",
                ValueList = "Dropup / Dropright /  Dropleft",
                DefaultValue = " None "
            },
            new AttributeItem() {
                Name = "Items",
                Description = "下拉框值",
                Type = "list",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "MenuAlignment",
                Description = "菜单对齐方式",
                Type = "Alignment",
                ValueList = "None / Left / Center / Right ",
                DefaultValue = " — "
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
                Name = "ShowSplit",
                Description = "分裂式按钮下拉菜单(使用分裂式组件时需要加上MenuType='MenuType.Btngroup')",
                Type = "bool",
                ValueList = "true / false ",
                DefaultValue = " false "
            },
            new AttributeItem() {
                Name = "Size",
                Description = "尺寸",
                Type = "Size",
                ValueList = "None / ExtraSmall / Small / Medium / Large / ExtraLarge",
                DefaultValue = "None"
            },
            new AttributeItem() {
                Name = "TagName",
                Description = "标签",
                Type = "string",
                ValueList = " a / button ",
                DefaultValue = " — "
            },
        };

        private IEnumerable<EventItem> GetEvents() => new EventItem[]
        {
            new EventItem()
            {
                Name = "OnSelectedItemChanged",
                Description="下拉框值发生改变时触发",
                Type ="EventCallback<SelectedItem>"
            }
       };
    }
}
