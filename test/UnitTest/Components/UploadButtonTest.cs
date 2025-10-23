// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using AngleSharp.Dom;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace UnitTest.Components;

public class UploadButtonTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task ButtonUpload_Ok()
    {
        UploadFile? uploadFile = null;
        var cut = Context.RenderComponent<ButtonUpload<string>>(pb =>
        {
            pb.Add(a => a.BrowserButtonClass, "browser-class");
            pb.Add(a => a.BrowserButtonIcon, "fa-solid fa-chrome");
            pb.Add(a => a.BrowserButtonColor, Color.Success);
        });
        cut.Contains("fa-solid fa-chrome");
        cut.Contains("browser-class");
        cut.Contains("btn btn-success");
        cut.DoesNotContain("form-label");

        // DefaultFileList
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowProgress, true);
            pb.Add(a => a.OnChange, file =>
            {
                uploadFile = file;
                return Task.CompletedTask;
            });
        });
        var input = cut.FindComponent<InputFile>();
        await cut.InvokeAsync(() => input.Instance.OnChange.InvokeAsync(new InputFileChangeEventArgs(new List<MockBrowserFile>()
        {
            new()
        })));
        cut.DoesNotContain("cancel-icon");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Size, Size.ExtraSmall);
        });
        cut.Contains("btn-xs");
    }

    [Fact]
    public void ButtonUpload_ChildContent()
    {
        var cut = Context.RenderComponent<ButtonUpload<string>>(pb =>
        {
            pb.Add(a => a.ChildContent, builder => builder.AddContent(0, new MarkupString("<div>test-child-content</div>")));
        });
        cut.Contains("<div>test-child-content</div>");
    }

    [Fact]
    public void ButtonUpload_IsDisabled_Ok()
    {
        var cut = Context.RenderComponent<ButtonUpload<string>>(pb =>
        {
            pb.Add(a => a.IsDisabled, true);
        });
        var button = cut.Find(".btn-browser");
        Assert.Contains("disabled=\"disabled\"", button.ToMarkup());

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsDisabled, false);
            pb.Add(a => a.IsMultiple, true);
        });
        Assert.DoesNotContain("disabled=\"disabled\"", button.ToMarkup());

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsDisabled, false);
            pb.Add(a => a.IsMultiple, false);
        });
        Assert.DoesNotContain("disabled=\"disabled\"", button.ToMarkup());
    }

    [Fact]
    public async Task InputUpload_IsMultiple()
    {
        var cut = Context.RenderComponent<ButtonUpload<string>>(pb =>
        {
            pb.Add(a => a.IsMultiple, false);
        });

        // 禁用多选功能
        cut.DoesNotContain("multiple=\"multiple\"");

        // 给定已上传文件后上传按钮应该被禁用
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.DefaultFileList,
            [
                new UploadFile() { FileName = "test1.png" },
                new UploadFile() { FileName = "test2.png" }
            ]);
        });
        var button = cut.Find(".btn-browser");
        Assert.True(button.IsDisabled());

        // 调用 Reset 方法
        await cut.InvokeAsync(() => cut.Instance.Reset());

        // 重置后上传按钮应该被启用
        button = cut.Find(".btn-browser");
        Assert.False(button.IsDisabled());

        // 开启多选功能
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsMultiple, true);
        });
        cut.Contains("multiple=\"multiple\"");

        // 给定已上传文件后上传按钮不应该被禁用
        button = cut.Find(".btn-browser");
        Assert.False(button.IsDisabled());
    }

    [Fact]
    public async Task ButtonUpload_ValidateForm_Ok()
    {
        var foo = new Foo();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<ButtonUpload<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Name);
                pb.Add(a => a.ValueExpression, foo.GenerateValueExpression());
            });
        });
        cut.Contains("form-label");

        // ValidateId 为空情况
        var uploader = cut.FindComponent<ButtonUpload<string>>();
        var pi = typeof(ButtonUpload<string>).GetProperty("UploadFiles", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        Assert.NotNull(pi);
        var filesValue = pi.GetValue(uploader.Instance);

        if (filesValue is List<UploadFile> fs)
        {
            fs.Add(new UploadFile());
        }

        var results = new List<ValidationResult>()
        {
            new("test", ["bb_validate_123"])
        };
        await cut.InvokeAsync(() => uploader.Instance.ToggleMessage(results));
    }

    [Fact]
    public async Task ButtonUpload_ShowDownload()
    {
        var clicked = false;
        var cut = Context.RenderComponent<ButtonUpload<string>>(pb =>
        {
            pb.Add(a => a.ShowDownloadButton, true);
            pb.Add(a => a.OnDownload, file =>
            {
                clicked = true;
                return Task.CompletedTask;
            });
            pb.Add(a => a.DefaultFileList,
            [
                new() { FileName = "Test-File1.text" }
            ]);
        });

        var button = cut.Find(".fa-download");
        await cut.InvokeAsync(() => button.Click());
        Assert.True(clicked);
    }

    [Fact]
    public async Task ButtonUpload_Validate_Ok()
    {
        var invalid = true;
        var foo = new Foo
        {
            Name = "abc"
        };
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<ButtonUpload<string>>(pb =>
            {
                pb.Add(a => a.Accept, "Image");
                pb.Add(a => a.Value, foo.Name);
                pb.Add(a => a.ValueExpression, foo.GenerateValueExpression());
                pb.Add(a => a.ShowUploadFileList, true);
            });
            pb.Add(a => a.OnValidSubmit, context =>
            {
                invalid = false;
                return Task.CompletedTask;
            });
        });

        // 由于设置了属性 Name 值 Validate 方法通过
        var form = cut.Find("form");
        await cut.InvokeAsync(() => form.Submit());
        Assert.False(invalid);
    }

    [Fact]
    public void ShowUploadList_Ok()
    {
        var cut = Context.RenderComponent<ButtonUpload<string>>(pb =>
        {
            pb.Add(a => a.Accept, "Image");
        });

        cut.Contains("upload-body is-list");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowUploadFileList, false);
        });
        cut.WaitForState(() => !cut.Markup.Contains("upload-body is-list"));
        cut.DoesNotContain("upload-body is-list");
    }

    [Fact]
    public async Task ButtonUpload_OnDeleteFile_Ok()
    {
        UploadFile? deleteFile = null;
        var cut = Context.RenderComponent<ButtonUpload<string>>(pb =>
        {
            pb.Add(a => a.IsMultiple, true);
            pb.Add(a => a.DefaultFileList,
            [
                new() { FileName = "Test-File" }
            ]);
            pb.Add(a => a.OnDelete, file =>
            {
                deleteFile = file;
                return Task.FromResult(true);
            });
        });
        await cut.InvokeAsync(() => cut.Find(".delete-icon").Click());
        Assert.NotNull(deleteFile);
        Assert.Null(deleteFile!.Error);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.DefaultFileList, null);
        });
        // 增加代码覆盖率
        var ins = cut.Instance;
        var pi = ins.GetType().GetMethod("OnFileDelete", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;
        pi.Invoke(ins, [new UploadFile()]);

        deleteFile = null;
        // 上传失败测试
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.DefaultFileList,
            [
                new() { FileName = "Test-File2", Code = 1001, Error = "Error" }
            ]);
        });
        await cut.InvokeAsync(() => cut.Find(".delete-icon").Click());
        Assert.NotNull(deleteFile);
    }

    [Fact]
    public async Task ButtonUpload_ShowProgress_Ok()
    {
        var cancel = false;
        var cut = Context.RenderComponent<ButtonUpload<string>>(pb =>
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
    public async Task ButtonUpload_IsDirectory_Ok()
    {
        var fileCount = 0;
        var fileNames = new List<string>();
        List<UploadFile> fileList = [];
        var cut = Context.RenderComponent<ButtonUpload<string>>(pb =>
        {
            pb.Add(a => a.IsDirectory, true);
            pb.Add(a => a.OnChange, file =>
            {
                fileCount = file.FileCount;
                fileNames.Add(file.OriginFileName!);
                return Task.CompletedTask;
            });
            pb.Add(a => a.OnAllFileUploaded, files =>
            {
                fileList.AddRange(files);
                return Task.CompletedTask;
            });
        });
        var input = cut.FindComponent<InputFile>();
        await cut.InvokeAsync(() => input.Instance.OnChange.InvokeAsync(new InputFileChangeEventArgs(new List<MockBrowserFile>()
        {
            new(),
            new("UploadTestFile2")
        })));
        Assert.Equal(2, fileCount);
        Assert.Equal(2, fileNames.Count);
        Assert.Equal(2, fileList.Count);
    }

    [Fact]
    public void ButtonUpload_Accept_Ok()
    {
        var cut = Context.RenderComponent<ButtonUpload<string>>(pb =>
        {
            pb.Add(a => a.Accept, ".jpg");
        });
        cut.Contains("accept=\".jpg\"");
    }

    [Fact]
    public void ButtonUpload_OnGetFileFormat_Ok()
    {
        var cut = Context.RenderComponent<ButtonUpload<string>>(pb =>
        {
            pb.Add(a => a.LoadingIcon, "fa-loading");
            pb.Add(a => a.DeleteIcon, "fa-delte");
            pb.Add(a => a.CancelIcon, "fa-cancel");
            pb.Add(a => a.DownloadIcon, "fa-download");
            pb.Add(a => a.InvalidStatusIcon, "fa-invalid");
            pb.Add(a => a.ValidStatusIcon, "fa-valid");
            pb.Add(a => a.DefaultFileList,
            [
                new() { FileName = "1.csv" },
                new() { FileName = "1.xls" },
                new() { FileName = "1.xlsx" },
                new() { FileName = "1.doc" },
                new() { FileName = "1.docx" },
                new() { FileName = "1.dot" },
                new() { FileName = "1.dotx" },
                new() { FileName = "1.ppt" },
                new() { FileName = "1.pptx" },
                new() { FileName = "1.wav" },
                new() { FileName = "1.mp3" },
                new() { FileName = "1.mp4" },
                new() { FileName = "1.mov" },
                new() { FileName = "1.mkv" },
                new() { FileName = "1.cs" },
                new() { FileName = "1.html" },
                new() { FileName = "1.vb" },
                new() { FileName = "1.pdf" },
                new() { FileName = "1.zip" },
                new() { FileName = "1.rar" },
                new() { FileName = "1.iso" },
                new() { FileName = "1.txt" },
                new() { FileName = "1.log" },
                new() { FileName = "1.jpg" },
                new() { FileName = "1.jpeg" },
                new() { FileName = "1.png" },
                new() { FileName = "1.bmp" },
                new() { FileName = "1.gif" },
                new() { FileName = "1.test" },
                new() { FileName = "1" }
            ]);
        });
        cut.Contains("fa-file-excel");
        cut.Contains("fa-file-word");
        cut.Contains("fa-file-powerpoint");
        cut.Contains("fa-file-audio");
        cut.Contains("fa-file-video");
        cut.Contains("fa-file-code");
        cut.Contains("fa-file-pdf");
        cut.Contains("fa-file-archive");
        cut.Contains("fa-file-text");
        cut.Contains("fa-file-image");
        cut.Contains("fa-file");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.OnGetFileFormat, extensions =>
            {
                return "fa-format-test";
            });
        });
        cut.Contains("fa-format-test");

        // Empty Items
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.DefaultFileList, null);
        });
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
