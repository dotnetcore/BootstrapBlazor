namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// Renders status badges for the migrated navigation menu.
/// </summary>
public sealed partial class State
{
    /// <summary>
    /// Gets or sets whether the item is new.
    /// </summary>
    [Parameter]
    public bool IsNew { get; set; }

    /// <summary>
    /// Gets or sets whether the item was updated.
    /// </summary>
    [Parameter]
    public bool IsUpdate { get; set; }

    /// <summary>
    /// Gets or sets the item count.
    /// </summary>
    [Parameter]
    public int Count { get; set; }
}
