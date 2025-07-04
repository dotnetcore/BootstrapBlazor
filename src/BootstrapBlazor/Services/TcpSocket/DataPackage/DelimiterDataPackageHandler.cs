// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Buffers;
using System.Text;

namespace BootstrapBlazor.Components;

/// <summary>
/// Handles data packages that are delimited by a specific sequence of bytes or characters.
/// </summary>
/// <remarks>This class provides functionality for processing data packages that are separated by a defined
/// delimiter. The delimiter can be specified as a string with an optional encoding or as a byte array.</remarks>
public class DelimiterDataPackageHandler : DataPackageHandlerBase
{
    private readonly ReadOnlyMemory<byte> _delimiter;

    /// <summary>
    /// Initializes a new instance of the <see cref="DelimiterDataPackageHandler"/> class with the specified delimiter
    /// and optional encoding.
    /// </summary>
    /// <param name="delimiter">The string delimiter used to separate data packages. This value cannot be null or empty.</param>
    /// <param name="encoding">The character encoding used to convert the delimiter to bytes. If null, <see cref="Encoding.UTF8"/> is used as
    /// the default.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="delimiter"/> is null or empty.</exception>
    public DelimiterDataPackageHandler(string delimiter, Encoding? encoding = null)
    {
        if (string.IsNullOrEmpty(delimiter))
        {
            throw new ArgumentNullException(nameof(delimiter), "Delimiter cannot be null or empty.");
        }

        encoding ??= Encoding.UTF8;
        _delimiter = encoding.GetBytes(delimiter);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DelimiterDataPackageHandler"/> class with the specified delimiters.
    /// </summary>
    /// <param name="delimiter">An array of bytes representing the delimiters used to parse data packages. Cannot be <see langword="null"/>.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="delimiter"/> is <see langword="null"/>.</exception>
    public DelimiterDataPackageHandler(byte[] delimiter)
    {
        _delimiter = delimiter ?? throw new ArgumentNullException(nameof(delimiter), "Delimiter cannot be null.");
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="data"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public override async ValueTask HandlerAsync(ReadOnlyMemory<byte> data, CancellationToken token = default)
    {
        data = ConcatBuffer(data);

        while (data.Length > 0)
        {
            var index = data.Span.IndexOfAny(_delimiter.Span);
            var segment = index == -1 ? data : data[..index];
            var length = segment.Length + _delimiter.Length;
            using var buffer = MemoryPool<byte>.Shared.Rent(length);
            segment.CopyTo(buffer.Memory);

            if (index != -1)
            {
                SlicePackage(data, index + _delimiter.Length);

                _delimiter.CopyTo(buffer.Memory[index..]);
                if (ReceivedCallBack != null)
                {
                    await ReceivedCallBack(buffer.Memory[..length].ToArray());
                }

                data = data[(index + _delimiter.Length)..];
            }
            else
            {
                SlicePackage(data, 0);
                break;
            }
        }
    }
}
