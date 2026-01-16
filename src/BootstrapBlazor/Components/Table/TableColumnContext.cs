// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">TableColumn 上下文类
///</para>
/// <para lang="en">TableColumn 上下文类
///</para>
/// </summary>
/// <param name="model"></param>
/// <param name="value"></param>
public class TableColumnContext<TItem, TValue>(TItem model, TValue value)
{
    /// <summary>
    /// <para lang="zh">获得/设置 行数据实例
    ///</para>
    /// <para lang="en">Gets or sets 行datainstance
    ///</para>
    /// </summary>
    [NotNull]
    public TItem Row { get; } = model ?? throw new ArgumentNullException(nameof(model));

    /// <summary>
    /// <para lang="zh">获得/设置 当前绑定字段数据实例
    ///</para>
    /// <para lang="en">Gets or sets 当前绑定字段datainstance
    ///</para>
    /// </summary>
    public TValue Value => value;
}
