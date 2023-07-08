using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// Live2DDisplay Live2D模型组件
/// </summary>
public partial class Live2DDisplay
{
    private bool _isInit;

    private string? _source;
    private double _scale;
    private int _xOffset;
    private int _yOffset;
    private bool _isDraggable;
    private bool _addHitAreaFrames;
    private string? _backgroundColor;
    private bool _backgroundAlpha;

    private LivePosition _position;

    private string? ClassString { get; set; }

    /// <summary>
    /// 获得/设置 ModelSource 模型源
    /// </summary>
    [Parameter]
    public string? Source { get; set; }

    /// <summary>
    /// 获得/设置 Scale 模型比例
    /// </summary>
    [Parameter]
    public double Scale { get; set; }

    /// <summary>
    /// 获得/设置 XOffset 模型X轴偏移
    /// </summary>
    [Parameter]
    public int XOffset { get; set; }

    /// <summary>
    /// 获得/设置 YOffset 模型Y轴偏移
    /// </summary>
    [Parameter]
    public int YOffset { get; set; }

    /// <summary>
    /// 获得/设置 IsDraggable 是否可以拖动模型
    /// </summary>
    //[Parameter]
    private bool IsDraggable { get; set; }

    /// <summary>
    /// 获得/设置 HitAreaFrames 是否渲染鼠标命中区域框
    /// </summary>
    [Parameter]
    public bool AddHitAreaFrames { get; set; }

    /// <summary>
    /// 获得/设置 Position 模型默认显示位置
    /// </summary>
    [Parameter]
    public LivePosition Position { get; set; }

    /// <summary>
    /// 获得/设置 BackgroundColor 模型画板背景颜色
    /// </summary>
    [Parameter]
    public string BackgroundColor { get; set; } = "#000000";

    /// <summary>
    /// 获得/设置 BackgroundAlpha 模型画板是否透明
    /// </summary>
    [Parameter]
    public bool BackgroundAlpha { get; set; }

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        ClassString = CssBuilder.Default("bb-model-default")
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();
    }

    /// <inheritdoc/>
    protected override async Task OnParametersSetAsync()
    {
        if (_isInit)
        {
            if (Source != _source)
            {
                await InvokeVoidAsync("changeSource", new { Id, Source, Scale, X = XOffset, Y = YOffset, IsDraggable, AddHitAreaFrames });
                _source = Source;
            }
            if (Scale != _scale)
            {
                await InvokeVoidAsync("changeScale", Id, Scale);
                _scale = Scale;
            }
            if (XOffset != _xOffset || YOffset != _yOffset)
            {
                await InvokeVoidAsync("changeXY", Id, XOffset, YOffset);
                _xOffset = XOffset;
                _yOffset = YOffset;
            }
            if (!(IsDraggable && _isDraggable))
            {
                await InvokeVoidAsync("changeDraggable", Id, IsDraggable);
                _isDraggable = IsDraggable;
            }
            if (!(AddHitAreaFrames && _addHitAreaFrames))
            {
                await InvokeVoidAsync("addHitAreaFrames", Id, AddHitAreaFrames);
                _isDraggable = IsDraggable;
            }
            if (Position != _position)
            {
                ClassString = $"bb-model-{Position}";
                _position = Position;
            }
            if ((BackgroundColor != _backgroundColor) || (BackgroundAlpha != _backgroundAlpha))
            {
                await InvokeVoidAsync("changeBackground", Id, BackgroundAlpha, BackgroundColor);
                _backgroundColor = BackgroundColor;
                _backgroundAlpha = BackgroundAlpha;
            }
        }
    }

    /// <inheritdoc/>
    protected override async Task InvokeInitAsync()
    {
        await InvokeVoidAsync("init", new { Id, Source, Scale, X = XOffset, Y = YOffset, IsDraggable, AddHitAreaFrames, BackgroundAlpha, BackgroundColor });
        _isInit = true;
        _source = Source;
        _scale = Scale;
        _xOffset = XOffset;
        _yOffset = YOffset;
        _position = Position;
        _backgroundColor = BackgroundColor;
        _backgroundAlpha = BackgroundAlpha;
        _addHitAreaFrames = AddHitAreaFrames;
        _isDraggable = IsDraggable;
    }
}
