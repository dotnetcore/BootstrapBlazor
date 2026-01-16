// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">JSModuleNotInheritedAttribute <see cref="JSModuleAutoLoaderAttribute"/></para>
///  <para lang="en">JSModuleNotInheritedAttribute <see cref="JSModuleAutoLoaderAttribute"/></para>
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class JSModuleNotInheritedAttribute : Attribute
{
    // <para lang="zh">增加 sealed 关键字防止二开写派生类导致 type.GetCustomAttribute&lt;JSModuleNotInheritedAttribute&gt;() 报错</para>
    // <para lang="zh">BootstrapModuleComponentBase 类 OnLoadJSModule 方法</para>
    // <para lang="en">Add sealed keyword to prevent derived classes from causing type.GetCustomAttribute&lt;JSModuleNotInheritedAttribute&gt;() error</para>
    // <para lang="en">BootstrapModuleComponentBase class OnLoadJSModule method</para>
}
