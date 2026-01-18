// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">PrintButton 打印按钮</para>
/// <para lang="en">PrintButton Component</para>
/// </summary>
public partial class PrintButton
{
    /// <summary>
    /// <para lang="zh">获得/设置 预览模板地址 默认为空</para>
    /// <para lang="en">Gets or sets Preview template URL. Default empty</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? PreviewUrl { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<PrintButton>? Localizer { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Text ??= Localizer[nameof(Text)];
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Icon ??= IconTheme.GetIconByKey(ComponentIcons.PrintButtonIcon);
    }
}
