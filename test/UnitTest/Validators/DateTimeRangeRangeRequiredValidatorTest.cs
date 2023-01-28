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
        Assert.Equal(validator.ErrorMessage, results.First().ErrorMessage);
        Assert.Equal(context.MemberName, results.First().MemberNames.First());

        results.Clear();
        context.MemberName = null;
        validator.Validate(null, context, results);
        Assert.Equal(validator.ErrorMessage, results.First().ErrorMessage);
        Assert.Empty(results.First().MemberNames);

        results.Clear();
        validator.Validate(new DateTimeRangeValue(), context, results);
        Assert.Equal(validator.ErrorMessage, results.First().ErrorMessage);
        Assert.Empty(results.First().MemberNames);

        results.Clear();
        validator.Validate(new DateTimeRangeValue() { Start = DateTime.Today, End = DateTime.Today }, context, results);
        Assert.Empty(results);
    }
}
