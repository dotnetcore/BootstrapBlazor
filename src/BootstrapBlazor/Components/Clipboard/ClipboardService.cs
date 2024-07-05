// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text;

namespace BootstrapBlazor.Components;

/// <summary>
/// 粘贴板服务
/// </summary>
public class ClipboardService : BootstrapServiceBase<ClipboardOption>
{
    /// <summary>
    /// 获得 回调委托缓存集合
    /// </summary>
    private readonly List<(string Key, Func<string, Task<byte[]?>> Callback)> _callbackCache = [];

    private const string GetClipboardContentByMimeTypeKey = "getClipboardContentByMimeType";

    /// <summary>
    /// 注册回调方法
    /// </summary>
    /// <param name="callback"></param>
    internal void RegisterGetClipboardContentByMimeType(Func<string, Task<byte[]?>> callback) => _callbackCache.Add((GetClipboardContentByMimeTypeKey, callback));

    /// <summary>
    /// 注销回调方法
    /// </summary>
    internal void UnRegisterGetClipboardContentByMimeType()
    {
        var item = _callbackCache.FirstOrDefault(i => i.Key == GetClipboardContentByMimeTypeKey);
        if (item.Key != null) _callbackCache.Remove(item);
    }

    /// <summary>
    /// 获取剪切板内容方法
    /// </summary>
    /// <param name="mimeType">MIME类型</param>
    /// <returns></returns>
    public async Task<byte[]?> Get(string mimeType)
    {
        byte[]? ret = null;
        var (Key, Callback) = _callbackCache.FirstOrDefault(i => i.Key == GetClipboardContentByMimeTypeKey);
        if (Key != null)
        {
            ret = await Callback(mimeType);
        }
        return ret;
    }

    /// <summary>
    /// 获得剪切板拷贝文字方法
    /// </summary>
    /// <returns></returns>
    public async Task<string?> GetText()
    {
        string? ret = null;
        var bytes = await Get("text/plain");

        if (bytes is not null)
        {
            ret = Encoding.UTF8.GetString(bytes);
        }
        return ret;
    }

    /// <summary>
    /// 拷贝方法
    /// </summary>
    /// <param name="text">要拷贝的文字</param>
    /// <param name="callback">拷贝后回调方法</param>
    /// <returns></returns>
    public Task Copy(string? text, Func<Task>? callback = null) => Invoke(new ClipboardOption() { Text = text, Callback = callback });

}
