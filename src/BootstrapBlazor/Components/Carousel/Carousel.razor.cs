// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Carousel 组件</para>
/// <para lang="en">Carousel component</para>
/// </summary>
public partial class Carousel
{
    /// <summary>
    /// <para lang="zh">获得 class 样式集合</para>
    /// <para lang="en">Get the class style collection</para>
    /// </summary>
    private string? ClassName => CssBuilder.Default("carousel slide")
        .AddClass("carousel-fade", IsFade)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 data-target 属性值</para>
    /// <para lang="en">Get data-target attribute value</para>
    /// </summary>
    /// <value></value>
    private string? TargetId => $"#{Id}";

    /// <summary>
    /// <para lang="zh">获得 Style 样式</para>
    /// <para lang="en">Get Style style</para>
    /// </summary>
    private string? StyleName => CssBuilder.Default()
        .AddClass($"width: {Width.ConvertToPercentString()};", !string.IsNullOrEmpty(Width))
        .Build();

    /// <summary>
    /// <para lang="zh">检查是否 active</para>
    /// <para lang="en">Check if active</para>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="css"></param>
    /// <returns></returns>
    private static string? CheckActive(int index, string? css = null) => CssBuilder.Default(css)
        .AddClass("active", index == 0)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 Images 集合</para>
    /// <para lang="en">Get Images collection</para>
    /// </summary>
    [Parameter]
    public IEnumerable<string> Images { get; set; } = [];

    /// <summary>
    /// <para lang="zh">获得/设置 内部图片的宽度</para>
    /// <para lang="en">Gets or sets the width of internal images</para>
    /// </summary>
    [Parameter]
    public string? Width { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否采用淡入淡出效果 默认为 false</para>
    /// <para lang="en">Gets or sets whether to use fade effect. Default is false</para>
    /// </summary>
    [Parameter]
    public bool IsFade { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 点击 Image 回调委托</para>
    /// <para lang="en">Gets or sets the Click Image callback delegate</para>
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnClick { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 幻灯片切换后回调方法</para>
    /// <para lang="en">Gets or sets the callback method after slide switch</para>
    /// </summary>
    [Parameter]
    public Func<int, Task>? OnSlideChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 子组件 要求使用 <see cref="CarouselItem"/></para>
    /// <para lang="en">Gets or sets child component. Requires <see cref="CarouselItem"/></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示控制按钮 默认 true</para>
    /// <para lang="en">Gets or sets whether to show control buttons. Default is true</para>
    /// </summary>
    [Parameter]
    public bool ShowControls { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示指示标志 默认 true</para>
    /// <para lang="en">Gets or sets whether to show indicators. Default is true</para>
    /// </summary>
    [Parameter]
    public bool ShowIndicators { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否禁用移动端手势滑动 默认 false</para>
    /// <para lang="en">Gets or sets whether to disable mobile touch swiping. Default is false</para>
    /// </summary>
    [Parameter]
    public bool DisableTouchSwiping { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 上一页图标</para>
    /// <para lang="en">Gets or sets the previous icon</para>
    /// </summary>
    [Parameter]
    public string? PreviousIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 下一页图标</para>
    /// <para lang="en">Gets or sets the next icon</para>
    /// </summary>
    [Parameter]
    public string? NextIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 鼠标悬停时是否暂停播放 默认 true</para>
    /// <para lang="en">Gets or sets whether to pause on hover. Default is true</para>
    /// </summary>
    [Parameter]
    public bool HoverPause { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 自动播放方式 默认 <see cref="CarouselPlayMode.AutoPlayOnload"/></para>
    /// <para lang="en">Gets or sets the auto play mode. Default is <see cref="CarouselPlayMode.AutoPlayOnload"/></para>
    /// </summary>
    [Parameter]
    public CarouselPlayMode PlayMode { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private string? DisableTouchSwipingString => DisableTouchSwiping ? "false" : null;

    private string? PauseString => HoverPause ? "hover" : "false";

    /// <summary>
    /// <para lang="zh">OnParametersSet 方法</para>
    /// <para lang="en">OnParametersSet method</para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        PreviousIcon ??= IconTheme.GetIconByKey(ComponentIcons.CarouselPreviousIcon);
        NextIcon ??= IconTheme.GetIconByKey(ComponentIcons.CarouselNextIcon);

        if (Items.Count == 0)
        {
            foreach (var image in Images)
            {
                var item = new CarouselItem();
#if NET5_0
                item.SetParametersAsync(ParameterView.FromDictionary(new Dictionary<string, object>()
#else
                item.SetParametersAsync(ParameterView.FromDictionary(new Dictionary<string, object?>()
#endif
                {
                    [nameof(CarouselItem.ChildContent)] = new RenderFragment(builder =>
                    {
                        builder.OpenComponent<CarouselImage>(0);
                        builder.AddAttribute(1, nameof(CarouselImage.ImageUrl), image);
                        if (OnClick != null)
                        {
                            builder.AddAttribute(2, nameof(CarouselImage.OnClick), OnClickImage);
                        }
                        builder.CloseComponent();
                    })
                }));
                Items.Add(item);
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, new { Invoke = Interop, Method = InvokeMethodName });

    private string? InvokeMethodName => OnSlideChanged == null ? null : nameof(TriggerSlideChanged);

    /// <summary>
    /// <para lang="zh">点击 Image 是触发此方法</para>
    /// <para lang="en">Trigger this method when clicking Image</para>
    /// </summary>
    /// <returns></returns>
    protected async Task OnClickImage(string imageUrl)
    {
        if (OnClick != null) await OnClick(imageUrl);
    }

    private List<CarouselItem> Items { get; } = new(10);

    /// <summary>
    /// <para lang="zh">添加子项</para>
    /// <para lang="en">Add child item</para>
    /// </summary>
    /// <param name="item"></param>
    internal void AddItem(CarouselItem item) => Items.Add(item);

    /// <summary>
    /// <para lang="zh">移除子项</para>
    /// <para lang="en">Remove child item</para>
    /// </summary>
    /// <param name="item"></param>
    internal void RemoveItem(CarouselItem item) => Items.Remove(item);

    /// <summary>
    /// <para lang="zh">幻灯片切换事件回调 由 JavaScript 调用</para>
    /// <para lang="en">Slide switch event callback called by JavaScript</para>
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    [JSInvokable]
    public async ValueTask TriggerSlideChanged(int index)
    {
        if (OnSlideChanged != null)
        {
            await OnSlideChanged(index);
        }
    }
}
