// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace UnitTest.Services;

public class ConfigureIconMapperOptionsTest
{
    [Fact]
    public void ConfigureIconMapperOptions_Ok()
    {
        var context = new TestContext();
        context.Services.AddSingleton<IIconTheme, MockIconTheme>();

        var iconService = context.Services.GetRequiredService<IIconTheme>();
        Assert.Equal("mdi mdi-link-variant", iconService.GetIconByKey(ComponentIcons.AnchorLinkIcon));
        Assert.Equal("mdi mdi-test", iconService.GetIconByKey(ComponentIcons.TableSortIcon, "mdi mdi-test"));
    }

    internal class MockIconTheme : IIconTheme
    {
        private Dictionary<ComponentIcons, string> Icons { get; }

        public MockIconTheme()
        {
            Icons = new Dictionary<ComponentIcons, string>()
            {
                { ComponentIcons.AnchorLinkIcon, "mdi mdi-link-variant" }
            };
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Dictionary<ComponentIcons, string> GetIcons() => Icons;
    }
}
