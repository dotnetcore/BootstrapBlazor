// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Captcha 组件
/// </summary>
public partial class Captcha
{
    private static Random ImageRandomer { get; } = new();

    private int OriginX { get; set; }

    /// <summary>
    /// 获得 组件宽度
    /// </summary>
    private string? StyleString => CssBuilder.Default()
        .AddStyle("width", $"{Width + 42}px", Width > 0)
        .Build();

    /// <summary>
    /// 获得 加载图片失败样式
    /// </summary>
    private string? FailedStyle => CssBuilder.Default()
        .AddStyle("width", $"{Width}px", Width > 0)
        .AddStyle("height", $"{Height}px", Height > 0)
        .Build();

    /// <summary>
    /// 获得/设置 Header 显示文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? HeaderText { get; set; }

    /// <summary>
    /// 获得/设置 Bar 显示文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? BarText { get; set; }

    /// <summary>
    /// 获得/设置 Bar 显示文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? FailedText { get; set; }

    /// <summary>
    /// 获得/设置 Bar 显示文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? LoadText { get; set; }

    /// <summary>
    /// 获得/设置 验证码结果回调委托
    /// </summary>
    [Parameter]
    public Func<bool, Task>? OnValidAsync { get; set; }

    /// <summary>
    /// 获得/设置 图床路径 默认值为 images
    /// </summary>
    [Parameter]
    public string ImagesPath { get; set; } = "images";

    /// <summary>
    /// 获得/设置 图床路径 默认值为 Pic.jpg
    /// </summary>
    [Parameter]
    public string ImagesName { get; set; } = "Pic.jpg";

    /// <summary>
    /// 获得/设置 获取背景图方法委托
    /// </summary>
    [Parameter]
    public Func<string>? GetImageName { get; set; }

    /// <summary>
    /// 获得/设置 容错偏差
    /// </summary>
    [Parameter]
    public int Offset { get; set; } = 5;

    /// <summary>
    /// 获得/设置 图片宽度
    /// </summary>
    [Parameter]
    public int Width { get; set; } = 280;

    /// <summary>
    /// 获得/设置 拼图边长
    /// </summary>
    [Parameter]
    public int SideLength { get; set; } = 42;

    /// <summary>
    /// 获得/设置 拼图直径
    /// </summary>
    [Parameter]
    public int Diameter { get; set; } = 9;

    /// <summary>
    /// 获得/设置 图片高度
    /// </summary>
    [Parameter]
    public int Height { get; set; } = 155;

    /// <summary>
    /// 获得/设置 刷新按钮图标 默认值 fa-solid fa-arrows-rotate
    /// </summary>
    [Parameter]
    [NotNull]
    public string? RefreshIcon { get; set; }

    /// <summary>
    /// 获得/设置 随机图片最大张数 默认 1024
    /// </summary>
    [Parameter]
    public int Max { get; set; } = 1024;

    /// <summary>
    /// 获得/设置 刷新按钮图标 默认值 fa-solid fa-arrow-right
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
    /// <inheritdoc/>
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
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(Verify), GetCaptchaOption());

    /// <summary>
    /// 点击刷新按钮时回调此方法
    /// </summary>
    private Task OnClickRefresh() => Reset();

    /// <summary>
    /// 验证方差方法
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
        if (trails.Any())
        {
            var average = trails.Sum() * 1.0 / trails.Count;
            var dev = trails.Select(t => t - average);
            var stddev = Math.Sqrt(dev.Sum() * 1.0 / dev.Count());
            ret = stddev != 0;
        }
        return ret;
    }

    /// <summary>
    /// 重置组件方法
    /// </summary>
    public async Task Reset()
    {
        var option = GetCaptchaOption();
        await InvokeVoidAsync("reset", Id, option);
    }
}
