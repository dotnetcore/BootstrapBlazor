// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// ISerialPort 接口
/// </summary>
public interface ISerialPort
{
    /// <summary>
    /// 获得 端口是否打开
    /// </summary>
    bool IsOpen { get; }

    /// <summary>
    /// 关闭端口方法
    /// </summary>
    /// <returns></returns>
    Task Close();

    /// <summary>
    /// 打开端口方法
    /// </summary>
    /// <returns></returns>
    Task Open(SerialOptions options);

    /// <summary>
    /// 读取数据方法
    /// </summary>
    /// <returns></returns>
    List<byte> Read();

    /// <summary>
    /// 写入数据方法
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    Task Write(byte[] data);
}
