// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using System.ComponentModel.DataAnnotations;

namespace UnitTest.Validators;

public class FormItemValidatorTest
{
    [Fact]
    public void Validate_Ok()
    {
        var foo = new Foo();
        var context = new ValidationContext(foo)
        {
            MemberName = "Name"
        };
        var results = new List<ValidationResult>();
        var validator = new FormItemValidator(new RequiredAttribute());
        validator.Validate(null, context, results);
        Assert.Single(results);
    }
}
