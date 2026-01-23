// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">模型比较器</para>
/// <para lang="en">Model comparer</para>
/// </summary>
/// <typeparam name="TItem"></typeparam>
public class ModelHashSetComparer<TItem>(IModelEqualityComparer<TItem> comparer) : IEqualityComparer<TItem>
{
    /// <summary>
    /// <para lang="zh">Equals 方法</para>
    /// <para lang="en">Equals method</para>
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public bool Equals(TItem? x, TItem? y) => comparer.Equals(x, y);

    /// <summary>
    /// <para lang="zh">GetHashCode 方法</para>
    /// <para lang="en">GetHashCode method</para>
    /// </summary>
    /// <param name="obj"></param>
    public int GetHashCode([DisallowNull] TItem obj)
    {
        var keyValue = Utility.GetKeyValue<TItem, object>(obj, comparer.CustomKeyAttribute);
        return keyValue?.GetHashCode() ?? obj.GetHashCode();
    }
}
