// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// ITcpSocketFactory Interface
/// </summary>
public interface ITcpSocketFactory : IAsyncDisposable
{
    /// <summary>
    /// Retrieves an existing TCP socket client by name or creates a new one using the specified configuration.
    /// </summary>
    /// <param name="name">The unique name of the TCP socket client to retrieve or create. Cannot be null or empty.</param>
    /// <param name="valueFactory">A delegate used to configure the <see cref="SocketClientOptions"/> for the new TCP socket client if it does not
    /// already exist. This delegate is invoked only when a new client is created.</param>
    /// <returns>An instance of <see cref="ITcpSocketClient"/> corresponding to the specified name. If the client already exists,
    /// the existing instance is returned; otherwise, a new instance is created and returned.</returns>
    ITcpSocketClient GetOrCreate(string name, Action<SocketClientOptions> valueFactory);

    /// <summary>
    /// Removes the TCP socket client associated with the specified name.
    /// </summary>
    /// <param name="name">The name of the TCP socket client to remove. Cannot be <see langword="null"/> or empty.</param>
    /// <returns>The removed <see cref="ITcpSocketClient"/> instance if a client with the specified name exists;  otherwise, <see
    /// langword="null"/>.</returns>
    ITcpSocketClient? Remove(string name);
}
