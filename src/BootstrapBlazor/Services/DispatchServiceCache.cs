// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">供 WebAssembly 客户端拉取的消息分发缓存</para>
/// <para lang="en">Dispatch cache polled by WebAssembly clients.</para>
/// </summary>
/// <param name="cache"></param>
public sealed class DispatchServiceCache(IMemoryCache cache)
{
    private static readonly TimeSpan _expiration = TimeSpan.FromSeconds(8);

    private static readonly JsonSerializerOptions _serializerOptions = new(JsonSerializerDefaults.Web);

    /// <summary>
    /// <para lang="zh">缓存增加时回调事件，携带消息类型全名与消息内容</para>
    /// <para lang="en">Callback event carrying the entry type's full name and message when the cache is added.</para>
    /// </summary>
    public event Func<string, JsonElement, Task>? OnDispatchAsync;

    /// <summary>
    /// <para lang="zh">缓存指定类型的最新消息</para>
    /// <para lang="en">Cache the latest message for the entry type.</para>
    /// </summary>
    /// <typeparam name="TEntry"></typeparam>
    /// <param name="entry"></param>
    /// <param name="triggerDispatch"></param>
    public void Set<TEntry>(DispatchEntry<TEntry> entry, bool triggerDispatch = true)
    {
        var entryType = typeof(TEntry).FullName ?? typeof(TEntry).Name;
        Set(entryType, JsonSerializer.SerializeToElement(entry, _serializerOptions), triggerDispatch);
    }

    /// <summary>
    /// <para lang="zh">缓存指定类型的最新 JSON 消息，八秒后过期</para>
    /// <para lang="en">Cache the latest JSON message for the entry type with an eight-second expiration.</para>
    /// </summary>
    /// <param name="entryType"></param>
    /// <param name="entry"></param>
    /// <param name="triggerDispatch"></param>
    /// <returns>The identifier of this dispatch.</returns>
    public string Set(string entryType, JsonElement entry, bool triggerDispatch = true)
    {
        var item = entry.Clone();
        var id = Guid.NewGuid().ToString("N");
        cache.Set($"Dispatch-{entryType}", (id, item), _expiration);
        if (triggerDispatch)
        {
            OnDispatchAsync?.Invoke(entryType, item);
        }

        return id;
    }

    /// <summary>
    /// <para lang="zh">获取指定类型的最新消息及其唯一标识，不移除缓存</para>
    /// <para lang="en">Get the latest message and its unique identifier without removing it from the cache.</para>
    /// </summary>
    /// <param name="entryType"></param>
    /// <param name="entry"></param>
    /// <returns></returns>
    public bool TryGetValue(string entryType, out (string Id, JsonElement Entry) entry)
        => cache.TryGetValue($"Dispatch-{entryType}", out entry);
}
