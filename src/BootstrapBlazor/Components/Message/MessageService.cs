// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public class MessageService : BootstrapServiceBase<MessageOption>, IDisposable
{
    private readonly IDisposable _optionsReloadToken;
    private BootstrapBlazorOptions _option;

    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="option"></param>
    public MessageService(IOptionsMonitor<BootstrapBlazorOptions> option)
    {
        _option = option.CurrentValue;
        _optionsReloadToken = option.OnChange(op => _option = op);
    }

    /// <summary>
    /// Show 方法
    /// </summary>
    /// <param name="option"></param>
    /// <param name="message">指定弹窗组件 默认为 null 使用 <see cref="BootstrapBlazorRoot"/> 组件内置弹窗组件</param>
    public async Task Show(MessageOption option, Message? message = null)
    {
        if (!option.ForceDelay && _option.MessageDelay != 0)
        {
            option.Delay = _option.MessageDelay;
        }

        await Invoke(option, message);
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
