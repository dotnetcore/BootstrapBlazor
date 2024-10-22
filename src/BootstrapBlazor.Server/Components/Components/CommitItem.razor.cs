﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Components;

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
