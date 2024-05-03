// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class ExportPdfButtonTest : ExportPdfTestBase
{
    [Fact]
    public async Task Normal_Ok()
    {
        Context.JSInterop.Setup<string?>("getHtml", v => true).SetResult("test-html-result");
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<ExportPdfButton>(pb =>
            {
                pb.Add(p => p.Text, "Export Pdf");
                pb.Add(p => p.ElementId, "test-id");
                pb.Add(p => p.Selector, ".modal-body");
            });
        });
        var button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());
    }

    [Fact]
    public async Task Property_Ok()
    {
        Context.JSInterop.Setup<string?>("getHtml", v => true).SetResult("test-html-result");
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<ExportPdfButton>(pb =>
            {
                pb.Add(p => p.Text, "Export Pdf");
                pb.Add(p => p.Icon, "fa-solid fa-pdf");
                pb.Add(p => p.ElementId, "test-id");
                pb.Add(p => p.Selector, ".modal-body");
                pb.Add(p => p.StyleTags, ["test.css"]);
                pb.Add(p => p.ScriptTags, ["test.js"]);
                pb.Add(p => p.PdfFileName, "test.pdf");
            });
        });
        var button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());
    }

    [Fact]
    public void ExportPdfButtonOptions_Ok()
    {
        var options = new ExportPdfButtonOptions()
        {
            ElementId = "test-id",
            Selector = ".modal-body",
            StyleTags = ["test.css"],
            ScriptTags = ["test.js"],
            FileName = "test.pdf",
            Color = Color.Primary,
            Text = "test",
            Icon = "icon"
        };

        Assert.Equal("test-id", options.ElementId);
        Assert.Equal(".modal-body", options.Selector);
        Assert.Equal(["test.css"], options.StyleTags);
        Assert.Equal(["test.js"], options.ScriptTags);
        Assert.Equal(Color.Primary, options.Color);
        Assert.Equal("test", options.Text);
        Assert.Equal("icon", options.Icon);
        Assert.Equal("test.pdf", options.FileName);
    }
}
