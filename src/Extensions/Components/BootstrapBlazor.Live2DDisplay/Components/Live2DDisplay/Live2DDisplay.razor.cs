using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

public partial class Live2DDisplay
{
    private bool _isInit;

    private string? _source;
    private double _scale;
    private int _x;
    private int _y;
    private bool _isDraggble;
    private bool _addHitAreaFrames;
    private string? _backgroundColor;
    private bool _backgroundAlpha;

    private LivePosition _position;

    [Parameter] public string? Source { get; set; }
    [Parameter] public double Scale { get; set; }
    [Parameter] public int X { get; set; }
    [Parameter] public int Y { get; set; }
    [Parameter] public bool IsDraggble { get; set; }
    [Parameter] public bool AddHitAreaFrames { get; set; }
    [Parameter] public LivePosition Position { get; set; }
    [Parameter] public string? BackgroundColor { get; set; }
    [Parameter] public bool BackgroundAlpha { get; set; }

    private string? ClassString { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        ClassString = CssBuilder.Default("bb-model-default")
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();
    }

    protected override async Task OnParametersSetAsync()
    {
        if (_isInit)
        {
            if (Source != _source)
            {
                await InvokeVoidAsync("changeSource", new { Id, Source, Scale, X, Y, IsDraggble, AddHitAreaFrames });
                _source = Source;
            }
            if (Scale != _scale)
            {
                await InvokeVoidAsync("changeScale", Id, Scale);
                _scale = Scale;
            }
            if (X != _x || Y != _y)
            {
                await InvokeVoidAsync("changeXY", Id, X, Y);
                _x = X;
                _y = Y;
            }
            if (!(IsDraggble && _isDraggble))
            {
                await InvokeVoidAsync("changeDraggble", Id, IsDraggble);
                _isDraggble = IsDraggble;
            }
            if (!(AddHitAreaFrames && _addHitAreaFrames))
            {
                await InvokeVoidAsync("addHitAreaFrames", Id, AddHitAreaFrames);
                _isDraggble = IsDraggble;
            }
            if (Position != _position)
            {
                ClassString = $"bb-model-{Position}";
                _position = Position;
            }
            if ((BackgroundColor != _backgroundColor) || (BackgroundAlpha != _backgroundAlpha))
            {
                await InvokeVoidAsync("changebackground", Id, BackgroundAlpha, BackgroundColor);
                _backgroundColor = BackgroundColor;
                _backgroundAlpha = BackgroundAlpha;
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task InvokeInitAsync()
    {
        await InvokeVoidAsync("init", new { Id, Source, Scale, X, Y, IsDraggble, AddHitAreaFrames, BackgroundAlpha, BackgroundColor });
        _isInit = true;
    }
}