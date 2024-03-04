// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples.Tutorials;

/// <summary>
/// 瀑布流组件示例文档
/// </summary>
public partial class Waterfall
{
    private static Task<IEnumerable<WaterfallItem>> GetItems(WaterfallItem? item) => Task.FromResult(Enumerable.Range(1, 18).Select(i => new WaterfallItem() { Url = $"Waterfall?id={i}", Id = $"{i}" }));
}
