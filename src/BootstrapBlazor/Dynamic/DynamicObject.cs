// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh"><see cref="IDynamicObject"/> 实现类</para>
/// <para lang="en"><see cref="IDynamicObject"/> implementation class</para>
/// </summary>
public class DynamicObject : IDynamicObject
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [AutoGenerateColumn(Ignore = true)]
    public Guid DynamicObjectPrimaryKey { get; set; } = Guid.NewGuid();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public virtual object? GetValue(string propertyName) => Utility.GetPropertyValue(this, propertyName);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public virtual void SetValue(string propertyName, object? value) => Utility.SetPropertyValue<object, object?>(this, propertyName, value);
}
