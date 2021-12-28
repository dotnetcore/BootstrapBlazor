// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using UnitTest.Core;
using Xunit;

namespace UnitTest.Localization;

public class LocalizerEnTest : BootstrapBlazorEnTestBase
{
    private IStringLocalizer<Foo> Localizer { get; }

    public LocalizerEnTest()
    {
        Localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
    }

    [Fact]
    public void Foo_Json_Ok()
    {
        var foo = Foo.Generate(Localizer);

        Assert.Equal("Zhangsan 1000", foo.Name);
    }

    [Fact]
    public void Dummy_Resource_Ok()
    {
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
