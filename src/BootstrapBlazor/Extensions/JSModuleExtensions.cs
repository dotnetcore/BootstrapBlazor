// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">JSModule extensions class</para>
/// <para lang="en">JSModule extensions class</para>
/// </summary>
public static class JSModuleExtensions
{
    /// <summary>
    /// <para lang="zh">Load utility js module</para>
    /// <para lang="en">Load utility js module</para>
    /// </summary>
    /// <param name="jsRuntime">The <see cref="IJSRuntime"/> instance</param>
    /// <param name="version">The version of the module</param>
    /// <returns>A <see cref="Task"/><![CDATA[<]]><see cref="JSModule"/><![CDATA[>]]> module loader</returns>
    public static Task<JSModule> LoadUtility(this IJSRuntime jsRuntime, string? version = null) => LoadModuleByName(jsRuntime, "utility", version);

    /// <summary>
    /// <para lang="zh">Load built-in script module by name</para>
    /// <para lang="en">Load built-in script module by name</para>
    /// </summary>
    /// <param name="jsRuntime">The <see cref="IJSRuntime"/> instance</param>
    /// <param name="moduleName">The name of the module</param>
    /// <param name="version">The version of the module</param>
    /// <returns>A <see cref="Task"/><![CDATA[<]]><see cref="JSModule"/><![CDATA[>]]> module loader</returns>
    public static Task<JSModule> LoadModuleByName(this IJSRuntime jsRuntime, string moduleName, string? version = null)
    {
        var fileName = $"./_content/BootstrapBlazor/modules/{moduleName}.js";
        return LoadModule(jsRuntime, fileName, version);
    }

    /// <summary>
    /// <para lang="zh">IJSRuntime extension method to dynamically load scripts</para>
    /// <para lang="en">IJSRuntime extension method to dynamically load scripts</para>
    /// </summary>
    /// <param name="jsRuntime">The <see cref="IJSRuntime"/> instance</param>
    /// <param name="fileName">The file name of the script</param>
    /// <param name="version">The version of the script</param>
    /// <returns>A <see cref="Task"/><![CDATA[<]]><see cref="JSModule"/><![CDATA[>]]> module loader</returns>
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
    /// <para lang="zh">Get the module name of the specified 类型</para>
    /// <para lang="en">Get the module name of the specified type</para>
    /// </summary>
    /// <param name="type">The type</param>
    /// <returns>The module name</returns>
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
    /// <para lang="zh">Open the specified URL in a new tab</para>
    /// <para lang="en">Open the specified URL in a new tab</para>
    /// </summary>
    /// <param name="module"><see cref="JSModule"/> instance</param>
    /// <param name="url">The URL to open</param>
    /// <param name="target">The target window, default is _blank</param>
    /// <param name="features">The features of the new window, default is null</param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    public static ValueTask OpenUrl(this JSModule module, string url, string? target = "_blank", string? features = null) => module.InvokeVoidAsync("openUrl", url, target, features);

    /// <summary>
    /// <para lang="zh">Dynamically run js code</para>
    /// <para lang="en">Dynamically run js code</para>
    /// </summary>
    /// <param name="module"><see cref="JSModule"/> instance</param>
    /// <param name="script">The script to run</param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    public static async ValueTask Eval(this JSModule module, string script) => await module.InvokeVoidAsync("runEval", script);

    /// <summary>
    /// <para lang="zh">Dynamically run JavaScript code via Eval</para>
    /// <para lang="en">Dynamically run JavaScript code via Eval</para>
    /// </summary>
    /// <param name="module"><see cref="JSModule"/> instance</param>
    /// <param name="script">The script to run</param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    public static ValueTask<TValue?> Eval<TValue>(this JSModule module, string script) => module.InvokeAsync<TValue?>("runEval", script);

    /// <summary>
    /// <para lang="zh">Dynamically run JavaScript code via Function</para>
    /// <para lang="en">Dynamically run JavaScript code via Function</para>
    /// </summary>
    /// <param name="module"><see cref="JSModule"/> instance</param>
    /// <param name="script">The script to run</param>
    /// <param name="args">The arguments to pass to the script</param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    public static ValueTask Function(this JSModule module, string script, params object?[]? args) => module.InvokeVoidAsync("runFunction", script, args);

    /// <summary>
    /// <para lang="zh">Dynamically run js code</para>
    /// <para lang="en">Dynamically run js code</para>
    /// </summary>
    /// <typeparam name="TValue">The return type</typeparam>
    /// <param name="module"><see cref="JSModule"/> instance</param>
    /// <param name="script">The script to run</param>
    /// <param name="args">The arguments to pass to the script</param>
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
    /// <para lang="zh">Check if the current terminal is a mobile device</para>
    /// <para lang="en">Check if the current terminal is a mobile device</para>
    /// </summary>
    /// <param name="module"><see cref="JSModule"/> instance</param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    public static ValueTask<bool> IsMobile(this JSModule module) => module.InvokeAsync<bool>("isMobile");

    /// <summary>
    /// <para lang="zh">Get a unique element ID on a page</para>
    /// <para lang="en">Get a unique element ID on a page</para>
    /// </summary>
    /// <param name="module">An instance of <see cref="JSModule"/></param>
    /// <param name="prefix">A prefix of type <see cref="string"/></param>
    /// <returns>Returns a <see cref="string"/> formatted element ID</returns>
    public static ValueTask<string?> GenerateId(this JSModule module, string? prefix = null) => module.InvokeAsync<string?>("getUID", prefix);

    /// <summary>
    /// <para lang="zh">Get the HTML string of a specified element on a page</para>
    /// <para lang="en">Get the HTML string of a specified element on a page</para>
    /// </summary>
    /// <param name="module">An instance of <see cref="JSModule"/></param>
    /// <param name="id">The ID of the element</param>
    /// <param name="selector">The selector of the element</param>
    /// <returns>Returns a <see cref="string"/> formatted element ID</returns>
    public static ValueTask<string?> GetHtml(this JSModule module, string? id = null, string? selector = null) => module.InvokeAsync<string?>("getHtml", new { id, selector });

    /// <summary>
    /// <para lang="zh">Set the theme method</para>
    /// <para lang="en">Set the theme method</para>
    /// </summary>
    /// <param name="module">An instance of <see cref="JSModule"/></param>
    /// <param name="themeName">The name of the theme</param>
    /// <returns></returns>
    public static ValueTask SetThemeAsync(this JSModule module, string themeName) => module.InvokeVoidAsync("setTheme", themeName, true);

    /// <summary>
    /// <para lang="zh">Get the theme method</para>
    /// <para lang="en">Get the theme method</para>
    /// </summary>
    /// <param name="module">An instance of <see cref="JSModule"/></param>
    /// <returns></returns>
    public static ValueTask<string?> GetThemeAsync(this JSModule module) => module.InvokeAsync<string?>("getTheme");

    /// <summary>
    /// <para lang="zh">Set memorial mode</para>
    /// <para lang="en">Set memorial mode</para>
    /// </summary>
    /// <param name="module">An instance of <see cref="JSModule"/></param>
    /// <param name="isMemorial">Whether it is memorial mode</param>
    /// <returns></returns>
    public static ValueTask SetMemorialModeAsync(this JSModule module, bool isMemorial) => module.InvokeVoidAsync("setMemorialMode", isMemorial);
}
