// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ExportPdfButtonSettings 配置类</para>
/// <para lang="en">ExportPdfButtonSettings configuration class</para>
/// </summary>
public class ExportPdfButtonSettings : ComponentBase
{
    /// <summary>
    /// <para lang="zh">获得/设置 ExportPdfButtonOptions 参数配置类实例</para>
    /// <para lang="en">Gets or sets the ExportPdfButtonOptions parameter configuration class instance</para>
    /// </summary>
    [Parameter, NotNull, EditorRequired]
    public ExportPdfButtonOptions? Options { get; set; }

    [CascadingParameter]
    private Button? Button { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
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
