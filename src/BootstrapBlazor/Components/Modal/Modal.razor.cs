// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Modal 组件
/// </summary>
public partial class Modal
{
    /// <summary>
    /// 获得 样式字符串
    /// </summary>
    private string? ClassString => CssBuilder.Default("modal")
        .AddClass("fade", IsFade)
        .Build();

    /// <summary>
    /// 获得 ModalDialog 集合
    /// </summary>
    protected List<ModalDialog> Dialogs { get; } = new(8);

    /// <summary>
    /// 获得/设置 是否后台关闭弹窗 默认 false
    /// </summary>
    [Parameter]
    public bool IsBackdrop { get; set; }

    /// <summary>
    /// 获得/设置 是否开启键盘支持 默认 true 响应键盘 ESC 按键
    /// </summary>
    [Parameter]
    public bool IsKeyboard { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否开启淡入淡出动画 默认为 true 开启动画
    /// </summary>
    [Parameter]
    public bool IsFade { get; set; } = true;

    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 组件已经渲染完毕回调方法
    /// </summary>
    [Parameter]
    public Func<Modal, Task>? FirstAfterRenderCallbackAsync { get; set; }

    /// <summary>
    /// 获得/设置 弹窗已显示时回调此方法
    /// </summary>
    [Parameter]
    public Func<Task>? OnShownAsync { get; set; }

    /// <summary>
    /// 获得/设置 关闭弹窗回调委托
    /// </summary>
    [Parameter]
    public Func<Task>? OnCloseAsync { get; set; }

    /// <summary>
    /// 获得 后台关闭弹窗设置
    /// </summary>
    private string? Backdrop => IsBackdrop ? null : "static";

    private string? KeyboardString => IsKeyboard ? "true" : "false";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender && FirstAfterRenderCallbackAsync != null)
        {
            await FirstAfterRenderCallbackAsync(this);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(ShownCallback), nameof(CloseCallback));

    /// <summary>
    /// 添加对话框方法
    /// </summary>
    /// <param name="dialog"></param>
    internal void AddDialog(ModalDialog dialog)
    {
        Dialogs.Add(dialog);
        ResetShownDialog(dialog);
    }

    /// <summary>
    /// 移除对话框方法
    /// </summary>
    /// <param name="dialog"></param>
    internal void RemoveDialog(ModalDialog dialog)
    {
        // 移除当前弹窗
        Dialogs.Remove(dialog);

        if (Dialogs.Any())
        {
            ResetShownDialog(Dialogs.Last());
        }
    }

    private void ResetShownDialog(ModalDialog dialog)
    {
        // 保证新添加的 Dialog 为当前弹窗
        Dialogs.ForEach(d =>
        {
            d.IsShown = d == dialog;
        });
    }

    /// <summary>
    /// 弹窗已经弹出回调方法 JSInvoke 调用
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task ShownCallback()
    {
        if (OnShownAsync != null)
        {
            await OnShownAsync();
        }
    }

    /// <summary>
    /// 弹窗已经关闭回调方法 JSInvoke 调用
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task CloseCallback()
    {
        // 移除当前弹窗
        var dialog = Dialogs.FirstOrDefault(d => d.IsShown);
        if (dialog != null)
        {
            Dialogs.Remove(dialog);
        }

        // 多级弹窗支持
        if (Dialogs.Any())
        {
            ResetShownDialog(Dialogs.Last());
        }

        if (OnCloseAsync != null)
        {
            await OnCloseAsync();
        }
    }

    /// <summary>
    /// 弹窗状态切换方法
    /// </summary>
    public Task Toggle() => InvokeVoidAsync("execute", Id, "toggle");

    /// <summary>
    /// 显示弹窗方法
    /// </summary>
    /// <returns></returns>
    public Task Show() => InvokeVoidAsync("execute", Id, "show");

    /// <summary>
    /// 关闭当前弹窗方法
    /// </summary>
    /// <returns></returns>
    public Task Close() => InvokeVoidAsync("execute", Id, "hide");

    /// <summary>
    /// 设置 Header 文字方法
    /// </summary>
    /// <param name="text"></param>
    public void SetHeaderText(string text)
    {
        var dialog = Dialogs.FirstOrDefault(d => d.IsShown);
        dialog?.SetHeaderText(text);
    }
}
