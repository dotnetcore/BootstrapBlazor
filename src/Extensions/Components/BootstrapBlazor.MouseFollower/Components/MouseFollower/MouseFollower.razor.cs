// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// MouseFollower 鼠标跟随器组件
/// </summary>
[JSModuleAutoLoader("./_content/BootstrapBlazor.MouseFollower/Components/MouseFollower/MouseFollower.razor.js", JSObjectReference = true)]
public partial class MouseFollower
{
    /// <summary>
    /// 获得/设置 Container 容器
    /// </summary>
    private ElementReference? Container { get; set; }

    /// <summary>
    /// 获得/设置 MouseFollowerMode 模式
    /// </summary>
    [Parameter]
    public MouseFollowerMode FollowerMode { get; set; } = MouseFollowerMode.Normal;

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

        await InvokeVoidAsync("init", GlobalMode, Container, Options);

        switch (FollowerMode)
        {
            case MouseFollowerMode.Text:
                await InvokeVoidAsync("SetText", Container, Content);
                break;
            case MouseFollowerMode.Icon:
                await InvokeVoidAsync("SetIcon", Container, Content);
                break;
            case MouseFollowerMode.Image:
                await InvokeVoidAsync("SetImage", Container, Content);
                break;
            case MouseFollowerMode.Video:
                await InvokeVoidAsync("SetVideo", Container, Content);
                break;
            case MouseFollowerMode.Normal:
                await InvokeVoidAsync("SetNormal", Container, Options);
                break;
            default:
                await InvokeVoidAsync("SetNormal", Container, Options);
                break;
        }
    }

    /// <inheritdoc/>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        await InvokeVoidAsync("destroy", Container);
        await base.DisposeAsync(disposing);
    }
}
