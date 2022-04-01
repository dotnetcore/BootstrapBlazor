// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using System.ComponentModel.DataAnnotations;

namespace UnitTest.Validators;

public class MaxValidatorTest : BootstrapBlazorTestBase
{
    [Fact]
    public void ErrorMessage_Ok()
    {
        var foo = new Foo();
        var validator = new MaxValidator()
        {
            Value = 1
        };
        var context = new ValidationContext(foo);
        var results = new List<ValidationResult>();
        validator.Validate("v1, v2", context, results);
        Assert.Equal($"At most {validator.Value} items can be selected", results[0].ErrorMessage);
    }

    [Fact]
    public void SplitCallback_Ok()
    {
        var foo = new Foo();
        var validator = new MaxValidator()
        {
            Value = 1,
            ErrorMessage = "最多可以选择 {0} 项"
        };
        var context = new ValidationContext(foo)
        {
            MemberName = "Name"
        };
        var results = new List<ValidationResult>();
        validator.Validate("v1, v2", context, results);
        Assert.Equal($"最多可以选择 {validator.Value} 项", results[0].ErrorMessage);

        validator.SplitCallback = value => value.Split(',', StringSplitOptions.RemoveEmptyEntries).Length;
        validator.Validate("v1, v2", context, results);
        Assert.Equal($"最多可以选择 {validator.Value} 项", results[0].ErrorMessage);
    }

    [Fact]
    public void GenericType_Ok()
    {
        var foo = new Foo();
        var validator = new MaxValidator()
        {
            Value = 1,
            ErrorMessage = "最多可以选择 {0} 项"
        };
        var context = new ValidationContext(foo)
        {
            MemberName = "Name"
        };
        var results = new List<ValidationResult>();
        validator.Validate(new List<string> { "v1", "v2" }, context, results);
        Assert.Equal($"最多可以选择 {validator.Value} 项", results[0].ErrorMessage);

        results.Clear();
        validator.Validate("v1", context, results);
        Assert.Empty(results);
    }

    [Fact]
    public void Array_Ok()
    {
        var foo = new Foo();
        var validator = new MaxValidator()
        {
            Value = 1,
            ErrorMessage = "最多可以选择 {0} 项"
        };
        var context = new ValidationContext(foo)
        {
            MemberName = "Name"
        };
        var results = new List<ValidationResult>();
        validator.Validate(new string[] { "v1", "v2" }, context, results);
        Assert.Equal($"最多可以选择 {validator.Value} 项", results[0].ErrorMessage);
    }

    [Fact]
    public void Null_Ok()
    {
        var foo = new Foo();
        var validator = new MaxValidator()
        {
            Value = 1,
            ErrorMessage = "最多可以选择 {0} 项"
        };
        var context = new ValidationContext(foo)
        {
            MemberName = "Name"
        };
        var results = new List<ValidationResult>();
        validator.Validate(null, context, results);
        Assert.Equal($"最多可以选择 {validator.Value} 项", results[0].ErrorMessage);
    }
}
