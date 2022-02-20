// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using Microsoft.AspNetCore.Components.Forms;

namespace UnitTest.Components;

public class UploadTest : BootstrapBlazorTestBase
{
    [Fact]
    public void InputUpload_Ok()
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
        });
        var input = cut.FindComponent<InputFile>();
        cut.InvokeAsync(() => input.Instance.OnChange.InvokeAsync(new InputFileChangeEventArgs(new List<MockBrowserFile>()
        {
            new MockBrowserFile()
        })));
        Assert.Equal("UploadTestFile", uploadFile!.OriginFileName);
        cut.Contains("fa fa-folder-open-o");
        cut.Contains("btn-primary");
        cut.Contains("TestPlaceHolder");

        // 参数
        cut.SetParametersAndRender(pb => pb.Add(a => a.BrowserButtonIcon, "fa fa-browser"));
        cut.Contains("fa fa-browser");

        cut.SetParametersAndRender(pb => pb.Add(a => a.BrowserButtonClass, "btn btn-browser"));
        cut.Contains("btn btn-browser");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowDeleteButton, true);
            pb.Add(a => a.DeleteButtonText, "Delete-Test");
            pb.Add(a => a.DeleteButtonIcon, "fa fa-delete-icon");
        });
        cut.Contains("fa fa-delete-icon");
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
        cut.Contains("btn btn-delete");

        var button = cut.Find(".input-group button");
        cut.InvokeAsync(() => button.Click());
        Assert.True(deleted);

        // IsDisable
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsDisabled, true);
        });
        cut.Contains("btn btn-delete");
    }

    [Fact]
    public void InputUpload_ValidateForm_Ok()
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
        cut.InvokeAsync(() => form.Submit());
        Assert.True(invalid);

        var input = cut.FindComponent<InputFile>();
        cut.InvokeAsync(() => input.Instance.OnChange.InvokeAsync(new InputFileChangeEventArgs(new List<MockBrowserFile>()
        {
            new MockBrowserFile()
        })));
        cut.InvokeAsync(() => form.Submit());
        Assert.False(invalid);
    }

    [ExcludeFromCodeCoverage]
    private class MockBrowserFile : IBrowserFile
    {
        public MockBrowserFile()
        {
            Name = "UploadTestFile";
            LastModified = DateTimeOffset.Now;
            Size = 10 * 1024;
            ContentType = "jpg";
        }

        public string Name { get; }

        public DateTimeOffset LastModified { get; }

        public long Size { get; }

        public string ContentType { get; }

        public Stream OpenReadStream(long maxAllowedSize = 512000, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
