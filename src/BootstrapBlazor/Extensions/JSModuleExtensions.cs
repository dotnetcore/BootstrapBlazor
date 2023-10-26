// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Enums;

namespace BootstrapBlazor.Components;

/// <summary>
/// JSModule extensions class
/// </summary>
public static class JSModuleExtensions
{
    /// <summary>
    /// 导入js模块
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <returns></returns>
    private static async Task<IJSObjectReference> GetModule(IJSRuntime jsRuntime)
    {
        return await jsRuntime.InvokeAsync<IJSObjectReference>(identifier: "import", "./_content/BootstrapBlazor/modules/utility.js");
    }

    /// <summary>
    /// IJSRuntime 扩展方法 动态加载脚本 脚本目录为 modules
    /// <para>
    /// C# 示例：
    /// <code>
    /// [Inject]
    /// [NotNull]
    /// private <see cref="IJSRuntime"/> JSRuntime
    /// 
    /// [Inject]
    /// [NotNull]
    /// private <see cref="IVersionService"/> JSVersionService
    /// 
    /// var Module = await JSRuntime.LoadModule("xxx.razor.js",JSVersionService.GetVersion());
    /// </code>
    /// </para>
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="fileName"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    public static async Task<JSModule> LoadModule(this IJSRuntime jsRuntime, string fileName, string? version = null)
    {
        if (!string.IsNullOrEmpty(version))
        {
            fileName = $"{fileName}?v={version}";
        }
        var jSObjectReference = await jsRuntime.InvokeAsync<IJSObjectReference>(identifier: "import", fileName);
        return new JSModule(jSObjectReference);
    }

    /// <summary>
    /// 获得指定类型的加载 Module 名称
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static string GetTypeModuleName(this Type type)
    {
        var name = type.Name;
        if (type.IsGenericType)
        {
            var index = type.Name.IndexOf('`');
            name = type.Name[..index];
        }
        return name;
    }

    /// <summary>
    /// 在新标签页打开指定网址
    /// <para>
    /// C# 示例：
    /// <code>
    /// [Inject]
    /// [NotNull]
    /// private <see cref="IJSRuntime"/> JSRuntime
    /// 
    /// await JSRuntime.OpenBlankUrl(<see href="https://www.blazor.zone/"/>);
    /// </code>
    /// </para>
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="url"></param>
    /// <returns></returns>
    public static async ValueTask OpenBlankUrl(this IJSRuntime jsRuntime, string url)
    {
        var module = await GetModule(jsRuntime);
        await module.InvokeVoidAsync("openBlankUrl", url);
    }

    /// <summary>
    /// 获取是否为移动设备
    /// <para>
    /// C# 示例：
    /// <code>
    /// [Inject]
    /// [NotNull]
    /// private <see cref="IJSRuntime"/> JSRuntime
    /// 
    /// var res = await JSRuntime.GetIsMobileDevice();
    /// </code>
    /// </para>
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <returns></returns>
    public static async Task<bool> GetIsMobileDevice(this IJSRuntime jsRuntime)
    {
        var module = await GetModule(jsRuntime);
        return await module.InvokeAsync<bool>("isMobileDevice");
    }

    /// <summary>
    /// 通过Eval动态运行js代码
    /// <para>
    /// C# 示例：
    /// <code>
    /// [Inject]
    /// [NotNull]
    /// private <see cref="IJSRuntime"/> JSRuntime
    /// 
    /// var res = await JSRuntime.Eval<![CDATA[<string>]]>("your js code");
    /// </code>
    /// </para>
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
    /// <para>
    /// C# 示例：
    /// <code>
    /// [Inject]
    /// [NotNull]
    /// private <see cref="IJSRuntime"/> JSRuntime
    /// 
    /// await JSRuntime.Eval("your js code");
    /// </code>
    /// </para>
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
    /// 动态运行js代码
    /// <para>
    /// C# 示例：
    /// <code>
    /// [Inject]
    /// [NotNull]
    /// private <see cref="IJSRuntime"/> JSRuntime
    /// 
    /// var res = await JSRuntime.Function<![CDATA[<string>]]>("your js code");
    /// </code>
    /// </para>
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="script"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static async Task Function(this IJSRuntime jsRuntime, string script, params object?[]? args)
    {
        var module = await GetModule(jsRuntime);
        await module.InvokeVoidAsync("runFunction", script, args);
    }

    /// <summary>
    /// 动态运行js代码
    /// <para>
    /// C# 示例：
    /// <code>
    /// [Inject]
    /// [NotNull]
    /// private <see cref="IJSRuntime"/> JSRuntime
    /// 
    /// await JSRuntime.Function("your js code");
    /// </code>
    /// </para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="jsRuntime"></param>
    /// <param name="script"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static async Task<T> Function<T>(this IJSRuntime jsRuntime, string script, params object?[]? args)
    {
        var module = await GetModule(jsRuntime);
        return await module.InvokeAsync<T>("runFunction", script, args);
    }

    /// <summary>
    /// 清空浏览器控制台
    /// </summary>
    /// <param name="jsRuntime"></param>
    public static async Task ConsoleClear(this IJSRuntime jsRuntime)
    {
        await jsRuntime.InvokeVoidAsync("eval", "console.clear()");
    }

    /// <summary>
    /// 输出到浏览器控制台
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static async Task ConsoleLog(this IJSRuntime jsRuntime, params object?[]? args)
    {
        var module = await GetModule(jsRuntime);
        await module.InvokeVoidAsync("outPtuToConsole", "log", args);
    }

    /// <summary>
    /// 输出到浏览器控制台
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static async Task ConsoleWarn(this IJSRuntime jsRuntime, params object?[]? args)
    {
        var module = await GetModule(jsRuntime);
        await module.InvokeVoidAsync("outPtuToConsole", "warn", args);
    }

    /// <summary>
    /// 输出到浏览器控制台
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static async Task ConsoleError(this IJSRuntime jsRuntime, params object?[]? args)
    {
        var module = await GetModule(jsRuntime);
        await module.InvokeVoidAsync("outPtuToConsole", "error", args);
    }

    /// <summary>
    /// 输出到浏览器控制台
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static async Task ConsoleInfo(this IJSRuntime jsRuntime, params object?[]? args)
    {
        var module = await GetModule(jsRuntime);
        await module.InvokeVoidAsync("outPtuToConsole", "info", args);
    }

    /// <summary>
    /// 输出到浏览器控制台
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static async Task ConsoleAssert(this IJSRuntime jsRuntime, params object?[]? args)
    {
        var module = await GetModule(jsRuntime);
        await module.InvokeVoidAsync("outPtuToConsole", "assert", args);
    }

    /// <summary>
    /// 输出到浏览器控制台
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static async Task ConsoleDir(this IJSRuntime jsRuntime, params object?[]? args)
    {
        var module = await GetModule(jsRuntime);
        await module.InvokeVoidAsync("outPtuToConsole", "dir", args);
    }

    /// <summary>
    /// 输出到浏览器控制台
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static async Task ConsoleTime(this IJSRuntime jsRuntime, params object?[]? args)
    {
        var module = await GetModule(jsRuntime);
        await module.InvokeVoidAsync("outPtuToConsole", "time", args);
    }

    /// <summary>
    /// 输出到浏览器控制台
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static async Task ConsoleTimeEnd(this IJSRuntime jsRuntime, params object?[]? args)
    {
        var module = await GetModule(jsRuntime);
        await module.InvokeVoidAsync("outPtuToConsole", "timeEnd", args);
    }

    /// <summary>
    /// 输出到浏览器控制台
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static async Task ConsoleCount(this IJSRuntime jsRuntime, params object?[]? args)
    {
        var module = await GetModule(jsRuntime);
        await module.InvokeVoidAsync("outPtuToConsole", "count", args);
    }

    /// <summary>
    /// 输出到浏览器控制台
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static async Task ConsoleGroup(this IJSRuntime jsRuntime, params object?[]? args)
    {
        var module = await GetModule(jsRuntime);
        await module.InvokeVoidAsync("outPtuToConsole", "group", args);
    }

    /// <summary>
    /// 输出到浏览器控制台
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static async Task ConsoleGroupEnd(this IJSRuntime jsRuntime, params object?[]? args)
    {
        var module = await GetModule(jsRuntime);
        await module.InvokeVoidAsync("outPtuToConsole", "groupEnd", args);
    }

    /// <summary>
    /// 输出到浏览器控制台
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static async Task ConsoleTable(this IJSRuntime jsRuntime, params object?[]? args)
    {
        var module = await GetModule(jsRuntime);
        await module.InvokeVoidAsync("outPtuToConsole", "table", args);
    }

    /// <summary>
    /// 输出到浏览器控制台
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static async Task ConsoleTrace(this IJSRuntime jsRuntime, params object?[]? args)
    {
        var module = await GetModule(jsRuntime);
        await module.InvokeVoidAsync("outPtuToConsole", "trace", args);
    }

    /// <summary>
    /// 动态修改head标签
    /// <para>
    /// C# 示例：
    /// 
    /// <code>
    /// [Inject]
    /// [NotNull]
    /// private <see cref="IJSRuntime"/> JSRuntime
    /// 
    /// 添加一个link标签
    /// var result = await JSRuntime.ChangeMetaAsync(<see langword="true"/>,<see cref="HeadMetaType.Link"/>,"stylesheet","styles.css")
    /// 
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
    /// <para>
    /// C# 示例：
    /// 
    /// <code>
    /// [Inject]
    /// [NotNull]
    /// private <see cref="IJSRuntime"/> JSRuntime
    /// 
    /// var clientHeight = await JSRuntime.GetElementProperties<![CDATA[<]]><see langword="decimal"/><![CDATA[>]]>("element id", "clientHeight");
    /// </code>
    /// </para>
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="id">html 元素id</param>
    /// <param name="tag">html 元素属性名称</param>
    /// <returns></returns>
    public static async Task<T> GetElementProperties<T>(this IJSRuntime jsRuntime, string id, string tag)
    {
        var module = await GetModule(jsRuntime);
        return await module.InvokeAsync<T>("getElementProperties", id, tag);
    }

    /// <summary>
    /// 获取指定元素 CSS 的值
    /// <para>
    /// C# 示例：
    /// 
    /// <code>
    /// [Inject]
    /// [NotNull]
    /// private <see cref="IJSRuntime"/> JSRuntime
    /// 
    /// var Height = await JSRuntime.GetCSSValue<![CDATA[<]]><see langword="decimal"/><![CDATA[>]]>("element id", "height");
    /// </code>
    /// </para>
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
