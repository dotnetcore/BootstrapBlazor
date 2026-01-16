// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Avatar 头像框组件</para>
/// <para lang="en">Avatar component</para>
/// </summary>
public partial class Avatar
{
    /// <summary>
    /// <para lang="zh">获得 样式集合</para>
    /// <para lang="en">Gets the class name</para>
    /// </summary>
    /// <returns></returns>
    private string? ClassName => CssBuilder.Default("avatar")
        .AddClass("avatar-circle", IsCircle)
        .AddClass($"avatar-{Size.ToDescriptionString()}", Size != Size.None && Size != Size.Medium)
        .AddClass("border border-info", IsBorder)
        .AddClass("border-success", IsBorder && IsLoaded.HasValue && IsLoaded.Value && !IsIcon && !IsText)
        .AddClass("border-danger", IsBorder && IsLoaded.HasValue && !IsLoaded.Value)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 图片样式</para>
    /// <para lang="en">Gets the image class string</para>
    /// </summary>
    private string? ImgClassString => (IsLoaded.HasValue && IsLoaded.Value) ? null : "d-none";

    /// <summary>
    /// <para lang="zh">获得/设置 是否为圆形</para>
    /// <para lang="en">Gets or sets whether it is a circle</para>
    /// </summary>
    [Parameter]
    public bool IsCircle { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Image 头像路径地址</para>
    /// <para lang="en">Gets or sets the image path</para>
    /// </summary>
    [Parameter]
    public string? Url { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否为图标</para>
    /// <para lang="en">Gets or sets whether it is an icon</para>
    /// </summary>
    [Parameter]
    public bool IsIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 头像框显示图标</para>
    /// <para lang="en">Gets or sets the icon</para>
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示为文字</para>
    /// <para lang="en">Gets or sets whether to display text</para>
    /// </summary>
    [Parameter]
    public bool IsText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 头像框显示文字</para>
    /// <para lang="en">Gets or sets the text</para>
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 头像框大小</para>
    /// <para lang="en">Gets or sets the size</para>
    /// </summary>
    [Parameter]
    public Size Size { get; set; } = Size.Medium;

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 Border 默认为 false</para>
    /// <para lang="en">Gets or sets whether to show border. Default is false</para>
    /// </summary>
    [Parameter]
    public bool IsBorder { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 获取图片地址异步回调方法</para>
    /// <para lang="en">Gets or sets the async callback method to get image url</para>
    /// </summary>
    [Parameter]
    public Func<Task<string>>? GetUrlAsync { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示图片</para>
    /// <para lang="en">Gets or sets whether to show image</para>
    /// </summary>
    private bool? IsLoaded { get; set; }

    /// <summary>
    /// <para lang="zh">OnParametersSetAsync 方法</para>
    /// <para lang="en">OnParametersSetAsync method</para>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (string.IsNullOrEmpty(Url) && GetUrlAsync != null)
        {
            Url = await GetUrlAsync();
        }
    }

    /// <summary>
    /// <para lang="zh">OnParametersSet 方法</para>
    /// <para lang="en">OnParametersSet method</para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        Icon ??= IconTheme.GetIconByKey(ComponentIcons.AvatarIcon);
    }

    /// <summary>
    /// <para lang="zh">图片加载失败时回调此方法</para>
    /// <para lang="en">Callback method when image load fails</para>
    /// </summary>
    private void OnError()
    {
        IsIcon = true;
        IsLoaded = false;
    }

    /// <summary>
    /// <para lang="zh">图片加载成功时回调此方法</para>
    /// <para lang="en">Callback method when image load succeeds</para>
    /// </summary>
    private void OnLoad()
    {
        IsLoaded = true;
    }
}
