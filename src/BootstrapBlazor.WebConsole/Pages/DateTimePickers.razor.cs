using BootstrapBlazor.Components;
using BootstrapBlazor.WebConsole.Pages.Components;
using System;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    /// 
    /// </summary>
    partial class DateTimePickers
    {
        /// <summary>
        /// 
        /// </summary>
        protected TimeSpan SpanValue { get; set; } = DateTime.Now.Subtract(DateTime.Today);

        /// <summary>
        /// 
        /// </summary>
        protected string SpanValue2 { get; set; } = DateTime.Now.ToString("HH:mm:ss");

        /// <summary>
        /// 
        /// </summary>
        protected Logger? Date { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected Logger? Time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        protected void DateValueChanged(DateTime d)
        {
            Date?.Log($"选择的日期为: {d:yyyy-MM-dd}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        protected void TimeValueChanged(TimeSpan d)
        {
            Time?.Log($"选择的时间为: {d:hh\\:mm\\:ss}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ts"></param>
        protected void OnValueChange(TimeSpan ts)
        {
            SpanValue2 = ts.ToString("hh\\:mm\\:ss");
            StateHasChanged();
        }
    }
}
