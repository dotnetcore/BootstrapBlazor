// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 动态类型实体类 <see cref="IDynamicObject" /> 实例
/// </summary>
public class DynamicObject : IDynamicObject
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [AutoGenerateColumn(Ignore = true)]
    public Guid DynamicObjectPrimaryKey { get; set; }

    /// <summary>
    /// 获得指定属性值方法
    /// </summary>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    public virtual object? GetValue(string propertyName) => Utility.GetPropertyValue(this, propertyName);

    /// <summary>
    /// 给指定属性设置值方法
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="value"></param>
    public virtual void SetValue(string propertyName, object? value) => Utility.SetPropertyValue<object, object?>(this, propertyName, value);
}
