// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Components;

/// <summary>
/// Editor 组件基类
/// </summary>
public partial class Editor : IDisposable
{
    /// <summary>
    /// 获得/设置 组件 DOM 实例
    /// </summary>
    private ElementReference EditorElement { get; set; }

    /// <summary>
    /// 获得/设置 JSInterop 实例
    /// </summary>
    private JSInterop<Editor>? Interope { get; set; }

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
    /// 获得/设置 设置组件高度
    /// </summary>
    [Parameter]
    public int Height { get; set; }

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

    [Inject]
    [NotNull]
    private IStringLocalizer<Editor>? Localizer { get; set; }

    private string? _value;
    private bool _renderValue;
    /// <summary>
    /// 获得/设置 组件值
    /// </summary>
    [Parameter]
    public string? Value
    {
        get { return _value; }
        set
        {
            if (_value != value)
            {
                _value = value;
                _renderValue = true;
            }
        }
    }

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
    /// OnInitialized 方法
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
        CustomerToolbarButtons ??= Enumerable.Empty<EditorToolbarButton>();
    }

    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            Interope = new JSInterop<Editor>(JSRuntime);
            var methodGetPluginAttrs = "";
            var methodClickPluginItem = "";
            if (CustomerToolbarButtons.Any())
            {
                methodGetPluginAttrs = nameof(GetPluginAttrs);
                methodClickPluginItem = nameof(ClickPluginItem);
            }
            await Interope.InvokeVoidAsync(this, EditorElement, "bb_editor", methodGetPluginAttrs, methodClickPluginItem, nameof(Update), Height, Value ?? "");
        }
        else if (_renderValue)
        {
            _renderValue = false;
            await JSRuntime.InvokeVoidAsync(EditorElement, "bb_editor", "code", "", "", "", Height, Value ?? "");
        }
    }

    /// <summary>
    /// Update 方法
    /// </summary>
    /// <param name="value"></param>
    [JSInvokable]
    public async Task Update(string value)
    {
        Value = value;
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }

        if (OnValueChanged != null)
        {
            await OnValueChanged.Invoke(value);
        }

        _renderValue = false;
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
    public Task<IEnumerable<EditorToolbarButton>> GetPluginAttrs()
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
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Interope?.Dispose();
            Interope = null;
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
