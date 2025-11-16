// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel.DataAnnotations;

namespace UnitTest.Validators;

public class ValidatorTest
{
    [Fact]
    public void Validate_Ok()
    {
        var validator = new MockValidator();
        var results = new List<ValidationResult>();
        validator.ValidateEmptyTest(results);
        Assert.Empty(results);
    }

    class MockValidator : ValidatorBase
    {
        private Foo foo { get; }

        public MockValidator()
        {
            foo = new Foo();
        }

        public override void Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results)
        {

        }

        public void ValidateEmptyTest(List<ValidationResult> results)
        {
            var context = new ValidationContext(foo);
            Validate(null, context, results);
        }
    }
}
