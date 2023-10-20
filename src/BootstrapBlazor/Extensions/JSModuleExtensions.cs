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
    /// IJSRuntime 扩展方法 动态加载脚本 脚本目录为 modules
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

    private static readonly string modulepath = $"./_content/BootstrapBlazor/modules/module-extensions.js";

    /// <summary>
    /// 清空浏览器控制台
    /// </summary>
    /// <param name="jsRuntime"></param>
    public static async Task ConsoleClear(this IJSRuntime jsRuntime)
    {
        var module = await jsRuntime.InvokeAsync<IJSObjectReference>("import", modulepath);
        await module.InvokeVoidAsync("doConsoleClear");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="consoleType"></param>
    /// <param name="args"></param>
    public static async Task Console(this IJSRuntime jsRuntime, ConsoleType consoleType, params object?[]? args)
    {
        var module = await jsRuntime.InvokeAsync<IJSObjectReference>("import", modulepath);
        await module.InvokeVoidAsync("doConsole", consoleType.ToDescriptionString(), args);
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
        var module = await jsRuntime.InvokeAsync<IJSObjectReference>("import", modulepath);
        return await module.InvokeAsync<bool>("changeMeta", isAdd, headMetaType.ToDescriptionString(), rel, href);
    }
}
