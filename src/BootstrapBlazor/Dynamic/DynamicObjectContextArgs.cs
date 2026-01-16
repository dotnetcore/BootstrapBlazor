// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">DynamicObjectContextArgs 类</para>
/// <para lang="en">DynamicObjectContextArgs 类</para>
/// </summary>
public class DynamicObjectContextArgs
{
    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    public DynamicObjectContextArgs(IEnumerable<IDynamicObject> items, DynamicItemChangedType changedType = DynamicItemChangedType.Add)
    {
        Items = items;
        ChangedType = changedType;
    }

    /// <summary>
    /// <para lang="zh">获得 编辑数据集合</para>
    /// <para lang="en">Gets 编辑datacollection</para>
    /// </summary>
    public IEnumerable<IDynamicObject> Items { get; }

    /// <summary>
    /// <para lang="zh">获得 数据改变类型 默认 Add</para>
    /// <para lang="en">Gets data改变type Default is Add</para>
    /// </summary>
    public DynamicItemChangedType ChangedType { get; }
}
