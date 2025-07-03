// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Provides a base implementation for adapting data packages between different systems or formats.
/// </summary>
/// <remarks>This abstract class serves as a foundation for implementing custom data package adapters. It defines
/// common methods for sending, receiving, and handling data packages, as well as a property for accessing the
/// associated data package handler. Derived classes should override the virtual methods to provide specific behavior
/// for handling data packages.</remarks>
public abstract class DataPackageAdapterBase<TEntity> : DataPackageAdapter
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="data"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    public abstract bool TryConvertTo(ReadOnlyMemory<byte> data, [NotNullWhen(true)] out TEntity? entity);
}
