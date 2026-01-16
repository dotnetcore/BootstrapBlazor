// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ISelect 接口</para>
/// <para lang="en">ISelect Interface</para>
/// </summary>
public interface ISelectGeneric<TValue>
{
    /// <summary>
    /// <para lang="zh">增加 SelectedItem 项方法</para>
    /// <para lang="en">Add SelectedItem Method</para>
    /// </summary>
    /// <param name="item"></param>
    void Add(SelectedItem<TValue> item);
}
