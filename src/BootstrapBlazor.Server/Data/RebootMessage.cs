// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Data;

/// <summary>
/// 网站重启消息类
/// </summary>
public class RebootMessage
{
    /// <summary>
    /// 消息标题
    /// </summary>
    public required string Title { get; init; }

    /// <summary>
    /// 消息内容
    /// </summary>
    public required string Content { get; init; }
}
