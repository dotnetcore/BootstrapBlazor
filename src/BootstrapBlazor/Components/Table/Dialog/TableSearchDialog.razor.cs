using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TableSearchDialog<TModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Parameter]
        public Func<Task>? OnResetSearchClick { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Parameter]
        public Func<Task>? OnSearchClick { get; set; }
    }
}
