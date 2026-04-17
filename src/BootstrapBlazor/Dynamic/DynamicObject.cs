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
    public Guid DynamicObjectPrimaryKey { get; set; }

    /// <summary>
    /// <para lang="zh">获得指定属性值方法</para>
    /// <para lang="en">Gets the value of a specified property</para>
    /// </summary>
    /// <param name="propertyName"></param>
    public virtual object? GetValue(string propertyName) => null;

    /// <summary>
    /// <para lang="zh">给指定属性设置值方法</para>
    /// <para lang="en">Sets the value of a specified property</para>
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="value"></param>
    public virtual void SetValue(string propertyName, object? value) { }
}
