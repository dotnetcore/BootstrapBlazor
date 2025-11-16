// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel.DataAnnotations;

namespace UnitTest.Validators;

public class ValidatorAsyncTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task Validate_Ok()
    {
        var invalid = false;
        var foo = new Foo() { Name = "Test" };
        var cut = Context.Render<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.Add(a => a.OnInvalidSubmit, context =>
            {
                invalid = true;
                return Task.CompletedTask;
            });
            pb.AddChildContent<BootstrapInput<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Name);
                pb.Add(a => a.ValueExpression, foo.GenerateValueExpression());
                pb.Add(a => a.ValidateRules, [new MockValidator()]);
            });
            pb.AddChildContent<Button>(pb =>
            {
                pb.Add(a => a.ButtonType, ButtonType.Submit);
            });
        });
        var input = cut.Find("input");
        await cut.InvokeAsync(() => input.Change("1234"));

        var button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());
        cut.WaitForState(() => invalid);
    }

    class MockValidator : ValidatorAsyncBase
    {
        public override async Task ValidateAsync(object? propertyValue, ValidationContext context, List<ValidationResult> results)
        {
            await Task.Delay(100);
            results.Add(new ValidationResult("InValid", new string[] { context.DisplayName }));
        }
    }
}
