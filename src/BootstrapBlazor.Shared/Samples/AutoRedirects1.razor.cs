namespace BootstrapBlazor.Shared.Samples;

public partial class AutoRedirects
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private Task<bool> OnBeforeRedirectAsync()
    {
        Logger.Log("Ready to redirect");
        return Task.FromResult(true);
    }
}
