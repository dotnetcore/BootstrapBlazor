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
    /// <returns>A <see cref="Task"/><![CDATA[<]]><see cref="JSModule"/><![CDATA[>]]> 模块加载器</returns>
    public static Task<JSModule> LoadUtility(this IJSRuntime jsRuntime) => jsRuntime.LoadModule("./_content/BootstrapBlazor/modules/utility.js");

    /// <summary>
    /// IJSRuntime 扩展方法 动态加载脚本 脚本目录为 modules
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
    /// 通过 Eval 动态运行 javascript 代码
    /// </summary>
    /// <param name="module"><see cref="JSModule"/> 实例</param>
    /// <param name="script"></param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    public static ValueTask<T> Eval<T>(this JSModule module, string script) => module.InvokeAsync<T>("runEval", script);

    /// <summary>
    /// 通过 Function 动态运行 javascript 代码
    /// </summary>
    /// <param name="module"><see cref="JSModule"/> 实例</param>
    /// <param name="script"></param>
    /// <param name="args"></param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    public static ValueTask Function(this JSModule module, string script, params object?[]? args) => module.InvokeVoidAsync("runFunction", script, args);

    /// <summary>
    /// 动态运行js代码
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="module"><see cref="JSModule"/> 实例</param>
    /// <param name="script"></param>
    /// <param name="args"></param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    public static ValueTask<T> Function<T>(this JSModule module, string script, params object?[]? args) => module.InvokeAsync<T>("runFunction", script, args);

    ///// <summary>
    ///// 动态增加 head 标签
    ///// <para>
    ///// 示例：
    ///// <code>
    ///// [Inject]
    ///// [NotNull]
    ///// private <see cref="IJSRuntime"/> JSRuntime
    ///// 
    ///// [NotNull]
    ///// private <see cref="JSModule"/>? Module { get; set; }
    ///// 
    ///// protected override <see langword="async"/> <see cref="Task"/> OnAfterRenderAsync(bool firstRender)
    ///// {
    /////     <see langword="await"/> <see langword="base"/>.OnAfterRenderAsync(firstRender);
    /////     
    /////     <see langword="if"/>(firstRender)
    /////     {
    /////         Module = <see langword="await"/> JSRuntime.LoadUtility();
    /////     }
    ///// }
    /////
    ///// private <see langword="async"/> <see cref="Task"/> OnClick()
    ///// {
    /////     var result = <see langword="await"/> Module.AddMetaAsync("styles.css")
    ///// }
    ///// </code>
    ///// </para>
    ///// </summary>
    ///// <param name="module"><see cref="JSModule"/> 实例</param>
    ///// <param name="content">添加的 Meta 内容</param>
    ///// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    //public static ValueTask<bool> AddMetaAsync(this JSModule module, string content) => module.InvokeAsync<bool>("addMeta", content);

    ///// <summary>
    ///// 动态移除 head 标签
    ///// <para>
    ///// 示例：
    ///// <code>
    ///// [Inject]
    ///// [NotNull]
    ///// private <see cref="IJSRuntime"/> JSRuntime
    ///// 
    ///// [NotNull]
    ///// private <see cref="JSModule"/>? Module { get; set; }
    ///// 
    ///// protected override <see langword="async"/> <see cref="Task"/> OnAfterRenderAsync(bool firstRender)
    ///// {
    /////     <see langword="await"/> <see langword="base"/>.OnAfterRenderAsync(firstRender);
    /////     
    /////     <see langword="if"/>(firstRender)
    /////     {
    /////         Module = <see langword="await"/> JSRuntime.LoadUtility();
    /////     }
    ///// }
    /////
    ///// private <see langword="async"/> <see cref="Task"/> OnClick()
    ///// {
    /////     var result = <see langword="await"/> Module.RemoveMetaAsync("styles.css")
    ///// }
    ///// </code>
    ///// </para>
    ///// </summary>
    ///// <param name="module"><see cref="JSModule"/> 实例</param>
    ///// <param name="content">移除 Meta 内容</param>
    ///// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation.</returns>
    //public static ValueTask<bool> RemoveMetaAsync(this JSModule module, string content) => module.InvokeAsync<bool>("removeMeta", content);

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
