// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 串口通讯类
/// </summary>
class SerialPort(JSModule jsModule, string serialPortId) : ISerialPort
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool IsOpen { get; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public byte[] Read()
    {

        return new byte[0];
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public Task Write(byte[] data)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public async Task Open()
    {
        await jsModule.InvokeVoidAsync("open", serialPortId);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public async Task Close()
    {
        await jsModule.InvokeVoidAsync("close", serialPortId);
    }
}
