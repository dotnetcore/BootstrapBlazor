// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <see cref="SelectedItem"/> 泛型实现类
/// </summary>
public class SelectedItem<T> : SelectedItem
{
    /// <summary>
    /// 获得/设置 泛型值
    /// </summary>
    public new T? Value { get; set; }
}
