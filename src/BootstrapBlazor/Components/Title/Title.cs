// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Title 组件
/// </summary>
[BootstrapModuleAutoLoader(ModuleName = "title", AutoInvokeInit = false, AutoInvokeDispose = false)]
public class Title : BootstrapModuleComponentBase
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

        TitleService.Register(this, SetTitle);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender && Text != null)
        {
            var op = new TitleOption() { Title = Text };
            await SetTitle(op);
        }
    }

    private async Task SetTitle(TitleOption op)
    {
        if (Module != null)
        {
            await InvokeVoidAsync("setTitle", op.Title);
        }
        else
        {
            Text = op.Title;
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        await base.DisposeAsync(disposing);

        if (disposing)
        {
            TitleService.UnRegister(this);
        }
    }
}
