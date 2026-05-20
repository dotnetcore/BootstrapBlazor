// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// JitViewer 组件示例
/// </summary>
public partial class JitViewers : ComponentBase
{
    [Inject, NotNull]
    private IStringLocalizer<JitViewers>? Localizer { get; set; }

    private readonly List<SelectedItem> _docs =
    [
        new SelectedItem("./samples/sample.docx", "sample.docx"),
        new SelectedItem("./samples/sample.xlsx", "sample.xlsx"),
        new SelectedItem("./samples/sample.pptx", "sample.pptx"),
        new SelectedItem("./samples/sample.pdf", "sample.pdf"),
        new SelectedItem("./samples/ebook.pdf", "ebook.pdf"),
        new SelectedItem("./samples/sample.txt", "sample.txt"),
        new SelectedItem("./samples/sample.csv", "sample.csv"),
        new SelectedItem("./samples/sample.md", "sample.md"),
        new SelectedItem("./samples/sample.css", "sample.css"),
        new SelectedItem("./samples/sample.js", "sample.js"),
        new SelectedItem("./samples/sample.cs", "sample.cs"),
        new SelectedItem("./samples/sample.png", "sample.png"),
        new SelectedItem("https://cdn.plyr.io/static/demo/View_From_A_Blue_Moon_Trailer-576p.mp4", "sample.mp4"),
        new SelectedItem("./samples/sample.dxf", "sample.dxf"),
        new SelectedItem("./samples/sample.ofd", "sample.ofd")
    ];

    private string _doc = "./samples/sample.docx";
}
