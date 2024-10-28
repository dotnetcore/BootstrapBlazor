// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples.Tutorials;

/// <summary>
/// 瀑布流组件示例文档
/// </summary>
public partial class Waterfall
{
    private static Task<IEnumerable<WaterfallItem>> GetItems(WaterfallItem? item) => Task.FromResult(Enumerable.Range(1, 18).Select(i => new WaterfallItem() { Url = $"Waterfall?id={i}", Id = $"{i}" }));
}
