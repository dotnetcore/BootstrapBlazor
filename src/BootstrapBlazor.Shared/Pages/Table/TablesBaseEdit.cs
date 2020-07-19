using BootstrapBlazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class TablesBaseEdit : TablesBaseQuery
    {
        private static readonly object _objectLock = new object();

        /// <summary>
        /// 
        /// </summary>
        protected List<BindItem> EditItems { get; set; } = GenerateItems();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        protected Task<QueryData<BindItem>> OnEditQueryAsync(QueryPageOptions options) => BindItemQueryAsync(EditItems, options);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected Task<BindItem> OnAddAsync()
        {
            return Task.FromResult(new BindItem() { DateTime = DateTime.Now });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected Task<bool> OnSaveAsync(BindItem item)
        {
            // 增加数据演示代码
            if (item.Id == 0)
            {
                lock (_objectLock)
                {
                    item.Id = EditItems.Max(i => i.Id) + 1;
                    EditItems.Add(item);
                }
            }
            else
            {
                var oldItem = EditItems.FirstOrDefault(i => i.Id == item.Id);
                oldItem.Name = item.Name;
                oldItem.Address = item.Address;
                oldItem.DateTime = item.DateTime;
                oldItem.Count = item.Count;
            }
            return Task.FromResult(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        protected Task<bool> OnDeleteAsync(IEnumerable<BindItem> items)
        {
            items.ToList().ForEach(i => EditItems.Remove(i));
            return Task.FromResult(true);
        }
    }
}
