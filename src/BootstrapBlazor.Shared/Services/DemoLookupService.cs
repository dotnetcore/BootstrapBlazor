// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared;
using Microsoft.Extensions.Localization;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 演示网站示例数据注入服务实现类
/// </summary>
internal class DemoLookupService : ILookupService
{
    private IServiceProvider Provider { get; }

    public DemoLookupService(IServiceProvider provider) => Provider = provider;

    public IEnumerable<SelectedItem>? GetItemsByKey(string? key)
    {
        IEnumerable<SelectedItem>? items = null;
        if (key == "Foo.Complete")
        {
            var localizer = Provider.GetRequiredService<IStringLocalizer<Foo>>();
            items = new List<SelectedItem>()
            {
                new() { Value = "true", Text = localizer["True"].Value },
                new() { Value = "false", Text = localizer["False"].Value }
            };
        }
        return items;
    }
}
