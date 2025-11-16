// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using BootstrapBlazor.Server.Data;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace UnitTest.Localization;

public class LocalizerEnTest : BootstrapBlazorEnTestBase
{
    [Fact]
    public void Foo_Json_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var foo = Foo.Generate(localizer);

        Assert.Equal("Zhangsan 1000", foo.Name);
    }

    [Fact]
    public void Dummy_Resource_Ok()
    {
        Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        Assert.Equal("en-US", CultureInfo.CurrentUICulture.Name);

        var val = Utility.GetDisplayName(typeof(DummyEn), nameof(DummyEn.Name));
        Assert.Equal("TestName", val);

        var model = new DummyEn() { Name = "Name", Address = "Address" };
        val = Utility.GetDisplayName(model, nameof(DummyEn.Address));
        Assert.Equal("Address1", val);
        Assert.Equal("Name", model.Name);
        Assert.Equal("Address", model.Address);
    }

    class DummyEn
    {
        [Display(Name = "Name1")]
        public string? Name { get; set; }

        [Display(Name = "Address1")]
        public string? Address { get; set; }
    }
}
