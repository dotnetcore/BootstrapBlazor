// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// PlaceHolderAttribute class used to define a placeholder for a property.
/// </summary>
/// <param name="placeholder">The placeholder text.</param>
[AttributeUsage(AttributeTargets.Property)]
public class PlaceHolderAttribute(string placeholder) : Attribute
{
    /// <summary>
    /// Gets the placeholder text.
    /// </summary>
    public string Text { get; } = placeholder;
}
