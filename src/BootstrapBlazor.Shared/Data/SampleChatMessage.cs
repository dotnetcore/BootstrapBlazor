// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Data;

/// <summary><para lang="zh">与 Azure SDK 无关的聊天显示消息。</para><para lang="en">A display message independent of the Azure SDK.</para></summary>
public sealed class SampleChatMessage
{
    /// <summary><para lang="zh">是否为助手消息。</para><para lang="en">Whether this is an assistant message.</para></summary>
    public bool IsAssistant { get; set; }
    /// <summary><para lang="zh">消息文本。</para><para lang="en">Message text.</para></summary>
    public string Content { get; set; } = "";
    /// <summary><para lang="zh">显示时间。</para><para lang="en">Display timestamp.</para></summary>
    public DateTimeOffset Time { get; set; } = DateTimeOffset.Now;
}

/// <summary><para lang="zh">逐行传输的流式聊天事件。</para><para lang="en">A newline-delimited streaming chat event.</para></summary>
public sealed class SampleChatUpdate
{
    /// <summary><para lang="zh">本段文本。</para><para lang="en">The text delta.</para></summary>
    public string? Content { get; set; }
    /// <summary><para lang="zh">后台剩余额度，仅完成时返回。</para><para lang="en">Server quota remaining, returned on completion.</para></summary>
    public int? Remaining { get; set; }
    /// <summary><para lang="zh">是否正常完成。</para><para lang="en">Whether generation completed normally.</para></summary>
    public bool Completed { get; set; }
    /// <summary><para lang="zh">流中发生的错误。</para><para lang="en">An error occurring during streaming.</para></summary>
    public string? Error { get; set; }
}
