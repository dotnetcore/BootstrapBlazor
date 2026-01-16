// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">Captcha 组件</para>
///  <para lang="en">Captcha component</para>
/// </summary>
public partial class Captcha
{
    private static Random ImageRandomer { get; } = new();

    private int OriginX { get; set; }

    /// <summary>
    ///  <para lang="zh">获得 组件宽度</para>
    ///  <para lang="en">Gets componentwidth</para>
    /// </summary>
    private string? StyleString => CssBuilder.Default()
        .AddClass($"width: {Width + 42}px;", Width > 0)
        .Build();

    /// <summary>
    ///  <para lang="zh">获得 加载图片失败样式</para>
    ///  <para lang="en">Gets 加载图片失败style</para>
    /// </summary>
    private string? FailedStyle => CssBuilder.Default()
        .AddClass($"width: {Width}px;", Width > 0)
        .AddClass($"height: {Height}px;", Height > 0)
        .Build();

    /// <summary>
    ///  <para lang="zh">获得/设置 Header 显示文本</para>
    ///  <para lang="en">Gets or sets Header display文本</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? HeaderText { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 Bar 显示文本</para>
    ///  <para lang="en">Gets or sets Bar display文本</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? BarText { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 Bar 显示文本</para>
    ///  <para lang="en">Gets or sets Bar display文本</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? FailedText { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 Bar 显示文本</para>
    ///  <para lang="en">Gets or sets Bar display文本</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? LoadText { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 验证码结果回调委托</para>
    ///  <para lang="en">Gets or sets 验证码结果回调delegate</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<bool, Task>? OnValidAsync { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 图床路径 默认值为 images</para>
    ///  <para lang="en">Gets or sets 图床路径 Default is值为 images</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string ImagesPath { get; set; } = "images";

    /// <summary>
    ///  <para lang="zh">获得/设置 图床路径 默认值为 Pic.jpg</para>
    ///  <para lang="en">Gets or sets 图床路径 Default is值为 Pic.jpg</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string ImagesName { get; set; } = "Pic.jpg";

    /// <summary>
    ///  <para lang="zh">获得/设置 获取背景图方法委托</para>
    ///  <para lang="en">Gets or sets 获取背景图方法delegate</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<string>? GetImageName { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 容错偏差</para>
    ///  <para lang="en">Gets or sets 容错偏差</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int Offset { get; set; } = 5;

    /// <summary>
    ///  <para lang="zh">获得/设置 图片宽度</para>
    ///  <para lang="en">Gets or sets 图片width</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int Width { get; set; } = 280;

    /// <summary>
    ///  <para lang="zh">获得/设置 拼图边长</para>
    ///  <para lang="en">Gets or sets 拼图边长</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int SideLength { get; set; } = 42;

    /// <summary>
    ///  <para lang="zh">获得/设置 拼图直径</para>
    ///  <para lang="en">Gets or sets 拼图直径</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int Diameter { get; set; } = 9;

    /// <summary>
    ///  <para lang="zh">获得/设置 图片高度</para>
    ///  <para lang="en">Gets or sets 图片height</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int Height { get; set; } = 155;

    /// <summary>
    ///  <para lang="zh">获得/设置 刷新按钮图标 默认值 fa-solid fa-arrows-rotate</para>
    ///  <para lang="en">Gets or sets 刷新buttonicon Default is值 fa-solid fa-arrows-rotate</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? RefreshIcon { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 随机图片最大张数 默认 1024</para>
    ///  <para lang="en">Gets or sets 随机图片最大张数 Default is 1024</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int Max { get; set; } = 1024;

    /// <summary>
    ///  <para lang="zh">获得/设置 刷新按钮图标 默认值 fa-solid fa-arrow-right</para>
    ///  <para lang="en">Gets or sets 刷新buttonicon Default is值 fa-solid fa-arrow-right</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? BarIcon { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Captcha>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        HeaderText ??= Localizer[nameof(HeaderText)];
        BarText ??= Localizer[nameof(BarText)];
        FailedText ??= Localizer[nameof(FailedText)];
        LoadText ??= Localizer[nameof(LoadText)];

        RefreshIcon ??= IconTheme.GetIconByKey(ComponentIcons.CaptchaRefreshIcon);
        BarIcon ??= IconTheme.GetIconByKey(ComponentIcons.CaptchaBarIcon);
    }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(Verify), GetCaptchaOption());

    /// <summary>
    ///  <para lang="zh">点击刷新按钮时回调此方法</para>
    ///  <para lang="en">点击刷新button时回调此方法</para>
    /// </summary>
    private Task OnClickRefresh() => Reset();

    /// <summary>
    ///  <para lang="zh">验证方差方法</para>
    ///  <para lang="en">验证方差方法</para>
    /// </summary>
    [JSInvokable]
    public async Task<bool> Verify(int offset, List<int> trails)
    {
        var ret = Math.Abs(offset - OriginX) < Offset && CalcStddev(trails);
        if (OnValidAsync != null)
        {
            await OnValidAsync(ret);
        }
        return ret;
    }

    private CaptchaOption GetCaptchaOption()
    {
        var option = new CaptchaOption()
        {
            Width = Width,
            Height = Height,
            SideLength = SideLength,
            Diameter = Diameter
        };
        option.BarWidth = option.SideLength + option.Diameter * 2 + 6; // 滑块实际边长
        var start = option.BarWidth + 10;
        var end = option.Width - start;
        option.OffsetX = Convert.ToInt32(Math.Ceiling(ImageRandomer.Next(0, 100) / 100.0 * (end - start) + start));
        OriginX = option.OffsetX;

        start = 10 + option.Diameter * 2;
        end = option.Height - option.SideLength - 10;
        option.OffsetY = Convert.ToInt32(Math.Ceiling(ImageRandomer.Next(0, 100) / 100.0 * (end - start) + start));

        if (GetImageName == null)
        {
            var index = Convert.ToInt32(ImageRandomer.Next(0, Max) / 1.0);
            var imageName = Path.GetFileNameWithoutExtension(ImagesName);
            var extendName = Path.GetExtension(ImagesName);
            var fileName = $"{imageName}{index}{extendName}";
            option.ImageUrl = Path.Combine(ImagesPath, fileName);
        }
        else
        {
            option.ImageUrl = GetImageName();
        }
        return option;
    }

    private static bool CalcStddev(List<int> trails)
    {
        var ret = false;
        if (trails.Count > 0)
        {
            var average = trails.Sum() * 1.0 / trails.Count;
            var dev = trails.Select(t => t - average);
            var stddev = Math.Sqrt(dev.Sum() * 1.0 / dev.Count());
            ret = stddev != 0;
        }
        return ret;
    }

    /// <summary>
    ///  <para lang="zh">重置组件方法</para>
    ///  <para lang="en">重置component方法</para>
    /// </summary>
    public async Task Reset()
    {
        var option = GetCaptchaOption();
        await InvokeVoidAsync("reset", Id, option);
    }
}
