namespace BootstrapBlazor.Shared.Samples;

public partial class Breadcrumbs
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
