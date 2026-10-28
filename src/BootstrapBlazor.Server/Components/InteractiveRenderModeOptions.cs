using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Server.Components;

/// <summary>
/// Selects the interactive rendering strategy for the application.
/// </summary>
public enum InteractiveRenderMode
{
    /// <summary>Render all pages interactively on the server.</summary>
    Server,

    /// <summary>Allow shared pages to start in WebAssembly and server-only pages to remain on the server.</summary>
    Auto
}

/// <summary>
/// Configures the interactive rendering strategy for the application.
/// </summary>
public sealed class InteractiveRenderModeOptions
{
    /// <summary>
    /// The configuration section name.
    /// </summary>
    public const string SectionName = "InteractiveRenderMode";

    /// <summary>
    /// Gets or sets the application interactive render mode.
    /// </summary>
    public InteractiveRenderMode Mode { get; set; } = InteractiveRenderMode.Auto;

    /// <summary>
    /// Gets the effective render mode for a page.
    /// </summary>
    /// <param name="isSharedPage">Whether the page is part of the shared client assembly.</param>
    /// <returns>The selected interactive render mode.</returns>
    public IComponentRenderMode GetRenderMode(bool isSharedPage = true) => Mode switch
    {
        InteractiveRenderMode.Auto when isSharedPage => new InteractiveAutoRenderMode(),
        _ => new InteractiveServerRenderMode()
    };
}
