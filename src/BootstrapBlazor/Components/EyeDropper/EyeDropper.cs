// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// EyeDropper 组件
/// </summary>
[BootstrapModuleAutoLoader(ModuleName = "eye-dropper", AutoInvokeInit = false, AutoInvokeDispose = false)]
public class EyeDropper : BootstrapModuleComponentBase
{
    /// <summary>
    /// EyeDropperService 服务实例
    /// </summary>
    [Inject]
    [NotNull]
    private EyeDropperService? EyeDropperService { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 注册 EyeDropperService 弹窗事件
        EyeDropperService.Register(this, Pick);
    }

    private async Task Pick(EyeDropperOption option)
    {
        option.Value = await InvokeAsync<string>("open");
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="disposing"></param>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            EyeDropperService.UnRegister(this);
        }

        await base.DisposeAsync(disposing);
    }
}
