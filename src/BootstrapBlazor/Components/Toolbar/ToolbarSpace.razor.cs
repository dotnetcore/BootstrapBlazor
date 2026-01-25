// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ToolbarSpace 组件用于在工具栏中添加空白空间</para>
/// <para lang="en">ToolbarSpace Component for adding whitespace in the toolbar</para>
/// </summary>
public partial class ToolbarSpace
{
    private string? ClassString => CssBuilder.Default("bb-toolbar-space")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
}

