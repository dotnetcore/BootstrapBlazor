using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class TablesBase : ComponentBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Inject]
        protected ToastService? ToastService { get; set; }

        private static readonly Random random = new Random();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected static List<BindItem> GenerateItems() => new List<BindItem>(Enumerable.Range(1, 80).Select(i => new BindItem()
        {
            Id = i,
            Name = $"张三 {i:d4}",
            DateTime = DateTime.Now.AddDays(i - 1),
            Address = $"上海市普陀区金沙江路 {random.Next(1000, 2000)} 弄",
            Count = random.Next(1, 100),
            Complete = random.Next(1, 100) > 50
        }));

        /// <summary>
        /// 
        /// </summary>
        protected static IEnumerable<BindItem> Items { get; } = GenerateItems();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<AttributeItem> GetTableColumnAttributes() => new AttributeItem[]
        {
            new AttributeItem() {
                Name = "Sortable",
                Description = "是否排序",
                Type = "string",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Filterable",
                Description = "是否可过滤数据",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Editable",
                Description = "是否生成编辑组件",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "Text",
                Description = "表头显示文字",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Width",
                Description = "列宽度（像素px）",
                Type = "int",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "FormatString",
                Description = "数值格式化字符串",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Formatter",
                Description = "TableHeader 实例",
                Type = "RenderFragment<TItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Template",
                Description = "模板",
                Type = "RenderFragment<TableColumnContext<object, TItem>>",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Height",
                Description = "固定表头",
                Type = "int",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "HeaderTemplate",
                Description = "TableHeader 实例",
                Type = "RenderFragment<TItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "TableFooter",
                Description = "Table Footer 模板",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "TableToolbarTemplate",
                Description = "自定义按钮模板",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "EditTemplate",
                Description = "编辑弹窗模板",
                Type = "RenderFragment<TItem?>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "SearchTemplate",
                Description = "高级搜索模板",
                Type = "RenderFragment<TItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "RowButtonTemplate",
                Description = "Table 行按钮模板",
                Type = "RenderFragment<TItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "IsBordered",
                Description = "边框",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "IsPagination",
                Description = "显示分页",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "IsStriped",
                Description = "斑马纹",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Items",
                Description = "数据集合",
                Type = "IEnumerable<TItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "PageItems",
                Description = "IsPagination=true 设置每页显示数据数量",
                Type = "int",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "PageItemsSource",
                Description = "IsPagination=true 设置每页显示数据数量的外部数据源",
                Type = "IEnumerable<int>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "IsMultipleSelect",
                Description = "是否为多选模式，为 true 时第一列自动为复选框列",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ClickToSelect",
                Description = "单选模式下点击行即选中本行",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "ShowCheckboxText",
                Description = "显示文字的选择列",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ShowFooter",
                Description = "是否显示表脚",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = " false "
            },
            new AttributeItem() {
                Name = "ShowSearch",
                Description = "显示搜索栏",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ShowToolbar",
                Description = "显示 Toolbar",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ShowDefaultButtons",
                Description = "显示默认按钮 增加编辑删除",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "ShowNewButton",
                Description = "显示增加按钮",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "ShowEditButton",
                Description = "显示编辑按钮",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "ShowDeleteButton",
                Description = "显示删除按钮",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "ShowExtendButtons",
                Description = "显示行操作按钮",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ExtendButtonColumnWidth",
                Description = "行操作按钮列宽度",
                Type = "int",
                ValueList = " — ",
                DefaultValue = "130"
            },
            new AttributeItem() {
                Name = "OnQueryAsync",
                Description = "异步查询回调方法",
                Type = "Func<QueryPageOptions, Task<QueryData<TItem>>>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnAddAsync",
                Description = "新建按钮回调方法",
                Type = "Func<Task<TItem>>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnSaveAsync",
                Description = "保存按钮异步回调方法",
                Type = "Func<TItem, Task>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnDeleteAsync",
                Description = "删除按钮异步回调方法",
                Type = "Func<IEnumerable<TItem>, Task<bool>>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnResetSearchAsync",
                Description = "重置搜索按钮异步回调方法",
                Type = "Func<TItem, Task>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnSortAsync",
                Description = "排序方法",
                Type = "Func<string, SortOrder, Task>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnDoubleClickRowCallback",
                Description = "双击行回调委托方法",
                Type = "Func<TItem, Task>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "SortIcon",
                Description = "排序默认图标",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "fa fa-sort"
            },
            new AttributeItem() {
                Name = "SortIconAsc",
                Description = "排序升序图标",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "fa fa-sort-asc"
            },
            new AttributeItem() {
                Name = "SortIconDesc",
                Description = "排序降序图标",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "fa fa-sort-desc"
            }
        };

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<MethodItem> GetMethods() => new MethodItem[]
        {
            new MethodItem()
            {
                Name = "AddAsync",
                Description = "手工添加数据方法",
                Parameters = " - ",
                ReturnValue = "Task"
            },
            new MethodItem()
            {
                Name = "Edit",
                Description = "手工编辑数据方法",
                Parameters = " - ",
                ReturnValue = " - "
            },
            new MethodItem()
            {
                Name = "QueryAsync",
                Description = "手工查询数据方法",
                Parameters = " - ",
                ReturnValue = "Task"
            },
        };
    }

    /// <summary>
    /// 
    /// </summary>
    public class BindItem
    {
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("主键")]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("姓名")]
        [Required(ErrorMessage = "姓名不能为空")]
        public string? Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("日期")]
        public DateTime? DateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("地址")]
        [Required(ErrorMessage = "地址不能为空")]
        public string? Address { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("数量")]
        public int Count { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("是/否")]
        public bool Complete { get; set; }
    }
}
