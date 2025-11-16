// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
    public void EnumerableValue_Ok()
    {
        int[] value = [1, 2];
        var foo = new Foo();
        var validator = new RequiredValidator()
        {
            AllowEmptyString = false,
            ErrorMessage = "Test-ErrorMessage"
        };
        var context = new ValidationContext(foo);
        var results = new List<ValidationResult>();
        validator.Validate(value, context, results);
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

        validator.Validate("", context, results);
        Assert.Single(results);

        results.Clear();
        var provider = Context.Services.GetRequiredService<IServiceProvider>();
        validator = new RequiredValidator();
        context = new ValidationContext(foo, provider, null);
        validator.Validate(null, context, results);
        Assert.Single(results);
    }
}
