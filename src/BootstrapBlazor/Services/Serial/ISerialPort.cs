// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
