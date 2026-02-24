// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">编辑弹窗配置类</para>
/// <para lang="en">Edit Dialog Settings Class</para>
/// </summary>
public class EditDialogSettings
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否显示关闭弹窗确认弹窗。默认为 null 未配置</para>
    /// <para lang="en">Gets or sets whether to show the close confirm dialog. Default is null</para>
    /// </summary>
    public bool? ShowConfirmCloseSwal { get; set; }
}
