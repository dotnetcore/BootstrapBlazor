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
        var option = new JsonLocalizationOptions
        {
            AdditionalJsonFiles = new string[]
            {
                "zh-CN.json"
            }
        };
        var configs = option.GetJsonStringFromAssembly(this.GetType().Assembly);
        var section = configs.FirstOrDefault(i => i.Key == "BootstrapBlazor.Shared.Foo");
        var v = section.GetValue("Name", "");
        Assert.NotEmpty(v);
    }

    [Fact]
    public void IgnoreLocalizerMissing_Ok()
    {
        var option = new JsonLocalizationOptions
        {
            IgnoreLocalizerMissing = true
        };
        Assert.True(option.IgnoreLocalizerMissing);
    }

    [Fact]
    public void GetJsonStringConfig_Fallback()
    {
        // 获得 it-it 文化信息
        // 回落默认语音为 en 测试用例为 zh 找不到资源文件
        var option = new JsonLocalizationOptions();
        var configs = option.GetJsonStringFromAssembly(this.GetType().Assembly, "it-it");
        Assert.Empty(configs);
    }

    [Fact]
    public void GetJsonStringConfig_Culture()
    {
        // 获得 it-it 文化信息
        // 回落默认语音为 en 测试用例为 zh 找不到资源文件
        var option = new JsonLocalizationOptions();
        var configs = option.GetJsonStringFromAssembly(this.GetType().Assembly, "en-US");
        Assert.NotEmpty(configs);
    }
}
