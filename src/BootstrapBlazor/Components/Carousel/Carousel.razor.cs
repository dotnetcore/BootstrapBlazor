// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Carousel 组件
/// </summary>
public partial class Carousel
{
    /// <summary>
    /// 获得 class 样式集合
    /// </summary>
    private string? ClassName => CssBuilder.Default("carousel slide")
        .AddClass("carousel-fade", IsFade)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 data-target 属性值
    /// </summary>
    /// <value></value>
    private string? TargetId => $"#{Id}";

    /// <summary>
    /// 获得 Style 样式
    /// </summary>
    private string? StyleName => CssBuilder.Default()
        .AddClass($"width: {Width.ConvertToPercentString()};", !string.IsNullOrEmpty(Width))
        .Build();

    /// <summary>
    /// 检查是否 active
    /// </summary>
    /// <param name="index"></param>
    /// <param name="css"></param>
    /// <returns></returns>
    private static string? CheckActive(int index, string? css = null) => CssBuilder.Default(css)
        .AddClass("active", index == 0)
        .Build();

    /// <summary>
    /// 获得 Images 集合
    /// </summary>
    [Parameter]
    public IEnumerable<string> Images { get; set; } = Enumerable.Empty<string>();

    /// <summary>
    /// 获得/设置 内部图片的宽度
    /// </summary>
    [Parameter]
    public string? Width { get; set; }

    /// <summary>
    /// 获得/设置 是否采用淡入淡出效果 默认为 false
    /// </summary>
    [Parameter]
    public bool IsFade { get; set; }

    /// <summary>
    /// 获得/设置 点击 Image 回调委托
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnClick { get; set; }

    /// <summary>
    /// 获得/设置 子组件 要求使用 <see cref="CarouselItem"/>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 是否显示控制按钮 默认 true
    /// </summary>
    [Parameter]
    public bool ShowControls { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示指示标志 默认 true
    /// </summary>
    [Parameter]
    public bool ShowIndicators { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否禁用移动端手势滑动 默认 false
    /// </summary>
    [Parameter]
    public bool DisableTouchSwiping { get; set; }

    /// <summary>
    /// 获得/设置 上一页图标
    /// </summary>
    [Parameter]
    public string? PreviousIcon { get; set; }

    /// <summary>
    /// 获得/设置 下一页图标
    /// </summary>
    [Parameter]
    public string? NextIcon { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private string? DisableTouchSwipingString => DisableTouchSwiping ? "false" : null;

    /// <summary>
    /// OnParametersSet 方法
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
    /// 点击 Image 是触发此方法
    /// </summary>
    /// <returns></returns>
    protected async Task OnClickImage(string imageUrl)
    {
        if (OnClick != null) await OnClick(imageUrl);
    }

    private List<CarouselItem> Items { get; } = new(10);

    /// <summary>
    /// 添加子项
    /// </summary>
    /// <param name="item"></param>
    internal void AddItem(CarouselItem item) => Items.Add(item);

    /// <summary>
    /// 移除子项
    /// </summary>
    /// <param name="item"></param>
    internal void RemoveItem(CarouselItem item) => Items.Remove(item);
}
