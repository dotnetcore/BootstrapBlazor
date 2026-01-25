// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">PlaceHolderAttribute 类，用于定义属性的占位符</para>
/// <para lang="en">PlaceHolderAttribute class used to define a placeholder for a property</para>
/// </summary>
/// <param name="placeholder"><para lang="zh">占位符文本</para><para lang="en">The placeholder text</para></param>
[AttributeUsage(AttributeTargets.Property)]
public class PlaceHolderAttribute(string placeholder) : Attribute
{
    /// <summary>
    /// <para lang="zh">获得 占位符文本</para>
    /// <para lang="en">Gets the placeholder text</para>
    /// </summary>
    public string Text => placeholder;
}
