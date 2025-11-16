// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
