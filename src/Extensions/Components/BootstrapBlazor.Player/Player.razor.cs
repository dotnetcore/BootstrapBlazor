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
    /// 获得/设置 组件模式 默认为 <see cref="PlayerMode.Video"/>
    /// </summary>
    [Parameter]
    public PlayerMode Mode { get; set; }

    /// <summary>
    /// 获得/设置 PlayerOption 实例
    /// </summary>
    [Parameter]
    [EditorRequired]
    public PlayerOptions? Options { get; set; }

    private string? ClassString => CssBuilder.Default("bb-video-player")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task InvokeInitAsync()
    {
        if (Options != null)
        {
            Options.Language ??= CultureInfo.CurrentUICulture.Name;
        }
        await InvokeVoidAsync("init", Id, Interop, "", Options);
    }

    /// <summary>
    /// 重新配置播放器方法
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public Task Reload(PlayerOptions option) => InvokeVoidAsync("reload", Id, option);
}
