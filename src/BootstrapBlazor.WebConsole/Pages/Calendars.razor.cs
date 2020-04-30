using BootstrapBlazor.WebConsole.Pages.Components;
using System;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    /// 
    /// </summary>
    sealed partial class Calendars
    {
        /// <summary>
        /// 
        /// </summary>
        private Logger? Trace { get; set; }

        private DateTime BindValue { get; set; } = DateTime.Today;

        private void OnValueChanged(DateTime ts)
        {
            Trace?.Log($"{ts:yyyy-MM-dd}");
        }

        private string Formatter(DateTime ts) => ts.ToString("yyyy-MM-dd");
    }
}
