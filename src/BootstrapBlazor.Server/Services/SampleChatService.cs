// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace BootstrapBlazor.Server.Services;

/// <summary><para lang="zh">Server 直接调用 Azure，并在后台统一管理示例额度。</para><para lang="en">Calls Azure directly on Server and manages sample quota centrally.</para></summary>
public sealed class SampleChatService(IAzureOpenAIService service) : ISampleChatService
{
    private sealed class Quota
    {
        public int Remaining = ISampleChatService.TotalCount;
        public readonly SemaphoreSlim Gate = new(1, 1);
    }

    // 浏览器指纹仅延续示例限额，不是身份认证；额度始终保存在后台。
    // Browser fingerprints preserve the demo limit, not authentication; quota always stays on the server.
    private static readonly ConcurrentDictionary<string, Quota> _quotas = new();

    /// <inheritdoc />
    public Task<int> GetRemainingAsync(string code, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        Validator.ValidateProperty(code, new ValidationContext(new SampleChatRequest()) { MemberName = nameof(SampleChatRequest.Code) });
        return Task.FromResult(Volatile.Read(ref _quotas.GetOrAdd(code, _ => new()).Remaining));
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<SampleChatUpdate> StreamAsync(SampleChatRequest request, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        Validator.ValidateObject(request, new ValidationContext(request), true);
        var quota = _quotas.GetOrAdd(request.Code, _ => new());
        if (!await quota.Gate.WaitAsync(0, cancellationToken))
        {
            throw new InvalidOperationException("A chat request is already in progress.");
        }
        try
        {
            if (quota.Remaining < 1)
            {
                throw new InvalidOperationException("The chat quota is exhausted.");
            }
            var hasContent = false;
            await foreach (var update in service.GetChatCompletionsStreamingAsync(request.Content, cancellationToken))
            {
                if (!string.IsNullOrEmpty(update.Content))
                {
                    hasContent = true;
                    yield return new() { Content = update.Content };
                }
            }
            if (!hasContent)
            {
                throw new InvalidOperationException("The chat provider did not return a response.");
            }
            // 仅正常完成扣减一次，生成失败或取消不扣减。
            // Charge once on normal completion, not on failure or cancellation.
            cancellationToken.ThrowIfCancellationRequested();
            yield return new() { Completed = true, Remaining = Interlocked.Decrement(ref quota.Remaining) };
        }
        finally
        {
            quota.Gate.Release();
        }
    }
}
