// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// DynamicObjectContextArgs 类
/// </summary>
public class DynamicObjectContextArgs
{
    /// <summary>
    /// 
    /// </summary>
    public DynamicObjectContextArgs(IEnumerable<IDynamicObject> items, DynamicItemChangedType changedType = DynamicItemChangedType.Add)
    {
        Items = items;
        ChangedType = changedType;
    }

    /// <summary>
    /// 获得 编辑数据集合
    /// </summary>
    public IEnumerable<IDynamicObject> Items { get; }

    /// <summary>
    /// 获得 数据改变类型 默认 Add
    /// </summary>
    public DynamicItemChangedType ChangedType { get; }
}
