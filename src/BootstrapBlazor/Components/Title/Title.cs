// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Components;

/// <summary>
/// Title 组件
/// </summary>
public class Title : BootstrapComponentBase, IDisposable
{
    [Inject]
    [NotNull]
    private TitleService? TitleService { get; set; }

    /// <summary>
    /// 获得/设置 当前页标题文字
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (string.IsNullOrEmpty(Text))
        {
            TitleService.Register(this, SetTitle);
        }
    }

    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!string.IsNullOrEmpty(Text))
        {
            await SetTitle(Text);
        }
    }

    /// <summary>
    /// 设置网站 Title 方法
    /// </summary>
    /// <param name="title"></param>
    /// <returns></returns>
    private ValueTask SetTitle(string title) => JSRuntime.InvokeVoidAsync(identifier: "$.bb_setTitle", title);

    /// <summary>
    /// Dispose 方法
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            TitleService.UnRegister(this);
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
