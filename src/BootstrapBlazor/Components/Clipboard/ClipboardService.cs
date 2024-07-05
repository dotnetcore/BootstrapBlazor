// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 粘贴板服务
/// </summary>
public class ClipboardService : BootstrapServiceBase<ClipboardOption>
{
    /// <summary>
    /// 获得 回调委托缓存集合
    /// </summary>
    private readonly List<(string Key, Func<Task<List<ClipboardItem>?>> Callback)> _callbackCache = [];

    private const string GetAllClipboardContentsKey = "getAllClipboardContents";

    /// <summary>
    /// 注册回调方法
    /// </summary>
    /// <param name="callback"></param>
    internal void RegisterGetAllClipboardContents(Func<Task<List<ClipboardItem>?>> callback) => _callbackCache.Add((GetAllClipboardContentsKey, callback));

    /// <summary>
    /// 注销回调方法
    /// </summary>
    internal void UnRegisterGetAllClipboardContents()
    {
        var item = _callbackCache.FirstOrDefault(i => i.Key == GetAllClipboardContentsKey);
        if (item.Key != null) _callbackCache.Remove(item);
    }

    /// <summary>
    /// 获取剪切板内容方法
    /// </summary>
    /// <param name="mimeType">MIME类型</param>
    /// <returns></returns>
    public async Task<List<ClipboardItem>?> Get()
    {
        List<ClipboardItem>? ret = null;
        var (Key, Callback) = _callbackCache.FirstOrDefault(i => i.Key == GetAllClipboardContentsKey);
        if (Key != null)
        {
            ret = await Callback();
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
