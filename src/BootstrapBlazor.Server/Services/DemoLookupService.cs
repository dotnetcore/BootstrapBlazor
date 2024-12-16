// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
