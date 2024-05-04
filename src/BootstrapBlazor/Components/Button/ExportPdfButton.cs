// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 导出 Pdf 按钮
/// </summary>
public class ExportPdfButton : Button
{
    /// <summary>
    /// 获得/设置 导出 Pdf 选择器 默认为 null
    /// </summary>
    [Parameter]
    public string? Selector { get; set; }

    /// <summary>
    /// 获得/设置 导出 Pdf 元素 Id 默认为 null 
    /// </summary>
    [Parameter]
    public string? ElementId { get; set; }

    /// <summary>
    /// 获得/设置 导出 Pdf 所需样式表文件集合 默认为 null
    /// </summary>
    [Parameter]
    public List<string>? StyleTags { get; set; }

    /// <summary>
    /// 获得/设置 导出 Pdf 所需脚本文件集合 默认为 null
    /// </summary>
    [Parameter]
    public List<string>? ScriptTags { get; set; }

    /// <summary>
    /// 获得/设置 导出 Pdf 文件名 默认为 null 未设置时使用 pdf-时间戳.pdf
    /// </summary>
    [Parameter]
    public string? FileName { get; set; }

    /// <summary>
    /// 获得/设置 导出 Pdf 之前回调委托 默认为 null
    /// </summary>
    [Parameter]
    public Func<Task>? OnBeforeExport { get; set; }

    /// <summary>
    /// 获得/设置 下载 Pdf 之前回调委托 默认为 null
    /// </summary>
    [Parameter]
    public Func<Stream, Task>? OnBeforeDownload { get; set; }

    /// <summary>
    /// 获得/设置 下载 Pdf 之后回调委托 默认为 null
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnAfterDownload { get; set; }

    /// <summary>
    /// 获得/设置 是否自动下载 Pdf 默认为 false
    /// </summary>
    [Parameter]
    public bool AutoDownload { get; set; }

    [Inject, NotNull]
    private IHtml2Pdf? Html2PdfService { get; set; }

    [Inject, NotNull]
    private NavigationManager? NavigationManager { get; set; }

    [Inject, NotNull]
    private DownloadService? DownloadService { get; set; }

    private JSModule? _getHtmlModule;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Icon ??= IconTheme.GetIconByKey(ComponentIcons.TableExportPdfIcon);
        ButtonIcon = Icon;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task HandlerClick()
    {
        await base.HandlerClick();

        if (OnBeforeExport != null)
        {
            await OnBeforeExport();
        }

        _getHtmlModule ??= await JSRuntime.LoadUtility();
        var html = await _getHtmlModule.GetHtml(ElementId, Selector);
        if (!string.IsNullOrEmpty(html))
        {
            // 通过模板生成完整的 Html
            var htmlString = $"""
                <!DOCTYPE html>

                <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
                <head>
                    <meta charset="utf-8" />
                </head>
                <body class="p-3">
                    {html}
                </body>
                </html>
                """;

            // 增加网页所需样式表文件
            List<string> styles = [$"{NavigationManager.BaseUri}_content/BootstrapBlazor/css/bootstrap.blazor.bundle.min.css"];
            if (StyleTags != null)
            {
                styles.AddRange(StyleTags);
            }

            // 增加网页所需脚本文件
            var scripts = ScriptTags ?? [];

            // 生成 Pdf 流
            using var stream = await Html2PdfService.PdfStreamFromHtmlAsync(htmlString, styles, scripts);

            if (OnBeforeDownload != null)
            {
                await OnBeforeDownload(stream);
            }

            if (AutoDownload)
            {
                // 下载 Pdf 文件
                var downloadFileName = FileName ?? $"pdf-{DateTime.Now:yyyyMMddHHmmss}.pdf";
                await DownloadService.DownloadFromStreamAsync(downloadFileName, stream);

                if (OnAfterDownload != null)
                {
                    await OnAfterDownload(downloadFileName);
                }
            }
        }
    }

    /// <summary>
    /// 设置配置方法
    /// </summary>
    /// <param name="options"></param>
    internal void SetOptions(ExportPdfButtonOptions options)
    {
        Color = options.Color;
        Text = options.Text;
        Icon = options.Icon;
        ElementId = options.ElementId;
        Selector = options.Selector;
        StyleTags = options.StyleTags;
        ScriptTags = options.ScriptTags;
        FileName = options.FileName;
        AutoDownload = options.AutoDownload;
        OnBeforeExport = options.OnBeforeExport;
        OnBeforeDownload = options.OnBeforeDownload;
        OnAfterDownload = options.OnAfterDownload;
        IsAsync = options.IsAsync;
    }
}
