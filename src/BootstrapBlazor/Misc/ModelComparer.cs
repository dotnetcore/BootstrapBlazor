// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 模型比较器
/// </summary>
/// <typeparam name="TItem"></typeparam>
public class ModelComparer<TItem> : IEqualityComparer<TItem>
{
    private readonly Func<TItem, TItem, bool> _comparer;
    /// <summary>
    /// 构造函数
    /// </summary>
    public ModelComparer(Func<TItem, TItem, bool> comparer)
    {
        _comparer = comparer;
    }

    /// <summary>
    /// Equals 方法
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public bool Equals(TItem? x, TItem? y) => x is not null && y is not null
        ? _comparer(x, y)
        : x is null && y is null;

    /// <summary>
    /// GetHashCode 方法
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public int GetHashCode([DisallowNull] TItem obj) => obj.GetHashCode();
}

internal static class ModelComparer
{
    public static bool? EqualityComparer<TItem>(TItem x, TItem y)
    {
        bool? ret = null;
        if (x is IEqualityComparer<TItem> comparer)
        {
            ret = comparer.Equals(x, y);
        }
        return ret;
    }
}
