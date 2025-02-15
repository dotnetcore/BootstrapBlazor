// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace UnitTest.Components;

public class UploadTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task InputUpload_Ok()
    {
        UploadFile? uploadFile = null;
        var cut = Context.RenderComponent<InputUpload<string>>(pb =>
        {
            pb.Add(a => a.PlaceHolder, "TestPlaceHolder");
            pb.Add(a => a.OnChange, file =>
            {
                uploadFile = file;
                return Task.CompletedTask;
            });
            pb.Add(a => a.Value, "test.jpg");
        });
        cut.Contains("value=\"test.jpg\"");

        var input = cut.FindComponent<InputFile>();
        await cut.InvokeAsync(() => input.Instance.OnChange.InvokeAsync(new InputFileChangeEventArgs(new List<MockBrowserFile>()
        {
            new()
        })));
        Assert.Equal("UploadTestFile", uploadFile!.OriginFileName);
        cut.Contains("fa-regular fa-folder-open");
        cut.Contains("btn-primary");
        cut.Contains("TestPlaceHolder");

        // 参数
        cut.SetParametersAndRender(pb => pb.Add(a => a.BrowserButtonIcon, "fa-solid fa-chrome"));
        cut.Contains("fa-solid fa-chrome");

        cut.SetParametersAndRender(pb => pb.Add(a => a.BrowserButtonClass, "btn btn-browser"));
        cut.Contains("btn btn-browser");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowDeleteButton, true);
            pb.Add(a => a.DeleteButtonText, "Delete-Test");
            pb.Add(a => a.DeleteButtonIcon, "fa-solid fa-trash");
        });
        cut.WaitForAssertion(() => cut.Contains("fa-solid fa-trash"));
        cut.Contains("btn-danger");

        // 删除逻辑
        var deleted = false;
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.DeleteButtonClass, "btn btn-delete");
            pb.Add(a => a.OnDelete, file =>
            {
                deleted = true;
                return Task.FromResult(true);
            });
        });
        cut.WaitForAssertion(() => cut.Contains("btn btn-delete"));

        var button = cut.Find(".input-group button");
        await cut.InvokeAsync(() => button.Click());
        Assert.True(deleted);

        // IsDisable
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsDisabled, true);
        });
        cut.WaitForAssertion(() => cut.Contains("btn btn-delete"));
    }

    [Fact]
    public async Task InputUpload_ValidateForm_Ok()
    {
        var invalid = false;
        var foo = new Foo();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<InputUpload<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Name);
                pb.Add(a => a.ValueExpression, foo.GenerateValueExpression());
            });
            pb.Add(a => a.OnValidSubmit, context =>
            {
                invalid = false;
                return Task.CompletedTask;
            });
            pb.Add(a => a.OnInvalidSubmit, context =>
            {
                invalid = true;
                return Task.CompletedTask;
            });
        });

        // 提交表单
        var form = cut.Find("form");
        await cut.InvokeAsync(() => form.Submit());
        Assert.True(invalid);

        var input = cut.FindComponent<InputFile>();
        await cut.InvokeAsync(() => input.Instance.OnChange.InvokeAsync(new InputFileChangeEventArgs(new List<MockBrowserFile>()
        {
            new()
        })));
        await cut.InvokeAsync(() => form.Submit());
        Assert.False(invalid);
    }

    [Fact]
    public void InputUpload_FileValidate_OK()
    {
        var foo = new Person();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<InputUpload<IBrowserFile>>(pb =>
            {
                pb.Add(a => a.Value, foo.Picture);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(foo, nameof(Person.Picture), typeof(IBrowserFile)));
            });
        });

        var input = cut.FindComponent<InputFile>();
        cut.InvokeAsync(() => input.Instance.OnChange.InvokeAsync(new InputFileChangeEventArgs(new List<MockBrowserFile>()
        {
            new()
        })));

        // 提交表单
        var form = cut.Find("form");
        cut.InvokeAsync(() => form.Submit());
    }

    [Fact]
    public void AvatarUpload_Ok()
    {
        UploadFile? uploadFile = null;
        var cut = Context.RenderComponent<AvatarUpload<string>>(pb =>
        {
            pb.Add(a => a.IsSingle, true);
            pb.Add(a => a.OnChange, file =>
            {
                uploadFile = file;
                return Task.CompletedTask;
            });
        });
        var input = cut.FindComponent<InputFile>();
        cut.InvokeAsync(() => input.Instance.OnChange.InvokeAsync(new InputFileChangeEventArgs(new List<MockBrowserFile>()
        {
            new()
        })));

        // Height/Width
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Height, 40);
            pb.Add(a => a.Width, 50);
        });
        cut.WaitForAssertion(() => cut.Contains("width: 50px;"));
        cut.WaitForAssertion(() => cut.Contains("height: 40px;"));

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsCircle, true);
        });
        cut.WaitForAssertion(() => cut.Contains("height: 50px;"));

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Height, 0);
        });
        cut.WaitForAssertion(() => cut.Contains("height: 50px;"));

        // DefaultFileList
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.OnChange, null);
            pb.Add(a => a.ShowProgress, true);
            pb.Add(a => a.DefaultFileList,
            [
                new() { FileName = "Test-File" }
            ]);
        });
        input = cut.FindComponent<InputFile>();
        cut.InvokeAsync(() => input.Instance.OnChange.InvokeAsync(new InputFileChangeEventArgs(new List<MockBrowserFile>()
        {
            new()
        })));

        // upload-item-delete
        var button = cut.Find(".upload-item-delete");
        cut.InvokeAsync(() => button.Click());

        // isdisable
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsDisabled, true);
        });

        // IsUploadButtonAtFirst
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsUploadButtonAtFirst, true);
        });
    }

    [Fact]
    public void AvatarUpload_Value_Ok()
    {
        var cut = Context.RenderComponent<AvatarUpload<IBrowserFile>>(pb => pb.Add(a => a.IsSingle, true));
        var input = cut.FindComponent<InputFile>();
        cut.InvokeAsync(() => input.Instance.OnChange.InvokeAsync(new InputFileChangeEventArgs(new List<MockBrowserFile>()
        {
            new()
        })));
        cut.WaitForAssertion(() => Assert.Equal("UploadTestFile", cut.Instance.Value!.Name));
    }

    [Fact]
    public async Task AvatarUpload_ListValue_Ok()
    {
        var cut = Context.RenderComponent<AvatarUpload<List<IBrowserFile>>>();
        var input = cut.FindComponent<InputFile>();
        await cut.InvokeAsync(() => input.Instance.OnChange.InvokeAsync(new InputFileChangeEventArgs(new List<MockBrowserFile>()
        {
            new()
        })));
        Assert.Single(cut.Instance.Value);
    }

    [Fact]
    public async Task AvatarUpload_ValidateForm_Ok()
    {
        var invalid = false;
        var foo = new Foo();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<AvatarUpload<string>>(pb =>
            {
                pb.Add(a => a.Accept, "Image");
                pb.Add(a => a.Value, foo.Name);
                pb.Add(a => a.ValueExpression, foo.GenerateValueExpression());
            });
            pb.Add(a => a.OnValidSubmit, context =>
            {
                invalid = false;
                return Task.CompletedTask;
            });
            pb.Add(a => a.OnInvalidSubmit, context =>
            {
                invalid = true;
                return Task.CompletedTask;
            });
        });

        // 提交表单
        var form = cut.Find("form");
        await cut.InvokeAsync(() => form.Submit());
        Assert.True(invalid);

        var input = cut.FindComponent<InputFile>();
        await cut.InvokeAsync(async () =>
        {
            await input.Instance.OnChange.InvokeAsync(new InputFileChangeEventArgs(new List<MockBrowserFile>()
            {
                new()
            }));
            form.Submit();
        });
        Assert.False(invalid);
    }

    [Fact]
    public async Task AvatarUpload_Validate_Ok()
    {
        var invalid = true;
        var foo = new Foo
        {
            Name = "abc"
        };
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<AvatarUpload<string>>(pb =>
            {
                pb.Add(a => a.Accept, "Image");
                pb.Add(a => a.Value, foo.Name);
                pb.Add(a => a.ValueExpression, foo.GenerateValueExpression());
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
    public void AvatarUpload_FileValidate_Ok()
    {
        var foo = new Person();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<AvatarUpload<IBrowserFile>>(pb =>
            {
                pb.Add(a => a.Value, foo.Picture);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(foo, nameof(Person.Picture), typeof(IBrowserFile)));
            });
        });

        var input = cut.FindComponent<InputFile>();
        cut.InvokeAsync(() => input.Instance.OnChange.InvokeAsync(new InputFileChangeEventArgs(new List<MockBrowserFile>()
        {
            new()
        })));

        // 提交表单
        var form = cut.Find("form");
        cut.InvokeAsync(() => form.Submit());
    }

    [Fact]
    public async Task ButtonUpload_Ok()
    {
        UploadFile? uploadFile = null;
        var cut = Context.RenderComponent<ButtonUpload<string>>(pb =>
        {
            pb.Add(a => a.IsSingle, true);
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
            pb.Add(a => a.IsSingle, false);
        });
        Assert.DoesNotContain("disabled=\"disabled\"", button.ToMarkup());

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsDisabled, false);
            pb.Add(a => a.IsSingle, true);
        });
        Assert.DoesNotContain("disabled=\"disabled\"", button.ToMarkup());

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsDisabled, false);
            pb.Add(a => a.IsSingle, true);
        });
        var input = cut.FindComponent<InputFile>();
        cut.InvokeAsync(() => input.Instance.OnChange.InvokeAsync(new InputFileChangeEventArgs(new List<MockBrowserFile>()
        {
            new()
        })));
    }

    private static readonly string[] memberNames = ["bb_validate_123"];

    [Fact]
    public void ButtonUpload_ValidateForm_Ok()
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
        var pi = typeof(ButtonUpload<string>).GetProperty("UploadFiles", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        Assert.NotNull(pi);
        var filesValue = pi.GetValue(uploader.Instance);

        if (filesValue is List<UploadFile> fs)
        {
            fs.Add(new UploadFile());
        }
        var results = new List<ValidationResult>()
        {
            new("test", memberNames)
        };
        uploader.Instance.ToggleMessage(results);
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
            pb.Add(a => a.IsSingle, false);
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
                new() { FileName = "Test-File2", Code = 1001 }
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
            pb.Add(a => a.DefaultFileList,
            [
                new() { FileName = "1.csv" },
                new() { FileName = "1.xls" },
                new() { FileName = "1.xlsx" },
                new() { FileName = "1.doc" },
                new() { FileName = "1.docx" },
                new() { FileName = "1.dot" },
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
        cut.Contains("fa-regular fa-file-excel");
        cut.Contains("fa-regular fa-file-word");
        cut.Contains("fa-regular fa-file-powerpoint");
        cut.Contains("fa-regular fa-file-audio");
        cut.Contains("fa-regular fa-file-video");
        cut.Contains("fa-regular fa-file-code");
        cut.Contains("fa-regular fa-file-pdf");
        cut.Contains("fa-regular fa-file-archive");
        cut.Contains("fa-regular fa-file-text");
        cut.Contains("fa-regular fa-file-image");
        cut.Contains("fa-regular fa-file");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.OnGetFileFormat, extensions =>
            {
                return "fa-format-test";
            });
        });
        cut.Contains("fa-format-test");
    }

    [Fact]
    public async Task CardUpload_Ok()
    {
        var zoom = false;
        var deleted = false;
        var cut = Context.RenderComponent<CardUpload<string>>(pb =>
        {
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

        // OnZoom
        await cut.InvokeAsync(() => cut.Find(".btn-zoom").Click());
        Assert.False(zoom);

        cut.SetParametersAndRender(pb =>
        {
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

        // OnDelete
        await cut.InvokeAsync(() => cut.Find(".btn-outline-danger").Click());
        Assert.True(deleted);

        // CanPreviewCallback
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.CanPreviewCallback, p =>
            {
                return false;
            });
        });
        await cut.InvokeAsync(() => cut.Find(".btn-zoom").Click());

        // ShowProgress
        cut.SetParametersAndRender(pb =>
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
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsUploadButtonAtFirst, true);
        });
    }

    [Fact]
    public async Task CardUpload_Reset()
    {
        var cut = Context.RenderComponent<CardUpload<string>>();
        await cut.InvokeAsync(() => cut.Instance.Reset());
        Assert.Null(cut.Instance.DefaultFileList);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.DefaultFileList,
            [
                new UploadFile() { FileName = "Test-File1.text" }
            ]);
        });
        await cut.InvokeAsync(() => cut.Instance.Reset());
        Assert.NotNull(cut.Instance.DefaultFileList);
        Assert.Empty(cut.Instance.DefaultFileList);
    }

    [Fact]
    public async Task CardUpload_ShowDownload()
    {
        var clicked = false;
        var cut = Context.RenderComponent<CardUpload<string>>(pb =>
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

        var button = cut.Find(".btn-download");
        await cut.InvokeAsync(() => button.Click());
        Assert.True(clicked);
    }

    [Fact]
    public void CardUpload_ShowZoom()
    {
        var clicked = false;
        var cut = Context.RenderComponent<CardUpload<string>>(pb =>
        {
            pb.Add(a => a.ShowZoomButton, true);
            pb.Add(a => a.OnZoomAsync, file =>
            {
                clicked = true;
                return Task.CompletedTask;
            });
            pb.Add(a => a.DefaultFileList,
            [
                new UploadFile() { FileName = "Test-File1.text" }
            ]);
        });

        var button = cut.Find(".btn-zoom");
        button.Click();
        cut.WaitForState(() => clicked);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowZoomButton, false);
        });
        cut.WaitForAssertion(() => cut.DoesNotContain("btn-zoom"));
    }

    [Fact]
    public void ShowDeletedButton_Ok()
    {
        var cut = Context.RenderComponent<CardUpload<string>>(pb =>
        {
            pb.Add(a => a.ShowDeletedButton, true);
            pb.Add(a => a.DefaultFileList,
            [
                new() { FileName = "Test-File1.text" }
            ]);
        });
        cut.Contains("aria-label=\"delete\"");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowDeletedButton, false);
        });
        cut.WaitForAssertion(() => cut.DoesNotContain("aria-label=\"delete\""));
    }

    [Fact]
    public void CardUpload_ValidateForm_Ok()
    {
        var foo = new Foo();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
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
    public void CardUpload_IconTemplate_Ok()
    {
        var foo = new Foo();
        var cut = Context.RenderComponent<CardUpload<string>>(pb =>
        {
            pb.Add(a => a.DefaultFileList,
            [
                new() { FileName = "Test-File1.text" }
            ]);
            pb.Add(a => a.IconTemplate, file => builder =>
            {
                builder.AddContent(0, "custom-file-icon-template");
            });
        });
        cut.Contains("custom-file-icon-template");
    }

    [Fact]
    public async Task CardUpload_ShowProgress_Ok()
    {
        var cancel = false;
        var cut = Context.RenderComponent<CardUpload<string>>(pb =>
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
    public async Task CardUpload_Max_Ok()
    {
        var cut = Context.RenderComponent<CardUpload<string>>(pb =>
        {
            pb.Add(a => a.ShowProgress, true);
            pb.Add(a => a.Max, 1);
        });
        var input = cut.FindComponent<InputFile>();
        await cut.InvokeAsync(async () =>
        {
            await input.Instance.OnChange.InvokeAsync(new InputFileChangeEventArgs(new List<MockBrowserFile>()
            {
                new()
            }));
        });
    }

    [Fact]
    public void FileSize_Ok()
    {
        var validator = new FileValidationAttribute()
        {
            FileSize = 5
        };
        var p = new Person()
        {
            Picture = new MockBrowserFile("test.log")
        };
        var result = validator.GetValidationResult(p.Picture, new ValidationContext(p));
        Assert.NotEqual(ValidationResult.Success, result);
    }

    [Fact]
    public void FileExtensions_Ok()
    {
        var validator = new FileValidationAttribute()
        {
            Extensions = ["jpg"]
        };
        var p = new Person()
        {
            Picture = new MockBrowserFile("test.log")
        };
        var result = validator.GetValidationResult(p.Picture, new ValidationContext(p));
        Assert.NotEqual(ValidationResult.Success, result);
    }

    [Fact]
    public void IsValid_Ok()
    {
        var validator = new FileValidationAttribute()
        {
            Extensions = ["jpg"]
        };
        var p = new Person()
        {
            Picture = new MockBrowserFile("test.log")
        };
        Assert.False(validator.IsValid(p.Picture));
    }

    [Fact]
    public void Validate_Ok()
    {
        var validator = new FileValidationAttribute()
        {
            Extensions = ["jpg"]
        };
        var p = new Person()
        {
            Picture = new MockBrowserFile("test.log")
        };
        Assert.Throws<ValidationException>(() => validator.Validate(p.Picture, "Picture"));
        Assert.Throws<ValidationException>(() => validator.Validate(p.Picture, new ValidationContext(p)));
    }

    [Fact]
    public void Capture_Ok()
    {
        var cut = Context.RenderComponent<ButtonUpload<string>>(pb =>
        {
            pb.Add(a => a.Capture, "camera");
        });
        cut.Contains("capture=\"camera\"");
    }

    [Fact]
    public void DropUpload_BodyTemplate_Ok()
    {
        var cut = Context.RenderComponent<DropUpload>(pb =>
        {
            pb.Add(a => a.BodyTemplate, b => b.AddContent(0, "drop-upload-body-template"));
        });
        cut.MarkupMatches("<div class=\"upload is-drop\" diff:ignore><div class=\"upload-drop-body\">drop-upload-body-template</div><ul class=\"upload-drop-list\"></ul><input hidden=\"hidden\" type=\"file\" diff:ignore></input></div>");
    }

    [Fact]
    public void DropUpload_IconTemplate_Ok()
    {
        var cut = Context.RenderComponent<DropUpload>(pb =>
        {
            pb.Add(a => a.IconTemplate, b => b.AddContent(0, "drop-upload-icon-template"));
        });
        cut.Contains("<div class=\"upload-drop-icon\">drop-upload-icon-template</div>");
    }

    [Fact]
    public void DropUpload_TextTemplate_Ok()
    {
        var cut = Context.RenderComponent<DropUpload>(pb =>
        {
            pb.Add(a => a.TextTemplate, b => b.AddContent(0, "drop-upload-text-template"));
        });
        cut.Contains("<div class=\"upload-drop-text\">drop-upload-text-template</div>");
    }

    [Fact]
    public void DropUpload_Footer_Ok()
    {
        var cut = Context.RenderComponent<DropUpload>(pb =>
        {
            pb.Add(a => a.ShowFooter, true);
        });
        cut.Contains("<div class=\"upload-drop-footer\"></div>");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.FooterTemplate, b => b.AddContent(0, "drop-upload-footer-text"));
        });
        cut.Contains("<div class=\"upload-drop-footer\">drop-upload-footer-text</div>");
    }

    [Fact]
    public async Task DropUpload_OnChanged_Ok()
    {
        var cut = Context.RenderComponent<DropUpload>(pb =>
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

    private class Person
    {
        [Required]
        [FileValidation(Extensions = [".png", ".jpg", ".jpeg"])]

        public IBrowserFile? Picture { get; set; }
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
