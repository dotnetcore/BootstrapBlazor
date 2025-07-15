// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Represents an attribute used to mark a field as a socket data field.
/// </summary>
/// <remarks>This attribute can be applied to fields to indicate that they are part of the data transmitted over a
/// socket connection. It is intended for use in scenarios where socket communication requires specific fields to be
/// identified for processing.</remarks>
[AttributeUsage(AttributeTargets.Property)]
public class SocketDataFieldAttribute : Attribute
{
    /// <summary>
    /// 
    /// </summary>
    public Type? Type { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Start { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? EncodingName { get; set; }
}
