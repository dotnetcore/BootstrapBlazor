// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text;

namespace BootstrapBlazor.Components;

/// <summary>
/// <see cref="ITcpSocketClient"/> 扩展方法类
/// </summary>
public static class ITcpSocketClientExtensions
{
    /// <summary>
    /// Sends the specified string content to the connected TCP socket client asynchronously.
    /// </summary>
    /// <remarks>This method converts the provided string content into a byte array using the specified
    /// encoding  (or UTF-8 by default) and sends it to the connected TCP socket client. Ensure the client is connected 
    /// before calling this method.</remarks>
    /// <param name="client">The TCP socket client to which the content will be sent. Cannot be <see langword="null"/>.</param>
    /// <param name="content">The string content to send. Cannot be <see langword="null"/> or empty.</param>
    /// <param name="encoding">The character encoding to use for converting the string content to bytes.  If <see langword="null"/>, UTF-8
    /// encoding is used by default.</param>
    /// <param name="token">A <see cref="CancellationToken"/> to observe while waiting for the operation to complete.</param>
    /// <returns>A <see cref="ValueTask{TResult}"/> that represents the asynchronous operation.  The result is <see
    /// langword="true"/> if the content was sent successfully; otherwise, <see langword="false"/>.</returns>
    public static ValueTask<bool> SendAsync(this ITcpSocketClient client, string content, Encoding? encoding = null, CancellationToken token = default)
    {
        var buffer = encoding?.GetBytes(content) ?? Encoding.UTF8.GetBytes(content);
        return client.SendAsync(buffer, token);
    }
}
