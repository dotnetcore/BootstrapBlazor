// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace UnitTest.Components;

public class UploadCardTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task CardUpload_Ok()
    {
        var zoom = false;
        var deleted = false;
        var cut = Context.Render<CardUpload<string>>(pb =>
        {
            pb.Add(a => a.ShowZoomButton, true);
            pb.Add(a => a.ShowDeleteButton, true);
            pb.Add(a => a.ShowFileSize, false);
            pb.Add(a => a.OnDelete, file =>
            {
                deleted = true;
                return Task.FromResult(true);
            });
            pb.Add(a => a.DefaultFileList,
            [
                new() { FileName = "Test-File1.text" },
                new() { FileName = "Test-File2.jpg" },
                new() { PrevUrl = "Test-File3.png" },
                new() { PrevUrl = "Test-File4.bmp" },
                new() { PrevUrl = "Test-File5.jpeg" },
                new() { PrevUrl = "Test-File6.gif" },
                new() { PrevUrl = "data:image/png;base64,iVBORw0KGgoAAAANS=" },
                new() { FileName = null! }
            ]);
        });
        cut.Contains("bb-previewer collapse active");
        cut.Contains("aria-label=\"zoom\"");
        cut.Contains("aria-label=\"delete\"");

        cut.Render(pb =>
        {
            pb.Add(a => a.IconTemplate, file => builder =>
           {
               builder.AddContent(0, "custom-file-icon-template");
           });
        });
        cut.Contains("custom-file-icon-template");

        // OnZoom
        await cut.InvokeAsync(() => cut.Find(".btn-zoom").Click());
        Assert.False(zoom);

        cut.Render(pb =>
        {
            pb.Add(a => a.IconTemplate, (RenderFragment<UploadFile>?)null);
            pb.Add(a => a.OnZoomAsync, file =>
            {
                zoom = true;
                return Task.CompletedTask;
            });
        });
        await cut.InvokeAsync(() => cut.Find(".btn-zoom").Click());
        Assert.True(zoom);

        zoom = false;
        await cut.InvokeAsync(() => cut.Find(".upload-item-body-image").Click());
        Assert.True(zoom);

        // ShowDownload
        var clicked = false;
        cut.Render(pb =>
        {
            pb.Add(a => a.ShowDownloadButton, true);
            pb.Add(a => a.OnDownload, file =>
            {
                clicked = true;
                return Task.CompletedTask;
            });

        });
        Assert.Contains("btn-download", cut.Markup);
        var button = cut.Find(".btn-download");
        await cut.InvokeAsync(() => button.Click());
        Assert.True(clicked);

        // OnDelete
        await cut.InvokeAsync(() => cut.Find(".btn-outline-danger").Click());
        Assert.True(deleted);

        // CanPreviewCallback
        cut.Render(pb =>
        {
            pb.Add(a => a.CanPreviewCallback, p =>
            {
                return false;
            });
        });
        await cut.InvokeAsync(() => cut.Find(".btn-zoom").Click());

        // ShowProgress
        cut.Render(pb =>
        {
            pb.Add(a => a.ShowProgress, true);
            pb.Add(a => a.OnChange, async file =>
            {
                await file.SaveToFileAsync("1.txt");
            });
        });
        var input = cut.FindComponent<InputFile>();
        await cut.InvokeAsync(() => input.Instance.OnChange.InvokeAsync(new InputFileChangeEventArgs(new List<MockBrowserFile>()
        {
            new("test.txt", "Image-Png")
        })));
        await cut.InvokeAsync(() => input.Instance.OnChange.InvokeAsync(new InputFileChangeEventArgs(new List<MockBrowserFile>()
        {
            new("test.png")
        })));

        // IsUploadButtonAtFirst
        cut.Render(pb =>
        {
            pb.Add(a => a.IsUploadButtonAtFirst, true);
            pb.Add(a => a.IsMultiple, true);
            pb.Add(a => a.ShowZoomButton, false);
            pb.Add(a => a.ShowDeleteButton, false);
        });
        cut.DoesNotContain("aria-label=\"zoom\"");
        cut.DoesNotContain("aria-label=\"delete\"");
    }

    [Fact]
    public void CardUpload_ValidateForm_Ok()
    {
        var foo = new Foo();
        var cut = Context.Render<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<CardUpload<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Name);
                pb.Add(a => a.ValueExpression, foo.GenerateValueExpression());
            });
        });
        cut.Contains("form-label");
    }

    [Fact]
    public void AllowExtensions_Ok()
    {
        var cut = Context.Render<CardUpload<string>>(pb =>
        {
            pb.Add(a => a.AllowExtensions, [".dba"]);
            pb.Add(a => a.DefaultFileList,
            [
                new() { FileName = "test.dba" }
            ]);
        });
        cut.Contains("<span>test.dba</span> (0 B)");

        cut.Render(pb =>
        {
            pb.Add(a => a.DefaultFileList,
            [
                new() { File = new MockBrowserFile("demo.dba") }
            ]);
        });
        cut.Contains("<span>demo.dba</span> (0 B)");
    }

    [Fact]
    public async Task CardUpload_ShowProgress_Ok()
    {
        var cancel = false;
        var cut = Context.Render<CardUpload<string>>(pb =>
        {
            pb.Add(a => a.ShowProgress, true);
            pb.Add(a => a.OnChange, async file =>
            {
                await Task.Delay(100);
                await file.SaveToFileAsync("1.txt");
            });
            pb.Add(a => a.OnCancel, file =>
            {
                cancel = true;
                return Task.CompletedTask;
            });
        });
        var input = cut.FindComponent<InputFile>();
        await cut.InvokeAsync(() =>
        {
            input.Instance.OnChange.InvokeAsync(new InputFileChangeEventArgs(new List<MockBrowserFile>()
            {
                new()
            }));
            var button = cut.Find(".btn-cancel");
            Assert.NotNull(button);
            button.Click();
        });
        Assert.True(cancel);
    }

    [Fact]
    public async Task ShowDeleteButton_Ok()
    {
        var cut = Context.Render<CardUpload<string>>(pb =>
        {
            pb.Add(a => a.ShowDeleteButton, true);
            pb.Add(a => a.IsDisabled, true);
        });

        var input = cut.FindComponent<InputFile>();
        await cut.InvokeAsync(() => input.Instance.OnChange.InvokeAsync(new InputFileChangeEventArgs(new List<MockBrowserFile>()
        {
            new("test3.png", delay: TimeSpan.FromMilliseconds(300)),
        })));

        var btn = cut.Find(".btn-outline-danger");
        btn.InnerHtml.Contains("disabled=\"disabled\"");
    }

    [Fact]
    public void ActionButtonTemplate_Ok()
    {
        var cut = Context.Render<CardUpload<string>>(pb =>
        {
            pb.Add(a => a.DefaultFileList,
            [
                new() { FileName = "test.png" }
            ]);
            pb.Add(a => a.BeforeActionButtonTemplate, file => pb =>
            {
                pb.AddMarkupContent(0, "<button class=\"before-action-button-test\"></button>");
            });
            pb.Add(a => a.ActionButtonTemplate, file => pb =>
            {
                pb.AddMarkupContent(0, "<button class=\"action-button-test\"></button>");
            });
        });

        cut.Contains("before-action-button-test");
        cut.Contains("action-button-test");
    }

    private class MockBrowserFile(string name = "UploadTestFile", string contentType = "text", TimeSpan? delay = null) : IBrowserFile
    {
        public string Name { get; } = name;

        public DateTimeOffset LastModified { get; } = DateTimeOffset.Now;

        public long Size { get; } = 10;

        public string ContentType { get; } = contentType;

        public Stream OpenReadStream(long maxAllowedSize = 512000, CancellationToken cancellationToken = default)
        {
            if (delay != null)
            {
                Thread.Sleep(delay.Value.Milliseconds);
            }
            return new MemoryStream([0x01, 0x02]);
        }
    }
}
