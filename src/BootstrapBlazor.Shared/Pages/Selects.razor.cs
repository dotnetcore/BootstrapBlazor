using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using System;
using System.Collections.Generic;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 下拉框操作类
    /// </summary>
    public sealed partial class Selects
    {
        /// <summary>
        /// 
        /// </summary>
        private Foo Model { get; set; } = new Foo();

        /// <summary>
        /// 
        /// </summary>
        private Foo BindModel { get; set; } = new Foo() { Name = "" };

        /// <summary>
        /// 获得/设置 Logger 实例
        /// </summary>
        private Logger? Trace { get; set; }

        /// <summary>
        /// 获得 默认数据集合
        /// </summary>
        private readonly IEnumerable<SelectedItem> Items = new SelectedItem[]
        {
            new SelectedItem ("Beijing", "北京"),
            new SelectedItem ("Shanghai", "上海") { Active = true },
        };

        /// <summary>
        /// 获得 默认数据集合
        /// </summary>
        private readonly IEnumerable<SelectedItem> Items3 = new SelectedItem[]
        {
            new SelectedItem ("", "请选择 ..."),
            new SelectedItem ("Beijing", "北京"),
            new SelectedItem ("Shanghai", "上海"),
            new SelectedItem ("Hangzhou", "杭州")
        };

        private readonly IEnumerable<SelectedItem> GroupItems = new SelectedItem[]
        {
            new SelectedItem ("Jilin", "吉林") { GroupName = "东北"},
            new SelectedItem ("Liaoning", "辽宁") {GroupName = "东北", Active = true },
            new SelectedItem ("Beijing", "北京") { GroupName = "华中"},
            new SelectedItem ("Shijiazhuang", "石家庄") { GroupName = "华中"},
            new SelectedItem ("Shanghai", "上海") {GroupName = "华东", Active = true },
            new SelectedItem ("Ningbo", "宁波") {GroupName = "华东", Active = true }
        };

        private Guid CurrentGuid { get; set; }

        private readonly IEnumerable<SelectedItem> GuidItems = new SelectedItem[]
        {
            new SelectedItem(Guid.NewGuid().ToString(), "Guid1"),
            new SelectedItem(Guid.NewGuid().ToString(), "Guid2")
        };

        /// <summary>
        /// 下拉选项改变时调用此方法
        /// </summary>
        /// <param name="item"></param>
        private void OnItemChanged(SelectedItem item)
        {
            Trace?.Log($"SelectedItem Text: {item.Text} Value: {item.Value} Selected");
        }

        /// <summary>
        /// 级联绑定菜单
        /// </summary>
        /// <param name="item"></param>
        private void OnCascadeBindSelectClick(SelectedItem item)
        {
            _item2.Clear();
            if (item.Value == "Beijing")
            {
                _item2.AddRange(new SelectedItem[]
                {
                    new SelectedItem("1","朝阳区"),
                    new SelectedItem("2","海淀区"),
                });
            }
            else if (item.Value == "Shanghai")
            {
                _item2.AddRange(new SelectedItem[]
                {
                    new SelectedItem("1","静安区"),
                    new SelectedItem("2","黄浦区"),
                });
            }
        }

        private readonly List<SelectedItem> _item2 = new List<SelectedItem>();

        private IEnumerable<SelectedItem> Items2 => _item2;

        /// <summary>
        /// 获得事件方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<EventItem> GetEvents() => new EventItem[]
        {
            new EventItem()
            {
                Name = "OnSelectedItemChanged",
                Description="下拉框选项改变时触发此事件",
                Type ="EventCallback<SelectedItem>"
            }
        };

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "ShowLabel",
                Description = "是否显示前置标签",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "DisplayText",
                Description = "前置标签显示文本",
                Type = "string",
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
                ValueList = "Primary / Secondary / Success / Danger / Warning / Info / Dark",
                DefaultValue = "Primary"
            },
            new AttributeItem() {
                Name = "IsDisabled",
                Description = "是否禁用",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Items",
                Description = "数据集合",
                Type = "IEnumerable<SelectedItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "SelectItems",
                Description = "静态数据模板",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ChildContent",
                Description = "数据模板",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };
    }
}
