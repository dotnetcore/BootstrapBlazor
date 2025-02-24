﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 泛型实现类
/// </summary>
public class SelectedItem<T>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public SelectedItem() { Value = default!; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="value"></param>
    /// <param name="text"></param>
    public SelectedItem(T value, string text)
    {
        Value = value;
        Text = text;
    }

    /// <summary>
    /// 获得/设置 显示名称
    /// </summary>
    public string Text { get; set; } = "";

    /// <summary>
    /// 获得/设置 选项值
    /// </summary>
    public T Value { get; set; }

    /// <summary>
    /// 获得/设置 是否选中
    /// </summary>
    public bool Active { get; set; }

    /// <summary>
    /// 获得/设置 是否禁用
    /// </summary>
    public bool IsDisabled { get; set; }

    /// <summary>
    /// 获得/设置 分组名称
    /// </summary>
    public string GroupName { get; set; } = "";
}
