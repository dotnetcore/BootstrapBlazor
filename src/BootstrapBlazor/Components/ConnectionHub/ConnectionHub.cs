// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 客户端链接组件
/// </summary>
[BootstrapModuleAutoLoader(ModuleName = "hub", JSObjectReference = true, AutoInvokeInit = true, AutoInvokeDispose = false)]
public class ConnectionHub : BootstrapModuleComponentBase
{
    [Inject]
    [NotNull]
    private IConnectionService? ConnectionService { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", new { Invoke = Interop, Method = nameof(Callback) });

    /// <summary>
    /// JSInvoke 回调方法
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    [JSInvokable]
    public Task Callback(ClientInfo client)
    {
        if (!string.IsNullOrEmpty(client.Id))
        {
            ConnectionService.AddOrUpdate(client);
        }
        return Task.CompletedTask;
    }
}
