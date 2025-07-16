// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Sokcet 数据转换为 bool 数据转换器
/// </summary>
public class SocketDataBoolConverter : ISocketDataPropertyConverter
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="data"></param>
    public object? Convert(ReadOnlyMemory<byte> data)
    {
        var ret = false;
        if (data.Length == 1)
        {
            ret = data.Span[0] != 0x00;
        }
        return ret;
    }
}
