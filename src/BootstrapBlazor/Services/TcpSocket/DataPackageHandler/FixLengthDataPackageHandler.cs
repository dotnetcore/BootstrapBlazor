// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Handles fixed-length data packages by processing incoming data of a specified length.
/// </summary>
/// <remarks>This class is designed to handle data packages with a fixed length, as specified during
/// initialization. It extends <see cref="DataPackageHandlerBase"/> and overrides its behavior to process fixed-length
/// data.</remarks>
/// <param name="length">The data package total data length.</param>
public class FixLengthDataPackageHandler(int length) : DataPackageHandlerBase
{
    private readonly Memory<byte> _data = new byte[length];

    private int _receivedLength;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="data"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public override async ValueTask HandlerAsync(ReadOnlyMemory<byte> data, CancellationToken token = default)
    {
        while (data.Length > 0)
        {
            // 拷贝数据
            var len = length - _receivedLength;
            var segment = data.Length > len ? data[..len] : data;
            segment.CopyTo(_data[_receivedLength..]);

            // 更新数据
            data = data[segment.Length..];

            // 更新已接收长度
            _receivedLength += segment.Length;

            // 如果已接收长度等于总长度则触发回调
            if (_receivedLength == length)
            {
                // 重置已接收长度
                _receivedLength = 0;
                if (ReceivedCallBack != null)
                {
                    await ReceivedCallBack(_data);
                }
            }
        }
    }
}
