using BootstrapBlazor.WebConsole.Pages.Components;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Rates
    {
        private int BindValue { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        private Logger? Trace { get; set; }

        private void OnValueChanged(int val)
        {
            Trace?.Log($"评星: {val}");
        }
    }
}
