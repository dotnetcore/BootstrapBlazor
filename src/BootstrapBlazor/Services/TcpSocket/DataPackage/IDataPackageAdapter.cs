// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Defines an adapter for handling and transmitting data packages to a target destination.
/// </summary>
/// <remarks>This interface provides methods for sending data asynchronously and configuring a data handler.
/// Implementations of this interface are responsible for managing the interaction between the caller and the underlying
/// data transmission mechanism.</remarks>
public interface IDataPackageAdapter
{
    /// <summary>
    /// Gets or sets the callback function to be invoked when data is received.
    /// </summary>
    /// <remarks>The callback function is expected to handle the received data asynchronously. Ensure that the
    /// implementation of the callback does not block the calling thread and completes promptly to avoid performance
    /// issues.</remarks>
    Func<ReadOnlyMemory<byte>, ValueTask>? ReceivedCallBack { get; set; }

    /// <summary>
    /// Gets the handler responsible for processing data packages.
    /// </summary>
    IDataPackageHandler? DataPackageHandler { get; }

    /// <summary>
    /// Asynchronously receives data from a source and processes it.
    /// </summary>
    /// <remarks>This method does not return any result directly. It is intended for scenarios where data is received
    /// and processed asynchronously. Ensure that the <paramref name="data"/> parameter contains valid data before calling
    /// this method.</remarks>
    /// <param name="data">A read-only memory region containing the data to be received. The caller must ensure the memory is valid and
    /// populated.</param>
    /// <param name="token">An optional cancellation token that can be used to cancel the operation. Defaults to <see langword="default"/> if
    /// not provided.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation. The task completes when the data has been
    /// successfully received and processed.</returns>
    ValueTask HandlerAsync(ReadOnlyMemory<byte> data, CancellationToken token = default);

    /// <summary>
    /// Attempts to convert the specified binary data into an object representation.
    /// </summary>
    /// <remarks>This method does not throw an exception if the conversion fails. Instead, it returns  <see
    /// langword="false"/> and sets <paramref name="entity"/> to <see langword="null"/>.</remarks>
    /// <param name="data">The binary data to be converted. Must not be empty.</param>
    /// <param name="entity">When this method returns <see langword="true"/>, contains the converted object.  When this method returns <see
    /// langword="false"/>, contains <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if the conversion was successful; otherwise, <see langword="false"/>.</returns>
    bool TryConvertTo(ReadOnlyMemory<byte> data, [NotNullWhen(true)] out object? entity);
}
