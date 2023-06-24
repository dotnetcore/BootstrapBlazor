// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// MouseFollowerOptions
/// </summary>
public class MouseFollowerOptions
{
    /// <summary>
    /// Existed cursor element. If not specified, the cursor will be created automatically.
    /// </summary>
    public ElementReference? El { get; set; } = null;

    /// <summary>
    /// Cursor root element class name.
    /// </summary>
    public string ClassName { get; set; } = "mf-cursor";

    /// <summary>
    /// Inner element class name.
    /// </summary>
    public string InnerClassName { get; set; } = "mf-cursor-inner";

    /// <summary>
    /// Text element class name.
    /// </summary>
    public string TextClassName { get; set; } = "mf-cursor-text";

    /// <summary>
    /// Media element class name.
    /// </summary>
    public string MediaClassName { get; set; } = "mf-cursor-media";

    /// <summary>
    /// Media inner element class name.
    /// </summary>
    public string MediaBoxClassName { get; set; } = "mf-cursor-media-box";

    /// <summary>
    /// SVG sprite class name.
    /// </summary>
    public string IconSvgClassName { get; set; } = "mf-svgsprite";

    /// <summary>
    /// SVG sprite class name prefix of icon.
    /// </summary>
    public string IconSvgNamePrefix { get; set; } = "-";

    /// <summary>
    /// SVG sprite source. If you are not using SVG sprites leave this blank.
    /// </summary>
    public string IconSvgSrc { get; set; } = "";

    /// <summary>
    /// Name of data attribute for changing cursor state directly in HTML markdown. Uses an event delegation.
    /// </summary>
    public string? DataAttr { get; set; } = "cursor";

    /// <summary>
    /// Hidden class name state.
    /// </summary>
    public string HiddenState { get; set; } = "-hidden";

    /// <summary>
    /// Text class name state.
    /// </summary>
    public string TextState { get; set; } = "-text";

    /// <summary>
    /// Icon class name state.
    /// </summary>
    public string IconState { get; set; } = "-icon";

    /// <summary>
    /// Active (mousedown) class name state. Set false to disable.
    /// </summary>
    public string? ActiveState { get; set; } = "-active";

    /// <summary>
    /// Media (image/video) class name state.
    /// </summary>
    public string MediaState { get; set; } = "-media";

    /// <summary>
    /// Is cursor visible by default.
    /// </summary>
    public bool Visible { get; set; } = true;

    /// <summary>
    /// Automatically show/hide cursor when state added. Can be useful when implementing a hidden cursor follower.
    /// </summary>
    public bool VisibleOnState { get; set; } = false;

    /// <summary>
    /// Cursor movement speed.
    /// </summary>
    public decimal Speed { get; set; } = 0.55M;

    /// <summary>
    /// Timing function of cursor movement. See gsap easing.
    /// </summary>
    public string Ease { get; set; } = "expo.out";

    /// <summary>
    /// Overwrite or remain cursor position when mousemove event happened. See gsap overwrite modes.
    /// </summary>
    public bool Overwrite { get; set; } = true;

    /// <summary>
    /// Default "skewing" factor.
    /// </summary>
    public decimal Skewing { get; set; } = 0;

    /// <summary>
    /// Skew effect factor in a text state. Set 0 to disable skew in this mode.
    /// </summary>
    public decimal SkewingText { get; set; } = 2;

    /// <summary>
    /// Skew effect factor in a icon state. Set 0 to disable skew in this mode.
    /// </summary>
    public decimal SkewingIcon { get; set; } = 2;

    /// <summary>
    /// Skew effect factor in a media (image/video) state. Set 0 to disable skew in this mode.
    /// </summary>
    public decimal SkewingMedia { get; set; } = 2;

    /// <summary>
    /// Skew effect base delta. Set 0 to disable skew in this mode.
    /// </summary>
    public decimal SkewingDelta { get; set; } = 0.001M;

    /// <summary>
    /// Skew effect max delta. Set 0 to disable skew in this mode.
    /// </summary>
    public decimal SkewingDeltaMax { get; set; } = 0.15M;

    /// <summary>
    /// Stick effect delta.
    /// </summary>
    public decimal StickDelta { get; set; } = 0.15M;

    /// <summary>
    /// Delay before show. May be useful for the spawn animation to work properly.
    /// </summary>
    public decimal ShowTimeout { get; set; } = 20;

    /// <summary>
    /// Hide the cursor when mouse leave container.
    /// </summary>
    public bool HideOnLeave { get; set; } = true;

    /// <summary>
    /// Hiding delay. Should be equal to the CSS hide animation time.
    /// </summary>
    public decimal HideTimeout { get; set; } = 300;

    /// <summary>
    /// Hiding delay. Should be equal to the CSS hide animation time.
    /// </summary>
    public decimal HideMediaTimeout { get; set; } = 300;

}
