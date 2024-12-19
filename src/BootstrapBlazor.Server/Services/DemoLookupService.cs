// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections.Concurrent;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 演示网站示例数据注入服务实现类
/// </summary>
internal class DemoLookupService(IServiceProvider provider) : LookupServiceBase
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public override IEnumerable<SelectedItem>? GetItemsByKey(string? key, object? data) => null;

    private static readonly ConcurrentDictionary<string, List<SelectedItem>> _cache = [];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public override async Task<IEnumerable<SelectedItem>?> GetItemsByKeyAsync(string? key, object? data)
    {
        IEnumerable<SelectedItem>? items = null;
        if (key == "Foo.Complete")
        {
            // 使用缓存技术防止多次调用提高应用性能
            if (_cache.TryGetValue(key, out var value))
            {
                items = value;
            }
            else
            {
                // 模拟异步延时实战中大概率从数据库中获得数据
                await Task.Delay(1);

                var localizer = provider.GetRequiredService<IStringLocalizer<Foo>>();
                var v = new List<SelectedItem>()
                {
                    new() { Value = "True", Text = localizer["True"].Value },
                    new() { Value = "False", Text = localizer["False"].Value }
                };
                _cache.TryAdd(key, v);
                items = v;
            }
        }
        return items;
    }
}
