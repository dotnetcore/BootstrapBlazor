// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections.Concurrent;

namespace BootstrapBlazor.Components;

/// <summary>
/// TabItem Extension
/// </summary>
internal static class TabItemExtensions
{
    public static RenderFragment RenderContent(this TabItem item, ConcurrentDictionary<TabItem, TabItemContent> cache) => builder =>
    {
        builder.OpenComponent<TabItemContent>(0);
        builder.AddAttribute(10, nameof(TabItemContent.Item), item);
        builder.AddComponentReferenceCapture(20, content =>
        {
            var tabItemContent = (TabItemContent)content;
            cache.AddOrUpdate(item, tabItemContent, (_, _) => tabItemContent);
        });
        builder.CloseComponent();
    };

    public static void Refresh(this TabItem item, ConcurrentDictionary<TabItem, TabItemContent> cache)
    {
        if (cache.TryGetValue(item, out var content))
        {
            content.Render();
        }
    }
}
