// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Enums;
using BootstrapBlazor.Extensions;

using System.Linq.Expressions;

namespace BootstrapBlazor.Components;

/// <summary>
/// JSModule extensions class
/// </summary>
public static class JSRuntimeExtensions
{
    /// <summary>
    /// 导入js模块
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <returns></returns>
    private static async Task<IJSObjectReference> GetModule(IJSRuntime jsRuntime) => await jsRuntime.InvokeAsync<IJSObjectReference>("import", $"./_content/BootstrapBlazor/modules/jsruntime-extensions.js");

    /// <summary>
    /// 获取是否为移动设备
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <returns></returns>
    public static async Task<bool> GetIsMobileDevice(this IJSRuntime jsRuntime)
    {
        var module = await GetModule(jsRuntime);
        return await module.InvokeAsync<bool>("isMobileDevice");
    }

    /// <summary>
    /// 动态运行js代码
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="script"></param>
    /// <returns></returns>
    public static async Task<T> Eval<T>(this IJSRuntime jsRuntime, string script)
    {
        var module = await GetModule(jsRuntime);
        return await module.InvokeAsync<T>("runEval", script);
    }

    /// <summary>
    /// 动态运行js代码
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="script"></param>
    /// <returns></returns>
    public static async Task Eval(this IJSRuntime jsRuntime, string script)
    {
        var module = await GetModule(jsRuntime);
        await module.InvokeVoidAsync("runEval", script);
    }

    /// <summary>
    /// 清空浏览器控制台
    /// </summary>
    /// <param name="jsRuntime"></param>
    public static async Task ConsoleClear(this IJSRuntime jsRuntime) => await jsRuntime.InvokeVoidAsync("eval", "console.clear()");

    /// <summary>
    /// 输出到浏览器控制台
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="consoleType"></param>
    /// <param name="args"></param>
    public static async Task Console(this IJSRuntime jsRuntime, ConsoleType consoleType, params object?[]? args)
    {
        var module = await GetModule(jsRuntime);
        await module.InvokeVoidAsync("doConsole", consoleType.ToDescriptionString(), args);
    }

    /// <summary>
    /// 输出到浏览器控制台
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static async Task Console(this IJSRuntime jsRuntime, params object?[]? args)
    {
        var module = await GetModule(jsRuntime);
        await module.InvokeVoidAsync("doConsole", ConsoleType.Info.ToDescriptionString(), args);
    }

    /// <summary>
    /// 动态修改head标签
    /// <para>
    /// razor示例：
    /// <code>
    /// @inject <see cref="IJSRuntime"/> JSRuntime
    /// 添加一个link标签
    /// var result = await JSRuntime.ChangeMetaAsync(<see langword="true"/>,<see cref="HeadMetaType.Link"/>,"stylesheet","styles.css")
    /// 移除一个link标签
    /// var result = await JSRuntime.ChangeMetaAsync(<see langword="false"/>,<see cref="HeadMetaType.Link"/>,"stylesheet","styles.css")
    /// </code>
    /// </para>
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="isAdd"><see langword="true"/> 添加，<see langword="false"/> 移除</param>
    /// <param name="headMetaType">head标签元素类型</param>
    /// <param name="rel">类型</param>
    /// <param name="href">地址</param>
    /// <returns></returns>
    public static async Task<bool> ChangeMetaAsync(this IJSRuntime jsRuntime, bool isAdd, HeadMetaType headMetaType, string rel, string href)
    {
        var module = await GetModule(jsRuntime);
        return await module.InvokeAsync<bool>("changeMeta", isAdd, headMetaType.ToDescriptionString(), rel, href);
    }

    /// <summary>
    /// 获取元素的指定属性
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="id"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    public static async Task<T> GetElementProperties<T>(this IJSRuntime jsRuntime, string id, string tag)
    {
        var module = await GetModule(jsRuntime);
        return await module.InvokeAsync<T>("getElementProperties", id, tag);
    }

    /// <summary>
    /// 获取指定元素 CSS 的值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="jsRuntime"></param>
    /// <param name="id"></param>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    public static async Task<T?> GetCSSValue<T>(this IJSRuntime jsRuntime, string id, string propertyName)
    {
        var module = await GetModule(jsRuntime);
        return await module.InvokeAsync<T?>("getCSSValue", id, propertyName);
    }
}
