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
    /// Load utility js module
    /// </summary>
    /// <param name="jsRuntime">The <see cref="IJSRuntime"/> instance</param>
    /// <param name="version">The version of the module</param>
    /// <returns>A <see cref="Task"/><![CDATA[<]]><see cref="JSModule"/><![CDATA[>]]> module loader</returns>
    public static Task<JSModule> LoadUtility(this IJSRuntime jsRuntime, string? version = null) => LoadModuleByName(jsRuntime, "utility", version);

    /// <summary>
    /// Load built-in script module by name
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
    /// IJSRuntime extension method to dynamically load scripts
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
    /// Get the module name of the specified type
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
    /// Open the specified URL in a new tab
    /// </summary>
    /// <param name="module"><see cref="JSModule"/> instance</param>
    /// <param name="url">The URL to open</param>
    /// <param name="target">The target window, default is _blank</param>
    /// <param name="features">The features of the new window, default is null</param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    public static ValueTask OpenUrl(this JSModule module, string url, string? target = "_blank", string? features = null) => module.InvokeVoidAsync("openUrl", url, target, features);

    /// <summary>
    /// Dynamically run js code
    /// </summary>
    /// <param name="module"><see cref="JSModule"/> instance</param>
    /// <param name="script">The script to run</param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    public static async ValueTask Eval(this JSModule module, string script) => await module.InvokeVoidAsync("runEval", script);

    /// <summary>
    /// Dynamically run JavaScript code via Eval
    /// </summary>
    /// <param name="module"><see cref="JSModule"/> instance</param>
    /// <param name="script">The script to run</param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    public static ValueTask<TValue?> Eval<TValue>(this JSModule module, string script) => module.InvokeAsync<TValue?>("runEval", script);

    /// <summary>
    /// Dynamically run JavaScript code via Function
    /// </summary>
    /// <param name="module"><see cref="JSModule"/> instance</param>
    /// <param name="script">The script to run</param>
    /// <param name="args">The arguments to pass to the script</param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    public static ValueTask Function(this JSModule module, string script, params object?[]? args) => module.InvokeVoidAsync("runFunction", script, args);

    /// <summary>
    /// Dynamically run js code
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
    /// Check if the current terminal is a mobile device
    /// </summary>
    /// <param name="module"><see cref="JSModule"/> instance</param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    public static ValueTask<bool> IsMobile(this JSModule module) => module.InvokeAsync<bool>("isMobile");

    /// <summary>
    /// Get a unique element ID on a page
    /// </summary>
    /// <param name="module">An instance of <see cref="JSModule"/></param>
    /// <param name="prefix">A prefix of type <see cref="string"/></param>
    /// <returns>Returns a <see cref="string"/> formatted element ID</returns>
    public static ValueTask<string?> GenerateId(this JSModule module, string? prefix = null) => module.InvokeAsync<string?>("getUID", prefix);

    /// <summary>
    /// Get the HTML string of a specified element on a page
    /// </summary>
    /// <param name="module">An instance of <see cref="JSModule"/></param>
    /// <param name="id">The ID of the element</param>
    /// <param name="selector">The selector of the element</param>
    /// <returns>Returns a <see cref="string"/> formatted element ID</returns>
    public static ValueTask<string?> GetHtml(this JSModule module, string? id = null, string? selector = null) => module.InvokeAsync<string?>("getHtml", new { id, selector });

    /// <summary>
    /// Set the theme method
    /// </summary>
    /// <param name="module">An instance of <see cref="JSModule"/></param>
    /// <param name="themeName">The name of the theme</param>
    /// <returns></returns>
    public static ValueTask SetThemeAsync(this JSModule module, string themeName) => module.InvokeVoidAsync("setTheme", themeName, true);

    /// <summary>
    /// Get the theme method
    /// </summary>
    /// <param name="module">An instance of <see cref="JSModule"/></param>
    /// <returns></returns>
    public static ValueTask<string?> GetThemeAsync(this JSModule module) => module.InvokeAsync<string?>("getTheme");

    /// <summary>
    /// Set memorial mode
    /// </summary>
    /// <param name="module">An instance of <see cref="JSModule"/></param>
    /// <param name="isMemorial">Whether it is memorial mode</param>
    /// <returns></returns>
    public static ValueTask SetMemorialModeAsync(this JSModule module, bool isMemorial) => module.InvokeVoidAsync("setMemorialMode", isMemorial);
}
