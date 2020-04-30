using System.Diagnostics;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    /// 
    /// </summary>
    sealed partial class Install
    {
        private string? Version => FileVersionInfo.GetVersionInfo(typeof(BootstrapBlazor.Components.Alert).Assembly.Location).ProductVersion;
    }
}
