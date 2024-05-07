// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Meilisearch;
using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// Pre 组件
/// </summary>
public partial class GlobalSearch
{
    [NotNull]
    private MeilisearchClient? MeilisearchClient { get; set; }

    [NotNull]
    private string? Name { get; set; }

    [NotNull]
    private string? Content { get; set; } = "";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        MeilisearchClient = new MeilisearchClient("http://47.92.144.33:7700", "BootstrapBlazorSearch");
    }

    private async Task OnSearch(ChangeEventArgs args)
    {
        var index = MeilisearchClient.Index("bbsearch");
        var doc = await index.SearchAsync<SearchModel>(args.Value!.ToString());
        var str = "";
        foreach (var item in doc.Hits)
        {
            str += $"""
                <br/><h5>{item.Title}<h5/><hr/>
                """;
            foreach (var demo in item.DemoBlocks!)
            {
                str += $"""
                <a href="{item.Url}">{demo.AnchorText}<a/><br/>
                """;
            }
        }
        Content = str;
    }
}
