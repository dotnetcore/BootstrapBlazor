// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
