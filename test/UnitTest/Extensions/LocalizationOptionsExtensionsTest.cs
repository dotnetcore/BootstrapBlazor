// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Localization.Json;
using Microsoft.Extensions.Configuration;
using System.Reflection;

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
        // 回落默认语言为 en 测试用例为 zh 找不到资源文件
        var option = new JsonLocalizationOptions();
        var configs = option.GetJsonStringFromAssembly(this.GetType().Assembly, "it-it");
        Assert.Empty(configs);
    }

    [Fact]
    public void GetJsonStringConfig_Culture()
    {
        // 回落默认语音为 en 测试用例为 en-US 找不到资源文件
        var option = new JsonLocalizationOptions();
        var configs = option.GetJsonStringFromAssembly(this.GetType().Assembly, "en-US");
        Assert.NotEmpty(configs);

        var pi = option.GetType().GetProperty("EnableFallbackCulture", BindingFlags.NonPublic | BindingFlags.Instance);
        pi!.SetValue(option, false);
        configs = option.GetJsonStringFromAssembly(this.GetType().Assembly, "en");

        // 禁用回落机制
        // UniTest 未提供 en 资源文件 断言为 Empty
        Assert.Empty(configs);
    }
}
