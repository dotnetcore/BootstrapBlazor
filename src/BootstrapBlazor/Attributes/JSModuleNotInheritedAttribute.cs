// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">表示该类不继承 JSModuleAutoLoaderAttribute 的特性</para>
/// <para lang="en">Indicates that this class does not inherit the JSModuleAutoLoaderAttribute</para>
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class JSModuleNotInheritedAttribute : Attribute
{

}
