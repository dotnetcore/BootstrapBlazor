// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// JSModuleNotInheritedAttribute <see cref="JSModuleAutoLoaderAttribute"/>
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class JSModuleNotInheritedAttribute : Attribute
{
    // 增加 sealed 关键字防止二开写派生类导致 type.GetCustomAttribute<JSModuleNotInheritedAttribute>() 报错
    // BootstrapModuleComponentBase 类 OnLoadJSModule 方法
}
