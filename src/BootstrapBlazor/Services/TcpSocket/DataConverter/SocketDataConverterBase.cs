// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Provides a base class for converting socket data into a specified entity type.
/// </summary>
/// <typeparam name="TEntity">The type of entity to convert the socket data into.</typeparam>
public abstract class SocketDataConverterBase<TEntity> : ISocketDataConverter<TEntity>
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="data"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    public abstract bool TryConvertTo(ReadOnlyMemory<byte> data, [NotNullWhen(true)] out TEntity? entity);
}
