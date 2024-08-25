// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Server.Data;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace UnitTest.Localization;

public class LocalizerZhTest : BootstrapBlazorZhTestBase
{
    [Fact]
    public void Foo_Json_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var foo = Foo.Generate(localizer);

        Assert.Equal("张三 1000", foo.Name);
    }

    [Fact]
    public void Dummy_Resource_Ok()
    {
        Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        Assert.Equal("zh-CN", CultureInfo.CurrentUICulture.Name);

        var val = Utility.GetDisplayName(typeof(Dummy), nameof(Dummy.Name));
        Assert.Equal("姓名", val);

        var model = new Dummy() { Name = "Name", Address = "Address" };
        val = Utility.GetDisplayName(model, nameof(Dummy.Address));
        Assert.Equal("Address1", val);
        Assert.Equal("Name", model.Name);
        Assert.Equal("Address", model.Address);
    }

    class Dummy
    {
        [Display(Name = "Name1")]
        public string? Name { get; set; }

        [Display(Name = "Address1")]
        public string? Address { get; set; }
    }
}
