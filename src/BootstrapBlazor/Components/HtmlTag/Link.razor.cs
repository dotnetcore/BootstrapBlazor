// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Link 组件
/// </summary>
public partial class Link
{
    /// <summary>
    /// 获得/设置 href 属性值
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string? Href { get; set; }

    /// <summary>
    /// 获得/设置 Rel 属性值, 默认 stylesheet
    /// </summary>
    [Parameter]
    public string? Rel { get; set; } = "stylesheet";

    /// <summary>
    /// 获得/设置 版本号 默认 null 自动生成
    /// </summary>
    [Parameter]
    public string? Version { get; set; }

    /// <summary>
    /// 是否将样式添加到 head 元素内。
    /// <para>同时在多个组件中使用同一个样式时可以添加到 head 中，减少 DOM 节点。</para>
    /// </summary>
    [Parameter]
    public bool AddToHead { get; set; } = false;

    [Inject, NotNull]
    private IVersionService? VersionService { get; set; }

    private string GetHref() => $"{Href}?v={Version ?? VersionService.GetVersion(Href)}";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (AddToHead && firstRender)
        {
            var obj = new { Href = GetHref(), Rel };
            await InvokeVoidAsync("init", obj);
        }
    }
}
