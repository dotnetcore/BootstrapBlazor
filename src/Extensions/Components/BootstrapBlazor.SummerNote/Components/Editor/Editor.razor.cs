// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Editor 组件基类
/// </summary>
public partial class Editor
{
    /// <summary>
    /// 获得 Editor 样式
    /// </summary>
    private string? EditClassString => CssBuilder.Default("editor-body form-control")
        .AddClass("open", IsEditor)
        .Build();

    /// <summary>
    /// 获得/设置 Placeholder 提示消息
    /// </summary>
    [Parameter]
    [NotNull]
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// 获得/设置 是否直接显示为富文本编辑框
    /// </summary>
    [Parameter]
    public bool IsEditor { get; set; }

    /// <summary>
    /// 获得/设置 设置组件高度 默认高度为 80px
    /// </summary>
    [Parameter]
    public int Height { get; set; } = 80;

    /// <summary>
    /// 获得/设置 富文本框工具栏工具，默认为空使用默认值
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<object>? ToolbarItems { get; set; }

    /// <summary>
    /// 获得/设置 自定义按钮
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<EditorToolbarButton>? CustomerToolbarButtons { get; set; }

    /// <summary>
    /// 获得/设置 是否显示工具栏提交按钮 默认 true 显示
    /// </summary>
    /// <remarks>工具栏提交按钮在工具栏最后位置，此按钮设计上是避免编辑内容变化时频繁提交代码到服务器，点击提交按钮后再发送到服务器，禁用此按钮时每次更新内容均提交代码到服务器端</remarks>
    [Parameter]
    public bool ShowSubmit { get; set; } = true;

    private bool _lastShowSubmit = true;

    private string? ShowSubmitString => ShowSubmit ? "true" : null;

    [Inject]
    [NotNull]
    private IStringLocalizer<Editor>? Localizer { get; set; }

    private string? _lastValue;

    /// <summary>
    /// 获得/设置 组件值
    /// </summary>
    [Parameter]
    public string? Value { get; set; }

    /// <summary>
    /// 获得/设置 语言，默认为 null 自动判断，内置中英文额外语言包需要自行引入语言包
    /// </summary>
    [Parameter]
    public string? Language { get; set; }

    /// <summary>
    /// 获得/设置 语言扩展脚本路径 默认 null 如加载德语可设置为 
    /// </summary>
    [Parameter]
    public string? LanguageUrl { get; set; }

    /// <summary>
    /// 获得/设置 组件值变化后的回调委托
    /// </summary>
    [Parameter]
    public EventCallback<string?> ValueChanged { get; set; }

    /// <summary>
    /// 获得/设置 组件值变化后的回调委托
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnValueChanged { get; set; }

    /// <summary>
    /// 获取/设置 插件点击时的回调委托
    /// </summary>
    [Parameter]
    public Func<string, Task<string>>? OnClickButton { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        PlaceHolder ??= Localizer[nameof(PlaceHolder)];

        ToolbarItems ??= new List<object>
        {
            new List<object> { "style", new List<string>() { "style" } },
            new List<object> { "font", new List<string>() { "bold", "underline", "clear" } },
            new List<object> { "fontname", new List<string>() { "fontname"} },
            new List<object> { "color", new List<string>() { "color"} },
            new List<object> { "para", new List<string>() { "ul", "ol", "paragraph"} },
            new List<object> { "table", new List<string>() { "table"} },
            new List<object> { "insert", new List<string>() { "link", "picture", "video" } },
            new List<object> { "view", new List<string>() { "fullscreen", "codeview", "help"} }
        };
        CustomerToolbarButtons ??= [];
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (string.IsNullOrEmpty(Language))
        {
            Language = CultureInfo.CurrentUICulture.Name;
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _lastShowSubmit = ShowSubmit;
            _lastValue = Value;
        }

        // ShowSubmit 处理
        if (_lastShowSubmit != ShowSubmit)
        {
            _lastShowSubmit = ShowSubmit;
            await InvokeVoidAsync("reset", Id, Value ?? "");
        }

        // Value 处理
        if (_lastValue != Value)
        {
            _lastValue = Value;
            await InvokeVoidAsync("update", Id, Value ?? "");
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task InvokeInitAsync()
    {
        var methodGetPluginAttributes = "";
        var methodClickPluginItem = "";
        if (CustomerToolbarButtons.Any())
        {
            methodGetPluginAttributes = nameof(GetPluginAttributes);
            methodClickPluginItem = nameof(ClickPluginItem);
        }

        await InvokeVoidAsync("init", Id, Interop, methodGetPluginAttributes, methodClickPluginItem, Height, Value ?? "", Language, LanguageUrl);
    }

    /// <summary>
    /// Update 方法
    /// </summary>
    /// <param name="value"></param>
    [JSInvokable]
    public async Task Update(string value)
    {
        Value = value;
        _lastValue = Value;

        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }

        if (OnValueChanged != null)
        {
            await OnValueChanged.Invoke(value);
        }
    }

    /// <summary>
    /// 获取编辑器的 toolbar
    /// </summary>
    /// <returns>toolbar</returns>
    [JSInvokable]
    public Task<List<object>> GetToolBar()
    {
        var list = new List<object>(50);
        list.AddRange(ToolbarItems);

        var itemList = new List<object>
        {
            "custom",
            CustomerToolbarButtons.Select(p => p.ButtonName).ToList()
        };
        list.Add(itemList);

        return Task.FromResult(list);
    }

    /// <summary>
    /// 获取插件信息
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public Task<IEnumerable<EditorToolbarButton>> GetPluginAttributes()
    {
        return Task.FromResult(CustomerToolbarButtons);
    }

    /// <summary>
    /// 插件点击事件
    /// </summary>
    /// <param name="pluginItemName">插件名</param>
    /// <returns>插件回调的文本</returns>
    [JSInvokable]
    public async Task<string> ClickPluginItem(string pluginItemName)
    {
        var ret = "";
        if (OnClickButton != null)
        {
            ret = await OnClickButton(pluginItemName);
        }
        return ret;
    }

    /// <summary>
    /// 执行 editor 的方法
    /// </summary>
    public async ValueTask DoMethodAsync(string method, params object[] value)
    {
        if (Module != null)
        {
            await Module.InvokeVoidAsync("invoke", Id, method, value);
        }
    }
}
