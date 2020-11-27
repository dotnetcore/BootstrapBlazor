// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

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
        protected IEnumerable<SelectedItem>? Educations { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Educations = typeof(EnumEducation).ToSelectList();
        }

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
        protected static Task<BindItem> OnAddAsync()
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
                if (oldItem != null)
                {
                    oldItem.Name = item.Name;
                    oldItem.Address = item.Address;
                    oldItem.DateTime = item.DateTime;
                    oldItem.Count = item.Count;
                    oldItem.Complete = item.Complete;
                    oldItem.Education = item.Education;
                }
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
