// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text;

namespace BootstrapBlazor.Components;

/// <summary>
/// Sokcet 数据转换为 string 数据转换器
/// </summary>
public class SocketDataStringConverter(string? encodingName) : ISocketDataPropertyConverter
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="data"></param>
    public object? Convert(ReadOnlyMemory<byte> data)
    {
        var encoding = string.IsNullOrEmpty(encodingName) ? Encoding.UTF8 : Encoding.GetEncoding(encodingName);
        return encoding.GetString(data.Span);
    }
}
