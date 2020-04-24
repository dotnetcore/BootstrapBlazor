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
        protected Logger? DateLogger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected Logger? TimeLogger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected Logger? DateTimeLogger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected DateTime BindValue { get; set; } = DateTime.Today;

        /// <summary>
        /// 
        /// </summary>
        protected string BindValueString
        {
            get
            {
                return BindValue.ToString("yyyy-MM-dd");
            }
            set
            {
                if (DateTime.TryParse(value, out var d))
                {
                    BindValue = d;
                }
                else
                {
                    BindValue = DateTime.Today;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        protected void DateValueChanged(DateTime d)
        {
            DateLogger?.Log($"选择的日期为: {d:yyyy-MM-dd}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        protected void TimeValueChanged(TimeSpan d)
        {
            TimeLogger?.Log($"选择的时间为: {d:hh\\:mm\\:ss}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        protected void DateTimeValueChanged(DateTime d)
        {
            DateTimeLogger?.Log($"选择的时间为: {d:yyyy-MM-dd}");
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
