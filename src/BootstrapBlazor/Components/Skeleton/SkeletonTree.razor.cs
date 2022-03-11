// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 树状组件骨架屏组件
/// </summary>
public partial class SkeletonTree
{
    private string? TreeClassString => CssBuilder.Default("skeleton tree")
        .AddClass(ClassString)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
}
