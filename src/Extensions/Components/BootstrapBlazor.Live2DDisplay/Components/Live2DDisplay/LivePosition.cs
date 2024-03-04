using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// LivePosition enum
/// </summary>
public enum LivePosition
{
    /// <summary>
    /// Default
    /// </summary>
    [Description("Default")]
    Default,
    /// <summary>
    /// BottomLeft
    /// </summary>
    [Description("BottomLeft")]
    BottomLeft,
    /// <summary>
    /// BottomRight
    /// </summary>
    [Description("BottomRight")]
    BottomRight,
    /// <summary>
    /// TopLeft
    /// </summary>
    [Description("TopLeft")]
    TopLeft,
    /// <summary>
    /// TopRight
    /// </summary>
    [Description("TopRight")]
    TopRight
}
