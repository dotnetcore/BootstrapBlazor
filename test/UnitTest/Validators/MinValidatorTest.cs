// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel.DataAnnotations;

namespace UnitTest.Validators;

public class MinValidatorTest
{
    [Fact]
    public void Validate_Ok()
    {
        var foo = new Foo();
        var validator = new MinValidator()
        {
            Value = 2
        };
        var context = new ValidationContext(foo);
        var results = new List<ValidationResult>();
        validator.Validate("v1", context, results);
        Assert.Equal($"Select at least {validator.Value} items", results[0].ErrorMessage);
    }
}
