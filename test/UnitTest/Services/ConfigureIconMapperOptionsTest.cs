// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace UnitTest.Services;

public class ConfigureIconMapperOptionsTest
{
    [Fact]
    public void ConfigureIconMapperOptions_NoKey()
    {
        var context = new TestContext();
        context.Services.AddConfiguration();
        context.Services.AddBootstrapBlazor();
        context.Services.ConfigureIconThemeOptions(options =>
        {
            options.ThemeKey = "mock";
        });

        var iconService = context.Services.GetRequiredService<IIconTheme>();
        Assert.Empty(iconService.GetIcons());
    }

    [Fact]
    public void ConfigureIconMapperOptions_Ok()
    {
        var context = new TestContext();
        context.Services.AddConfiguration();
        context.Services.AddSingleton<IIconTheme, MockIconTheme>();
        context.Services.AddOptionsMonitor<IconThemeOptions>();
        context.Services.ConfigureIconThemeOptions(options =>
        {
            options.ThemeKey = "mock";
            options.Icons["mock"] = new()
            {
                { ComponentIcons.AnchorLinkIcon, "mdi mdi-link-variant" }
            };
        });

        var iconService = context.Services.GetRequiredService<IIconTheme>();
        Assert.Equal("mdi mdi-link-variant", iconService.GetIconByKey(ComponentIcons.AnchorLinkIcon));
        Assert.Null(iconService.GetIconByKey(ComponentIcons.AutoFillIcon));
    }

    internal class MockIconTheme : IIconTheme
    {
        private IOptions<IconThemeOptions> Options { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public MockIconTheme(IOptions<IconThemeOptions> options)
        {
            Options = options;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public Dictionary<ComponentIcons, string> GetIcons()
        {
            if (!Options.Value.Icons.TryGetValue(Options.Value.ThemeKey, out var icons))
            {
                icons = new Dictionary<ComponentIcons, string>();
            }
            return icons;
        }
    }
}
