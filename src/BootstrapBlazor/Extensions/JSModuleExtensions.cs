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

    /// <summary>
    /// 导入js模块
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <returns></returns>
    private static async Task<IJSObjectReference> ImportModuleAsync(IJSRuntime jsRuntime) =>
        await jsRuntime.InvokeAsync<IJSObjectReference>("import", $"./_content/BootstrapBlazor/modules/module-extensions.js");

    /// <summary>
    /// 清空浏览器控制台
    /// </summary>
    /// <param name="jsRuntime"></param>
    public static async Task ConsoleClear(this IJSRuntime jsRuntime)
    {
        var module = await ImportModuleAsync(jsRuntime);
        await module.InvokeVoidAsync("doConsoleClear");
    }

    /// <summary>
    /// 输出到浏览器控制台
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="consoleType"></param>
    /// <param name="args"></param>
    public static async Task Console(this IJSRuntime jsRuntime, ConsoleType consoleType, params object?[]? args)
    {
        var module = await ImportModuleAsync(jsRuntime);
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
        var module = await ImportModuleAsync(jsRuntime);
        return await module.InvokeAsync<bool>("changeMeta", isAdd, headMetaType.ToDescriptionString(), rel, href);
    }

    /// <summary>
    /// 获取属性值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="jsRuntime"></param>
    /// <param name="properties"></param>
    /// <returns></returns>
    public static async Task<T> GetProperties<T>(this IJSRuntime jsRuntime, string properties)
    {
        var module = await ImportModuleAsync(jsRuntime);
        return await module.InvokeAsync<T>(properties);
    }

    /// <summary>
    /// 视口高度
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <returns></returns>
    public static async Task<decimal> GetDocumentClientHeight(this IJSRuntime jsRuntime) => await GetProperties<decimal>(jsRuntime, "document.documentElement.clientHeight");

    /// <summary>
    /// 视口宽度
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <returns></returns>
    public static async Task<decimal> GetDocumentClientWidth(this IJSRuntime jsRuntime) => await GetProperties<decimal>(jsRuntime, "document.documentElement.clientWidth");

    /// <summary>
    /// 元素的总高度
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <returns></returns>
    public static async Task<decimal> GetDocumentOffsetHeight(this IJSRuntime jsRuntime) => await GetProperties<decimal>(jsRuntime, "document.documentElement.offsetHeight");

    /// <summary>
    /// 元素的总宽度
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <returns></returns>
    public static async Task<decimal> GetDocumentOffsetWidth(this IJSRuntime jsRuntime) => await GetProperties<decimal>(jsRuntime, "document.documentElement.offsetWidth");

    /// <summary>
    /// 整个文档的高度
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <returns></returns>
    public static async Task<decimal> GetDocumentScrollHeight(this IJSRuntime jsRuntime) => await GetProperties<decimal>(jsRuntime, "document.documentElement.scrollHeight");

    /// <summary>
    /// 滚动条距离顶部的距离
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <returns></returns>
    public static async Task<decimal> GetDocumentScrollTop(this IJSRuntime jsRuntime) => await GetProperties<decimal>(jsRuntime, "document.documentElement.scrollTop");

    /// <summary>
    /// 元素内容的总高度
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <returns></returns>
    public static async Task<decimal> GetBodyScrollHeight(this IJSRuntime jsRuntime) => await GetProperties<decimal>(jsRuntime, "document.body.scrollHeight");

    /// <summary>
    /// 元素内容的总宽度
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <returns></returns>
    public static async Task<decimal> GetBodyScrollWidth(this IJSRuntime jsRuntime) => await GetProperties<decimal>(jsRuntime, "document.body.scrollWidth");

    /// <summary>
    /// 滚动条距离顶部的距离
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <returns></returns>
    public static async Task<decimal> GetBodyScrollTop(this IJSRuntime jsRuntime) => await GetProperties<decimal>(jsRuntime, "document.body.scrollTop");

    /// <summary>
    /// 滚动条距离左侧的距离
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <returns></returns>
    public static async Task<decimal> GetBodyScrollLeft(this IJSRuntime jsRuntime) => await GetProperties<decimal>(jsRuntime, "document.body.scrollLeft");


}
