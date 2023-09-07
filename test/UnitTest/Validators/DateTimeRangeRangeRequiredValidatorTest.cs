// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using System.ComponentModel.DataAnnotations;

namespace UnitTest.Validators;

public class DateTimeRangeRequiredValidatorTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Validate_Ok()
    {
        var validator = new DateTimeRangeRequiredValidator()
        {
            AllowEmptyString = false,
            ErrorMessage = "Test-ErrorMessage"
        };
        var foo = new Foo() { Name = "Test-Name" };
        var context = new ValidationContext(foo) { MemberName = "Name" };
        var results = new List<ValidationResult>();
        validator.Validate(null, context, results);
        Assert.Equal(validator.ErrorMessage, results[0].ErrorMessage);
        Assert.Equal(context.MemberName, results[0].MemberNames.First());

        results.Clear();
        context.MemberName = null;
        validator.Validate(null, context, results);
        Assert.Equal(validator.ErrorMessage, results[0].ErrorMessage);
        Assert.Empty(results[0].MemberNames);

        results.Clear();
        validator.Validate(new DateTimeRangeValue(), context, results);
        Assert.Equal(validator.ErrorMessage, results[0].ErrorMessage);
        Assert.Empty(results[0].MemberNames);

        results.Clear();
        validator.Validate(new DateTimeRangeValue() { Start = DateTime.Today, End = DateTime.Today }, context, results);
        Assert.Empty(results);

        validator.Validate(new DateTimeRangeValue() { Start = DateTime.Today }, context, results);
        Assert.Single(results);

        results.Clear();
        validator.Validate(new DateTimeRangeValue() { Start = DateTime.Today, End = DateTime.MinValue }, context, results);
        Assert.Single(results);
    }
}
