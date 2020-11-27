// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class TablesWrap
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("$.table_wrap");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<BindItem> GenerateCellItems() => Enumerable.Range(1, 4).Select(i => new BindItem()
        {
            Id = i,
            Name = $"张三 {i:d4}",
            DateTime = DateTime.Now.AddDays(i - 1),
            Address = $"地球、中国、上海市普陀区金沙江路 {random.Next(1000, 2000)} 弄 这里是超长单元格示例",
            Count = random.Next(1, 100),
            Complete = random.Next(1, 100) > 50,
            Education = random.Next(1, 100) > 50 ? EnumEducation.Primary : EnumEducation.Middel
        }).ToList();

        private IEnumerable<BindItem> CellItems => GenerateCellItems();
    }
}
