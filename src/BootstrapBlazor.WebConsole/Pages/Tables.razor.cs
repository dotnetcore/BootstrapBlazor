using BootstrapBlazor.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Tables
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

        private List<BindItem> GenerateItems()
        {
            var random = new Random();
            return new List<BindItem>(Enumerable.Range(1, 80).Select(i => new BindItem()
            {
                Id = i.ToString(),
                Name = $"张三 {i:d4}",
                DateTime = DateTime.Now.AddDays(i - 1),
                Address = $"上海市普陀区金沙江路 {random.Next(1000, 2000)} 弄",
                Count = random.Next(1, 100),
                Complete = random.Next(1, 100) > 50
            }));
        }

        private QueryData<BindItem> OnQuery(QueryPageOptions options)
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

            return new QueryData<BindItem>()
            {
                Items = items,
                TotalCount = total,
                IsFiltered = !string.IsNullOrEmpty(SearchModel.Name) || !string.IsNullOrEmpty(SearchModel.Address)
            };
        }

        private void OnResetSearch(BindItem item)
        {
            item.Name = "";
            item.Address = "";
        }

        private BindItem OnAdd()
        {
            return new BindItem() { DateTime = DateTime.Now };
        }

        private bool OnSave(BindItem item)
        {
            // 增加数据演示代码
            if (Items != null)
            {
                if (string.IsNullOrEmpty(item.Id)) Items.Add(item);
                else
                {
                    var oldItem = Items.FirstOrDefault(i => i.Id == item.Id);
                    oldItem.Name = item.Name;
                    oldItem.Address = item.Address;
                }
            }
            return true;
        }

        private bool OnDelete(IEnumerable<BindItem> items)
        {
            if (Items != null) items.ToList().ForEach(i => Items.Remove(i));
            return true;
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
        public string? Id { get; set; }

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
