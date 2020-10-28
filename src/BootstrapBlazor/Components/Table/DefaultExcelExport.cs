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
