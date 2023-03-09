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
        context.Services.ConfigureIconMapperOptions(options =>
        {
            options.Items = new()
            {
                { ComponentIcons.AnchorLinkIcon, "mdi mdi-link-variant" }
            };
        });

        var iconService = context.Services.GetRequiredService<IOptions<IconMapperOptions>>();
        Assert.Equal("mdi mdi-link-variant", iconService.Value.Items[ComponentIcons.AnchorLinkIcon]);

        Assert.Equal("mdi mdi-link-variant", iconService.Value.GetIcon(ComponentIcons.AnchorLinkIcon));
        Assert.Equal("mdi mdi-test", iconService.Value.GetIcon(ComponentIcons.TableSortIcon, "mdi mdi-test"));
    }
}
