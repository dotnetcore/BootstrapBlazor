// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class Message : IDisposable
{
    /// <summary>
    /// 获得 组件样式
    /// </summary>
    private string? ClassString => CssBuilder.Default("message")
        .AddClass("is-bottom", Placement != Placement.Top)
        .Build();

    /// <summary>
    /// 获得 Toast 组件样式设置
    /// </summary>
    private string? StyleName => CssBuilder.Default()
        .AddClass("top: 1rem;", Placement != Placement.Bottom)
        .AddClass("bottom: 1rem;", Placement == Placement.Bottom)
        .Build();

    /// <summary>
    /// 获得 弹出窗集合
    /// </summary>
    private List<MessageOption> Messages { get; } = new List<MessageOption>();

    /// <summary>
    /// 获得/设置 显示位置 默认为 Top
    /// </summary>
    [Parameter]
    public Placement Placement { get; set; } = Placement.Top;

    /// <summary>
    /// ToastServices 服务实例
    /// </summary>
    [Inject]
    [NotNull]
    public MessageService? MessageService { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 注册 Toast 弹窗事件
        MessageService.Register(this, Show);
    }

    /// <summary>
    /// 设置 Toast 容器位置方法
    /// </summary>
    /// <param name="placement"></param>
    public void SetPlacement(Placement placement)
    {
        Placement = placement;
        StateHasChanged();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    protected async Task Show(MessageOption option)
    {
        Messages.Add(option);
        await InvokeAsync(StateHasChanged);
    }

    /// <summary>
    /// 清除 ToastBox 方法
    /// </summary>
    [JSInvokable]
    public async Task Clear()
    {
        Messages.Clear();
        await InvokeAsync(StateHasChanged);
    }

    private List<MessageOption> GetMessages()
    {
        if (Placement != Placement.Top)
        {
            Messages.Reverse();
        }

        return Messages;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            MessageService.UnRegister(this);
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
