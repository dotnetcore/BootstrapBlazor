// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
            RenderDrawer(builder, _option);
        }
    }

    private async Task Show(DrawerOption option)
    {
        _option = option;
        await InvokeAsync(StateHasChanged);
    }

    private void RenderDrawer(RenderTreeBuilder builder, DrawerOption option)
    {
        builder.OpenComponent<Drawer>(0);
        builder.SetKey(option);

        if (!string.IsNullOrEmpty(option.Class))
        {
            builder.AddAttribute(1, "class", option.Class);
        }
        builder.AddMultipleAttributes(2, GetParameters(option));
        builder.CloseComponent();
    }

    private Dictionary<string, object> GetParameters(DrawerOption option)
    {
        var parameters = new Dictionary<string, object>()
        {
            [nameof(Drawer.IsOpen)] = true,
            [nameof(Drawer.IsKeyboard)] = option.IsKeyboard,
            [nameof(Drawer.BodyScroll)] = option.BodyScroll,
            [nameof(Drawer.IsBackdrop)] = option.IsBackdrop,
            [nameof(Drawer.ShowBackdrop)] = option.ShowBackdrop,
            [nameof(Drawer.Placement)] = option.Placement,
            [nameof(Drawer.AllowResize)] = option.AllowResize,
            [nameof(Drawer.OnCloseAsync)] = new Func<Task>(() => OnCloseAsync(option))
        };
        if (!string.IsNullOrEmpty(option.Width))
        {
            parameters.Add(nameof(Drawer.Width), option.Width);
        }
        if (!string.IsNullOrEmpty(option.Height))
        {
            parameters.Add(nameof(Drawer.Height), option.Height);
        }
        if (option.ZIndex.HasValue)
        {
            parameters.Add(nameof(Drawer.ZIndex), option.ZIndex);
        }
        var content = option.GetContent();
        if (content != null)
        {
            parameters.Add(nameof(Drawer.ChildContent), content);
        }
        if (option.OnClickBackdrop != null)
        {
            parameters.Add(nameof(Drawer.OnClickBackdrop), option.OnClickBackdrop);
        }
        if (option.BodyContext != null)
        {
            parameters.Add(nameof(Drawer.BodyContext), option.BodyContext);
        }
        return parameters;
    }

    private async Task OnCloseAsync(DrawerOption option)
    {
        if (option.OnCloseAsync != null)
        {
            await option.OnCloseAsync();
        }

        _option = null;
        StateHasChanged();
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    private void Dispose(bool disposing)
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
