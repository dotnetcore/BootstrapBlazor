// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using System.ComponentModel.DataAnnotations;

namespace UnitTest.Validators;

public class ValidatorTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task Validate_Ok()
    {
        var validator = new MockValidator();
        var results = new List<ValidationResult>();
        await validator.ValidateEmptyTest(results);
        Assert.Empty(results);
    }

    class MockValidator : ValidatorBase
    {
        private Foo foo { get; }

        public MockValidator()
        {
            foo = new Foo();
        }

        public async Task ValidateEmptyTest(List<ValidationResult> results)
        {
            var context = new ValidationContext(foo);
            await ValidateAsync(null, context, results);
        }
    }
}
