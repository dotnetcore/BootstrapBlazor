// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// ToolbarSeparator 组件用于在工具栏中添加分隔符
/// </summary>
public partial class ToolbarSeparator
{
    private string? ClassString => CssBuilder.Default("bb-toolbar-vr vr")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
}
