using BootstrapBlazor.Components;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Breadcrumbs
    {
        [NotNull]
        private IEnumerable<BreadcrumbItem>? DataSource { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            DataSource = new List<BreadcrumbItem>
            {
                new BreadcrumbItem("Home", "#"),
                new BreadcrumbItem("Library", "#"),
                new BreadcrumbItem("Data")
            };
        }
    }
}
