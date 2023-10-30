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
    /// 导入 utility js 模块
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <returns></returns>
    public static Task<JSModule> LoadUtility(this IJSRuntime jsRuntime) => jsRuntime.LoadModule("./_content/BootstrapBlazor/modules/utility.js");

    /// <summary>
    /// IJSRuntime 扩展方法 动态加载脚本 脚本目录为 modules
    /// <para>
    /// C# 示例：
    /// <code>
    /// [Inject]
    /// [NotNull]
    /// private <see cref="IJSRuntime"/> JSRuntime
    /// 
    /// var Module = await JSRuntime.LoadModule("xxx.razor.js");
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
    /// await JSRuntime.OpenUrl(<see href="https://www.blazor.zone/"/>);
    /// </code>
    /// </para>
    /// </summary>
    /// <param name="module"><see cref="JSModule"/> 实例</param>
    /// <param name="url">打开网页地址</param>
    /// <param name="target">默认 _blank</param>
    /// <param name="features">默认 null</param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    public static ValueTask OpenUrl(this JSModule module, string url, string? target = "_blank", string? features = null) => module.InvokeVoidAsync("openUrl", url, target, features);

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
    /// <param name="module"><see cref="JSModule"/> 实例</param>
    /// <param name="script"></param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    public static ValueTask<T> Eval<T>(this JSModule module, string script) => module.InvokeAsync<T>("runEval", script);

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
    /// <param name="module"><see cref="JSModule"/> 实例</param>
    /// <param name="script"></param>
    /// <returns></returns>
    public static async ValueTask Eval(this JSModule module, string script)
    {
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
    /// <param name="module"><see cref="JSModule"/> 实例</param>
    /// <param name="script"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static ValueTask Function(this JSModule module, string script, params object?[]? args) => module.InvokeVoidAsync("runFunction", script, args);

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
    /// <param name="module"><see cref="JSModule"/> 实例</param>
    /// <param name="script"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static ValueTask<T> Function<T>(this JSModule module, string script, params object?[]? args) => module.InvokeAsync<T>("runFunction", script, args);

    /// <summary>
    /// 动态修改 head 标签
    /// <para>
    /// C# 示例：
    /// 
    /// <code>
    /// [Inject]
    /// [NotNull]
    /// private <see cref="IJSRuntime"/> JSRuntime
    /// 
    /// 添加一个link标签
    /// var result = await JSRuntime.ChangeMetaAsync(<see langword="true"/>, "link", "stylesheet", "styles.css")
    /// 
    /// 移除一个link标签
    /// var result = await JSRuntime.ChangeMetaAsync(<see langword="false"/>, "link", "stylesheet", "styles.css")
    /// </code>
    /// </para>
    /// </summary>
    /// <param name="module"><see cref="JSModule"/> 实例</param>
    /// <param name="isAdd"><see langword="true"/> 添加，<see langword="false"/> 移除</param>
    /// <param name="headMetaType">head标签元素类型</param>
    /// <param name="rel">类型</param>
    /// <param name="href">地址</param>
    /// <returns></returns>
    public static ValueTask<bool> ChangeMetaAsync(this JSModule module, bool isAdd, string headMetaType, string rel, string href) => module.InvokeAsync<bool>("changeMeta", isAdd, headMetaType, rel, href);

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
    /// <param name="module"><see cref="JSModule"/> 实例</param>
    /// <param name="id">html 元素id</param>
    /// <param name="tag">html 元素属性名称</param>
    /// <returns></returns>
    public static ValueTask<T> GetElementProperties<T>(this JSModule module, string id, string tag) => module.InvokeAsync<T>("getElementProperties", id, tag);

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
    /// <param name="module"><see cref="JSModule"/> 实例</param>
    /// <param name="id"></param>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    public static ValueTask<T?> GetCSSValue<T>(this JSModule module, string id, string propertyName) => module.InvokeAsync<T?>("getCSSValue", id, propertyName);

    /// <summary>
    /// 获取是否为移动设备
    /// <para>
    /// C# 示例：
    /// <code>
    /// [Inject]
    /// [NotNull]
    /// private <see cref="IJSRuntime"/> JSRuntime
    /// 
    /// var res = await JSRuntime.IsMobile();
    /// </code>
    /// </para>
    /// </summary>
    /// <param name="module"><see cref="JSModule"/> 实例</param>
    /// <returns></returns>
    public static ValueTask<bool> IsMobile(this JSModule module) => module.InvokeAsync<bool>("isMobileDevice");
}
