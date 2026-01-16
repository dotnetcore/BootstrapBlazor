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

}
