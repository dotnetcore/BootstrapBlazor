// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;

namespace UnitTest.Components;

public class ValidateFormTest : BootstrapBlazorTestBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddBootstrapBlazor();
        services.ConfigureJsonLocalizationOptions(op => op.AdditionalJsonAssemblies = new[] { GetType().Assembly });
    }

    [Fact]
    public void BootstrapBlazorDataAnnotationsValidator_Error()
    {
        Assert.ThrowsAny<InvalidOperationException>(() => Context.RenderComponent<BootstrapBlazorDataAnnotationsValidator>());
    }

    [Fact]
    public async Task Validate_Ok()
    {
        var valid = false;
        var invalid = false;
        var foo = new Foo();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.ShowLabelTooltip, true);
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
                pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<string?>(this, v => foo.Name = v));
                pb.Add(a => a.ValueExpression, foo.GenerateValueExpression());
            });
        });
        var form = cut.Find("form");
        await cut.InvokeAsync(() => form.Submit());
        Assert.True(invalid);

        await cut.InvokeAsync(() =>
        {
            cut.Find("input").Change("Test");
            form.Submit();
        });
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
                pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<string?>(this, v => foo.Name = v));
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
                pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<string?>(this, v => foo.Name = v));
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
                pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<string?>(this, v => foo.Name = v));
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
                pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<string?>(this, v => foo.Name = v));
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
    public void LabelWidth_Ok()
    {
        var foo = new Foo();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.Add(a => a.LabelWidth, 120);
            pb.AddChildContent<BootstrapInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Name);
                pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<string?>(this, v => foo.Name = v));
                pb.Add(a => a.ValueExpression, foo.GenerateValueExpression());
            });
        });

        cut.Contains("style=\"--bb-row-label-width: 120px;\"");
    }

    [Fact]
    public async Task SetError_Ok()
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
        await cut.InvokeAsync(() => cut.Instance.SetError("Name", "Test_SetError"));
        await cut.InvokeAsync(() => cut.Instance.SetError("Test.Name", "Test_SetError"));
        await cut.InvokeAsync(() => cut.Instance.SetError<Foo>(f => f.Name, "Name_SetError"));

        // 利用反射提高代码覆盖率
        var method = typeof(ValidateForm).GetMethod("TryGetValidator", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        Assert.NotNull(method);

        var ret = method.Invoke(cut.Instance, [typeof(Dummy), "Test", null]);
        Assert.False((bool?)ret);
    }

    [Fact]
    public async Task SetError_UnaryExpression()
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
        await cut.InvokeAsync(() => cut.Instance.SetError<Dummy>(f => f.Value, "Name_SetError"));

        // 利用反射提高代码覆盖率
        var fieldInfo = cut.Instance.GetType().GetField("_validatorCache", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;
        var cache = (ConcurrentDictionary<(string FieldName, Type ModelType), (FieldIdentifier FieldIdentifier, IValidateComponent ValidateComponent)>)fieldInfo.GetValue(cut.Instance)!;
        cache.Remove(("Value", typeof(Dummy)), out _);
        await cut.InvokeAsync(() => cut.Instance.SetError<Dummy>(f => f.Value, "Name_SetError"));
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
    public void MetadataTypeIValidatableObject_Ok()
    {
        var foo = new Dummy() { Password1 = "password", Password2 = "Password2" };
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<MockInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Password1);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(foo, "Password1", typeof(string)));
            });
            pb.AddChildContent<MockInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Password2);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(foo, "Password2", typeof(string)));
            });
        });
        var form = cut.Find("form");
        cut.InvokeAsync(() => form.Submit());
        var message = cut.FindComponent<MockInput<string>>().Instance.GetErrorMessage();
        Assert.Equal("两次密码必须一致。", message);
    }

    [Fact]
    public async Task MetadataTypeIValidateCollection_Ok()
    {
        var model = new Dummy2() { Value1 = 0, Value2 = 0 };
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, model);
            pb.AddChildContent<MockInput<int>>(pb =>
            {
                pb.Add(a => a.Value, model.Value1);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(model, "Value1", typeof(int)));
            });
            pb.AddChildContent<MockInput<int>>(pb =>
            {
                pb.Add(a => a.Value, model.Value2);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(model, "Value2", typeof(int)));
            });
        });
        var form = cut.Find("form");
        await cut.InvokeAsync(() => form.Submit());
        var input = cut.FindComponent<MockInput<int>>();
        var all = cut.FindComponents<MockInput<int>>();
        var input2 = all[all.Count - 1];
        Assert.Null(input.Instance.GetErrorMessage());
        Assert.Equal("Value2 必须大于 0", input2.Instance.GetErrorMessage());

        model.Value1 = 0;
        model.Value2 = 2;
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Model, model);
        });
        await cut.InvokeAsync(() => form.Submit());
        Assert.Equal("Value1 必须大于 Value2", input.Instance.GetErrorMessage());
        Assert.Equal("Value1 必须大于 Value2", input2.Instance.GetErrorMessage());
    }

    [Fact]
    public void Validate_Class_Ok()
    {
        var dummy = new Dummy();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, dummy);
            pb.Add(a => a.ValidateAllProperties, true);
            pb.AddChildContent<BootstrapInput<string>>(pb =>
            {
                pb.Add(a => a.Value, dummy.Foo.Name);
            });
            pb.AddChildContent<BootstrapInput<Foo>>(pb =>
            {
                pb.Add(a => a.Value, dummy.Foo);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(dummy, nameof(dummy.Foo), typeof(Foo)));
            });
        });
        var form = cut.Find("form");
        cut.InvokeAsync(() => form.Submit());
    }

    [Fact]
    public async Task ValidateAll_Ok()
    {
        var invalid = false;
        var dummy = new Dummy();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, dummy);
            pb.Add(a => a.ValidateAllProperties, false);
            pb.AddChildContent<BootstrapInput<Foo>>(pb =>
            {
                pb.Add(a => a.Value, dummy.Foo);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(dummy, nameof(dummy.Foo), typeof(Foo)));
            });
            pb.Add(a => a.OnInvalidSubmit, context =>
            {
                invalid = true;
                return Task.CompletedTask;
            });
        });
        var form = cut.Find("form");
        await cut.InvokeAsync(() => form.Submit());
        Assert.False(invalid);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ValidateAllProperties, true);
        });
        await cut.InvokeAsync(() => form.Submit());
        Assert.True(invalid);
    }

    [Fact]
    public async Task Validate_UploadFile_Ok()
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
        await cut.InvokeAsync(() => form.Submit());
    }

    [Fact]
    public async Task Validate_Localizer_Ok()
    {
        var foo = new MockFoo();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<MockInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Name);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(foo, "Name", typeof(string)));
            });
        });
        var form = cut.Find("form");
        await cut.InvokeAsync(() => form.Submit());
        var msg = cut.FindComponent<MockInput<string>>().Instance.GetErrorMessage();
        Assert.Equal("Name is Required", msg);
    }

    [Fact]
    public async Task Validate_Attribute_Ok()
    {
        var foo = new MockFoo();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<MockInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Rule);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(foo, "Rule", typeof(string)));
            });
        });
        var form = cut.Find("form");
        await cut.InvokeAsync(() => form.Submit());
        var msg = cut.FindComponent<MockInput<string>>().Instance.GetErrorMessage();
        Assert.Equal("Rule is Required", msg);
    }

    [Fact]
    public async Task Validate_MemberName_Ok()
    {
        var foo = new MockFoo();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<MockInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Member);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(foo, "Member", typeof(string)));
            });
        });
        var form = cut.Find("form");
        await cut.InvokeAsync(() => form.Submit());
        var msg = cut.FindComponent<MockInput<string>>().Instance.GetErrorMessage();
        Assert.Equal("Member is Required", msg);
    }

    [Fact]
    public void Validate_Address_Ok()
    {
        var foo = new MockFoo();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<MockInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Address);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(foo, "Address", typeof(string)));
            });
        });
        var form = cut.Find("form");
        cut.InvokeAsync(() => form.Submit());
        var msg = cut.FindComponent<MockInput<string>>().Instance.GetErrorMessage();
        Assert.Equal("Address must fill", msg);
    }

    [Fact]
    public async Task ValidateFormButton_Valid()
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

    [Fact]
    public async Task ValidateFormButton_Invalid()
    {
        var tcs = new TaskCompletionSource<bool>();
        var valid = true;
        var foo = new Foo();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(v => v.Model, foo);
            pb.Add(a => a.OnInvalidSubmit, context =>
            {
                valid = false;
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
        Assert.False(valid);
    }

    [Fact]
    public async Task ValidateFromCode_Ok()
    {
        var foo = new Foo();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<BootstrapInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Address);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(foo, "Address", typeof(string)));
            });
        });
        Assert.Contains("form-control valid", cut.Markup);

        var form = cut.Instance;
        await cut.InvokeAsync(() => form.Validate());
        Assert.Contains("form-control valid is-invalid", cut.Markup);
    }

    [Fact]
    public async Task Validate_Service_Ok()
    {
        var foo = new HasService();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<MockInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Tag);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(foo, "Tag", typeof(string)));
                pb.Add(a => a.ValidateRules, [new FormItemValidator(new HasServiceAttribute())]);
            });
        });
        var form = cut.Find("form");
        await cut.InvokeAsync(() => form.Submit());
        var msg = cut.FindComponent<MockInput<string>>().Instance.GetErrorMessage();
        Assert.Equal(HasServiceAttribute.Success, msg);
    }
    
    [Fact]
    public async Task TestService_Ok()
    {
        // 自定义验证规则没有使用约定 Attribute 结尾单元测试
        var foo = new HasService();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<MockInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Tag2);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(foo, "Tag2", typeof(string)));
                pb.Add(a => a.ValidateRules, [new FormItemValidator(new TestValidateRule())]);
            });
        });
        var form = cut.Find("form");
        await cut.InvokeAsync(() => form.Submit());
        var msg = cut.FindComponent<MockInput<string>>().Instance.GetErrorMessage();
        Assert.Equal("Test", msg);
    }

    [Fact]
    public async Task RequiredValidator_Ok()
    {
        var foo = new HasService();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<MockInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Tag);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(foo, "Tag", typeof(string)));
                pb.Add(a => a.ValidateRules, [new RequiredValidator()]);
            });
        });
        var form = cut.Find("form");
        await cut.InvokeAsync(() => form.Submit());
        var msg = cut.FindComponent<MockInput<string>>().Instance.GetErrorMessage();
        Assert.Equal(HasServiceAttribute.Success, msg);
    }

    [Fact]
    public void DisableAutoSubmitFormByEnter_Ok()
    {
        var options = Context.Services.GetRequiredService<IOptionsMonitor<BootstrapBlazorOptions>>();
        options.CurrentValue.DisableAutoSubmitFormByEnter = true;

        var foo = new Foo() { Name = "Test" };
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(v => v.Model, foo);
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

        Assert.True(cut.Instance.DisableAutoSubmitFormByEnter);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.DisableAutoSubmitFormByEnter, false);
        });
        Assert.False(cut.Instance.DisableAutoSubmitFormByEnter);
    }

    [Fact]
    public void ValidateFieldAsync_Ok()
    {
        var form = new ValidateForm();
        var method = typeof(ValidateForm).GetMethod("ValidateFieldAsync", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        Assert.NotNull(method);

        var context = new ValidationContext(new Foo());
        var result = new List<ValidationResult>();
        method.Invoke(form, [context, result]);
    }

    [Fact]
    public async Task IValidatableObject_Ok()
    {
        var model = new MockValidataModel() { Telephone1 = "123", Telephone2 = "123" };
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, model);
            pb.AddChildContent<MockInput<string>>(pb =>
            {
                pb.Add(a => a.Value, model.Telephone1);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(model, "Telephone1", typeof(string)));
            });
            pb.AddChildContent<MockInput<string>>(pb =>
            {
                pb.Add(a => a.Value, model.Telephone2);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(model, "Telephone2", typeof(string)));
            });
        });
        var form = cut.Find("form");
        await cut.InvokeAsync(() => form.Submit());
        var message = cut.FindComponent<MockInput<string>>().Instance.GetErrorMessage();
        Assert.Equal("Telephone1 and Telephone2 can not be the same", message);
    }

    [Fact]
    public async Task IValidateCollection_Ok()
    {
        var model = new MockValidateCollectionModel() { Telephone1 = "123", Telephone2 = "123" };
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, model);
            pb.AddChildContent<MockInput<string>>(pb =>
            {
                pb.Add(a => a.Value, model.Telephone1);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(model, "Telephone1", typeof(string)));
                pb.Add(a => a.ValueChanged, v => model.Telephone1 = v);
            });
            pb.AddChildContent<MockInput<string>>(pb =>
            {
                pb.Add(a => a.Value, model.Telephone2);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(model, "Telephone2", typeof(string)));
                pb.Add(a => a.ValueChanged, v => model.Telephone2 = v);
            });
        });
        var form = cut.Find("form");
        await cut.InvokeAsync(() => form.Submit());
        var input = cut.FindComponent<MockInput<string>>();
        var all = cut.FindComponents<MockInput<string>>();
        var input2 = all[all.Count - 1];
        Assert.Equal("Telephone1 and Telephone2 can not be the same", input.Instance.GetErrorMessage());
        Assert.Equal("Telephone1 and Telephone2 can not be the same", input2.Instance.GetErrorMessage());

        // 触发符合条件后联动
        var inputEl = cut.Find("input");
        await cut.InvokeAsync(() => inputEl.Change("1234"));
        var message = input.Instance.GetErrorMessage();
        Assert.Null(message);
        cut.SetParametersAndRender();
        message = input2.Instance.GetErrorMessage();
        Assert.Null(message);

        var allInputs = cut.FindAll("input");
        var inputEl2 = allInputs[all.Count - 1];
        await cut.InvokeAsync(() => inputEl2.Change("1234"));
        message = input2.Instance.GetErrorMessage();
        Assert.Equal("Telephone1 and Telephone2 can not be the same", message);
        cut.SetParametersAndRender();
        message = input.Instance.GetErrorMessage();
        Assert.Equal("Telephone1 and Telephone2 can not be the same", message);
    }

    [Fact]
    public void ShowAllInvalidResult_Ok()
    {
        var model = new Foo();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, model);
            pb.AddChildContent<BootstrapInput<string>>(pb =>
            {
                pb.Add(a => a.Value, model.Name);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(model, "Name", typeof(string)));
            });
        });
        cut.DoesNotContain("data-bb-invalid-result");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowAllInvalidResult, true);
        });
        cut.Contains("data-bb-invalid-result");
    }

    private class HasServiceAttribute : ValidationAttribute
    {
        public const string Success = "Has Service";
        private const string Error = "No Service";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var hasService = validationContext.GetService<IServiceProvider>();
            if (hasService is null)
                return new(Error);
            else
                return new(Success);
        }
    }

    private class TestValidateRule : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            return new("Test");
        }
    }

    private class HasService
    {
        [HasService]
        public string? Tag { get; set; }
        
        [TestValidateRule]
        public string? Tag2 { get; set; }
    }

    [MetadataType(typeof(DummyMetadata))]
    private class Dummy
    {
        public DateTime? Value { get; set; }

        public Foo Foo { get; set; } = new Foo();

        [Required]
        public string? File { get; set; }

        public string? Password1 { get; set; }

        public string? Password2 { get; set; }
    }

    private class DummyMetadata : IValidatableObject
    {
        [Required]
        public DateTime? Value { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = new List<ValidationResult>();
            if (validationContext.ObjectInstance is Dummy dy)
            {
                if (!string.Equals(dy.Password1, dy.Password2, StringComparison.InvariantCultureIgnoreCase))
                {
                    result.Add(new ValidationResult("两次密码必须一致。", [nameof(Dummy.Password1), nameof(Dummy.Password2)]));
                }
            }
            return result;
        }
    }

    [MetadataType(typeof(Dummy2MetadataCollection))]
    private class Dummy2
    {
        public int Value1 { get; set; }

        public int Value2 { get; set; }
    }

    public class Dummy2MetadataCollection : IValidateCollection
    {
        [Required]
        public int Value1 { get; set; }

        [CustomValidation(typeof(Dummy2MetadataCollection), nameof(CustomValidate), ErrorMessage = "{0} 必须大于 0")]
        [Required]
        public int Value2 { get; set; }

        private readonly List<string> _validMemberNames = [];

        public List<string> GetValidMemberNames() => _validMemberNames;

        private readonly List<ValidationResult> _invalidMemberNames = [];

        public List<ValidationResult> GetInvalidMemberNames() => _invalidMemberNames;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            _invalidMemberNames.Clear();
            _validMemberNames.Clear();
            if (validationContext.ObjectInstance is Dummy2 dummy)
            {
                if (dummy.Value1 < dummy.Value2)
                {
                    _invalidMemberNames.Add(new ValidationResult("Value1 必须大于 Value2", [nameof(Dummy2.Value1), nameof(Dummy2.Value2)]));
                }
                else
                {
                    _validMemberNames.AddRange([nameof(Dummy2.Value1), nameof(Dummy2.Value2)]);
                }
            }
            return _invalidMemberNames;
        }

        public static ValidationResult? CustomValidate(object value, ValidationContext context)
        {
            ValidationResult? ret = null;
            if (value is int v && v < 1)
            {
                ret = new ValidationResult("Value2 必须大于 0", ["Value2"]);
            }
            return ret;
        }
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

    private class MockValidataModel : IValidatableObject
    {
        public string? Telephone1 { get; set; }

        public string? Telephone2 { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.Equals(Telephone1, Telephone2, StringComparison.InvariantCultureIgnoreCase))
            {
                yield return new ValidationResult("Telephone1 and Telephone2 can not be the same", [nameof(Telephone1), nameof(Telephone2)]);
            }
        }
    }

    private class MockValidateCollectionModel : IValidateCollection
    {
        /// <summary>
        /// 联系电话1
        /// </summary>
        public string? Telephone1 { get; set; }

        /// <summary>
        /// 联系电话2
        /// </summary>
        public string? Telephone2 { get; set; }

        private readonly List<string> _validMemberNames = [];

        private readonly List<ValidationResult> _invalidMemberNames = [];

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            _validMemberNames.Clear();
            _invalidMemberNames.Clear();
            if (string.Equals(Telephone1, Telephone2, StringComparison.InvariantCultureIgnoreCase))
            {
                var errorMessage = "Telephone1 and Telephone2 can not be the same";
                if (validationContext.MemberName == nameof(Telephone1))
                {
                    _invalidMemberNames.Add(new ValidationResult(errorMessage, [nameof(Telephone2)]));
                }
                else if (validationContext.MemberName == nameof(Telephone2))
                {
                    _invalidMemberNames.Add(new ValidationResult(errorMessage, [nameof(Telephone1)]));
                }
                yield return new ValidationResult(errorMessage, [validationContext.MemberName!]);
            }
            else if (validationContext.MemberName == nameof(Telephone1))
            {
                _validMemberNames.Add(nameof(Telephone2));

            }
            else if (validationContext.MemberName == nameof(Telephone2))
            {
                _validMemberNames.Add(nameof(Telephone1));
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public List<string> GetValidMemberNames() => _validMemberNames;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public List<ValidationResult> GetInvalidMemberNames() => _invalidMemberNames;
    }

    private class MockInput<TValue> : BootstrapInput<TValue>
    {
        public string? GetErrorMessage() => base.ErrorMessage;
    }
}
