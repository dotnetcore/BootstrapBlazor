// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// PlaceHolderAttribute 占位符标签类
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class PlaceHolderAttribute : Attribute
{
    /// <summary>
    /// 获得 Text 属性
    /// </summary>
    public string Text { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="placeholder"></param>
    public PlaceHolderAttribute(string placeholder)
    {
        Text = placeholder;
    }
}
