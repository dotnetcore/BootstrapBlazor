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
        bool export = false;
        bool download = false;
        bool downloaded = false;
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
                pb.Add(p => p.AutoDownload, true);
                pb.Add(p => p.OnBeforeExport, () =>
                {
                    export = true;
                    return Task.CompletedTask;
                });
                pb.Add(p => p.OnBeforeDownload, _ =>
                {
                    download = true;
                    return Task.CompletedTask;
                });
                pb.Add(p => p.OnAfterDownload, _ =>
                {
                    downloaded = true;
                    return Task.CompletedTask;
                });
            });
        });
        var button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());
        Assert.True(export);
        Assert.True(download);
        Assert.True(downloaded);
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
            Icon = "icon",
            AutoDownload = true,
            OnBeforeExport = () => Task.CompletedTask,
            OnBeforeDownload = _ => Task.CompletedTask,
            OnAfterDownload = _ => Task.CompletedTask,
        };

        Assert.Equal("test-id", options.ElementId);
        Assert.Equal(".modal-body", options.Selector);
        Assert.Equal(["test.css"], options.StyleTags);
        Assert.Equal(["test.js"], options.ScriptTags);
        Assert.Equal(Color.Primary, options.Color);
        Assert.Equal("test", options.Text);
        Assert.Equal("icon", options.Icon);
        Assert.Equal("test.pdf", options.FileName);
        Assert.True(options.AutoDownload);
        Assert.NotNull(options.OnBeforeExport);
        Assert.NotNull(options.OnBeforeDownload);
        Assert.NotNull(options.OnAfterDownload);
    }

    [Fact]
    public void ExportPdfButtonSettings_Ok()
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
            Icon = "icon",
            AutoDownload = true,
            OnBeforeExport = () => Task.CompletedTask,
            OnBeforeDownload = _ => Task.CompletedTask,
            OnAfterDownload = _ => Task.CompletedTask,
            IsAsync = true
        };
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<ExportPdfButton>(pb =>
            {
                pb.Add(p => p.ChildContent, new RenderFragment(builder =>
                {
                    builder.OpenComponent<ExportPdfButtonSettings>(0);
                    builder.AddAttribute(1, nameof(ExportPdfButtonSettings.Options), options);
                    builder.CloseComponent();
                }));
            });
        });

        var button = cut.FindComponent<ExportPdfButton>().Instance;
        Assert.Equal("test-id", button.ElementId);
        Assert.Equal(".modal-body", button.Selector);
        Assert.Equal(["test.css"], button.StyleTags);
        Assert.Equal(["test.js"], button.ScriptTags);
        Assert.Equal(Color.Primary, button.Color);
        Assert.Equal("test", button.Text);
        Assert.Equal("icon", button.Icon);
        Assert.Equal("test.pdf", button.FileName);
        Assert.True(button.AutoDownload);
        Assert.NotNull(button.OnBeforeExport);
        Assert.NotNull(button.OnBeforeDownload);
        Assert.NotNull(button.OnAfterDownload);
        Assert.True(button.IsAsync);
    }
}
