using BootstrapBlazor.Components;
using BootstrapBlazor.WebConsole.Pages.Components;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    /// 
    /// </summary>
    partial class Transfers : ComponentBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected IEnumerable<SelectedItem>? Items { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected Logger? Trace { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Items = Enumerable.Range(1, 20).Select(i => new SelectedItem()
            {
                Text = $"备选 {i:d2}",
                Value = i.ToString()
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        protected void OnItemsChanged(IEnumerable<SelectedItem> items)
        {
            Trace?.Log(string.Join(" ", items.Where(i => i.Active).Select(i => i.Text)));
        }
    }
}
