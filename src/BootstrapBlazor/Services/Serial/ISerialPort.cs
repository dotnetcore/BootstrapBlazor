// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ISerialPort 接口</para>
/// <para lang="en">ISerialPort 接口</para>
/// </summary>
public interface ISerialPort : IAsyncDisposable
{
    /// <summary>
    /// <para lang="zh">获得 端口是否打开</para>
    /// <para lang="en">Gets 端口whether打开</para>
    /// </summary>
    bool IsOpen { get; }

    /// <summary>
    /// <para lang="zh">关闭端口方法</para>
    /// <para lang="en">关闭端口方法</para>
    /// </summary>
    Task<bool> Close(CancellationToken token = default);

    /// <summary>
    /// <para lang="zh">打开端口方法</para>
    /// <para lang="en">打开端口方法</para>
    /// </summary>
    Task<bool> Open(SerialPortOptions options, CancellationToken token = default);

    /// <summary>
    /// <para lang="zh">接收数据回调方法</para>
    /// <para lang="en">接收datacallback method</para>
    /// </summary>
    Func<byte[], Task>? DataReceive { get; set; }

    /// <summary>
    /// <para lang="zh">写入数据方法</para>
    /// <para lang="en">写入data方法</para>
    /// </summary>
    Task<bool> Write(byte[] data, CancellationToken token = default);

    /// <summary>
    /// <para lang="zh">获得 Usb 设备信息</para>
    /// <para lang="en">Gets Usb 设备信息</para>
    /// </summary>
    /// <param name="token"></param>
    Task<SerialPortUsbInfo?> GetUsbInfo(CancellationToken token = default);

    /// <summary>
    /// <para lang="zh">获得设备参数</para>
    /// <para lang="en">Gets设备参数</para>
    /// </summary>
    /// <param name="token"></param>
    Task<SerialPortSignals?> GetSignals(CancellationToken token = default);

    /// <summary>
    /// <para lang="zh">设置设备参数</para>
    /// <para lang="en">Sets设备参数</para>
    /// </summary>
    /// <param name="options"></param>
    /// <param name="token"></param>
    Task<bool> SetSignals(SerialPortSignalsOptions options, CancellationToken token = default);
}
