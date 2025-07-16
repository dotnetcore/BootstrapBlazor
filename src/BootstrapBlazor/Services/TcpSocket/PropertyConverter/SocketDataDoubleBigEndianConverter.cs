// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Buffers.Binary;

namespace BootstrapBlazor.Components;

/// <summary>
/// Sokcet 数据转换为 double 数据大端转换器
/// </summary>
public class SocketDataDoubleBigEndianConverter : ISocketDataPropertyConverter
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="data"></param>
    public object? Convert(ReadOnlyMemory<byte> data)
    {
        double ret = 0;
        if (data.Length <= 8)
        {
            Span<byte> paddedSpan = stackalloc byte[8];
            data.Span.CopyTo(paddedSpan[(8 - data.Length)..]);
            if (BinaryPrimitives.TryReadDoubleBigEndian(paddedSpan, out var v))
            {
                ret = v;
            }
        }
        return ret;
    }
}
