// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class SocketDataTypeConverterAttribute : Attribute
{
    /// <summary>
    /// Gets or sets the type of the <see cref="ISocketDataConverter{TEntity}"/>.
    /// </summary>
    public Type? Type { get; set; }
}
