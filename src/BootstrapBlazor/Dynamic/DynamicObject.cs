// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">动态类型实体类 <see cref="IDynamicObject" /> 实例</para>
///  <para lang="en">动态type实体类 <see cref="IDynamicObject" /> instance</para>
/// </summary>
public class DynamicObject : IDynamicObject
{
    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    [AutoGenerateColumn(Ignore = true)]
    public Guid DynamicObjectPrimaryKey { get; set; }

    /// <summary>
    ///  <para lang="zh">获得指定属性值方法</para>
    ///  <para lang="en">Gets指定property值方法</para>
    /// </summary>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    public virtual object? GetValue(string propertyName) => Utility.GetPropertyValue(this, propertyName);

    /// <summary>
    ///  <para lang="zh">给指定属性设置值方法</para>
    ///  <para lang="en">给指定propertySets值方法</para>
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="value"></param>
    public virtual void SetValue(string propertyName, object? value) => Utility.SetPropertyValue<object, object?>(this, propertyName, value);
}
