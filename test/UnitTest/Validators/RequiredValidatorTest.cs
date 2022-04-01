// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Localization.Json;
using BootstrapBlazor.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace UnitTest.Validators;

public class RequiredValidatorTest : BootstrapBlazorTestBase
{
    [Fact]
    public void AllowEmptyString_Ok()
    {
        var foo = new Foo();
        var validator = new RequiredValidator()
        {
            AllowEmptyString = false,
            ErrorMessage = "Test-ErrorMessage"
        };
        var context = new ValidationContext(foo);
        var results = new List<ValidationResult>();
        validator.Validate(null, context, results);
        Assert.Equal(validator.ErrorMessage, results[0].ErrorMessage);

        validator.Validate("", context, results);
        Assert.Equal(validator.ErrorMessage, results[0].ErrorMessage);

        validator.AllowEmptyString = true;
        results.Clear();
        validator.Validate("", context, results);
        Assert.Empty(results);
    }

    [Fact]
    public void EnnumerableValue_Ok()
    {
        var foo = new Foo();
        var validator = new RequiredValidator()
        {
            AllowEmptyString = false,
            ErrorMessage = "Test-ErrorMessage"
        };
        var context = new ValidationContext(foo);
        var results = new List<ValidationResult>();
        validator.Validate(new int[] { 1, 2 }, context, results);
        Assert.Empty(results);

        validator.Validate(Array.Empty<int>(), context, results);
        Assert.Single(results);
    }

    [Fact]
    public void Localizer_Ok()
    {
        var foo = new Foo();
        var validator = new RequiredValidator()
        {
            AllowEmptyString = false,
            ErrorMessage = "Name",
            LocalizerFactory = Context.Services.GetRequiredService<IStringLocalizerFactory>(),
        };
        var context = new ValidationContext(foo);
        var results = new List<ValidationResult>();
        validator.Validate("v1", context, results);
        Assert.Empty(results);

        context.MemberName = "Name";
        validator.Validate("v1", context, results);
        Assert.Empty(results);

        validator.Options = Context.Services.GetRequiredService<IOptions<JsonLocalizationOptions>>().Value;
        validator.Options.ResourceManagerStringLocalizerType = typeof(Foo);
        validator.Validate("v1", context, results);
        Assert.Empty(results);
    }
}
