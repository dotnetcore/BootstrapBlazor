// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// ISerialPort 接口
/// </summary>
public interface ISerialPort : IAsyncDisposable
{
    /// <summary>
    /// 获得 端口是否打开
    /// </summary>
    bool IsOpen { get; }

    /// <summary>
    /// 关闭端口方法
    /// </summary>
    /// <returns></returns>
    Task<bool> Close(CancellationToken token = default);

    /// <summary>
    /// 打开端口方法
    /// </summary>
    /// <returns></returns>
    Task<bool> Open(SerialPortOptions options, CancellationToken token = default);

    /// <summary>
    /// 接收数据回调方法
    /// </summary>
    /// <returns></returns>
    Func<byte[], Task>? DataReceive { get; set; }

    /// <summary>
    /// 写入数据方法
    /// </summary>
    /// <returns></returns>
    Task<bool> Write(byte[] data, CancellationToken token = default);

    /// <summary>
    /// 获得 Usb 设备信息
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<SerialPortUsbInfo?> GetUsbInfo(CancellationToken token = default);

    /// <summary>
    /// 获得设备参数
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<SerialPortSignals?> GetSignals(CancellationToken token = default);

    /// <summary>
    /// 设置设备参数
    /// </summary>
    /// <param name="options"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<bool> SetSignals(SerialPortSignalsOptions options, CancellationToken token = default);
}
