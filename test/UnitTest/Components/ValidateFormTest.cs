// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace UnitTest.Components;

public class ValidateFormTest : BootstrapBlazorTestBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddBootstrapBlazor();
        services.ConfigureJsonLocalizationOptions(op => op.AdditionalJsonAssemblies = new[] { GetType().Assembly });
        services.AddSingleton<ILogger<BootstrapBlazorDataAnnotationsValidator>, ValidateFormTestLogger>();
    }

    [Fact]
    public void BootstrapBlazorDataAnnotationsValidator_Error()
    {
        Assert.ThrowsAny<InvalidOperationException>(() => Context.Render<BootstrapBlazorDataAnnotationsValidator>());
    }

    [Fact]
    public async Task ValidateAsync_Invalid()
    {
        var foo = new Foo();
        var cut = Context.Render<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<BootstrapInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Name);
                pb.Add(a => a.ValueExpression, foo.GenerateValueExpression());
            });
        });
        var validator = cut.FindComponent<BootstrapBlazorDataAnnotationsValidator>().Instance;
        var property = typeof(BootstrapBlazorDataAnnotationsValidator).GetProperty(
            "CurrentEditContext",
            System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        var editContext = Assert.IsType<EditContext>(property?.GetValue(validator));

        var valid = await cut.InvokeAsync(() => cut.Instance.ValidateAsync(CancellationToken.None));

        Assert.False(valid);
        Assert.NotEmpty(editContext.GetValidationMessages());
    }

    [Fact]
    public async Task ValidateAsync_Exception()
    {
        var foo = new Foo() { Name = "Test" };
        var cut = Context.Render<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<BootstrapInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Name);
                pb.Add(a => a.ValueExpression, foo.GenerateValueExpression());
                pb.Add(a => a.ValidateRules, [new ThrowingValidator()]);
            });
        });

        var valid = await cut.InvokeAsync(() => cut.Instance.ValidateAsync(CancellationToken.None));

        Assert.False(valid);
    }

    [Fact]
    public async Task FieldValidation_CancelsPreviousOperation()
    {
        var rule = new CancellableFieldValidator();
        var foo = new Foo() { Name = "Initial" };
        var cut = Context.Render<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<BootstrapInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Name);
                pb.Add(a => a.ValueChanged, value => foo.Name = value);
                pb.Add(a => a.ValueExpression, foo.GenerateValueExpression());
                pb.Add(a => a.ValidateRules, [rule]);
            });
        });

        await cut.InvokeAsync(() => cut.Find("input").Change("First"));
        await rule.FirstValidationStarted.Task.WaitAsync(CancellationToken.None);
        await cut.InvokeAsync(() => cut.Find("input").Change("Second"));

        await rule.FirstValidationCancelled.Task.WaitAsync(CancellationToken.None);
        await rule.SecondValidationCompleted.Task.WaitAsync(CancellationToken.None);

        Assert.True(rule.FirstValidationCancelled.Task.IsCompletedSuccessfully);
        Assert.True(rule.SecondValidationCompleted.Task.IsCompletedSuccessfully);
    }

    [Fact]
    public async Task ValidateAsync_OperationCancellation()
    {
        var foo = new Foo() { Name = "Test" };
        var cut = Context.Render<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<BootstrapInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Name);
                pb.Add(a => a.ValueExpression, foo.GenerateValueExpression());
            });
        });
        using var tokenSource = new CancellationTokenSource();
        tokenSource.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            async () => await cut.InvokeAsync(() => cut.Instance.ValidateAsync(tokenSource.Token)));
    }

    [Fact]
    public async Task OnValidationRequested_Ok()
    {
        var foo = new Foo();
        var cut = Context.Render<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<BootstrapInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Name);
                pb.Add(a => a.ValueExpression, foo.GenerateValueExpression());
            });
        });
        var validator = cut.FindComponent<BootstrapBlazorDataAnnotationsValidator>().Instance;
        var property = typeof(BootstrapBlazorDataAnnotationsValidator).GetProperty(
            "CurrentEditContext",
            System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        var editContext = Assert.IsType<EditContext>(property?.GetValue(validator));

#if NET11_0_OR_GREATER
        await cut.InvokeAsync(() => editContext.ValidateAsync());
#else
        await cut.InvokeAsync(() => editContext.Validate());
        cut.WaitForAssertion(() => Assert.NotEmpty(editContext.GetValidationMessages()));
#endif
    }

    [Fact]
    public async Task OnValidationRequested_Exception()
    {
        var logger = Assert.IsType<ValidateFormTestLogger>(
            Context.Services.GetRequiredService<ILogger<BootstrapBlazorDataAnnotationsValidator>>());
        var foo = new Foo() { Name = "Test" };
        var cut = Context.Render<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<BootstrapInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Name);
                pb.Add(a => a.ValueExpression, foo.GenerateValueExpression());
                pb.Add(a => a.ValidateRules, [new ThrowingValidator()]);
            });
        });
        var validator = cut.FindComponent<BootstrapBlazorDataAnnotationsValidator>().Instance;
        var property = typeof(BootstrapBlazorDataAnnotationsValidator).GetProperty(
            "CurrentEditContext",
            System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        var editContext = Assert.IsType<EditContext>(property?.GetValue(validator));

#if NET11_0_OR_GREATER
        await cut.InvokeAsync(() => editContext.ValidateAsync());
#else
        await cut.InvokeAsync(() => editContext.Validate());
        cut.WaitForAssertion(() => Assert.IsType<InvalidOperationException>(logger.Exception));
#endif
    }

#if !NET11_0_OR_GREATER
    [Fact]
    public async Task ValidateFieldAndCleanupAsync_OperationCancellation()
    {
        var foo = new Foo() { Name = "Test" };
        var cut = Context.Render<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<BootstrapInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Name);
                pb.Add(a => a.ValueExpression, foo.GenerateValueExpression());
            });
        });
        var validator = cut.FindComponent<BootstrapBlazorDataAnnotationsValidator>().Instance;
        var validatorType = typeof(BootstrapBlazorDataAnnotationsValidator);
        var operationType = validatorType.GetNestedType(
            "FieldValidationOperation",
            System.Reflection.BindingFlags.NonPublic);
        Assert.NotNull(operationType);
        var operation = Activator.CreateInstance(operationType, nonPublic: true);
        Assert.NotNull(operation);
        var method = validatorType.GetMethod(
            "ValidateFieldAndCleanupAsync",
            System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        Assert.NotNull(method);
        var cancelMethod = operationType.GetMethod(nameof(CancellationTokenSource.Cancel));
        Assert.NotNull(cancelMethod);
        cancelMethod.Invoke(operation, null);

        await cut.InvokeAsync(async () =>
        {
            var validation = Assert.IsType<Task>(
                method.Invoke(validator, [new FieldIdentifier(foo, nameof(foo.Name)), operation]), exactMatch: false);
            await validation;
        });
    }
#endif

    [Fact]
    public async Task Validate_Ok()
    {
        var valid = false;
        var invalid = false;
        var changed = false;
        var foo = new Foo();
        var cut = Context.Render<ValidateForm>(pb =>
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
        await cut.InvokeAsync(() => form.Submit());
        Assert.True(invalid);
        Assert.False(changed);

        await cut.InvokeAsync(() =>
        {
            cut.Find("input").Change("Test");
            form.Submit();
        });
        Assert.True(valid);
        Assert.True(changed);
    }

    [Fact]
    public void ValidateAllProperties_Ok()
    {
        var foo = new Foo();
        var invalid = false;
        var cut = Context.Render<ValidateForm>(pb =>
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
        var cut = Context.Render<ValidateForm>(pb =>
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

        cut.Render(pb =>
        {
            pb.Add(a => a.ShowRequiredMark, false);
        });
        cut.DoesNotContain("required=\"true\"");
    }

    [Fact]
    public void ShowLabel_Ok()
    {
        var foo = new Foo();
        var cut = Context.Render<ValidateForm>(pb =>
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

        cut.Render(pb =>
        {
            pb.Add(a => a.ShowLabel, false);
        });
        cut.DoesNotContain("label");
    }

    [Fact]
    public void LabelWidth_Ok()
    {
        var foo = new Foo();
        var cut = Context.Render<ValidateForm>(pb =>
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
        var cut = Context.Render<ValidateForm>(pb =>
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
        var cut = Context.Render<ValidateForm>(pb =>
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
        var cache = (ConcurrentDictionary<FieldIdentifier, IValidateComponent>)fieldInfo.GetValue(cut.Instance)!;
        cache.Remove(new FieldIdentifier(dummy, "Value"), out _);
        await cut.InvokeAsync(() => cut.Instance.SetError<Dummy>(f => f.Value, "Name_SetError"));
    }

    [Fact]
    public void MetadataTypeAttribute_Ok()
    {
        var foo = new Dummy();
        var cut = Context.Render<ValidateForm>(pb =>
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
        var cut = Context.Render<ValidateForm>(pb =>
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
        var cut = Context.Render<ValidateForm>(pb =>
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
        cut.Render(pb =>
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
        var cut = Context.Render<ValidateForm>(pb =>
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
        var cut = Context.Render<ValidateForm>(pb =>
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

        cut.Render(pb =>
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
        var cut = Context.Render<ValidateForm>(pb =>
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
        var cut = Context.Render<ValidateForm>(pb =>
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
        var cut = Context.Render<ValidateForm>(pb =>
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
        var cut = Context.Render<ValidateForm>(pb =>
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
        var cut = Context.Render<ValidateForm>(pb =>
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
        var cut = Context.Render<ValidateForm>(pb =>
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
        var cut = Context.Render<ValidateForm>(pb =>
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
        var cut = Context.Render<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<BootstrapInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Address);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(foo, "Address", typeof(string)));
            });
        });
        Assert.Contains("form-control valid", cut.Markup);

        var form = cut.Find("form");
        await cut.InvokeAsync(() => form.Submit());
        Assert.Contains("form-control invalid is-invalid", cut.Markup);
    }

    [Fact]
    public async Task Validate_Service_Ok()
    {
        var foo = new HasService();
        var cut = Context.Render<ValidateForm>(pb =>
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
        var cut = Context.Render<ValidateForm>(pb =>
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
        var cut = Context.Render<ValidateForm>(pb =>
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
        var property = typeof(ValidateForm).GetProperty("DisableAutoSubmitString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        Assert.NotNull(property);

        var foo = new Foo() { Name = "Test" };
        var cut = Context.Render<ValidateForm>(pb =>
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
        Assert.Equal("true", property.GetValue(cut.Instance));
        Assert.Equal("true", cut.Find("form").GetAttribute("data-bb-dissubmit"));

        cut.Render(pb =>
        {
            pb.Add(a => a.DisableAutoSubmitFormByEnter, false);
        });
        Assert.False(cut.Instance.DisableAutoSubmitFormByEnter);
        Assert.Null(property.GetValue(cut.Instance));
        Assert.Null(cut.Find("form").GetAttribute("data-bb-dissubmit"));

        cut.Render(pb =>
        {
            pb.Add(a => a.DisableAutoSubmitFormByEnter, true);
            pb.Add(a => a.IsFormless, true);
        });

        Assert.True(cut.Instance.DisableAutoSubmitFormByEnter);
        Assert.True(cut.Instance.IsFormless);
        Assert.Null(property.GetValue(cut.Instance));
        Assert.Empty(cut.FindAll("form"));
    }

    [Fact]
    public void ValidateFieldAsync_Ok()
    {
        var form = new ValidateForm();
        var method = typeof(ValidateForm).GetMethod("ValidateFieldAsync", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        Assert.NotNull(method);

        var model = new Foo();
        var fieldIdentifier = new FieldIdentifier(model, "Name");
        var context = new ValidationContext(model)
        {
            MemberName = "Name"
        };
        var result = new List<ValidationResult>();
        method.Invoke(form, [fieldIdentifier, context, result, CancellationToken.None]);
    }

    [Fact]
    public async Task IValidatableObject_Ok()
    {
        var model = new MockValidataModel() { Telephone1 = "123", Telephone2 = "123" };
        var cut = Context.Render<ValidateForm>(pb =>
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
    public async Task IValidatableObject_ModelError()
    {
        var model = new MockModelError();
        var cut = Context.Render<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, model);
            pb.AddChildContent<MockInput<string>>(pb =>
            {
                pb.Add(a => a.Value, model.Name);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(model, nameof(model.Name), typeof(string)));
            });
        });
        var validator = cut.FindComponent<BootstrapBlazorDataAnnotationsValidator>().Instance;
        var property = typeof(BootstrapBlazorDataAnnotationsValidator).GetProperty(
            "CurrentEditContext",
            System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        var editContext = Assert.IsType<EditContext>(property?.GetValue(validator));

        var valid = await cut.InvokeAsync(() => cut.Instance.ValidateAsync(CancellationToken.None));

        Assert.False(valid);
        var messages = editContext.GetValidationMessages(new FieldIdentifier(model, string.Empty));
        Assert.Equal(["Model validation failed"], messages);
    }

    [Fact]
    public async Task IAsyncValidatableObject_Ok()
    {
        var model = new MockAsyncValidatableModel();
        var cut = Context.Render<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, model);
            pb.AddChildContent<MockInput<string>>(pb =>
            {
                pb.Add(a => a.Value, model.Name);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(model, nameof(model.Name), typeof(string)));
            });
        });

        var valid = await cut.InvokeAsync(() => cut.Instance.ValidateAsync(CancellationToken.None));

        Assert.False(valid);
        Assert.True(model.AsyncValidated);
        Assert.False(model.SyncValidated);
        Assert.Equal("Async validation failed", cut.FindComponent<MockInput<string>>().Instance.GetErrorMessage());
    }

    [Fact]
    public async Task IAsyncValidatableObject_ModelError()
    {
        var model = new MockAsyncModelError();
        var cut = Context.Render<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, model);
            pb.AddChildContent<MockInput<string>>(pb =>
            {
                pb.Add(a => a.Value, model.Name);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(model, nameof(model.Name), typeof(string)));
            });
        });

        var valid = await cut.InvokeAsync(() => cut.Instance.ValidateAsync(CancellationToken.None));
        Assert.False(valid);
    }

    [Fact]
    public async Task AsyncValidationAttribute_Ok()
    {
        var model = new MockAsyncValidationAttributeModel();
        var cut = Context.Render<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, model);
            pb.AddChildContent<MockInput<string>>(pb =>
            {
                pb.Add(a => a.Value, model.Name);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(model, nameof(model.Name), typeof(string)));
            });
        });

        var valid = await cut.InvokeAsync(() => cut.Instance.ValidateAsync(CancellationToken.None));

        Assert.False(valid);
        Assert.Equal("Async attribute validation failed", cut.FindComponent<MockInput<string>>().Instance.GetErrorMessage());
    }

    [Fact]
    public async Task AsyncValidationAttribute_PendingState()
    {
        var model = new MockPendingAsyncValidationModel();
        var cut = Context.Render<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, model);
            pb.AddChildContent<MockInput<string>>(pb =>
            {
                pb.Add(a => a.Value, model.Name);
                pb.Add(a => a.ValueChanged, value => model.Name = value);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(model, nameof(model.Name), typeof(string)));
            });
        });
        var input = cut.Find("input");

        await cut.InvokeAsync(() => input.Change("Blazor"));
        await model.ValidationStarted.Task.WaitAsync(CancellationToken.None);

        Assert.Null(cut.FindComponent<MockInput<string>>().Instance.GetValidationState());
        Assert.Contains("is-validating", input.ClassList);

        model.ContinueValidation.TrySetResult();
        cut.WaitForAssertion(() =>
        {
            Assert.False(cut.FindComponent<MockInput<string>>().Instance.GetValidationState());
            Assert.DoesNotContain("is-validating", input.ClassList);
            Assert.Contains("is-invalid", input.ClassList);
        }, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public async Task AsyncValidationAttribute_CancelBeforeValidation()
    {
        var model = new MockCancelBeforeAsyncValidationAttributeModel();
        MockCancelableAsyncValidationAttribute.Reset();
        var cut = Context.Render<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, model);
            pb.AddChildContent<MockInput<string>>(pb =>
            {
                pb.Add(a => a.Value, model.Name);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(model, nameof(model.Name), typeof(string)));
            });
        });

        try
        {
            await Assert.ThrowsAsync<OperationCanceledException>(async () => await cut.InvokeAsync(() => cut.Instance.ValidateAsync(model.TokenSource.Token)));
            Assert.Equal(0, MockCancelableAsyncValidationAttribute.ValidateCount);
        }
        finally
        {
            model.TokenSource.Dispose();
        }
    }

    [Fact]
    public async Task AsyncValidationAttribute_AllPassed()
    {
        var model = new MockSuccessAsyncValidationAttributeModel();
        MockFirstSuccessAsyncValidationAttribute.Reset();
        MockSecondSuccessAsyncValidationAttribute.Reset();
        var cut = Context.Render<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, model);
            pb.AddChildContent<MockInput<string>>(pb =>
            {
                pb.Add(a => a.Value, model.Name);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(model, nameof(model.Name), typeof(string)));
            });
        });

        var valid = await cut.InvokeAsync(() => cut.Instance.ValidateAsync(CancellationToken.None));

        Assert.True(valid);
        Assert.Equal(1, MockFirstSuccessAsyncValidationAttribute.ValidateCount);
        Assert.Equal(1, MockSecondSuccessAsyncValidationAttribute.ValidateCount);
        Assert.Null(cut.FindComponent<MockInput<string>>().Instance.GetErrorMessage());
    }

    [Fact]
    public async Task IValidateCollection_Ok()
    {
        var model = new MockValidateCollectionModel() { Telephone1 = "123", Telephone2 = "123" };
        var cut = Context.Render<ValidateForm>(pb =>
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
        cut.Render();
        message = input2.Instance.GetErrorMessage();
        Assert.Null(message);

        var allInputs = cut.FindAll("input");
        var inputEl2 = allInputs[all.Count - 1];
        await cut.InvokeAsync(() => inputEl2.Change("1234"));
        message = input2.Instance.GetErrorMessage();
        Assert.Equal("Telephone1 and Telephone2 can not be the same", message);
        cut.Render();
        message = input.Instance.GetErrorMessage();
        Assert.Equal("Telephone1 and Telephone2 can not be the same", message);
    }

    [Fact]
    public void ShowAllInvalidResult_Ok()
    {
        var model = new Foo();
        var cut = Context.Render<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, model);
            pb.AddChildContent<BootstrapInput<string>>(pb =>
            {
                pb.Add(a => a.Value, model.Name);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(model, "Name", typeof(string)));
            });
        });
        cut.DoesNotContain("data-bb-invalid-result");

        cut.Render(pb =>
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

    private sealed class MockModelError : IValidatableObject
    {
        public string? Name { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield return new ValidationResult("Model validation failed");
        }
    }

    private sealed class ThrowingValidator : ValidatorAsyncBase
    {
        public override Task ValidateAsync(
            object? propertyValue,
            ValidationContext context,
            List<ValidationResult> results,
            CancellationToken cancellationToken = default)
            => throw new InvalidOperationException("Validation failed");
    }

    private sealed class CancellableFieldValidator : ValidatorAsyncBase
    {
        public TaskCompletionSource FirstValidationStarted { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);

        public TaskCompletionSource FirstValidationCancelled { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);

        public TaskCompletionSource SecondValidationCompleted { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);

        public override async Task ValidateAsync(
            object? propertyValue,
            ValidationContext context,
            List<ValidationResult> results,
            CancellationToken cancellationToken = default)
        {
            if (propertyValue?.ToString() == "First")
            {
                FirstValidationStarted.TrySetResult();
                try
                {
                    await Task.Delay(Timeout.InfiniteTimeSpan, cancellationToken);
                }
                catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                {
                    FirstValidationCancelled.TrySetResult();
                    throw;
                }
            }
            else if (propertyValue?.ToString() == "Second")
            {
                SecondValidationCompleted.TrySetResult();
            }
        }
    }

    private sealed class ValidateFormTestLogger : ILogger<BootstrapBlazorDataAnnotationsValidator>
    {
        public Exception? Exception { get; private set; }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            Exception = exception;
        }
    }

    private sealed class MockAsyncValidatableModel : IAsyncValidatableObject
    {
        public string? Name { get; set; }

        public bool AsyncValidated { get; private set; }

        public bool SyncValidated { get; private set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            SyncValidated = true;
            yield break;
        }

        public async IAsyncEnumerable<ValidationResult> ValidateAsync(
            ValidationContext validationContext,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            await Task.Yield();
            cancellationToken.ThrowIfCancellationRequested();
            AsyncValidated = true;
            yield return new ValidationResult("Async validation failed", [nameof(Name)]);
        }
    }

    private sealed class MockAsyncModelError : IAsyncValidatableObject
    {
        public string? Name { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => [];

        public async IAsyncEnumerable<ValidationResult> ValidateAsync(
            ValidationContext validationContext,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            await Task.Yield();
            cancellationToken.ThrowIfCancellationRequested();
            yield return new ValidationResult("Model validation failed");
        }
    }

    private sealed class MockAsyncValidationAttributeModel
    {
        [MockAsyncValidation]
        public string? Name { get; set; }
    }

    private sealed class MockAsyncValidationAttribute : AsyncValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
            => throw new InvalidOperationException("The synchronous validation path should not be used.");

        protected override async Task<ValidationResult?> IsValidAsync(
            object? value,
            ValidationContext validationContext,
            CancellationToken cancellationToken)
        {
            await Task.Yield();
            cancellationToken.ThrowIfCancellationRequested();
            return new ValidationResult("Async attribute validation failed");
        }
    }

    private sealed class MockPendingAsyncValidationModel
    {
        [MockPendingAsyncValidation]
        public string? Name { get; set; }

        public TaskCompletionSource ValidationStarted { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);

        public TaskCompletionSource ContinueValidation { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);
    }

    private sealed class MockCancelBeforeAsyncValidationAttributeModel
    {
        [MockCancelBeforeAsyncValidation]
        [MockCancelableAsyncValidation]
        public string? Name { get; set; }

        public CancellationTokenSource TokenSource { get; } = new();
    }

    private sealed class MockSuccessAsyncValidationAttributeModel
    {
        [MockFirstSuccessAsyncValidation]
        [MockSecondSuccessAsyncValidation]
        public string? Name { get; set; } = "Test";
    }

    private sealed class MockPendingAsyncValidationAttribute : AsyncValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
            => throw new InvalidOperationException("The synchronous validation path should not be used.");

        protected override async Task<ValidationResult?> IsValidAsync(
            object? value,
            ValidationContext validationContext,
            CancellationToken cancellationToken)
        {
            var model = (MockPendingAsyncValidationModel)validationContext.ObjectInstance;
            model.ValidationStarted.TrySetResult();
            await model.ContinueValidation.Task.WaitAsync(cancellationToken);
            return new ValidationResult("Async attribute validation failed");
        }
    }

    private sealed class MockCancelBeforeAsyncValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var model = (MockCancelBeforeAsyncValidationAttributeModel)validationContext.ObjectInstance;
            model.TokenSource.Cancel();
            return ValidationResult.Success;
        }
    }

    private sealed class MockCancelableAsyncValidationAttribute : AsyncValidationAttribute
    {
        public static int ValidateCount { get; private set; }

        public static void Reset() => ValidateCount = 0;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
            => throw new InvalidOperationException("The synchronous validation path should not be used.");

        protected override Task<ValidationResult?> IsValidAsync(
            object? value,
            ValidationContext validationContext,
            CancellationToken cancellationToken)
        {
            ValidateCount++;
            return Task.FromResult<ValidationResult?>(ValidationResult.Success);
        }
    }

    private sealed class MockFirstSuccessAsyncValidationAttribute : AsyncValidationAttribute
    {
        public static int ValidateCount { get; private set; }

        public static void Reset() => ValidateCount = 0;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
            => throw new InvalidOperationException("The synchronous validation path should not be used.");

        protected override Task<ValidationResult?> IsValidAsync(
            object? value,
            ValidationContext validationContext,
            CancellationToken cancellationToken)
        {
            ValidateCount++;
            return Task.FromResult<ValidationResult?>(ValidationResult.Success);
        }
    }

    private sealed class MockSecondSuccessAsyncValidationAttribute : AsyncValidationAttribute
    {
        public static int ValidateCount { get; private set; }

        public static void Reset() => ValidateCount = 0;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
            => throw new InvalidOperationException("The synchronous validation path should not be used.");

        protected override Task<ValidationResult?> IsValidAsync(
            object? value,
            ValidationContext validationContext,
            CancellationToken cancellationToken)
        {
            ValidateCount++;
            return Task.FromResult<ValidationResult?>(ValidationResult.Success);
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

        public bool? GetValidationState() => base.IsValid;
    }
}
