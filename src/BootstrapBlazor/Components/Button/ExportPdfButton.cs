// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">导出 Pdf 按钮</para>
/// <para lang="en">Export PDF button</para>
/// </summary>
public class ExportPdfButton : Button
{
    /// <summary>
    /// <para lang="zh">获得/设置 导出 Pdf 选择器 默认为 null</para>
    /// <para lang="en">Gets or sets the export PDF selector. Default is null</para>
    /// </summary>
    [Parameter]
    public string? Selector { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 导出 Pdf 元素 Id 默认为 null </para>
    /// <para lang="en">Gets or sets the export PDF element Id. Default is null</para>
    /// </summary>
    [Parameter]
    public string? ElementId { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 导出 Pdf 所需样式表文件集合 默认为 null</para>
    /// <para lang="en">Gets or sets the export PDF properties style tags. Default is null</para>
    /// </summary>
    [Parameter]
    public List<string>? StyleTags { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 导出 Pdf 所需脚本文件集合 默认为 null</para>
    /// <para lang="en">Gets or sets the export PDF properties script tags. Default is null</para>
    /// </summary>
    [Parameter]
    public List<string>? ScriptTags { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 导出 Pdf 文件名 默认为 null 未设置时使用 pdf-时间戳.pdf</para>
    /// <para lang="en">Gets or sets the export PDF file name. Default is null (uses pdf-timestamp.pdf)</para>
    /// </summary>
    [Parameter]
    public string? FileName { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 导出 Pdf 之前回调委托 默认为 null</para>
    /// <para lang="en">Gets or sets the callback delegate before export PDF. Default is null</para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnBeforeExport { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 下载 Pdf 之前回调委托 默认为 null</para>
    /// <para lang="en">Gets or sets the callback delegate before download PDF. Default is null</para>
    /// </summary>
    [Parameter]
    public Func<Stream, Task>? OnBeforeDownload { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 下载 Pdf 之后回调委托 默认为 null</para>
    /// <para lang="en">Gets or sets the callback delegate after download PDF. Default is null</para>
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnAfterDownload { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否自动下载 Pdf 默认为 true</para>
    /// <para lang="en">Gets or sets whether to auto download PDF. Default is true</para>
    /// </summary>
    [Parameter]
    public bool AutoDownload { get; set; } = true;

    [Inject, NotNull]
    private IHtml2Pdf? Html2PdfService { get; set; }

    [Inject, NotNull]
    private NavigationManager? NavigationManager { get; set; }

    [Inject, NotNull]
    private DownloadService? DownloadService { get; set; }

    private JSModule? _getHtmlModule;

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Icon ??= IconTheme.GetIconByKey(ComponentIcons.TableExportPdfIcon);
    }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
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
            // <para lang="zh">通过模板生成完整的 Html</para>
            // <para lang="en">Generate complete HTML via template</para>
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

            // <para lang="zh">增加网页所需样式表文件</para>
            // <para lang="en">Add style tags required by the page</para>
            List<string> styles = [
                $"{NavigationManager.BaseUri}_content/BootstrapBlazor.FontAwesome/css/font-awesome.min.css",
                $"{NavigationManager.BaseUri}_content/BootstrapBlazor/css/bootstrap.blazor.bundle.min.css"
            ];
            if (StyleTags != null)
            {
                styles.AddRange(StyleTags);
            }

            // <para lang="zh">增加网页所需脚本文件</para>
            // <para lang="en">Add script tags required by the page</para>
            var scripts = ScriptTags ?? [];

            // <para lang="zh">生成 Pdf 流</para>
            // <para lang="en">Generate PDF stream</para>
            using var stream = await Html2PdfService.PdfStreamFromHtmlAsync(htmlString, styles, scripts);

            if (OnBeforeDownload != null)
            {
                await OnBeforeDownload(stream);
            }

            if (AutoDownload)
            {
                // <para lang="zh">下载 Pdf 文件</para>
                // <para lang="en">Download PDF file</para>
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
    /// <para lang="zh">设置配置方法</para>
    /// <para lang="en">Set configuration method</para>
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
