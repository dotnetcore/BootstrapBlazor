// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Azure.AI.OpenAI;

namespace BootstrapBlazor.Components;

/// <summary>
/// 消息体
/// </summary>
public class AzureOpenAIChatMessage
{
    /// <summary>
    /// 获得/设置 角色
    /// </summary>
    public ChatRole Role { get; set; }

    /// <summary>
    /// 获得/设置 聊天助手内容
    /// </summary>
    [NotNull]
    public string? Content { get; set; }

    /// <summary>
    /// 获得 响应时间戳
    /// </summary>
    public DateTimeOffset Time { get; } = DateTimeOffset.Now;
}
