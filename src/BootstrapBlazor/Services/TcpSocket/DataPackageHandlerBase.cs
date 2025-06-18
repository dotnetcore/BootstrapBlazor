// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Provides a base implementation for handling data packages in a communication system.
/// </summary>
/// <remarks>This abstract class defines the core contract for receiving and sending data packages. Derived
/// classes should override and extend its functionality to implement specific data handling logic. The default
/// implementation simply returns the provided data.</remarks>
public abstract class DataPackageHandlerBase : IDataPackageHandler
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public Task<Memory<byte>> ReceiveAsync(Memory<byte> data)
    {
        return Task.FromResult(data);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public Task<Memory<byte>> SendAsync(Memory<byte> data)
    {
        return Task.FromResult(data);
    }
}
