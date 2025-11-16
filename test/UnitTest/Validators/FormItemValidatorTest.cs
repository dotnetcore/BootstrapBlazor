// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
