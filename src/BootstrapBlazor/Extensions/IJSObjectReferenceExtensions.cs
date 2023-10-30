// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// IJSObjectReference 对象扩展方法
/// </summary>
public static class IJSObjectReferenceExtensions
{
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
    /// <param name="jsObject"><see cref="IJSObjectReference"/> 实例</param>
    /// <param name="url">打开网页地址</param>
    /// <param name="target">默认 _blank</param>
    /// <param name="features">默认 null</param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    public static ValueTask OpenUrl(this IJSObjectReference jsObject, string url, string? target = "_blank", string? features = null) => jsObject.InvokeVoidAsync("openUrl", url, target, features);

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
    /// <param name="jsObject"></param>
    /// <param name="script"></param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    public static ValueTask<T> Eval<T>(this IJSObjectReference jsObject, string script) => jsObject.InvokeAsync<T>("runEval", script);

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
    /// <param name="jsObject"></param>
    /// <param name="script"></param>
    /// <returns></returns>
    public static async ValueTask Eval(this IJSObjectReference jsObject, string script)
    {
        await jsObject.InvokeVoidAsync("runEval", script);
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
    /// <param name="jsObject"></param>
    /// <param name="script"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static ValueTask Function(this IJSObjectReference jsObject, string script, params object?[]? args) => jsObject.InvokeVoidAsync("runFunction", script, args);

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
    /// <param name="jsObject"></param>
    /// <param name="script"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static ValueTask<T> Function<T>(this IJSObjectReference jsObject, string script, params object?[]? args) => jsObject.InvokeAsync<T>("runFunction", script, args);

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
    /// <param name="jsObject"></param>
    /// <param name="isAdd"><see langword="true"/> 添加，<see langword="false"/> 移除</param>
    /// <param name="headMetaType">head标签元素类型</param>
    /// <param name="rel">类型</param>
    /// <param name="href">地址</param>
    /// <returns></returns>
    public static ValueTask<bool> ChangeMetaAsync(this IJSObjectReference jsObject, bool isAdd, string headMetaType, string rel, string href) => jsObject.InvokeAsync<bool>("changeMeta", isAdd, headMetaType, rel, href);

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
    /// <param name="jsObject"></param>
    /// <param name="id">html 元素id</param>
    /// <param name="tag">html 元素属性名称</param>
    /// <returns></returns>
    public static ValueTask<T> GetElementProperties<T>(this IJSObjectReference jsObject, string id, string tag) => jsObject.InvokeAsync<T>("getElementProperties", id, tag);

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
    /// <param name="jsObject"></param>
    /// <param name="id"></param>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    public static ValueTask<T?> GetCSSValue<T>(this IJSObjectReference jsObject, string id, string propertyName) => jsObject.InvokeAsync<T?>("getCSSValue", id, propertyName);

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
    /// <param name="jsObject"></param>
    /// <returns></returns>
    public static ValueTask<bool> IsMobile(this IJSObjectReference jsObject) => jsObject.InvokeAsync<bool>("isMobileDevice");
}
