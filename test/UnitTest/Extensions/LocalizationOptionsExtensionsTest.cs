// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Localization.Json;
using Microsoft.Extensions.Configuration;

namespace UnitTest.Extensions;

public class LocalizationOptionsExtensionsTest
{
    [Fact]
    public void GetJsonStringConfig_Ok()
    {
        var option = new JsonLocalizationOptions();
        option.AdditionalJsonFiles = new string[]
        {
            "zh-CN.json"
        };
        var configs = option.GetJsonStringConfig(this.GetType().Assembly);
        var section = configs.FirstOrDefault(i => i.Key == "BootstrapBlazor.Shared.Foo");
        var v = section.GetValue("Name", "");
        Assert.NotEmpty(v);
    }
}
