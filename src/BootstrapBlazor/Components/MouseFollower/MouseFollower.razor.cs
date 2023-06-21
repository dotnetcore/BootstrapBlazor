namespace BootstrapBlazor.Components;

/// <summary>
/// MouseFollower
/// 鼠标跟随器组件
/// </summary>
[BootstrapModuleAutoLoader("MouseFollower/MouseFollower.razor.js", JSObjectReference = true, AutoInvokeInit = false)]
public partial class MouseFollower
{
    /// <summary>
    /// 获得/设置 RenderFragment 实例
    /// </summary>
    [Parameter]
    [NotNull]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 EChart DOM 元素实例
    /// Existed cursor element. If not specified, the cursor will be created automatically.
    /// </summary>
    private ElementReference Element { get; set; }


    /// <summary>
    /// Cursor root element class name.
    /// </summary>
    [Parameter]
    [NotNull]
    public string ClassName { get; set; }

    /// <summary>
    /// Inner element class name.
    /// </summary>
    [Parameter]
    [NotNull]
    public string InnerClassName { get; set; }

    /// <summary>
    /// Text element class name.
    /// </summary>
    [Parameter]
    [NotNull]
    public string TextClassName { get; set; }

    /// <summary>
    /// Media element class name.
    /// </summary>
    [Parameter]
    [NotNull]
    public string MediaClassName { get; set; }

    /// <summary>
    /// Media inner element class name.
    /// </summary>
    [Parameter]
    [NotNull]
    public string MediaBoxClassName { get; set; }

    /// <summary>
    /// SVG sprite class name.
    /// </summary>
    [Parameter]
    [NotNull]
    public string IconSvgClassName { get; set; }

    /// <summary>
    /// SVG sprite class name prefix of icon.
    /// </summary>
    [Parameter]
    [NotNull]
    public string IconSvgNamePrefix { get; set; }

    /// <summary>
    /// SVG sprite source. If you are not using SVG sprites leave this blank.
    /// </summary>
    [Parameter]
    [NotNull]
    public string IconSvgSrc { get; set; }

    /// <summary>
    /// Name of data attribute for changing cursor state directly in HTML markdown. Uses an event delegation.
    /// </summary>
    [Parameter]
    public string? DataAttr { get; set; }

    /// <summary>
    /// Hidden class name state.
    /// </summary>
    [Parameter]
    [NotNull]
    public string HiddenState { get; set; }

    /// <summary>
    /// Text class name state.
    /// </summary>
    [Parameter]
    [NotNull]
    public string TextState { get; set; }

    /// <summary>
    /// Icon class name state.
    /// </summary>
    [Parameter]
    [NotNull]
    public string IconState { get; set; }

    /// <summary>
    /// Active (mousedown) class name state. Set false to disable.
    /// </summary>
    [Parameter]
    public string? ActiveState { get; set; }

    /// <summary>
    /// Media (image/video) class name state.
    /// </summary>
    [Parameter]
    [NotNull]
    public string MediaState { get; set; }

    /// <summary>
    /// Is cursor visible by default.
    /// </summary>
    [Parameter]
    [NotNull]
    public bool Visible { get; set; }

    /// <summary>
    /// Automatically show/hide cursor when state added. Can be useful when implementing a hidden cursor follower.
    /// </summary>
    [Parameter]
    [NotNull]
    public bool VisibleOnState { get; set; }

    /// <summary>
    /// Allow to set predefined states for different elements on page. Uses an event delegation.
    /// </summary>
    [Parameter]
    public object? StateDetection { get; set; }

    /// <summary>
    /// Cursor movement speed.
    /// </summary>
    [Parameter]
    [NotNull]
    public decimal Speed { get; set; }

    /// <summary>
    /// Timing function of cursor movement. See gsap easing.
    /// </summary>
    [Parameter]
    [NotNull]
    public string Ease { get; set; }

    /// <summary>
    /// Overwrite or remain cursor position when mousemove event happened. See gsap overwrite modes.
    /// </summary>
    [Parameter]
    [NotNull]
    public bool Overwrite { get; set; }

    /// <summary>
    /// Default "skewing" factor.
    /// </summary>
    [Parameter]
    [NotNull]
    public decimal Skewing { get; set; }

    /// <summary>
    /// Skew effect factor in a text state. Set 0 to disable skew in this mode.
    /// </summary>
    [Parameter]
    [NotNull]
    public decimal SkewingText { get; set; }

    /// <summary>
    /// Skew effect factor in a icon state. Set 0 to disable skew in this mode.
    /// </summary>
    [Parameter]
    [NotNull]
    public decimal SkewingIcon { get; set; }

    /// <summary>
    /// Skew effect factor in a media (image/video) state. Set 0 to disable skew in this mode.
    /// </summary>
    [Parameter]
    [NotNull]
    public decimal SkewingMedia { get; set; }

    /// <summary>
    /// Skew effect base delta. Set 0 to disable skew in this mode.
    /// </summary>
    [Parameter]
    [NotNull]
    public decimal SkewingDelta { get; set; }

    /// <summary>
    /// Skew effect max delta. Set 0 to disable skew in this mode.
    /// </summary>
    [Parameter]
    [NotNull]
    public decimal SkewingDeltaMax { get; set; }

    /// <summary>
    /// Stick effect delta.
    /// </summary>
    [Parameter]
    [NotNull]
    public decimal StickDelta { get; set; }

    /// <summary>
    /// Delay before show. May be useful for the spawn animation to work properly.
    /// </summary>
    [Parameter]
    [NotNull]
    public decimal ShowTimeout { get; set; }

    /// <summary>
    /// Hide the cursor when mouse leave container.
    /// </summary>
    [Parameter]
    [NotNull]
    public bool HideOnLeave { get; set; }

    /// <summary>
    /// Hiding delay. Should be equal to the CSS hide animation time.
    /// </summary>
    [Parameter]
    [NotNull]
    public decimal HideTimeout { get; set; }

    /// <summary>
    /// Array (x, y) of initial cursor position.
    /// </summary>
    [Parameter]
    [NotNull]
    public Array InitialPos { get; set; }


    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await InvokeVoidAsync("init", Element);
        }
    }
}
