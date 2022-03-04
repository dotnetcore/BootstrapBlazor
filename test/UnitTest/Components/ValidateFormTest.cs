// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;

namespace UnitTest.Components;

public class ValidateFormTest : BootstrapBlazorTestBase
{
    [Fact]
    public void BootstrapBlazorDataAnnotationsValidator_Error()
    {
        Assert.ThrowsAny<InvalidOperationException>(() => Context.RenderComponent<BootstrapBlazorDataAnnotationsValidator>());
    }

    [Fact]
    public void Validate_Ok()
    {
        var valid = false;
        var invalid = false;
        var foo = new Foo();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.Add(a => a.OnValidSubmit, context =>
            {
                valid = true;
                return Task.CompletedTask;
            });
            pb.Add(a => a.OnInvalidSubmit, context =>
            {
                invalid = true;
                return Task.CompletedTask;
            });
            pb.AddChildContent<BootstrapInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Name);
                pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<string>(this, v => foo.Name = v));
                pb.Add(a => a.ValueExpression, foo.GenerateValueExpression());
            });
        });
        var form = cut.Find("form");
        cut.InvokeAsync(() => form.Submit());
        Assert.True(invalid);

        cut.InvokeAsync(() => cut.Find("input").Change("Test"));
        cut.InvokeAsync(() => form.Submit());
        Assert.True(valid);
    }

    [Fact]
    public void OnFieldValueChanged_Ok()
    {
        var changed = false;
        var foo = new Foo();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.Add(a => a.OnFieldValueChanged, (fieldName, v) =>
            {
                changed = true;
            });
            pb.AddChildContent<BootstrapInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Name);
                pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<string>(this, v => foo.Name = v));
                pb.Add(a => a.ValueExpression, foo.GenerateValueExpression());
            });
        });
        var form = cut.Find("form");
        cut.InvokeAsync(() => cut.Find("input").Change("Test"));
        cut.InvokeAsync(() => form.Submit());
        Assert.True(changed);
    }

    [Fact]
    public void ValidateAllProperties_Ok()
    {
        var foo = new Foo();
        var invalid = false;
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.Add(a => a.ValidateAllProperties, true);
            pb.Add(a => a.OnInvalidSubmit, context =>
            {
                invalid = true;
                return Task.CompletedTask;
            });
            pb.AddChildContent<BootstrapInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Name);
                pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<string>(this, v => foo.Name = v));
                pb.Add(a => a.ValueExpression, foo.GenerateValueExpression());
            });
        });
        var form = cut.Find("form");
        cut.InvokeAsync(() => form.Submit());
        Assert.True(invalid);
    }

    [Fact]
    public void ShowRequiredMark_Ok()
    {
        var foo = new Foo();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.Add(a => a.ShowRequiredMark, true);
            pb.AddChildContent<BootstrapInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Name);
                pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<string>(this, v => foo.Name = v));
                pb.Add(a => a.ValueExpression, foo.GenerateValueExpression());
            });
        });
        cut.Contains("required=\"true\"");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowRequiredMark, false);
        });
        cut.DoesNotContain("required=\"true\"");
    }

    [Fact]
    public void ShowLabel_Ok()
    {
        var foo = new Foo();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.Add(a => a.ShowLabel, true);
            pb.AddChildContent<BootstrapInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Name);
                pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<string>(this, v => foo.Name = v));
                pb.Add(a => a.ValueExpression, foo.GenerateValueExpression());
            });
        });
        cut.Contains("label");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowLabel, false);
        });
        cut.DoesNotContain("label");
    }

    [Fact]
    public void SetError_Ok()
    {
        var foo = new Foo();
        var dummy = new Dummy();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<BootstrapInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Name);
                pb.Add(a => a.ValueExpression, foo.GenerateValueExpression());
            });
            pb.AddChildContent<DateTimePicker<DateTime?>>(pb =>
            {
                pb.Add(a => a.Value, dummy.Value);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(dummy, "Value", typeof(DateTime?)));
            });
        });
        cut.Instance.SetError("Name", "Test_SetError");
        cut.Instance.SetError("Test.Name", "Test_SetError");
        cut.Instance.SetError<Foo>(f => f.Name, "Name_SetError");
    }

    [Fact]
    public void SetError_UnaryExpression()
    {
        var foo = new Foo();
        var dummy = new Dummy();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, dummy);
            pb.AddChildContent<DateTimePicker<DateTime?>>(pb =>
            {
                pb.Add(a => a.Value, foo.DateTime);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(foo, "DateTime", typeof(DateTime?)));
            });
            pb.AddChildContent<DateTimePicker<DateTime?>>(pb =>
            {
                pb.Add(a => a.Value, dummy.Value);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(dummy, "Value", typeof(DateTime?)));
            });
        });
        cut.Instance.SetError<Dummy>(f => f.Value, "Name_SetError");
    }

    [Fact]
    public void MetadataTypeAttribute_Ok()
    {
        var foo = new Dummy();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<DateTimePicker<DateTime?>>(pb =>
            {
                pb.Add(a => a.Value, foo.Value);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(foo, "Value", typeof(DateTime?)));
            });
        });
        var form = cut.Find("form");
        cut.InvokeAsync(() => form.Submit());
    }

    [Fact]
    public void Validate_Class_Ok()
    {
        var foo = new Dummy();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.Add(a => a.ValidateAllProperties, true);
            pb.AddChildContent<BootstrapInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Foo.Name);
            });
        });
        var form = cut.Find("form");
        cut.InvokeAsync(() => form.Submit());
    }

    [Fact]
    public void Validate_UploadFile_Ok()
    {
        var foo = new Dummy() { File = "text.txt" };
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<ButtonUpload<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.File);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(foo, "File", typeof(string)));
            });
        });
        var form = cut.Find("form");
        cut.InvokeAsync(() => form.Submit());
    }

    [Fact]
    public void Validate_Localizer_Ok()
    {
        var foo = new MockFoo();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<BootstrapInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Name);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(foo, "Name", typeof(string)));
            });
        });
        var form = cut.Find("form");
        cut.InvokeAsync(() => form.Submit());
    }

    [Fact]
    public void Validate_Attribute_Ok()
    {
        var foo = new MockFoo();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<BootstrapInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Rule);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(foo, "Rule", typeof(string)));
            });
        });
        var form = cut.Find("form");
        cut.InvokeAsync(() => form.Submit());
    }

    [Fact]
    public void Validate_MemberName_Ok()
    {
        var foo = new MockFoo();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<BootstrapInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Member);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(foo, "Member", typeof(string)));
            });
        });
        var form = cut.Find("form");
        cut.InvokeAsync(() => form.Submit());
    }

    [Fact]
    public void Validate_Address_Ok()
    {
        var foo = new MockFoo();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<BootstrapInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Address);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(foo, "Address", typeof(string)));
            });
        });
        var form = cut.Find("form");
        cut.InvokeAsync(() => form.Submit());
    }

    [Fact]
    public async Task ValidateFormButton_Ok()
    {
        var tcs = new TaskCompletionSource<bool>();
        var valid = false;
        var foo = new Foo() { Name = "Test" };
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(v => v.Model, foo);
            pb.Add(v => v.OnValidSubmit, context =>
            {
                valid = true;
                tcs.SetResult(true);
                return Task.CompletedTask;
            });
            pb.AddChildContent<BootstrapInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Name);
                pb.Add(a => a.ValueExpression, foo.GenerateValueExpression());
            });
            pb.AddChildContent<Button>(pb =>
            {
                pb.Add(b => b.IsAsync, true);
                pb.Add(b => b.ButtonType, ButtonType.Submit);
            });
        });
        await cut.InvokeAsync(() => cut.Find("form").Submit());
        await tcs.Task;
        Assert.True(valid);
    }

    [MetadataType(typeof(DummyMetadata))]
    private class Dummy
    {
        [Required]
        public DateTime? Value { get; set; }

        public Foo Foo { get; set; } = new Foo();

        [Required]
        public string? File { get; set; }
    }

    private class DummyMetadata
    {
        [Required]
        public DateTime? Value { get; set; }
    }

    private class MockFoo
    {
        [Required(ErrorMessage = "{0} is Required")]
        public string? Name { get; set; }

        [EmailAddress(ErrorMessage = "{0} must fill")]
        [Display(Name = "Address")]
        public string? Address { get; set; } = "test";

        [Required()]
        public string? Rule { get; set; }

        [EmailAddress()]
        public string? Member { get; set; } = "test";
    }
}
