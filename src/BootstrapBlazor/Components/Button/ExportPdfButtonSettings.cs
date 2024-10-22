// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// ExportPdfButtonSettings 配置类
/// </summary>
public class ExportPdfButtonSettings : ComponentBase
{
    /// <summary>
    /// 获得/设置 ExportPdfButtonOptions 参数配置类实例
    /// </summary>
    [Parameter, NotNull, EditorRequired]
    public ExportPdfButtonOptions? Options { get; set; }

    [CascadingParameter]
    private Button? Button { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (Button is ExportPdfButton pdfButton)
        {
            pdfButton.SetOptions(Options);
        }
    }
}
