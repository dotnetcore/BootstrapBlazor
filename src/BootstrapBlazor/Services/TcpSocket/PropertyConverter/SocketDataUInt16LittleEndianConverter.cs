// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Buffers.Binary;

namespace BootstrapBlazor.Components;

/// <summary>
/// Sokcet 数据转换为 ushort 数据小端转换器
/// </summary>
public class SocketDataUInt16LittleEndianConverter : ISocketDataPropertyConverter
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="data"></param>
    public object? Convert(ReadOnlyMemory<byte> data)
    {
        ushort ret = 0;
        if (data.Length <= 2)
        {
            Span<byte> paddedSpan = stackalloc byte[2];
            data.Span.CopyTo(paddedSpan[(2 - data.Length)..]);
            if (BinaryPrimitives.TryReadUInt16LittleEndian(paddedSpan, out var v))
            {
                ret = v;
            }
        }
        return ret;
    }
}
