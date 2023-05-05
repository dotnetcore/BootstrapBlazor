// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// EyeDropper 组件
/// </summary>
[BootstrapModuleAutoLoader(ModuleName = "eye-dropper", AutoInvokeInit = false, AutoInvokeDispose = false)]
public class EyeDropper : BootstrapModuleComponentBase
{
    /// <summary>
    /// DialogServices 服务实例
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
    /// Dispose 方法
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
