// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 演示网站示例数据注入服务实现类
/// </summary>
internal class DemoLookupService(IServiceProvider provider) : LookupServiceBase
{
    private IServiceProvider Provider { get; } = provider;

    public override IEnumerable<SelectedItem>? GetItemsByKey(string? key, object? data)
    {
        IEnumerable<SelectedItem>? items = null;
        if (key == "Foo.Complete")
        {
            var localizer = Provider.GetRequiredService<IStringLocalizer<Foo>>();
            items = new List<SelectedItem>()
            {
                new() { Value = "True", Text = localizer["True"].Value },
                new() { Value = "False", Text = localizer["False"].Value }
            };
        }
        return items;
    }
}
