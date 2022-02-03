// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Components;

/// <summary>
/// SweetAlert 弹窗服务
/// </summary>
public class SwalService : BootstrapServiceBase<SwalOption>, IDisposable
{
    private readonly IDisposable _optionsReloadToken;
    private BootstrapBlazorOptions _option;

    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="option"></param>
    public SwalService(IOptionsMonitor<BootstrapBlazorOptions> option)
    {
        _option = option.CurrentValue;
        _optionsReloadToken = option.OnChange(op => _option = op);
    }

    /// <summary>
    /// Show 方法
    /// </summary>
    /// <param name="option"></param>
    /// <param name="swal">指定弹窗组件 默认为 null 使用 <see cref="BootstrapBlazorRoot"/> 组件内置弹窗组件</param>
    public async Task Show(SwalOption option, SweetAlert? swal = null)
    {
        if (!option.ForceDelay && _option.SwalDelay != 0)
        {
            option.Delay = _option.SwalDelay;
        }

        await Invoke(option, swal);
    }

    /// <summary>
    /// 异步回调方法
    /// </summary>
    /// <param name="option"></param>
    /// <param name="swal">指定弹窗组件 默认为 null 使用 <see cref="BootstrapBlazorRoot"/> 组件内置弹窗组件</param>
    /// <returns></returns>
    public async Task<bool> ShowModal(SwalOption option, SweetAlert? swal = null)
    {
        await Invoke(option, swal);
        return option.IsConfirm != true || await option.ReturnTask.Task;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _optionsReloadToken.Dispose();
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
