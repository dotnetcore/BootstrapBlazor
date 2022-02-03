// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 动态类型实体类 <see cref="IDynamicObject" /> 实例
/// </summary>
public class DynamicObject : IDynamicObject
{
    /// <summary>
    /// 
    /// </summary>
    [AutoGenerateColumn(Ignore = true)]
    public Guid DynamicObjectPrimaryKey { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    public virtual object? GetValue(string propertyName) => Utility.GetPropertyValue(this, propertyName);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="value"></param>
    public virtual void SetValue(string propertyName, object? value) => Utility.SetPropertyValue<object, object?>(this, propertyName, value);
}
