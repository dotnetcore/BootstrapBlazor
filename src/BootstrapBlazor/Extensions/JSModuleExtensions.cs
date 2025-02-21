// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// JSModule extensions class
/// </summary>
public static class JSModuleExtensions
{
    /// <summary>
    /// 导入 utility js 模块
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="version"></param>
    /// <returns>A <see cref="Task"/><![CDATA[<]]><see cref="JSModule"/><![CDATA[>]]> 模块加载器</returns>
    public static Task<JSModule> LoadUtility(this IJSRuntime jsRuntime, string? version = null) => LoadModuleByName(jsRuntime, "utility", version);

    /// <summary>
    /// 通过名称导入内置脚本模块
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="moduleName"></param>
    /// <param name="version"></param>
    /// <returns>A <see cref="Task"/><![CDATA[<]]><see cref="JSModule"/><![CDATA[>]]> 模块加载器</returns>
    public static Task<JSModule> LoadModuleByName(this IJSRuntime jsRuntime, string moduleName, string? version = null)
    {
        var fileName = $"./_content/BootstrapBlazor/modules/{moduleName}.js";
        return LoadModule(jsRuntime, fileName, version);
    }

    /// <summary>
    /// IJSRuntime 扩展方法 动态加载脚本
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="fileName"></param>
    /// <param name="version"></param>
    /// <returns>A <see cref="Task"/><![CDATA[<]]><see cref="JSModule"/><![CDATA[>]]> 模块加载器</returns>
    public static async Task<JSModule> LoadModule(this IJSRuntime jsRuntime, string fileName, string? version = null)
    {
        if (!string.IsNullOrEmpty(version))
        {
            fileName = $"{fileName}?v={version}";
        }

        IJSObjectReference? jSObjectReference = null;
        try
        {
            jSObjectReference = await jsRuntime.InvokeAsync<IJSObjectReference>(identifier: "import", fileName);
        }
        catch (JSDisconnectedException) { }
        catch (JSException)
        {
#if DEBUG
            System.Console.WriteLine($"{nameof(LoadModule)} throw {nameof(JSException)}. import {fileName} failed");
            throw;
#endif
        }
        catch (OperationCanceledException) { }
        catch (ObjectDisposedException) { }
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
    /// </summary>
    /// <param name="module"><see cref="JSModule"/> 实例</param>
    /// <param name="url">打开网页地址</param>
    /// <param name="target">默认 _blank</param>
    /// <param name="features">默认 null</param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    public static ValueTask OpenUrl(this JSModule module, string url, string? target = "_blank", string? features = null) => module.InvokeVoidAsync("openUrl", url, target, features);

    /// <summary>
    /// 动态运行js代码
    /// </summary>
    /// <param name="module"><see cref="JSModule"/> 实例</param>
    /// <param name="script"></param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    public static async ValueTask Eval(this JSModule module, string script) => await module.InvokeVoidAsync("runEval", script);

    /// <summary>
    /// 通过 Eval 动态运行 JavaScript 代码
    /// </summary>
    /// <param name="module"><see cref="JSModule"/> 实例</param>
    /// <param name="script"></param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    public static ValueTask<TValue?> Eval<TValue>(this JSModule module, string script) => module.InvokeAsync<TValue?>("runEval", script);

    /// <summary>
    /// 通过 Function 动态运行 JavaScript 代码
    /// </summary>
    /// <param name="module"><see cref="JSModule"/> 实例</param>
    /// <param name="script"></param>
    /// <param name="args"></param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    public static ValueTask Function(this JSModule module, string script, params object?[]? args) => module.InvokeVoidAsync("runFunction", script, args);

    /// <summary>
    /// 动态运行js代码
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="module"><see cref="JSModule"/> 实例</param>
    /// <param name="script"></param>
    /// <param name="args"></param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    public static async ValueTask<TValue?> Function<TValue>(this JSModule module, string script, params object?[]? args)
    {
        TValue? ret = default;
        if (module != null)
        {
            ret = await module.InvokeAsync<TValue?>("runFunction", script, args);
        }
        return ret;
    }

    /// <summary>
    /// 获取当前终端是否为移动设备
    /// </summary>
    /// <param name="module"><see cref="JSModule"/> 实例</param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    public static ValueTask<bool> IsMobile(this JSModule module) => module.InvokeAsync<bool>("isMobile");

    /// <summary>
    /// 获取一个页面上不重复的元素ID
    /// </summary>
    /// <param name="module">An instance of <see cref="JSModule"/></param>
    /// <param name="prefix">A prefix of type <see cref="string"/></param>
    /// <returns>Returns a <see cref="string"/> formatted element ID</returns>
    public static ValueTask<string?> GenerateId(this JSModule module, string? prefix = null) => module.InvokeAsync<string?>("getUID", prefix);

    /// <summary>
    /// 获取一个页面内指定元素 Html 字符串
    /// </summary>
    /// <param name="module">An instance of <see cref="JSModule"/></param>
    /// <param name="id"></param>
    /// <param name="selector"></param>
    /// <returns>Returns a <see cref="string"/> formatted element ID</returns>
    public static ValueTask<string?> GetHtml(this JSModule module, string? id = null, string? selector = null) => module.InvokeAsync<string?>("getHtml", new { id, selector });

    /// <summary>
    /// 设置主题方法
    /// </summary>
    /// <param name="module">An instance of <see cref="JSModule"/></param>
    /// <param name="themeName">theme name</param>
    /// <returns></returns>
    public static ValueTask SetThemeAsync(this JSModule module, string themeName) => module.InvokeVoidAsync("setTheme", themeName, true);

    /// <summary>
    /// 设置主题方法
    /// </summary>
    /// <param name="module">An instance of <see cref="JSModule"/></param>
    /// <returns></returns>
    public static ValueTask<string?> GetThemeAsync(this JSModule module) => module.InvokeAsync<string?>("getTheme");
}
