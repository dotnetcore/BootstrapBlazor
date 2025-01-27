﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// DropUpload 组件
/// </summary>
public partial class DropUpload<TValue>
{
    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public RenderFragment? BodyTemplate { get; set; }

    private string? DropUploadClassString => CssBuilder.Default(ClassString)
        .AddClass("is-drag")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
}
