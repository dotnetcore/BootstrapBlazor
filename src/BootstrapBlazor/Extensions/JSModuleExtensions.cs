// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// JSModule extensions class
/// </summary>
public static class JSModuleExtensions
{
    /// <summary>
    /// IJSRuntime 扩展方法 动态加载脚本 脚本目录为 modules
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="fileName"></param>
    /// <param name="relative">是否为相对路径 默认 true</param>
    /// <returns></returns>
    public static async Task<JSModule> LoadModule(this IJSRuntime jsRuntime, string fileName, bool relative = true)
    {
        var filePath = relative ? $"./_content/BootstrapBlazor/modules/{fileName}.js" : fileName;
        var jSObjectReference = await jsRuntime.InvokeAsync<IJSObjectReference>(identifier: "import", filePath);
        return new JSModule(jSObjectReference);
    }

    /// <summary>
    /// IJSRuntime 扩展方法 动态加载脚本 脚本目录为 modules
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="jsRuntime"></param>
    /// <param name="fileName"></param>
    /// <param name="value"></param>
    /// <param name="relative">是否为相对路径 默认 true</param>
    /// <returns></returns>
    public static async Task<JSModule<TValue>> LoadModule<TValue>(this IJSRuntime jsRuntime, string fileName, TValue value, bool relative = true) where TValue : class
    {
        var filePath = relative ? $"./_content/BootstrapBlazor/modules/{fileName}.js" : fileName;
        var jSObjectReference = await jsRuntime.InvokeAsync<IJSObjectReference>(identifier: "import", filePath);
        return new JSModule<TValue>(jSObjectReference, value);
    }
}
