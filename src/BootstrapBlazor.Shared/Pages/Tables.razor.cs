using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Tables
    {
        private List<BindItem>? Items { get; set; }

        private IEnumerable<int> PageItemsSource => new int[] { 2, 4, 10, 20 };

        private BindItem SearchModel { get; set; } = new BindItem();

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Items = GenerateItems();
        }

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Height",
                Description = "固定表头",
                Type = "int",
                ValueList = "—",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "HeaderTemplate",
                Description = "TableHeader 实例",
                Type = "RenderFragment<TItem>",
                ValueList = "—",
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
                ValueList = "—",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "PageItems",
                Description = "IsPagination=true 设置每页显示数据数量",
                Type = "int",
                ValueList = " — ",
                DefaultValue = "—"
            },
            new AttributeItem() {
                Name = "PageItemsSource",
                Description = "IsPagination=true 设置每页显示数据数量的外部数据源",
                Type = "IEnumerable<int>",
                ValueList = " — ",
                DefaultValue = "—"
            },
            new AttributeItem() {
                Name = "RowTemplate",
                Description = "RowTemplate 实例",
                Type = "RenderFragment<TItem>",
                ValueList = "—",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ShowCheckbox",
                Description = "选择列",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = " — "
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
                Name = "TableFooter",
                Description = "TableFooter 实例",
                Type = "RenderFragment<TItem>",
                ValueList = "—",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnQueryAsync",
                Description = "异步查询回调方法",
                Type = "Func<QueryPageOptions, Task<QueryData<TItem>>>",
                ValueList = "—",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnAddAsync",
                Description = "新建按钮回调方法",
                Type = "Func<Task<TItem>>",
                ValueList = "—",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnSaveAsync",
                Description = "保存按钮异步回调方法",
                Type = "Func<TItem, Task>",
                ValueList = "—",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnDeleteAsync",
                Description = "删除按钮异步回调方法",
                Type = "Func<IEnumerable<TItem>, Task<bool>>",
                ValueList = "—",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnResetSearchAsync",
                Description = "重置搜索按钮异步回调方法",
                Type = "Func<TItem, Task>",
                ValueList = "—",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnSortAsync",
                Description = "排序方法",
                Type = "Func<string, SortOrder, Task>",
                ValueList = "—",
                DefaultValue = " — "
            }
        };

        private IEnumerable<MethodItem> GetMethods() => new MethodItem[]
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

        private List<BindItem> GenerateItems()
        {
            var random = new Random();
            return new List<BindItem>(Enumerable.Range(1, 80).Select(i => new BindItem()
            {
                Id = i,
                Name = $"张三 {i:d4}",
                DateTime = DateTime.Now.AddDays(i - 1),
                Address = $"上海市普陀区金沙江路 {random.Next(1000, 2000)} 弄",
                Count = random.Next(1, 100),
                Complete = random.Next(1, 100) > 50
            }));
        }

        private Task<QueryData<BindItem>> OnQueryAsync(QueryPageOptions options)
        {
            var items = Items;
            if (!string.IsNullOrEmpty(SearchModel.Name)) items = items.Where(item => item.Name?.Contains(SearchModel.Name, StringComparison.OrdinalIgnoreCase) ?? false).ToList();
            if (!string.IsNullOrEmpty(SearchModel.Address)) items = items.Where(item => item.Address?.Contains(SearchModel.Address, StringComparison.OrdinalIgnoreCase) ?? false).ToList();
            if (!string.IsNullOrEmpty(options.SearchText)) items = items.Where(item => (item.Name?.Contains(options.SearchText) ?? false)
                 || (item.Address?.Contains(options.SearchText) ?? false)).ToList();

            // 设置记录总数
            var total = items.Count();

            // 排序
            var sortName = options.SortName;
            if (string.IsNullOrEmpty(sortName)) sortName = nameof(BindItem.Name);
            items = sortName switch
            {
                nameof(BindItem.Address) => options.SortOrder == SortOrder.Asc ? items.OrderBy(i => i.Address).ToList() : items.OrderByDescending(i => i.Address).ToList(),
                nameof(BindItem.DateTime) => options.SortOrder == SortOrder.Asc ? items.OrderBy(i => i.DateTime).ToList() : items.OrderByDescending(i => i.DateTime).ToList(),
                _ => options.SortOrder == SortOrder.Asc ? items.OrderBy(i => i.Name).ToList() : items.OrderByDescending(i => i.Name).ToList()
            };

            // 内存分页
            items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();

            return Task.FromResult(new QueryData<BindItem>()
            {
                Items = items,
                TotalCount = total,
                IsFiltered = !string.IsNullOrEmpty(SearchModel.Name) || !string.IsNullOrEmpty(SearchModel.Address)
            });
        }

        private Task OnResetSearchAsync(BindItem item)
        {
            item.Name = "";
            item.Address = "";
            return Task.CompletedTask;
        }

        private Task<BindItem> OnAddAsync()
        {
            return Task.FromResult(new BindItem() { DateTime = DateTime.Now });
        }

        private static readonly object _objectLock = new object();

        private Task<bool> OnSaveAsync(BindItem item)
        {
            // 增加数据演示代码
            if (Items != null)
            {
                if (item.Id == 0)
                {
                    lock (_objectLock)
                    {
                        item.Id = Items.Max(i => i.Id) + 1;
                        Items.Add(item);
                    }
                }
                else
                {
                    var oldItem = Items.FirstOrDefault(i => i.Id == item.Id);
                    oldItem.Name = item.Name;
                    oldItem.Address = item.Address;
                }
            }
            return Task.FromResult(true);
        }

        private Task<bool> OnDeleteAsync(IEnumerable<BindItem> items)
        {
            if (Items != null) items.ToList().ForEach(i => Items.Remove(i));
            return Task.FromResult(true);
        }
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
        public string? Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("日期")]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("地址")]
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
