// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json;

namespace BootstrapBlazor.Components;

/// <summary>
/// Ajax 组件
/// </summary>
[BootstrapModuleAutoLoader(ModuleName = "ajax", AutoInvokeInit = false, AutoInvokeDispose = false)]
public class Ajax : BootstrapModuleComponentBase
{
    [Inject]
    [NotNull]
    private AjaxService? AjaxService { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        AjaxService.Register(this, InvokeAsync);
        AjaxService.RegisterGoto(this, Goto);
    }

    private Task<JsonDocument?> InvokeAsync(AjaxOption option) => InvokeAsync<JsonDocument?>("execute", option);

    private Task Goto(string url) => InvokeVoidAsync("goto", url);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            AjaxService.UnRegister(this);
            AjaxService.UnRegisterGoto(this);
        }

        await base.DisposeAsync(disposing);
    }
}
