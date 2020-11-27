// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    internal class DefaultExcelExport : ITableExcelExport
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<bool> ExportAsync<TItem>(IEnumerable<TItem> items, IEnumerable<ITableColumn> cols, IJSRuntime jsRuntime) where TItem : class
        {
            return Task.FromResult(false);
        }
    }
}
