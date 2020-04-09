using System.Collections.Generic;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Carousels
    {
        private IEnumerable<string> Images => new List<string>()
        {
            "images/Pic0.jpg",
            "images/Pic1.jpg",
            "images/Pic2.jpg"
        };
    }
}