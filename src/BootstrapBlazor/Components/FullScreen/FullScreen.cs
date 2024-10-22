// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// FullScreen 组件部分类
/// </summary>
[BootstrapModuleAutoLoader(ModuleName = "fullscreen")]
public class FullScreen : BootstrapModuleComponentBase
{
    /// <summary>
    /// DialogServices 服务实例
    /// </summary>
    [Inject]
    [NotNull]
    private FullScreenService? FullScreenService { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 注册 FullScreen 弹窗事件
        FullScreenService.Register(this, Show);
    }

    private Task Show(FullScreenOption option) => InvokeVoidAsync("execute", Id, option);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="disposing"></param>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            FullScreenService.UnRegister(this);
        }

        await base.DisposeAsync(disposing);
    }
}
