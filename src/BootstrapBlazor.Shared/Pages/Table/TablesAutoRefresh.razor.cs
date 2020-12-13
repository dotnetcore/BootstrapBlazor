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

namespace BootstrapBlazor.Shared.Pages.Table
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TablesAutoRefresh
    {
        private List<BindItem> AutoItems { get; set; } = new List<BindItem>();

        /// <summary>
        /// 
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            AutoItems = Items.Take(2).ToList();
        }

        private Task<QueryData<BindItem>> OnRefreshQueryAsync(QueryPageOptions options)
        {
            // 设置记录总数
            var total = AutoItems.Count();

            AutoItems.Insert(0, new BindItem()
            {
                Id = total++,
                Name = $"张三 {total:d4}",
                DateTime = DateTime.Now.AddDays(total - 1),
                Address = $"上海市普陀区金沙江路 {random.Next(1000, 2000)} 弄",
                Count = random.Next(1, 100),
                Complete = random.Next(1, 100) > 50,
                Education = random.Next(1, 100) > 50 ? EnumEducation.Primary : EnumEducation.Middel
            });

            if (AutoItems.Count > 10)
            {
                AutoItems.RemoveRange(10, 1);
                total = 10;
            }

            // 内存分页
            var items = AutoItems.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();

            return Task.FromResult(new QueryData<BindItem>()
            {
                Items = items,
                TotalCount = total
            });
        }
    }
}
