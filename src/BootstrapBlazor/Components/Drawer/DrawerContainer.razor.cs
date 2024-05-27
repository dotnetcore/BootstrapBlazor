// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Drawer 抽屉容器组件
/// </summary>
public partial class DrawerContainer : IDisposable
{
    /// <summary>
    /// 获得 弹出窗集合
    /// </summary>
    private readonly List<DrawerOption> _options = [];

    [Inject]
    [NotNull]
    private DrawerService? DrawerService { get; set; }

    [Inject]
    [NotNull]
    private IOptionsMonitor<BootstrapBlazorOptions>? Options { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 注册 Drawer 弹窗事件
        DrawerService.Register(this, Show);
    }

    private async Task Show(DrawerOption option)
    {
        _options.Add(option);
        await InvokeAsync(StateHasChanged);
    }

    /// <summary>
    /// 关闭弹窗
    /// </summary>
    /// <param name="option"></param>
    public async Task Close(DrawerOption option)
    {
        if (option.OnCloseAsync != null)
        {
            await option.OnCloseAsync();
        }
        _options.Remove(option);
        StateHasChanged();
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            DrawerService.UnRegister(this);
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
