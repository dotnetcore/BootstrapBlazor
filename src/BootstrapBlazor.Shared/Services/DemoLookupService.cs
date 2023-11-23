// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;

namespace BootstrapBlazor.Shared.Services;

/// <summary>
/// 演示网站示例数据注入服务实现类
/// </summary>
public class DemoLookupService : ILookupService
{
    private IServiceProvider Provider { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="provider"></param>
    public DemoLookupService(IServiceProvider provider) => Provider = provider;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public IEnumerable<SelectedItem>? GetItemsByKey(string? key)
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
