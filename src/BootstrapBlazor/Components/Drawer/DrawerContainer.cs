// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components;

/// <summary>
/// Drawer 抽屉容器组件
/// </summary>
public class DrawerContainer : ComponentBase, IDisposable
{
    [Inject]
    [NotNull]
    private DrawerService? DrawerService { get; set; }

    private DrawerOption? _option;

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 注册 Drawer 弹窗事件
        DrawerService.Register(this, Show);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (_option != null)
        {
            var parameters = new Dictionary<string, object>()
            {
                [nameof(Drawer.IsOpen)] = true,
                [nameof(Drawer.IsBackdrop)] = _option.IsBackdrop,
                [nameof(Drawer.ShowBackdrop)] = _option.ShowBackdrop,
                [nameof(Drawer.Placement)] = _option.Placement,
                [nameof(Drawer.AllowResize)] = _option.AllowResize
            };
            if (!string.IsNullOrEmpty(_option.Width))
            {
                parameters.Add(nameof(Drawer.Width), _option.Width);
            }
            if (!string.IsNullOrEmpty(_option.Height))
            {
                parameters.Add(nameof(Drawer.Height), _option.Height);
            }
            if (_option.ChildContent != null)
            {
                parameters.Add(nameof(Drawer.ChildContent), _option.ChildContent);
            }
            parameters.Add(nameof(Drawer.IsOpenChanged), EventCallback.Factory.Create<bool>(this, v =>
            {
                _option = null;
            }));
            builder.OpenComponent<Drawer>(0);
            builder.AddMultipleAttributes(1, parameters);
            builder.CloseComponent();
        }
    }

    private async Task Show(DrawerOption option)
    {
        _option = option;
        await InvokeAsync(StateHasChanged);
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
