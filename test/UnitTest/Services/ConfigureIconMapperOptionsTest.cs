﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    internal class MockIconTheme(IOptions<IconThemeOptions> options) : IIconTheme
    {
        private IOptions<IconThemeOptions> Options { get; set; } = options;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public Dictionary<ComponentIcons, string> GetIcons()
        {
            if (!Options.Value.Icons.TryGetValue(Options.Value.ThemeKey, out var icons))
            {
                icons = [];
            }
            return icons;
        }
    }
}
