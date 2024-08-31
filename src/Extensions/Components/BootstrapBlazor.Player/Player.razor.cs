// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 视频播放器 Player 组件
/// </summary>
public partial class Player
{
    /// <summary>
    /// 获得/设置 资源地址
    /// </summary>
    [Parameter]
    public string? Url { get; set; }

    /// <summary>
    /// 获得/设置 资源类型 默认值 application/x-mpegURL
    /// </summary>
    [Parameter]
    public string MineType { get; set; } = "application/x-mpegURL";

    /// <summary>
    /// 获得/设置 视窗宽度 默认 null
    /// </summary>
    [Parameter]
    public int? Width { get; set; }

    /// <summary>
    /// 获得/设置 视窗高度 默认 null
    /// </summary>
    [Parameter]
    public int? Height { get; set; }

    /// <summary>
    /// 显示控制条,默认 true
    /// </summary>
    [Parameter]
    public bool Controls { get; set; } = true;

    /// <summary>
    /// 自动播放,默认 true
    /// </summary>
    [Parameter]
    public bool AutoPlay { get; set; } = true;

    /// <summary>
    /// 预载,默认 auto
    /// </summary>
    [Parameter]
    public string Preload { get; set; } = "auto";

    /// <summary>
    /// 获得/设置 封面 Url
    /// </summary>
    [Parameter]
    public string? Poster { get; set; }

    /// <summary>
    /// 获得/设置 界面语言
    /// </summary>
    [Parameter]
    public string? Language { get; set; }

    private string? _lastPoster;
    private string? _url;

    private string? ClassString => CssBuilder.Default("bb-video-player")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _lastPoster = Poster;
            _url = Url;
        }

        if (_lastPoster != Poster)
        {
            await SetPoster(Poster);
        }

        if (_url != Url)
        {
            await Reload(Url, MineType);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task InvokeInitAsync()
    {
        var option = new VideoPlayerOption();
        option.Sources.Add(new VideoPlayerSources() { Type = MineType, Src = Url });
        await InvokeVoidAsync("init", Id, Interop, "", option);
    }

    /// <summary>
    /// 设置封面方法
    /// </summary>
    /// <param name="poster"></param>
    /// <returns></returns>
    public async Task SetPoster(string? poster)
    {
        _lastPoster = poster;
        Poster = poster;
        await InvokeVoidAsync("setPoster", Id, poster);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public async Task Reload(string? url, string? type)
    {
        _url = url;
        Url = url;
        await InvokeVoidAsync("reload", Id, url, type);
    }
}
