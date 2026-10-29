// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Samples;

/// <summary>
/// <para lang="zh">Modal PDF 导出示例。</para>
/// <para lang="en">Modal PDF export sample.</para>
/// </summary>
public sealed partial class ModalPdf
{
    [Inject]
    [NotNull]
    private IStringLocalizer<ModalPdf>? Localizer { get; set; }

    [NotNull]
    private Modal? ScrollModal { get; set; }
}
