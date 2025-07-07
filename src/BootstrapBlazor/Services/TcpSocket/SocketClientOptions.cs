// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Net;

namespace BootstrapBlazor.Components;

/// <summary>
/// Represents configuration options for a socket client, including buffer sizes, timeouts, and endpoints.
/// </summary>
/// <remarks>Use this class to configure various settings for a socket client, such as connection timeouts, 
/// buffer sizes, and local or remote endpoints. These options allow fine-tuning of socket behavior  to suit specific
/// networking scenarios.</remarks>
public class SocketClientOptions
{
    /// <summary>
    /// Gets or sets the size, in bytes, of the receive buffer used by the connection.
    /// </summary>
    public int ReceiveBufferSize { get; set; } = 1024 * 64;

    /// <summary>
    /// Gets or sets a value indicating whether automatic receiving data is enabled. Default is true.
    /// </summary>
    public bool IsAutoReceive { get; set; } = true;

    /// <summary>
    /// Gets or sets the timeout duration, in milliseconds, for establishing a connection.
    /// </summary>
    public int ConnectTimeout { get; set; }

    /// <summary>
    /// Gets or sets the duration, in milliseconds, to wait for a send operation to complete before timing out.
    /// </summary>
    /// <remarks>If the send operation does not complete within the specified timeout period, an exception may
    /// be thrown.</remarks>
    public int SendTimeout { get; set; }

    /// <summary>
    /// Gets or sets the amount of time, in milliseconds, that the receiver will wait for a response before timing out.
    /// </summary>
    /// <remarks>Use this property to configure the maximum wait time for receiving a response. Setting an
    /// appropriate timeout can help prevent indefinite blocking in scenarios where responses may be delayed or
    /// unavailable.</remarks>
    public int ReceiveTimeout { get; set; }

    /// <summary>
    /// Gets or sets the local endpoint for the socket client. Default value is <see cref="IPAddress.Any"/>
    /// </summary>
    /// <remarks>This property specifies the local network endpoint that the socket client will bind to when establishing a connection.</remarks>
    public IPEndPoint LocalEndPoint { get; set; } = new IPEndPoint(IPAddress.Any, 0);

    /// <summary>
    /// Gets or sets a value indicating whether logging is enabled. Default value is false.
    /// </summary>
    public bool EnableLog { get; set; }
    
    /// <summary>    
    /// Gets or sets a value indicating whether the system should automatically attempt to reconnect  after a connection is lost. Default value is false.
    /// </summary>
    public bool IsAutoReconnect { get; set; }

    /// <summary>
    /// Gets or sets the interval, in milliseconds, between reconnection attempts. Default value is 5000.
    /// </summary>
    public int ReconnectInterval { get; set; } = 5000;
}
