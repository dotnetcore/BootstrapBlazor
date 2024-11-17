// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 模型比较器
/// </summary>
/// <typeparam name="TItem"></typeparam>
public class HashSetComparer<TItem>(IModelEqualityComparer<TItem> comparer) : IEqualityComparer<TItem>
{
    /// <summary>
    /// Equals 方法
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool Equals(TItem? x, TItem? y) => comparer.Equals(x, y);

    /// <summary>
    /// GetHashCode 方法
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public int GetHashCode([DisallowNull] TItem obj)
    {
        var keyValue = Utility.GetKeyValue<TItem, object>(obj, comparer.CustomKeyAttribute);
        return keyValue?.GetHashCode() ?? obj.GetHashCode();
    }
}
