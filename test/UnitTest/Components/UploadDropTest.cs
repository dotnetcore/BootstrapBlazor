// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace UnitTest.Components;

public class UploadDropTest : BootstrapBlazorTestBase
{
    [Fact]
    public void DropUpload_BodyTemplate_Ok()
    {
        var cut = Context.Render<DropUpload>(pb =>
        {
            pb.Add(a => a.BodyTemplate, b => b.AddContent(0, "drop-upload-body-template"));
        });
        cut.MarkupMatches("<div class=\"upload is-drop\" diff:ignore><div class=\"upload-drop-body\">drop-upload-body-template</div><ul class=\"upload-drop-list\"></ul><input hidden=\"hidden\" type=\"file\" diff:ignore></input></div>");
    }

    [Fact]
    public void DropUpload_IconTemplate_Ok()
    {
        var cut = Context.Render<DropUpload>(pb =>
        {
            pb.Add(a => a.IconTemplate, b => b.AddContent(0, "drop-upload-icon-template"));
        });
        cut.Contains("<div class=\"upload-drop-icon\">drop-upload-icon-template</div>");
    }

    [Fact]
    public void DropUpload_TextTemplate_Ok()
    {
        var cut = Context.Render<DropUpload>(pb =>
        {
            pb.Add(a => a.TextTemplate, b => b.AddContent(0, "drop-upload-text-template"));
        });
        cut.Contains("<div class=\"upload-drop-text\">drop-upload-text-template</div>");
    }

    [Fact]
    public void DropUpload_Footer_Ok()
    {
        var cut = Context.Render<DropUpload>(pb =>
        {
            pb.Add(a => a.ShowFooter, true);
            pb.Add(a => a.FooterText, "drop-upload-footer-text1");
        });
        cut.Contains("<div class=\"upload-drop-footer\"><span class=\"text-muted\">drop-upload-footer-text1</span></div>");

        cut.Render(pb =>
        {
            pb.Add(a => a.FooterTemplate, b => b.AddContent(0, "drop-upload-footer-text"));
        });
        cut.Contains("<div class=\"upload-drop-footer\">drop-upload-footer-text</div>");
    }

    [Fact]
    public async Task MaxFileCount_Ok()
    {
        var cut = Context.Render<DropUpload>(pb =>
        {
            pb.Add(a => a.IsMultiple, true);
            pb.Add(a => a.MaxFileCount, 2);
        });

        var input = cut.FindComponent<InputFile>();
        await cut.InvokeAsync(() => input.Instance.OnChange.InvokeAsync(new InputFileChangeEventArgs(new List<MockBrowserFile>()
        {
            new("test1.png"),
            new("test2.png")
        })));
        cut.Contains("test1.png");
        cut.Contains("test2.png");

        await cut.InvokeAsync(() => input.Instance.OnChange.InvokeAsync(new InputFileChangeEventArgs(new List<MockBrowserFile>()
        {
            new("test3.png")
        })));
        cut.DoesNotContain("test3.png");
    }

    [Fact]
    public async Task DropUpload_OnChanged_Ok()
    {
        var cut = Context.Render<DropUpload>(pb =>
        {
            pb.Add(a => a.ShowLabel, true);
            pb.Add(a => a.ShowProgress, true);
            pb.Add(a => a.OnChange, async files =>
            {
                await files.SaveToFileAsync("1.text");
            });
        });

        var input = cut.FindComponent<InputFile>();
        await cut.InvokeAsync(() => input.Instance.OnChange.InvokeAsync(new InputFileChangeEventArgs(new List<MockBrowserFile>()
        {
            new()
        })));
    }

    [Fact]
    public async Task ShowUploadList_Ok()
    {
        UploadFile? file = null;
        var cut = Context.Render<DropUpload>(pb =>
        {
            pb.Add(a => a.ShowUploadFileList, true);
            pb.Add(a => a.ShowDownloadButton, true);
            pb.Add(a => a.OnDownload, f =>
            {
                file = f;
                return Task.CompletedTask;
            });
            pb.Add(a => a.DefaultFileList,
            [
                new() { FileName = "Test-File1.text" },
                new() { FileName = "Test-File2.jpg" },
            ]);
        });
        cut.Contains("upload-body is-list");

        var button = cut.Find(".download-icon");
        await cut.InvokeAsync(() => button.Click());
        Assert.NotNull(file);
    }

    [Fact]
    public async Task DropUpload_ShowProgress_Ok()
    {
        var cancel = false;
        var cut = Context.Render<DropUpload>(pb =>
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
        await cut.InvokeAsync(async () =>
        {
            _ = input.Instance.OnChange.InvokeAsync(new InputFileChangeEventArgs(new List<MockBrowserFile>()
            {
                new()
            }));

            var button = cut.Find(".cancel-icon");
            Assert.NotNull(button);
            await cut.InvokeAsync(() => button.Click());
            Assert.True(cancel);
        });
    }


    [Fact]
    public void OnGetFileFormat_Ok()
    {
        var cut = Context.Render<DropUpload>(pb =>
        {
            pb.Add(a => a.LoadingIcon, "fa-loading");
            pb.Add(a => a.DeleteIcon, "fa-delte");
            pb.Add(a => a.CancelIcon, "fa-cancel");
            pb.Add(a => a.DownloadIcon, "fa-download");
            pb.Add(a => a.InvalidStatusIcon, "fa-invalid");
            pb.Add(a => a.ValidStatusIcon, "fa-valid");
            pb.Add(a => a.ShowUploadFileList, true);
            pb.Add(a => a.OnGetFileFormat, extensions =>
            {
                return "fa-format-test";
            });
            pb.Add(a => a.DefaultFileList,
            [
                new() { FileName = "1.csv" }
            ]);
        });
        cut.Contains("fa-format-test");
    }

    [Theory]
    [InlineData(true, true)]
    [InlineData(false, false)]
    public void ShowDeleteButton_Ok(bool showDeleteButton, bool result)
    {
        var cut = Context.Render<DropUpload>(pb =>
        {
            pb.Add(a => a.DefaultFileList, [new() { FileName = "1.csv" }]);
            pb.Add(a => a.ShowUploadFileList, true);
            pb.Add(a => a.ShowDeleteButton, showDeleteButton);
        });
        var exists = cut.Markup.Contains("delete-icon");
        Assert.Equal(result, exists);
    }

    private class MockBrowserFile(string name = "UploadTestFile", string contentType = "text") : IBrowserFile
    {
        public string Name { get; } = name;

        public DateTimeOffset LastModified { get; } = DateTimeOffset.Now;

        public long Size { get; } = 10;

        public string ContentType { get; } = contentType;

        public Stream OpenReadStream(long maxAllowedSize = 512000, CancellationToken cancellationToken = default)
        {
            return new MemoryStream([0x01, 0x02]);
        }
    }
}
