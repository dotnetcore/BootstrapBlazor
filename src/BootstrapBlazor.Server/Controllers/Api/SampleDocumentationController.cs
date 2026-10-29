// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Controllers.Api;

/// <summary>
/// Serves documentation without exposing server file paths to WebAssembly.
/// </summary>
[ApiController]
public class SampleDocumentationController(ISampleDocumentationService documentation, IOptions<WebsiteOptions> options) : ControllerBase
{
    private static readonly Type[] ExtensionDocumentationTypes =
    [
        typeof(SortableList),
        typeof(SortableOption),
        typeof(SortableEvent),
        typeof(SortableListItem),
        typeof(Splitting),
        typeof(BarcodeGenerator),
        typeof(BarcodeGeneratorOption),
        typeof(OnScreenKeyboard),
        typeof(KeyboardOption),
        typeof(QRCode),
        typeof(SelectProvince),
        typeof(SelectCity),
        typeof(SelectRegion),
        typeof(Mermaid),
        typeof(MouseFollower),
        typeof(BarcodeReader),
        typeof(DriverJs),
        typeof(SmilesDrawer),
        typeof(TreeViewItem<TreeFoo>),
        typeof(ImageCropper),
        typeof(CodeEditor),
        typeof(Editor),
        typeof(CherryMarkdown),
        typeof(BootstrapBlazor.Components.Markdown),
        typeof(MindMap),
        typeof(Vditor),
        typeof(VditorOptions),
        typeof(VditorMode),
        typeof(VditorIconStyle),
        typeof(Gantt),
        typeof(EmbedPDF),
        typeof(JitViewer),
        typeof(PdfViewer),
        typeof(Live2DDisplay),
        typeof(VideoPlayer),
        typeof(Player),
        typeof(WinBoxOption),
        typeof(PdfReader),
        typeof(UniverSheet),
        typeof(Term),
        typeof(TermOptions),
        typeof(Meet),
        typeof(Topology),
        typeof(EditDialogOption<>),
        typeof(SearchDialogOption<>),
        typeof(RDKit),
        typeof(Chart),
        typeof(DockView),
        typeof(DockContent),
        typeof(DockComponent),
        typeof(OfficeViewer)
    ];

    /// <summary>
    /// Gets localized source for a configured sample.
    /// </summary>
    [HttpGet("api/sample-documentation/code")]
    public async Task<IActionResult> Code(string codeFile)
    {
        var key = codeFile.Split('.')[0];
        if ((codeFile != $"{key}.razor" && codeFile != $"{key}.razor.cs")
            || !options.Value.SourceCodes.ContainsKey(key.ToLowerInvariant()))
        {
            return NotFound();
        }

        return Content(await documentation.GetCodeAsync(codeFile), "text/plain");
    }

    /// <summary>
    /// Gets an allowlisted source file for a tutorial download bundle.
    /// </summary>
    [HttpGet("api/sample-documentation/source")]
    public async Task<IActionResult> Source(string fileName)
    {
        if (!TutorialSourceFiles.Contains(fileName))
        {
            return NotFound();
        }

        try
        {
            return Content(await documentation.GetFileContentAsync(fileName), "text/plain");
        }
        catch (FileNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Gets localized parameter documentation for a library component or supported sample options.
    /// </summary>
    [HttpGet("api/sample-documentation/attributes")]
    public async Task<IActionResult> Attributes(string typeName)
    {
        var type = typeof(BootstrapBlazorRoot).Assembly.GetType(typeName)
            ?? ExtensionDocumentationTypes.FirstOrDefault(t => t.FullName == typeName);
        if (type == null || !type.IsPublic
            || (!typeof(IComponent).IsAssignableFrom(type)
                && type != typeof(CountUpOption)
                && type != typeof(NotificationItem)
                && type != typeof(MessageOption)
                && type != typeof(ConsoleMessageItem)
                && type != typeof(TimelineItem)
                && type != typeof(TableCellArgs)
                && !ExtensionDocumentationTypes.Contains(type)))
        {
            return NotFound();
        }

        return Ok(await documentation.GetAttributesAsync(type));
    }
}
