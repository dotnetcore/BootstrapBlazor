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
    /// 获得/设置 PlayerOption 实例
    /// </summary>
    [Parameter]
    [EditorRequired]
    public Func<Task<PlayerOption>>? OnInitAsync { get; set; }

    private string? ClassString => CssBuilder.Default("bb-video-player")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task InvokeInitAsync()
    {
        if (OnInitAsync != null)
        {
            var option = await OnInitAsync();
            option.Language ??= CultureInfo.CurrentUICulture.Name;
            await InvokeVoidAsync("init", Id, Interop, "", option);
        }
    }
}
