// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
/// <summary>
/// 打印组件类
/// </summary>
public class Print : BootstrapComponentBase, IDisposable
{
    /// <summary>
    /// PrintService 服务实例
    /// </summary>
    [Inject]
    [NotNull]
    private PrintService? PrintService { get; set; }

    /// <summary>
    /// 获得 弹窗注入服务
    /// </summary>
    [Inject]
    [NotNull]
    private DialogService? DialogService { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 注册 打印弹窗事件
        PrintService.Register(this, PrintDialogAsync);
    }

    private Task PrintDialogAsync(DialogOption option) => DialogService.Show(option);

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            PrintService.UnRegister(this);
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
