// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
