// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace UnitTest.Components;

public class UploadAvatarTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task AvatarUpload_Ok()
    {
        UploadFile? uploadFile = null;
        var cut = Context.Render<AvatarUpload<string>>(pb =>
        {
            pb.Add(a => a.CanPreviewCallback, null);
            pb.Add(a => a.IsMultiple, true);
            pb.Add(a => a.OnChange, file =>
            {
                uploadFile = file;
                return Task.CompletedTask;
            });
        });
        Assert.Contains("upload-item-plus", cut.Markup);

        var input = cut.FindComponent<InputFile>();
        await cut.InvokeAsync(() => input.Instance.OnChange.InvokeAsync(new InputFileChangeEventArgs(new List<MockBrowserFile>()
        {
            new()
        })));

        // Height/Width
        cut.Render(pb =>
        {
            pb.Add(a => a.Height, 40);
            pb.Add(a => a.Width, 50);
        });
        cut.Contains("width: 50px;");
        cut.Contains("height: 40px;");

        cut.Render(pb =>
        {
            pb.Add(a => a.IsCircle, true);
        });
        cut.Contains("height: 50px;");

        cut.Render(pb =>
        {
            pb.Add(a => a.Height, 0);
        });
        cut.Contains("height: 50px;");

        cut.Render(pb =>
        {
            pb.Add(a => a.BorderRadius, "10px");
        });
        cut.Contains("--bb-upload-item-border-radius: 10px;");

        // DefaultFileList
        cut.Render(pb =>
        {
            pb.Add(a => a.OnChange, null);
            pb.Add(a => a.ShowProgress, true);
            pb.Add(a => a.DefaultFileList,
            [
                new() { FileName = "Test-File" }
            ]);
        });
        input = cut.FindComponent<InputFile>();
        await cut.InvokeAsync(() => input.Instance.OnChange.InvokeAsync(new InputFileChangeEventArgs(new List<MockBrowserFile>()
        {
            new()
        })));

        // call preview // without using reflection, it is not possible to obtain the actual runtime values.
        await cut.InvokeAsync(() => cut.Instance.Preview("test-id"));

        // upload-item-actions
        var img = cut.Find(".upload-item-actions");
        await cut.InvokeAsync(() => img.Click());

        // upload-item-delete
        var button = cut.Find(".upload-item-delete");
        await cut.InvokeAsync(() => button.Click());

        cut.Contains("btn-browser");
        cut.Render(pb =>
        {
            pb.Add(a => a.IsDisabled, true);
        });

        // IsUploadButtonAtFirst
        cut.Render(pb =>
        {
            pb.Add(a => a.IsUploadButtonAtFirst, true);
            pb.Add(a => a.IsDisabled, false);
            pb.Add(a => a.IsMultiple, true);
        });
    }

    [Fact]
    public async Task AvatarUpload_ValidateForm_Ok()
    {
        var invalid = false;
        var foo = new Foo();
        var cut = Context.Render<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<AvatarUpload<string>>(pb =>
            {
                pb.Add(a => a.Accept, "Image");
                pb.Add(a => a.Value, foo.Name);
                pb.Add(a => a.ValueExpression, foo.GenerateValueExpression());
                pb.Add(a => a.AllowExtensions, [".jpg"]);
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

        // 设置 Disabled 取消校验
        var upload = cut.FindComponent<AvatarUpload<string>>();
        upload.Render(pb =>
        {
            pb.Add(a => a.IsDisabled, true);
        });

        Assert.DoesNotContain("is-invalid", upload.Markup);

        upload.Render(pb =>
        {
            pb.Add(a => a.IsDisabled, false);
        });
        // 清空所有文件
        var items = cut.FindAll(".upload-item-delete");
        Assert.Single(items);
        await cut.InvokeAsync(() => items[0].Click());
        form.Submit();
    }

    [Fact]
    public async Task DropUpload_ShowProgress_Ok()
    {
        var cut = Context.Render<AvatarUpload<string>>(pb =>
        {
            pb.Add(a => a.ShowProgress, true);
            pb.Add(a => a.OnChange, async file =>
            {
                await Task.Delay(100);
                await file.SaveToFileAsync("1.txt");
            });
        });
        var input = cut.FindComponent<InputFile>();
        await cut.InvokeAsync(() =>
        {
            _ = input.Instance.OnChange.InvokeAsync(new InputFileChangeEventArgs(new List<MockBrowserFile>()
            {
                new()
            }));
        });
    }

    [Fact]
    public void IsImage_Ok()
    {
        var file = new UploadFile
        {
            File = new MockBrowserFile("test.text")
        };
        Assert.True(file.IsImage([".text"]));

        file.File = new MockBrowserFile("test.jpg", "image/jpeg");
        Assert.True(file.IsImage());
    }

    [Fact]
    public async Task ValidateForm_ToggleMessage()
    {
        bool? invalid = null;
        var foo = new Person();
        var cut = Context.Render<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<AvatarUpload<List<IBrowserFile>>>(pb =>
            {
                pb.Add(a => a.IsMultiple, true);
                pb.Add(a => a.OnChange, async file =>
                {
                    await Task.Delay(10);
                });
                pb.Add(a => a.OnDelete, async file =>
                {
                    await Task.Delay(1);
                    return true;
                });
                pb.Add(a => a.Value, foo.Picture);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(foo, nameof(Person.Picture), typeof(List<IBrowserFile>)));
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

        // 直接提交表单
        var form = cut.Find("form");
        await cut.InvokeAsync(() => form.Submit());
        Assert.True(invalid);

        // 上传合规图片
        var input = cut.FindComponent<InputFile>();
        await cut.InvokeAsync(() => input.Instance.OnChange.InvokeAsync(new InputFileChangeEventArgs(new List<MockBrowserFile>()
        {
            new("test3.png"),
        })));
        form = cut.Find("form");
        await cut.InvokeAsync(() => form.Submit());
        Assert.False(invalid);

        // 上传不合规图片
        await cut.InvokeAsync(() => input.Instance.OnChange.InvokeAsync(new InputFileChangeEventArgs(new List<MockBrowserFile>()
        {
            new("test3.text"),
        })));
        form = cut.Find("form");
        await cut.InvokeAsync(() => form.Submit());
        Assert.True(invalid);

        // 删除不合规图片调用 RemoveValidResult 方法
        var items = cut.FindAll(".upload-item-delete");
        Assert.Equal(2, items.Count);
        await cut.InvokeAsync(() => items[1].Click());
    }

    private class Person
    {
        [Required]
        [FileValidation(Extensions = [".png", ".jpg", ".jpeg"])]
        public List<IBrowserFile>? Picture { get; set; }
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

    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "set_Uploaded")]
    static extern void SetUploaded(UploadFile @this, bool v);
}
