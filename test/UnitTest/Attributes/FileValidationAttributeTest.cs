// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace UnitTest.Attributes;

public class FileValidationAttributeTest : BootstrapBlazorTestBase
{
    [Fact]
    public void FileSize_Ok()
    {
        var validator = new FileValidationAttribute()
        {
            FileSize = 5
        };
        var p = new Person()
        {
            Picture = new MockBrowserFile("test.log")
        };
        var result = validator.GetValidationResult(p.Picture, new ValidationContext(p));
        Assert.NotEqual(ValidationResult.Success, result);
    }

    [Fact]
    public void FileExtensions_Ok()
    {
        var validator = new FileValidationAttribute()
        {
            Extensions = ["jpg"]
        };
        var p = new Person()
        {
            Picture = new MockBrowserFile("test.log")
        };
        var result = validator.GetValidationResult(p.Picture, new ValidationContext(p));
        Assert.NotEqual(ValidationResult.Success, result);

        result = validator.GetValidationResult(p.Picture, new ValidationContext(p) { MemberName = "Pic" });
        Assert.NotEqual(ValidationResult.Success, result);
    }

    [Fact]
    public void IsValid_Ok()
    {
        var validator = new FileValidationAttribute()
        {
            Extensions = ["jpg"]
        };
        var p = new Person()
        {
            Picture = new MockBrowserFile("test.log")
        };
        Assert.False(validator.IsValid(p.Picture));
    }

    [Fact]
    public void Validate_Ok()
    {
        var validator = new FileValidationAttribute()
        {
            Extensions = ["jpg"]
        };
        var p = new Person()
        {
            Picture = new MockBrowserFile("test.log")
        };
        Assert.Throws<ValidationException>(() => validator.Validate(p.Picture, "Picture"));
        Assert.Throws<ValidationException>(() => validator.Validate(p.Picture, new ValidationContext(p)));
    }

    private class Person
    {
        [Required]
        [FileValidation(Extensions = [".png", ".jpg", ".jpeg"])]

        public IBrowserFile? Picture { get; set; }
    }

    private class MockBrowserFile(string name = "UploadTestFile", string contentType = "text") : IBrowserFile
    {
        public string Name { get; } = name;

        public DateTimeOffset LastModified { get; } = DateTimeOffset.Now;

        public long Size { get; } = 10;

        public string ContentType { get; } = contentType;

        public Stream OpenReadStream(long maxAllowedSize = 512000, CancellationToken cancellationToken = default)
        {
            return new MemoryStream([0x01, 0x02]);
        }
    }
}
