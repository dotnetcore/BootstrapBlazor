// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Net;

namespace BootstrapBlazor.Components;

/// <summary>
/// ITcpSocketFactory Interface
/// </summary>
public interface ITcpSocketFactory : IDisposable
{
    /// <summary>
    /// Retrieves an existing TCP socket client by name or creates a new one using the specified factory function.
    /// </summary>
    /// <param name="name">The unique name identifying the TCP socket client. Cannot be null or empty.</param>
    /// <param name="valueFactory">A factory function that generates an <see cref="IPEndPoint"/> for the client.  The function is invoked if a
    /// client with the specified name does not already exist.</param>
    /// <returns>An instance of <see cref="ITcpSocketClient"/> associated with the specified name.  If a client with the given
    /// name already exists, the existing instance is returned; otherwise, a new instance is created.</returns>
    ITcpSocketClient GetOrCreate(string name, Func<string, IPEndPoint> valueFactory);

    /// <summary>
    /// Removes the TCP socket client associated with the specified name.
    /// </summary>
    /// <param name="name">The name of the TCP socket client to remove. Cannot be <see langword="null"/> or empty.</param>
    /// <returns>The removed <see cref="ITcpSocketClient"/> instance if a client with the specified name exists;  otherwise, <see
    /// langword="null"/>.</returns>
    ITcpSocketClient? Remove(string name);
}
