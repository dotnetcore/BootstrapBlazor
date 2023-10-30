﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
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
    /// <para>
    /// 示例：
    /// <code>
    /// [Inject]
    /// [NotNull]
    /// private <see cref="IJSRuntime"/> JSRuntime
    /// 
    /// [NotNull]
    /// private <see cref="JSModule"/>? Module { get; set; }
    /// 
    /// protected override <see langword="async"/> <see cref="Task"/> OnInitializedAsync()
    /// {
    ///     <see langword="await"/> <see langword="base"/>.OnInitializedAsync();
    ///     Module = <see langword="await"/> JSRuntime.LoadUtility();
    /// }
    /// </code>
    /// </para>
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <returns>A <see cref="Task"/><![CDATA[<]]><see cref="JSModule"/><![CDATA[>]]> 模块加载器</returns>
    public static Task<JSModule> LoadUtility(this IJSRuntime jsRuntime) => jsRuntime.LoadModule("./_content/BootstrapBlazor/modules/utility.js");

    /// <summary>
    /// IJSRuntime 扩展方法 动态加载脚本 脚本目录为 modules
    /// <para>
    /// 示例：
    /// <code>
    /// [Inject]
    /// [NotNull]
    /// private <see cref="IJSRuntime"/> JSRuntime
    ///
    /// [NotNull]
    /// private <see cref="JSModule"/>? Module { get; set; }
    /// 
    /// protected override <see langword="async"/> <see cref="Task"/> OnInitializedAsync()
    /// {
    ///     <see langword="await"/> <see langword="base"/>.OnInitializedAsync();
    ///     Module = <see langword="await"/> JSRuntime.LoadModule("xxx.js");
    /// }
    /// </code>
    /// </para>
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
    /// 示例：
    /// <code>
    /// [Inject]
    /// [NotNull]
    /// private <see cref="IJSRuntime"/> JSRuntime
    /// 
    /// [NotNull]
    /// private <see cref="JSModule"/>? Module { get; set; }
    /// 
    /// protected override <see langword="async"/> <see cref="Task"/> OnInitializedAsync()
    /// {
    ///     <see langword="await"/> <see langword="base"/>.OnInitializedAsync();
    ///     Module = <see langword="await"/> JSRuntime.LoadUtility();
    /// }
    ///
    /// private <see langword="async"/> <see cref="Task"/> OnClick()
    /// {
    ///     <see langword="await"/> Module.OpenUrl(<see href="https://www.blazor.zone/"/>);
    /// }
    /// 
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
    /// 动态运行js代码
    /// <para>
    /// 示例：
    /// <code>
    /// [Inject]
    /// [NotNull]
    /// private <see cref="IJSRuntime"/> JSRuntime
    /// 
    /// [NotNull]
    /// private <see cref="JSModule"/>? Module { get; set; }
    /// 
    /// protected override <see langword="async"/> <see cref="Task"/> OnInitializedAsync()
    /// {
    ///     <see langword="await"/> <see langword="base"/>.OnInitializedAsync();
    ///     Module = <see langword="await"/> JSRuntime.LoadUtility();
    /// }
    ///
    /// private <see langword="async"/> <see cref="Task"/> OnClick()
    /// {
    ///     <see langword="await"/> Module.Eval("your js code");
    /// }
    /// </code>
    /// </para>
    /// </summary>
    /// <param name="module"><see cref="JSModule"/> 实例</param>
    /// <param name="script"></param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    public static async ValueTask Eval(this JSModule module, string script) => await module.InvokeVoidAsync("runEval", script);

    /// <summary>
    /// 通过Eval动态运行js代码
    /// <para>
    /// 示例：
    /// <code>
    /// [Inject]
    /// [NotNull]
    /// private <see cref="IJSRuntime"/> JSRuntime
    /// 
    /// [NotNull]
    /// private <see cref="JSModule"/>? Module { get; set; }
    /// 
    /// protected override <see langword="async"/> <see cref="Task"/> OnInitializedAsync()
    /// {
    ///     <see langword="await"/> <see langword="base"/>.OnInitializedAsync();
    ///     Module = <see langword="await"/> JSRuntime.LoadUtility();
    /// }
    ///
    /// private <see langword="async"/> <see cref="Task"/> OnClick()
    /// {
    ///     var res = <see langword="await"/> Module.Eval<![CDATA[<string>]]>("your js code");
    /// }
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
    /// 示例：
    /// <code>
    /// [Inject]
    /// [NotNull]
    /// private <see cref="IJSRuntime"/> JSRuntime
    /// 
    /// [NotNull]
    /// private <see cref="JSModule"/>? Module { get; set; }
    /// 
    /// protected override <see langword="async"/> <see cref="Task"/> OnInitializedAsync()
    /// {
    ///     <see langword="await"/> <see langword="base"/>.OnInitializedAsync();
    ///     Module = <see langword="await"/> JSRuntime.LoadUtility();
    /// }
    ///
    /// private <see langword="async"/> <see cref="Task"/> OnClick()
    /// {
    ///     <see langword="await"/> Module.Function("your js code");
    /// }
    /// </code>
    /// </para>
    /// </summary>
    /// <param name="module"><see cref="JSModule"/> 实例</param>
    /// <param name="script"></param>
    /// <param name="args"></param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    public static ValueTask Function(this JSModule module, string script, params object?[]? args) => module.InvokeVoidAsync("runFunction", script, args);

    /// <summary>
    /// 动态运行js代码
    /// <para>
    /// 示例：
    /// <code>
    /// [Inject]
    /// [NotNull]
    /// private <see cref="IJSRuntime"/> JSRuntime
    /// 
    /// [NotNull]
    /// private <see cref="JSModule"/>? Module { get; set; }
    /// 
    /// protected override <see langword="async"/> <see cref="Task"/> OnInitializedAsync()
    /// {
    ///     <see langword="await"/> <see langword="base"/>.OnInitializedAsync();
    ///     Module = <see langword="await"/> JSRuntime.LoadUtility();
    /// }
    ///
    /// private <see langword="async"/> <see cref="Task"/> OnClick()
    /// {
    ///     var res = <see langword="await"/> Module.Function<![CDATA[<string>]]>("your js code"); 
    /// }
    /// </code>
    /// </para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="module"><see cref="JSModule"/> 实例</param>
    /// <param name="script"></param>
    /// <param name="args"></param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    public static ValueTask<T> Function<T>(this JSModule module, string script, params object?[]? args) => module.InvokeAsync<T>("runFunction", script, args);

    /// <summary>
    /// 动态增加 head 标签
    /// <para>
    /// 示例：
    /// <code>
    /// [Inject]
    /// [NotNull]
    /// private <see cref="IJSRuntime"/> JSRuntime
    /// 
    /// [NotNull]
    /// private <see cref="JSModule"/>? Module { get; set; }
    /// 
    /// protected override <see langword="async"/> <see cref="Task"/> OnInitializedAsync()
    /// {
    ///     <see langword="await"/> <see langword="base"/>.OnInitializedAsync();
    ///     Module = <see langword="await"/> JSRuntime.LoadUtility();
    /// }
    ///
    /// private <see langword="async"/> <see cref="Task"/> OnClick()
    /// {
    ///     //添加一个link标签
    ///     var result1 = <see langword="await"/> Module.ChangeMetaAsync(<see langword="true"/>, "link", "stylesheet", "styles.css")
    ///     //移除一个link标签
    ///     var result2 = <see langword="await"/> Module.ChangeMetaAsync(<see langword="false"/>, "link", "stylesheet", "styles.css")
    /// }
    /// </code>
    /// </para>
    /// </summary>
    /// <param name="module"><see cref="JSModule"/> 实例</param>
    /// <param name="content">添加的 Meta 内容</param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    public static ValueTask<bool> addMetaAsync(this JSModule module, string content) => module.InvokeAsync<bool>("addMeta", content);

    /// <summary>
    /// 动态移除 head 标签
    /// <para>
    /// 示例：
    /// <code>
    /// [Inject]
    /// [NotNull]
    /// private <see cref="IJSRuntime"/> JSRuntime
    /// 
    /// [NotNull]
    /// private <see cref="JSModule"/>? Module { get; set; }
    /// 
    /// protected override <see langword="async"/> <see cref="Task"/> OnInitializedAsync()
    /// {
    ///     <see langword="await"/> <see langword="base"/>.OnInitializedAsync();
    ///     Module = <see langword="await"/> JSRuntime.LoadUtility();
    /// }
    ///
    /// private <see langword="async"/> <see cref="Task"/> OnClick()
    /// {
    ///     //添加一个link标签
    ///     var result1 = <see langword="await"/> Module.ChangeMetaAsync(<see langword="true"/>, "link", "stylesheet", "styles.css")
    ///     //移除一个link标签
    ///     var result2 = <see langword="await"/> Module.ChangeMetaAsync(<see langword="false"/>, "link", "stylesheet", "styles.css")
    /// }
    /// </code>
    /// </para>
    /// </summary>
    /// <param name="module"><see cref="JSModule"/> 实例</param>
    /// <param name="content">移除 Meta 内容</param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    public static ValueTask<bool> RemoveMetaAsync(this JSModule module, string content) => module.InvokeAsync<bool>("removeMeta", content);

    /// <summary>
    /// 获取元素的指定属性
    /// <para>
    /// 示例：
    /// <code>
    /// [Inject]
    /// [NotNull]
    /// private <see cref="IJSRuntime"/> JSRuntime
    /// 
    /// [NotNull]
    /// private <see cref="JSModule"/>? Module { get; set; }
    /// 
    /// protected override <see langword="async"/> <see cref="Task"/> OnInitializedAsync()
    /// {
    ///     <see langword="await"/> <see langword="base"/>.OnInitializedAsync();
    ///     Module = <see langword="await"/> JSRuntime.LoadUtility();
    /// }
    ///
    /// private <see langword="async"/> <see cref="Task"/> OnClick()
    /// {
    ///     var clientHeight = <see langword="await"/> Module.GetElementProperties<![CDATA[<]]><see langword="decimal"/><![CDATA[>]]>("element id", "clientHeight");
    /// }
    /// </code>
    /// </para>
    /// </summary>
    /// <param name="module"><see cref="JSModule"/> 实例</param>
    /// <param name="id">html 元素id</param>
    /// <param name="tag">html 元素属性名称</param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    public static ValueTask<T> GetElementProperties<T>(this JSModule module, string id, string tag) => module.InvokeAsync<T>("getElementProperties", id, tag);

    /// <summary>
    /// 获取指定元素 CSS 的值
    /// <para>
    /// 示例：
    /// <code>
    /// [Inject]
    /// [NotNull]
    /// private <see cref="IJSRuntime"/> JSRuntime
    /// 
    /// [NotNull]
    /// private <see cref="JSModule"/>? Module { get; set; }
    /// 
    /// protected override <see langword="async"/> <see cref="Task"/> OnInitializedAsync()
    /// {
    ///     <see langword="await"/> <see langword="base"/>.OnInitializedAsync();
    ///     Module = <see langword="await"/> JSRuntime.LoadUtility();
    /// }
    ///
    /// private <see langword="async"/> <see cref="Task"/> OnClick()
    /// {
    ///     var Height = <see langword="await"/> Module.GetCSSValue<![CDATA[<]]><see langword="decimal"/><![CDATA[>]]>("element id", "height");
    /// }
    /// </code>
    /// </para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="module"><see cref="JSModule"/> 实例</param>
    /// <param name="id"></param>
    /// <param name="propertyName"></param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    public static ValueTask<T?> GetCSSValue<T>(this JSModule module, string id, string propertyName) => module.InvokeAsync<T?>("getCSSValue", id, propertyName);

    /// <summary>
    /// 获取是否为移动设备
    /// <para>
    /// 示例：
    /// <code>
    /// [Inject]
    /// [NotNull]
    /// private <see cref="IJSRuntime"/> JSRuntime
    /// 
    /// [NotNull]
    /// private <see cref="JSModule"/>? Module { get; set; }
    ///
    /// private <see cref="bool"/>? IsMobile { get; set; }
    /// 
    /// protected override <see langword="async"/> <see cref="Task"/> OnInitializedAsync()
    /// {
    ///     <see langword="await"/> <see langword="base"/>.OnInitializedAsync();
    ///     Module = <see langword="await"/> JSRuntime.LoadUtility();
    ///     IsMobile = <see langword="await"/> Module.IsMobile();
    /// }
    /// </code>
    /// </para>
    /// </summary>
    /// <param name="module"><see cref="JSModule"/> 实例</param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    public static ValueTask<bool> IsMobile(this JSModule module) => module.InvokeAsync<bool>("isMobile");
}
