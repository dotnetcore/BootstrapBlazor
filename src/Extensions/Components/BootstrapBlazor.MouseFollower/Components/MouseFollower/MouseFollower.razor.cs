// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// MouseFollower 鼠标跟随器组件
/// </summary>
[JSModuleAutoLoader("./_content/BootstrapBlazor.MouseFollower/Components/MouseFollower/MouseFollower.razor.js")]
public partial class MouseFollower
{
    /// <summary>
    /// 获得/设置 MouseFollowerMode 模式
    /// </summary>
    [Parameter]
    public MouseFollowerMode FollowerMode { get; set; }

    /// <summary>
    /// 获得/设置 GlobalMode 全局模式
    /// </summary>
    [Parameter]
    public bool GlobalMode { get; set; }

    /// <summary>
    /// 获得/设置 Content 文本，图标，图片路径，视频路径
    /// </summary>
    [Parameter]
    public string? Content { get; set; }

    /// <summary>
    /// 获得/设置 RenderFragment 实例
    /// </summary>
    [Parameter]
    [NotNull]
    public RenderFragment? ChildContent { get; set; }

    private string? ClassString => CssBuilder.Default("bb-mf")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? ModeString => FollowerMode == MouseFollowerMode.Normal ? null : FollowerMode.ToDescriptionString();

    private string? GlobalString => GlobalMode ? "true" : null;

    /// <summary>
    /// 获得/设置 MouseFollowerOptions
    /// </summary>
    [Parameter]
    public MouseFollowerOptions? Options { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task InvokeInitAsync()
    {
        Options ??= new MouseFollowerOptions();

        await InvokeVoidAsync("init", Id, Options);
    }
}
