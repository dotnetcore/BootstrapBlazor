using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Coms
    {
        private List<string> ComponentItems { get; set; } = new List<string>();

        private string? SearchText { get; set; }

        private Task OnSearch(string searchText)
        {
            SearchText = searchText;

            StateHasChanged();
            return Task.CompletedTask;
        }
    }
}
