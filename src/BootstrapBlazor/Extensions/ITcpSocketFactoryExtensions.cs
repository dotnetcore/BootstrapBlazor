// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Runtime.Versioning;

namespace BootstrapBlazor.Components;

/// <summary>
/// <see cref="ITcpSocketFactory"/> 扩展方法类
/// </summary>
[UnsupportedOSPlatform("browser")]
public static class ITcpSocketFactoryExtensions
{
    /// <summary>
    /// Retrieves an existing TCP socket client by name or creates a new one using the specified IP address and port.
    /// </summary>
    /// <param name="factory">The <see cref="ITcpSocketFactory"/> instance used to manage TCP socket clients.</param>
    /// <param name="name">The unique name identifying the TCP socket client. Cannot be null or empty.</param>
    /// <param name="ipAddress">The IP address of the endpoint to connect to. Must be a valid IPv4 or IPv6 address.</param>
    /// <param name="port">The port number of the endpoint to connect to. Must be within the range 0 to 65535.</param>
    /// <returns>An <see cref="ITcpSocketClient"/> instance representing the TCP socket client associated with the specified
    /// name. If no client exists with the given name, a new client is created and returned.</returns>
    public static ITcpSocketClient GetOrCreate(this ITcpSocketFactory factory, string name, string ipAddress, int port)
    {
        var endPoint = Utility.ConvertToIpEndPoint(ipAddress, port);
        return factory.GetOrCreate(name, endPoint);
    }
}
