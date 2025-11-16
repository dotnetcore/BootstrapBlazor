// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using AngleSharp.Dom;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace UnitTest.Components;

public class UploadInputTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task InputUpload_Ok()
    {
        UploadFile? uploadFile = null;
        var cut = Context.Render<InputUpload<string>>(pb =>
        {
            pb.Add(a => a.Capture, "capture");
            pb.Add(a => a.PlaceHolder, "TestPlaceHolder");
            pb.Add(a => a.OnChange, file =>
            {
                uploadFile = file;
                return Task.CompletedTask;
            });
            pb.Add(a => a.Value, "test.jpg");
        });
        cut.Contains("value=\"test.jpg\"");
        cut.Contains("capture=\"capture\"");

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
        cut.Render(pb => pb.Add(a => a.BrowserButtonIcon, "fa-solid fa-chrome"));
        cut.Contains("fa-solid fa-chrome");

        cut.Render(pb => pb.Add(a => a.BrowserButtonClass, "btn btn-browser"));
        cut.Contains("btn btn-browser");

        cut.Render(pb =>
        {
            pb.Add(a => a.ShowDeleteButton, true);
            pb.Add(a => a.DeleteButtonText, "Delete-Test");
            pb.Add(a => a.DeleteButtonIcon, "fa-solid fa-trash");
        });
        cut.WaitForAssertion(() => cut.Contains("fa-solid fa-trash"));
        cut.Contains("btn-danger");

        // 删除逻辑
        var deleted = false;
        cut.Render(pb =>
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
        cut.Render(pb =>
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
        var cut = Context.Render<ValidateForm>(pb =>
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
        var cut = Context.Render<ValidateForm>(pb =>
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
    public async Task InputUpload_Value()
    {
        var cut = Context.Render<InputUpload<List<string>>>(pb =>
        {
            pb.Add(a => a.Value,
            [
                "test1.png",
                "test2.png"
            ]);
        });
        Assert.Contains("test1.png;test2.png", cut.Markup);

        var input = cut.FindComponent<InputFile>();
        await cut.InvokeAsync(() => input.Instance.OnChange.InvokeAsync(new InputFileChangeEventArgs(new List<MockBrowserFile>()
        {
            new("test3.png"),
            new("test4.png")
        })));
        Assert.Contains("test3.png;test4.png", cut.Markup);
    }

    [Fact]
    public async Task InputUpload_Files()
    {
        var cut = Context.Render<InputUpload<List<IBrowserFile>>>(pb =>
        {
            pb.Add(a => a.Value,
            [
                new MockBrowserFile("test1.png"),
                new MockBrowserFile("test2.png")
            ]);
        });
        Assert.Contains("test1.png;test2.png", cut.Markup);

        var input = cut.FindComponent<InputFile>();
        await cut.InvokeAsync(() => input.Instance.OnChange.InvokeAsync(new InputFileChangeEventArgs(new List<MockBrowserFile>()
        {
            new("test3.png"),
            new("test4.png")
        })));
        Assert.Contains("test3.png;test4.png", cut.Markup);

        // 重置后不应该包含新上传的文件
        await cut.InvokeAsync(() => cut.Instance.Reset());
        Assert.DoesNotContain("test3.png;test4.png", cut.Markup);

        cut.Render(pb =>
        {
            pb.Add(a => a.DefaultFileList,
            [
                new UploadFile() { FileName = "test5.png" },
                new UploadFile() { FileName = "test6.png" }
            ]);
            pb.Add(a => a.Value,
            [
                new MockBrowserFile("test5.png"),
                new MockBrowserFile("test6.png")
            ]);
        });
        Assert.Contains("test5.png;test6.png", cut.Markup);
        await cut.InvokeAsync(() => cut.Instance.Reset());
        Assert.DoesNotContain("test5.png;test6.png", cut.Markup);
    }

    [Fact]
    public void InputUpload_IsMultiple()
    {
        var cut = Context.Render<InputUpload<List<string>>>(pb =>
        {
            pb.Add(a => a.IsMultiple, false);
        });

        // 禁用多选功能
        cut.DoesNotContain("multiple=\"multiple\"");

        // 给定已上传文件后上传按钮应该被禁用
        cut.Render(pb =>
        {
            pb.Add(a => a.DefaultFileList,
            [
                new UploadFile() { FileName = "test1.png" },
                new UploadFile() { FileName = "test2.png" }
            ]);
        });
        var button = cut.Find(".btn-browser");
        Assert.True(button.IsDisabled());

        // 开启多选功能
        cut.Render(pb =>
        {
            pb.Add(a => a.IsMultiple, true);
        });
        cut.Contains("multiple=\"multiple\"");

        // 给定已上传文件后上传按钮不应该被禁用
        button = cut.Find(".btn-browser");
        Assert.False(button.IsDisabled());
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
