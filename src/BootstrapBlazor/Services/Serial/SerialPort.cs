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
    public bool IsOpen { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public List<byte> Read()
    {
        var ret = new List<byte>();
        if (IsOpen)
        {
            //var ret = await jsModule.InvokeAsync<bool>("open", serialPortId, options);
        }
        return ret;
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
    public async Task Open(SerialOptions options)
    {
        var ret = await jsModule.InvokeAsync<bool>("open", serialPortId, options);
        if (ret)
        {
            IsOpen = true;
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public async Task Close()
    {
        var ret = await jsModule.InvokeAsync<bool>("close", serialPortId);
        if (ret)
        {
            IsOpen = false;
        }
    }
}
