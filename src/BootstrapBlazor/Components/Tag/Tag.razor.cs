// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Tag 组件类</para>
/// <para lang="en">Tag component类</para>
/// </summary>
public partial class Tag
{
    /// <summary>
    /// <para lang="zh">获得 样式集合</para>
    /// <para lang="en">Gets stylecollection</para>
    /// </summary>
    /// <returns></returns>
    private string? ClassName => CssBuilder.Default("tag fade show")
        .AddClass($"alert-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private async Task OnClick()
    {
        if (OnDismiss != null) await OnDismiss();
    }
}
