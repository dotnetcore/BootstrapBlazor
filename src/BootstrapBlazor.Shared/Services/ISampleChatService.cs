// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Services;

/// <summary><para lang="zh">两种运行模式共用的流式聊天服务。</para><para lang="en">Streaming chat service shared by both hosting modes.</para></summary>
public interface ISampleChatService
{
    /// <summary><para lang="zh">示例初始额度。</para><para lang="en">Initial sample quota.</para></summary>
    const int TotalCount = 50;

    /// <summary><para lang="zh">获取后台剩余额度。</para><para lang="en">Gets the quota remaining on the server.</para></summary>
    Task<int> GetRemainingAsync(string code, CancellationToken cancellationToken = default);

    /// <summary><para lang="zh">逐段获取本次问题的回复。</para><para lang="en">Streams a response to the current question.</para></summary>
    IAsyncEnumerable<SampleChatUpdate> StreamAsync(SampleChatRequest request, CancellationToken cancellationToken = default);
}
