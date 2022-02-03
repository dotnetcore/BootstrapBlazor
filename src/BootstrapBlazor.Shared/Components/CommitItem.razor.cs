// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Shared.Components;

/// <summary>
/// 
/// </summary>
public partial class CommitItem
{
    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    [NotNull]
    [EditorRequired]
    public GiteePostBody? Item { get; set; }

    private string? Author { get; set; }

    private string? Timestamp { get; set; }

    private string? Message { get; set; }

    private string? Url { get; set; }

    private string? Branch { get; set; }

    private string? TotalCount { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        var commit = Item.HeadCommit;
        TotalCount = Item.Commits?.Count.ToString() ?? "1";
        if (commit != null)
        {
            Timestamp = commit.Timestamp.ToString("yyyy-MM-dd HH:mm:ss");
            Author = commit.Author.Name;
            Message = commit.Message;
            Url = commit.Url;
            Branch = Item.GetBranchName();
        }
    }
}
