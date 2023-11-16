// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Shared;

/// <summary>
/// 
/// </summary>
public class WebhookPostBody
{
    /// <summary>
    /// 获得/设置 提交分支信息
    /// </summary>
    public string? Ref { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Sign { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public string GetBranchName() => Ref?.Replace("refs/heads/", "") ?? "";
}

/// <summary>
/// Gitee 提交事件参数实体类
/// </summary>
public class GiteePostBody : WebhookPostBody
{
    /// <summary>
    /// 获得/设置 提交信息集合
    /// </summary>
    public ICollection<GiteeCommit>? Commits { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("head_commit")]
    public GiteeCommit? HeadCommit { get; set; }
}

/// <summary>
/// 获得/设置 提交信息实体类
/// </summary>
public class GiteeCommit
{
    /// <summary>
    /// 获得/设置 提交消息
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// 获得/设置 提交时间戳
    /// </summary>
    public DateTimeOffset Timestamp { get; set; }

    /// <summary>
    /// 获得/设置 提交地址
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// 获得/设置 提交作者
    /// </summary>
    public GiteeAuthor Author { get; set; } = new GiteeAuthor();
}

/// <summary>
/// 获得/设置 提交作者信息
/// </summary>
public class GiteeAuthor
{
    /// <summary>
    /// 获得/设置 提交时间
    /// </summary>
    public DateTimeOffset Time { get; set; }

    /// <summary>
    /// 获得/设置 提交人 ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 获得/设置 提交人名称 Argo
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// 获得/设置 提交人邮件地址 argo@163.com
    /// </summary>
    public string Email { get; set; } = "";

    /// <summary>
    /// 获得/设置 提交人名称 Longbow
    /// </summary>
    public string UserName { get; set; } = "";

    /// <summary>
    /// 获得/设置 提交人 Gitee 地址
    /// </summary>
    public string Url { get; set; } = "";
}
