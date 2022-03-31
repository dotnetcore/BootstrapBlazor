// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using System.ComponentModel.DataAnnotations;

namespace UnitTest.Validators;

public class MaxValidatorTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task ErrorMessage_Ok()
    {
        var foo = new Foo();
        var validator = new MaxValidator()
        {
            Value = 1
        };
        var context = new ValidationContext(foo);
        var results = new List<ValidationResult>();
        await validator.ValidateAsync("v1, v2", context, results);
        Assert.Equal($"At most {validator.Value} items can be selected", results[0].ErrorMessage);
    }

    [Fact]
    public async Task SplitCallback_Ok()
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
        await validator.ValidateAsync("v1, v2", context, results);
        Assert.Equal($"最多可以选择 {validator.Value} 项", results[0].ErrorMessage);

        validator.SplitCallback = value => value.Split(',', StringSplitOptions.RemoveEmptyEntries).Length;
        await validator.ValidateAsync("v1, v2", context, results);
        Assert.Equal($"最多可以选择 {validator.Value} 项", results[0].ErrorMessage);
    }

    [Fact]
    public async Task GenericType_Ok()
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
        await validator.ValidateAsync(new List<string> { "v1", "v2" }, context, results);
        Assert.Equal($"最多可以选择 {validator.Value} 项", results[0].ErrorMessage);

        results.Clear();
        await validator.ValidateAsync("v1", context, results);
        Assert.Empty(results);
    }

    [Fact]
    public async Task Array_Ok()
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
        await validator.ValidateAsync(new string[] { "v1", "v2" }, context, results);
        Assert.Equal($"最多可以选择 {validator.Value} 项", results[0].ErrorMessage);
    }

    [Fact]
    public async Task Null_Ok()
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
        await validator.ValidateAsync(null, context, results);
        Assert.Equal($"最多可以选择 {validator.Value} 项", results[0].ErrorMessage);
    }
}
