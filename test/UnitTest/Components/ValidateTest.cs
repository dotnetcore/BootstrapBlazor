﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace UnitTest.Components;

public class ValidateTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task CascadedEditContext_Ok()
    {
        var model = new Foo() { Name = "Name-Test" };
        Context.RenderTree.Add<CascadingValue<EditContext>>(builder =>
        {
            builder.Add(a => a.Value, new EditContext(model));
        });
        var cut = Context.RenderComponent<BootstrapInput<string>>(builder =>
        {
            builder.Add(a => a.Value, model.Name);
            builder.Add(a => a.ValueChanged, v => model.Name = v);
            builder.Add(a => a.ValueExpression, model.GenerateValueExpression());
        });
        Assert.Equal(model.Name, cut.Instance.Value);
        await cut.InvokeAsync(() => cut.Find("input").Change("Test"));
        Assert.Equal("Test", model.Name);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(true)]
    [InlineData(false)]
    public void EditorForm_Ok(bool? showLabel)
    {
        var model = new Foo() { Name = "Name-Test" };
        var cut = Context.RenderComponent<EditorForm<Foo>>(builder =>
        {
            builder.Add(a => a.Model, model);
            builder.Add(a => a.ShowLabel, showLabel);
            builder.Add(a => a.AutoGenerateAllItem, false);
            builder.Add<EditorItem<Foo, string>, Foo>(a => a.FieldItems, f => p =>
            {
                p.Add(p => p.Field, f.Name);
                p.Add(p => p.FieldExpression, f.GenerateValueExpression());
                p.Add<BootstrapInput<string>, Foo>(e => e.EditTemplate, f => p =>
                {
                    p.Add(a => a.ShowLabel, null);
                    p.Add(a => a.Value, model.Name);
                    p.Add(a => a.ValueExpression, model.GenerateValueExpression());
                });
            });
        });

        // 内置 EditorForm 时 ShowLabel 为 null 或者 true 时显示标签
        // 内置 EditorForm 时 ShowLabel 为 false 时不显示标签
        if (showLabel == null || showLabel.Value)
        {
            Assert.Contains("label", cut.Markup);
        }
        else
        {
            Assert.DoesNotContain("label", cut.Markup);
        }
    }

    [Fact]
    public void ValidateForm_Ok()
    {
        var model = new Foo() { Name = "Name-Test" };
        var cut = Context.RenderComponent<ValidateForm>(builder =>
        {
            builder.Add(a => a.Model, model);
            builder.AddChildContent<BootstrapInput<string>>(p =>
            {
                p.Add(a => a.Value, model.Name);
                p.Add(a => a.ValueExpression, model.GenerateValueExpression());
            });
        });

        // 内置 ValidateForm 验证表单中 ShowLabel 默认 true 显示标签
        var label = cut.Find("label");
        Assert.Equal("姓名", label.InnerHtml);

        cut.SetParametersAndRender(builder =>
        {
            builder.Add(a => a.ShowLabel, false);
        });
        cut.WaitForAssertion(() => cut.DoesNotContain("label"));
    }

    [Fact]
    public void ValidateForm_Group_Ok()
    {
        var model = new Foo() { Name = "Name-Test" };
        var cut = Context.RenderComponent<ValidateForm>(builder =>
        {
            builder.Add(a => a.Model, model);
            builder.AddChildContent<BootstrapInputGroup>(pb =>
            {
                pb.AddChildContent<BootstrapInputGroupLabel>(p =>
                {
                    p.Add(a => a.DisplayText, "Name-Test");
                });
                pb.AddChildContent<BootstrapInput<string>>(p =>
                {
                    p.Add(a => a.Value, model.Name);
                    p.Add(a => a.ValueExpression, model.GenerateValueExpression());
                });
            });
        });

        // ValidateForm 验证表单中 使用 InputGroup 组件
        cut.Contains("Name-Test");

        var input = cut.FindComponent<BootstrapInput<string>>();
        input.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowLabel, true);
        });
        cut.Contains("姓名");
    }

    [Fact]
    public void ShowLabel_Ok()
    {
        var model = new Foo() { Name = "Name-Test" };

        // 显示设置 IsShowLabel=true 时显示标签
        var cut = Context.RenderComponent<BootstrapInput<string>>(builder =>
        {
            builder.Add(a => a.Value, model.Name);
            builder.Add(a => a.ShowLabel, true);
            builder.Add(a => a.DisplayText, model.Name);
        });
        var label = cut.Find("label");
        Assert.Equal(model.Name, label.InnerHtml);

        // 显示设置 IsShowLabel=false 时不显示标签
        cut.SetParametersAndRender(builder =>
        {
            builder.Add(a => a.ShowLabel, false);
        });
        cut.WaitForAssertion(() => cut.DoesNotContain("label"));

        // IsShowLabel 为空时 不显示标签
        cut.SetParametersAndRender(builder =>
        {
            builder.Add(a => a.ShowLabel, null);
        });
        cut.WaitForAssertion(() => cut.DoesNotContain("label"));

        // 开启双向绑定时 IsShowLabel 为空时 不显示标签
        cut.SetParametersAndRender(builder =>
        {
            builder.Add(a => a.ShowLabel, null);
            builder.Add(a => a.Value, model.Name);
            builder.Add(a => a.ValueExpression, model.GenerateValueExpression());
        });
        cut.WaitForAssertion(() => cut.DoesNotContain("label"));

        // 开启双向绑定时 IsShowLabel=false 时 不显示标签
        cut.SetParametersAndRender(builder =>
        {
            builder.Add(a => a.ShowLabel, null);
            builder.Add(a => a.Value, model.Name);
            builder.Add(a => a.ValueExpression, model.GenerateValueExpression());
        });
        cut.WaitForAssertion(() => cut.DoesNotContain("label"));

        // 开启双向绑定时 IsShowLabel=true 时 显示标签
        cut.SetParametersAndRender(builder =>
        {
            builder.Add(a => a.ShowLabel, true);
            builder.Add(a => a.Value, model.Name);
            builder.Add(a => a.ValueExpression, model.GenerateValueExpression());
        });
        cut.WaitForAssertion(() => cut.Contains("label"));
    }

    [Fact]
    public async Task SkipValidate_Ok()
    {
        var model = new Foo() { Name = "Name-Test" };
        var valid = false;
        var cut = Context.RenderComponent<ValidateForm>(builder =>
        {
            builder.Add(v => v.Model, model);
            builder.Add(v => v.OnValidSubmit, context =>
            {
                valid = true;
                return Task.CompletedTask;
            });
            builder.AddChildContent<BootstrapInput<string>>(pb =>
            {
                pb.Add(v => v.Value, model.Name);
                pb.Add(v => v.SkipValidate, true);
                pb.Add(v => v.ValueExpression, model.GenerateValueExpression());
            });
            builder.AddChildContent<Button>(pb =>
            {
                pb.Add(b => b.ButtonType, ButtonType.Submit);
            });
        });

        // 提交表单
        var form = cut.Find("form");
        await cut.InvokeAsync(() => form.Submit());

        // 内置 ValidateForm 验证表单中 设置 SkipValidate=true 提交表单时不进行验证
        Assert.True(valid);
    }

    [Required]
    private string? Test { get; set; }

    [Fact]
    public async Task IsRequired_Ok()
    {
        // 组件绑定非公开模型属性
        Test = "test";
        var model = new Foo() { Name = "Name-Test" };
        var cut = Context.RenderComponent<ValidateForm>(builder =>
        {
            builder.Add(a => a.Model, model);
            builder.AddChildContent<BootstrapInput<string>>(p =>
            {
                p.Add(a => a.Value, Test);
                p.Add(a => a.ValueChanged, v => Test = v);
                p.Add(a => a.ValueExpression, Utility.GenerateValueExpression(this, nameof(Test), typeof(string)));
            });
            builder.AddChildContent<BootstrapInput<int>>(p =>
            {
                p.Add(a => a.Value, model.Count);
                p.Add(a => a.ValueExpression, model.GenerateValueExpression(nameof(Foo.Count), typeof(int)));
            });
        });
        var input = cut.FindComponent<BootstrapInput<string>>();
        Assert.Contains("required", input.Markup);

        // 更改值测试
        await cut.InvokeAsync(() => input.Find("input").Change("test1"));
        Assert.Equal(Test, input.Instance.Value);
        Assert.Equal("test1", input.Instance.Value);

        var number = cut.FindComponent<BootstrapInput<int>>();
        Assert.Contains("required=\"true\"", number.Markup);
    }

    [Fact]
    public async Task SetDisable_Ok()
    {
        var cut = Context.RenderComponent<BootstrapInput<string>>(builder =>
        {
            builder.Add(a => a.IsDisabled, false);
        });
        Assert.False(cut.Instance.IsDisabled);
        await cut.InvokeAsync(() => cut.Instance.SetDisable(true));
        Assert.True(cut.Instance.IsDisabled);
    }

    [Fact]
    public async Task SetValue_Ok()
    {
        var cut = Context.RenderComponent<BootstrapInput<string>>(builder =>
        {
            builder.Add(a => a.Value, "test");
        });
        Assert.Equal("test", cut.Instance.Value);
        await cut.InvokeAsync(() => cut.Instance.SetValue("test2"));
        Assert.Equal("test2", cut.Instance.Value);
    }

    [Fact]
    public async Task SetLabel_Ok()
    {
        var cut = Context.RenderComponent<BootstrapInput<string>>(builder =>
        {
            builder.Add(a => a.DisplayText, "test");
            builder.Add(a => a.ShowLabel, true);
        });
        Assert.Equal("test", cut.Instance.DisplayText);
        await cut.InvokeAsync(() => cut.Instance.SetLabel("test1"));
        Assert.Equal("test1", cut.Instance.DisplayText);
    }

    [Fact]
    public void ParsingErrorMessage_Ok()
    {
        var cut = Context.RenderComponent<BootstrapInput<string>>(builder =>
        {
            builder.Add(a => a.ParsingErrorMessage, "test");
        });
        Assert.Equal("test", cut.Instance.ParsingErrorMessage);
    }

    [Fact]
    public async Task ValidateRules_Ok()
    {
        var model = new Foo() { Name = "test" };
        var invalid = false;
        var cut = Context.RenderComponent<ValidateForm>(builder =>
        {
            builder.Add(v => v.Model, model);
            builder.Add(v => v.OnInvalidSubmit, context =>
            {
                invalid = true;
                return Task.CompletedTask;
            });
            builder.AddChildContent<BootstrapInput<string>>(pb =>
            {
                pb.Add(v => v.Value, model.Name);
                pb.Add(v => v.ValueChanged, v => model.Name = v);
                pb.Add(v => v.ValueExpression, model.GenerateValueExpression());
            });
            builder.AddChildContent<Button>(pb =>
            {
                pb.Add(b => b.ButtonType, ButtonType.Submit);
            });
        });

        var form = cut.Find("form");
        await cut.InvokeAsync(() => form.Submit());
        // 提交表单验证通过
        Assert.False(invalid);

        // 设置 Name="" 验证不通过
        var input = cut.FindComponent<BootstrapInput<string>>();
        var c = input.Find("input");
        await cut.InvokeAsync(() =>
        {
            c.Change("");
            form.Submit();
        });
        Assert.True(invalid);

        // 增加邮箱验证规则
        var rules = new List<IValidator>
        {
            new FormItemValidator(new EmailAddressAttribute())
        };
        input.SetParametersAndRender(pb =>
        {
            pb.Add(v => v.ValidateRules, rules);
        });
        invalid = false;
        await cut.InvokeAsync(() =>
        {
            c.Change("argo@163.com");
            form.Submit();
        });
        Assert.False(invalid);

        // 更改值不符合邮箱规则验证不通过
        await cut.InvokeAsync(() =>
        {
            c.Change("argo");
            form.Submit();
        });
        Assert.True(invalid);

        // 自定义验证规则未设置 member name
        rules =
        [
            new FormItemValidator(new MockValidationAttribute())
        ];
        input.SetParametersAndRender(pb =>
        {
            pb.Add(v => v.ValidateRules, rules);
        });
        invalid = false;
        await cut.InvokeAsync(() =>
        {
            c.Change("argo@163.com");
            form.Submit();
        });
        Assert.True(invalid);
    }

    [Fact]
    public async Task AsyncRules_Ok()
    {
        var model = new Foo() { Name = "test" };
        var invalid = false;
        var cut = Context.RenderComponent<ValidateForm>(builder =>
        {
            builder.Add(v => v.Model, model);
            builder.Add(v => v.OnInvalidSubmit, context =>
            {
                invalid = true;
                return Task.CompletedTask;
            });
            builder.AddChildContent<FooAsync>(pb =>
            {
                pb.Add(v => v.Value, model.Name);
                pb.Add(v => v.ValueChanged, v => model.Name = v);
                pb.Add(v => v.ValueExpression, model.GenerateValueExpression());
            });
            builder.AddChildContent<Button>(pb =>
            {
                pb.Add(b => b.ButtonType, ButtonType.Submit);
            });
        });
        var form = cut.Find("form");
        var c = cut.Find("input");
        await cut.InvokeAsync(() => c.Change("Test"));
        await Task.Delay(300);
        await cut.InvokeAsync(() => form.Submit());
        Assert.True(invalid);
    }

    class FooAsync : BootstrapInput<string>
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Rules.Add(new MockAsyncValidator());
        }
    }

    class MockAsyncValidator : ValidatorAsyncBase
    {
        public override async Task ValidateAsync(object? propertyValue, ValidationContext context, List<ValidationResult> results)
        {
            await Task.Delay(100);
            results.Add(new ValidationResult("Invalid", new string[] { context.DisplayName }));
        }
    }

    [Fact]
    public async Task ValidateProperty_Ok()
    {
        var model = new Foo() { Hobby = Array.Empty<string>() };
        var invalid = false;
        var cut = Context.RenderComponent<ValidateForm>(builder =>
        {
            builder.Add(v => v.Model, model);
            builder.Add(v => v.OnInvalidSubmit, context =>
            {
                invalid = true;
                return Task.CompletedTask;
            });
            builder.AddChildContent<CheckboxList<IEnumerable<string>>>(pb =>
            {
                pb.Add(v => v.Value, model.Hobby);
                pb.Add(v => v.ValueExpression, model.GenerateValueExpression(nameof(Foo.Hobby), typeof(IEnumerable<string>)));
                pb.Add(v => v.Items, new List<SelectedItem>()
                {
                    new("1", "test1"),
                    new("2", "test2")
                });
            });
            builder.AddChildContent<Button>(pb =>
            {
                pb.Add(b => b.ButtonType, ButtonType.Submit);
            });
        });

        // 提交表单验证不通过
        var form = cut.Find("form");
        await cut.InvokeAsync(() => form.Submit());
        Assert.True(invalid);

        // 更新选中值
        // 提交表单验证通过
        model.Hobby = new string[] { "1" };
        invalid = false;
        cut.FindComponent<CheckboxList<IEnumerable<string>>>().SetParametersAndRender(pb =>
        {
            pb.Add(v => v.Value, model.Hobby);
        });
        await cut.InvokeAsync(() => form.Submit());
        Assert.False(invalid);
    }

    [Fact]
    public void CurrentValue_Ok()
    {
        var model = new Foo() { Count = 0 };
        var cut = Context.RenderComponent<RenderTemplate>(builder =>
        {
            builder.AddChildContent<MockValidate<int>>(pb =>
            {
                pb.Add(v => v.Value, model.Count);
                pb.Add(v => v.ValueExpression, model.GenerateValueExpression(nameof(Foo.Count), typeof(int)));
            });
            builder.AddChildContent<MockValidate<int?>>();
            builder.AddChildContent<MockValidate<object>>();
        });

        var intNullValidate = cut.FindComponent<MockValidate<int?>>();
        intNullValidate.Instance.Test();
        var intValidate = cut.FindComponent<MockValidate<int>>();
        intValidate.Instance.Test();
        var objValidate = cut.FindComponent<MockValidate<object>>();
        objValidate.Instance.Test();
    }

    [Fact]
    public void CurrentValue_Validate_Ok()
    {
        var model = new Foo();
        var cut = Context.RenderComponent<ValidateForm>(builder =>
        {
            builder.Add(v => v.Model, model);
            builder.AddChildContent<MockValidate<int>>(pb =>
            {
                pb.Add(v => v.IsDisabled, true);
                pb.Add(v => v.Value, model.Count);
                pb.Add(v => v.ValueExpression, model.GenerateValueExpression(nameof(Foo.Count), typeof(int)));
            });
            builder.AddChildContent<MockValidate<int?>>();
            builder.AddChildContent<MockValidate<object>>();
        });

        var intNullValidate = cut.FindComponent<MockValidate<int?>>();
        intNullValidate.Instance.Test();
        var intValidate = cut.FindComponent<MockValidate<int>>();
        intValidate.Instance.Test();
        var objValidate = cut.FindComponent<MockValidate<object>>();
        objValidate.Instance.Test();
    }

    [Fact]
    public void ValidateType_Ok()
    {
        var model = new Foo() { Count = 0 };
        var dog = new Dog() { Count = 10 };

        var cut = Context.RenderComponent<RenderTemplate>(builder =>
        {
            builder.AddChildContent<MockValidate<int>>(pb =>
            {
                pb.Add(v => v.Value, model.Count);
                pb.Add(v => v.ValueExpression, model.GenerateValueExpression(nameof(Foo.Count), typeof(int)));
            });
            builder.AddChildContent<MockValidate<int?>>(pb =>
            {
                pb.Add(v => v.Value, dog.Count);
            });
        });
        var intValidate = cut.FindComponent<MockValidate<int>>();
        cut.InvokeAsync(() => intValidate.Instance.ValidateTypeTest(model));

        var nullableIntValidate = cut.FindComponent<MockValidate<int?>>();
        cut.InvokeAsync(() => nullableIntValidate.Instance.ValidateTypeTest(dog));
    }

    [Fact]
    public async Task NoIdCheck_Ok()
    {
        var model = new Foo() { Count = 0 };
        var cut = Context.RenderComponent<RenderTemplate>(builder =>
        {
            builder.AddChildContent<NoIdValidate<int>>(pb =>
            {
                pb.Add(v => v.Value, model.Count);
                pb.Add(v => v.ValueExpression, model.GenerateValueExpression(nameof(Foo.Count), typeof(int)));
            });
        });
        var intValidate = cut.FindComponent<NoIdValidate<int>>();
        await intValidate.Instance.ShowValidResultTest();
    }

    [Fact]
    public void OnValidate_Ok()
    {
        var model = new Foo() { Count = 0 };
        var cut = Context.RenderComponent<RenderTemplate>(builder =>
        {
            builder.AddChildContent<MockValidate<int>>(pb =>
            {
                pb.Add(v => v.Value, model.Count);
                pb.Add(v => v.ValueExpression, model.GenerateValueExpression(nameof(Foo.Count), typeof(int)));
            });
        });
        var intValidate = cut.FindComponent<MockValidate<int>>();
        intValidate.Instance.OnValidateTest();
    }

    [Fact]
    public void Required_Ok()
    {
        var model = new Foo();
        var rules = new List<IValidator>
            {
                new FormItemValidator(new RequiredAttribute())
            };
        var cut = Context.RenderComponent<ValidateForm>(builder =>
        {
            builder.Add(v => v.Model, model);
            builder.AddChildContent<MockValidate<bool>>(pb =>
            {
                pb.Add(v => v.Value, model.Complete);
                pb.Add(v => v.ValueExpression, model.GenerateValueExpression(nameof(Foo.Complete), typeof(bool)));
                pb.Add(v => v.ValidateRules, rules);
            });
        });

        var boolValidate = cut.FindComponent<MockValidate<bool>>();
        boolValidate.SetParametersAndRender(pb =>
        {
            pb.Add(v => v.ValidateRules, null);
        });
    }

    [Fact]
    public void Required_AmbiguousMatch()
    {
        var model = new Cat();
        var rules = new List<IValidator>
            {
                new FormItemValidator(new RequiredAttribute())
            };
        var cut = Context.RenderComponent<ValidateForm>(builder =>
        {
            builder.Add(v => v.Model, model);
            builder.AddChildContent<MockValidate<int>>(pb =>
            {
                pb.Add(v => v.Value, model.Foo);
                pb.Add(v => v.ValueExpression, Utility.GenerateValueExpression(model, nameof(Cat.Foo), typeof(int)));
                pb.Add(v => v.ValidateRules, rules);
            });
        });

        // 不会报错 AmbiguousMatchException
    }

    [Fact]
    public void TooltipHost_Ok()
    {
        var cut = Context.RenderComponent<MockValidate<string>>(builder =>
        {
        });
    }

    private class MockValidate<TValue> : ValidateBase<TValue>
    {
        public void Test()
        {
            CurrentValueAsString = "";
            CurrentValueAsString = "test";
            CurrentValueAsString = "1";
        }

        public async Task ValidateTypeTest(object model)
        {
            CurrentValueAsString = "test";

            var results = new List<ValidationResult>();
            var context = new ValidationContext(model);
            await ValidatePropertyAsync(1, context, results);
        }

        public void OnValidateTest()
        {
            OnValidate(null);
            OnValidate(false);
            OnValidate(true);
        }
    }

    private class NoIdValidate<TValue> : ValidateBase<TValue>
    {
        public async ValueTask ShowValidResultTest()
        {
            Id = "";
            await base.ShowValidResult();
        }
    }

    class Dummy
    {
        public virtual string? Foo { get; set; }
    }

    class Cat : Dummy
    {
        [Required]
        public new int Foo { get; set; }
    }

    class Dog
    {
        public int? Count { get; set; }
    }

    class MockValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            return new ValidationResult($"The {validationContext.DisplayName} field must be a future date.");
        }
    }
}
