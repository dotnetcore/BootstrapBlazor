// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel.DataAnnotations;

namespace UnitTest.Attributes;

public class AsyncValidationAttributeTest
{
    [Fact]
    public async Task GetValidationResultAsync_Ok()
    {
        var attribute = new MockAsyncValidationAttribute()
        {
            ErrorMessage = "{0} is invalid"
        };
        var model = new MockModel();
        var context = new ValidationContext(model)
        {
            MemberName = nameof(MockModel.Name),
            DisplayName = nameof(MockModel.Name)
        };

        var result = await attribute.GetValidationResultAsync(model.Name, context, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal("Name is invalid", result.ErrorMessage);
        Assert.Equal([nameof(MockModel.Name)], result.MemberNames);
    }

    [Fact]
    public async Task GetValidationResultAsync_NullContext()
    {
        var attribute = new MockAsyncValidationAttribute();

        await Assert.ThrowsAsync<ArgumentNullException>(() => attribute.GetValidationResultAsync(null, null!, CancellationToken.None));
    }

    [Fact]
    public async Task GetValidationResultAsync_Success()
    {
        var attribute = new SuccessAsyncValidationAttribute();
        var context = new ValidationContext(new MockModel());

        var result = await attribute.GetValidationResultAsync(null, context, CancellationToken.None);

        Assert.Same(ValidationResult.Success, result);
    }

    [Fact]
    public async Task GetValidationResultAsync_WithErrorMessage()
    {
        var attribute = new MessageAsyncValidationAttribute();
        var context = new ValidationContext(new MockModel());

        var result = await attribute.GetValidationResultAsync(null, context, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal("Invalid value", result.ErrorMessage);
    }

    [Fact]
    public async Task StringConstructor_Ok()
    {
        var attribute = new StringConstructorAsyncValidationAttribute();
        var context = new ValidationContext(new MockModel())
        {
            DisplayName = "Name"
        };

        var result = await attribute.GetValidationResultAsync(null, context, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal("Name is invalid", result.ErrorMessage);
    }

    [Fact]
    public async Task ErrorMessageAccessorConstructor_Ok()
    {
        var attribute = new ErrorMessageAccessorAsyncValidationAttribute();
        var context = new ValidationContext(new MockModel())
        {
            DisplayName = "Name"
        };

        var result = await attribute.GetValidationResultAsync(null, context, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal("Name is invalid", result.ErrorMessage);
    }

    [Fact]
    public void IsValid_Ok()
    {
        Assert.True(new SuccessAsyncValidationAttribute().IsValid(null));
        Assert.False(new MessageAsyncValidationAttribute().IsValid(null));
    }

    private sealed class MockModel
    {
        [MockAsyncValidation(ErrorMessage = "{0} is invalid")]
        public string? Name { get; set; }
    }

    private sealed class MockAsyncValidationAttribute : AsyncValidationAttribute
    {
        public static int ValidateCount { get; private set; }

        public static void Reset() => ValidateCount = 0;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
            => throw new InvalidOperationException("The synchronous validation path should not be used.");

        protected override async Task<ValidationResult?> IsValidAsync(object? value, ValidationContext validationContext, CancellationToken cancellationToken)
        {
            await Task.Yield();
            cancellationToken.ThrowIfCancellationRequested();
            ValidateCount++;
            return new ValidationResult(null, [validationContext.MemberName!]);
        }
    }

    private sealed class SuccessAsyncValidationAttribute : AsyncValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) => ValidationResult.Success;

        protected override Task<ValidationResult?> IsValidAsync(object? value, ValidationContext validationContext, CancellationToken cancellationToken)
            => Task.FromResult<ValidationResult?>(ValidationResult.Success);
    }

    private sealed class MessageAsyncValidationAttribute : AsyncValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) => new("Invalid value");

        protected override Task<ValidationResult?> IsValidAsync(object? value, ValidationContext validationContext, CancellationToken cancellationToken)
            => Task.FromResult<ValidationResult?>(new ValidationResult("Invalid value"));
    }

    private sealed class StringConstructorAsyncValidationAttribute : AsyncValidationAttribute
    {
        public StringConstructorAsyncValidationAttribute() : base("{0} is invalid")
        {

        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) => new(null);

        protected override Task<ValidationResult?> IsValidAsync(object? value, ValidationContext validationContext, CancellationToken cancellationToken)
            => Task.FromResult<ValidationResult?>(new ValidationResult(null));
    }

    private sealed class ErrorMessageAccessorAsyncValidationAttribute : AsyncValidationAttribute
    {
        public ErrorMessageAccessorAsyncValidationAttribute() : base(() => "{0} is invalid")
        {

        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) => new(null);

        protected override Task<ValidationResult?> IsValidAsync(object? value, ValidationContext validationContext, CancellationToken cancellationToken)
            => Task.FromResult<ValidationResult?>(new ValidationResult(null));
    }

    private sealed class FirstAsyncValidationAttribute : AsyncValidationAttribute
    {
        public static int ValidateCount { get; private set; }

        public static void Reset() => ValidateCount = 0;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
            => throw new InvalidOperationException("The synchronous validation path should not be used.");

        protected override Task<ValidationResult?> IsValidAsync(object? value, ValidationContext validationContext, CancellationToken cancellationToken)
        {
            ValidateCount++;
            return Task.FromResult<ValidationResult?>(new ValidationResult("First invalid"));
        }
    }

    private sealed class SecondAsyncValidationAttribute : AsyncValidationAttribute
    {
        public static int ValidateCount { get; private set; }

        public static void Reset() => ValidateCount = 0;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
            => throw new InvalidOperationException("The synchronous validation path should not be used.");

        protected override Task<ValidationResult?> IsValidAsync(object? value, ValidationContext validationContext, CancellationToken cancellationToken)
        {
            ValidateCount++;
            return Task.FromResult<ValidationResult?>(ValidationResult.Success);
        }
    }
}
