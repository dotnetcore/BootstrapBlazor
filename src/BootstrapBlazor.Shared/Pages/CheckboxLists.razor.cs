using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using System.Collections.Generic;
using System.Linq;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CheckboxLists
    {
        private class Foo
        {
            public string Text { get; set; } = "";

            public int Value { get; set; }

            public bool Checked { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        private IEnumerable<Foo> Items { get; set; } = new List<Foo>()
        {
            new Foo { Text = "Item 1", Value = 1, Checked = true },
            new Foo { Text = "Item 2", Value = 2, Checked = false },
            new Foo { Text = "Item 3", Value = 3, Checked = true },
            new Foo { Text = "Item 4", Value = 4, Checked = false },
            new Foo { Text = "Item 5", Value = 5, Checked = true },
            new Foo { Text = "Item 6", Value = 6, Checked = false },
            new Foo { Text = "Item 7", Value = 7, Checked = true },
            new Foo { Text = "Item 8", Value = 8, Checked = false },
            new Foo { Text = "Item 9", Value = 9, Checked = true },
            new Foo { Text = "Item 10", Value = 10, Checked = false },
            new Foo { Text = "Item 11", Value = 11, Checked = true },
            new Foo { Text = "Item 12", Value = 12, Checked = false },
        };

        private Logger? Trace { get; set; }

        private void OnSelectedChanged(Foo foo)
        {
            Trace?.Log($"{foo.Text} - {foo.Checked} Value: {foo.Value} 共 {Items.Where(i => i.Checked).Count()} 项被选中");
        }

        private class Model
        {
            public IEnumerable<Foo> Items { get; set; } = new List<Foo>();
        }

        private Model FooModel { get; set; } = new Model();

        /// <summary>
        /// 
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            FooModel.Items = Items;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes()
        {
            return new AttributeItem[]
            {
                new AttributeItem() {
                    Name = "Items",
                    Description = "数据源",
                    Type = "IEnumerable<TItem>",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem(){
                    Name = "CheckboxClass",
                    Description = "组件布局样式",
                    Type = "string",
                    ValueList = " — ",
                    DefaultValue = "col-12 col-sm-6 col-md-4 col-lg-3 col-xl-2"
                },
                new AttributeItem() {
                    Name = "TextField",
                    Description = "显示列字段名称",
                    Type = "string",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem(){
                    Name = "CheckedField",
                    Description = "是否选中列字段名称",
                    Type = "string",
                    ValueList = " — ",
                    DefaultValue = " — "
                }
            };
        }

        /// <summary>
        /// 获得事件方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<EventItem> GetEvents() => new EventItem[]
        {
            new EventItem()
            {
                Name = "OnSelectedChanged",
                Description="复选框状态改变时回调此方法",
                Type ="EventCallback<TItem>"
            }
        };
    }
}
