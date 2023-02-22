// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// SweetAlert 组件
/// </summary>
public partial class SweetAlert : IAsyncDisposable
{
    /// <summary>
    /// 获得/设置 Modal 容器组件实例
    /// </summary>
    [NotNull]
    private Modal? ModalContainer { get; set; }

    /// <summary>
    /// DialogServices 服务实例
    /// </summary>
    [Inject]
    [NotNull]
    private SwalService? SwalService { get; set; }

    private bool IsShowDialog { get; set; }

    private bool IsAutoHide { get; set; }

    private int Delay { get; set; }

    private CancellationTokenSource DelayToken { get; set; } = new();

    [NotNull]
    private Dictionary<string, object>? DialogParameter { get; set; }

    [NotNull]
    private Func<Task>? OnCloseAsync { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 注册 Swal 弹窗事件
        SwalService.Register(this, Show);

        // 设置 OnCloseAsync 回调方法
        OnCloseAsync = () =>
        {
            IsShowDialog = false;
            DialogParameter = null;
            if (AutoHideCheck())
            {
                DelayToken.Cancel();
            }
            StateHasChanged();
            return Task.CompletedTask;
        };
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (IsShowDialog)
        {
            // 打开弹窗
            await ModalContainer.Show();

            // 自动关闭处理逻辑
            if (AutoHideCheck())
            {
                try
                {
                    if (DelayToken.IsCancellationRequested)
                    {
                        DelayToken = new();
                    }
                    await Task.Delay(Delay, DelayToken.Token);
                    await ModalContainer.Close();
                }
                catch (TaskCanceledException) { }
            }
        }
    }

    private bool AutoHideCheck() => IsAutoHide && Delay > 0;

    private Task Show(SwalOption option)
    {
        if (!IsShowDialog)
        {
            // 保证仅打开一个弹窗
            IsShowDialog = true;

            IsAutoHide = option.IsAutoHide;
            Delay = option.Delay;

            option.Modal = ModalContainer;
            var parameters = option.ToAttributes();
            parameters.Add(nameof(ModalDialog.BodyTemplate), BootstrapDynamicComponent.CreateComponent<SweetAlertBody>(SweetAlertBody.Parse(option)).Render());

            DialogParameter = parameters;
            StateHasChanged();
        }
        return Task.CompletedTask;
    }

    private RenderFragment RenderDialog() => builder =>
    {
        if (DialogParameter != null)
        {
            var index = 0;
            builder.OpenComponent<ModalDialog>(index++);
            builder.SetKey(DialogParameter);
            builder.AddMultipleAttributes(index++, DialogParameter);
            builder.CloseComponent();
        }
    };

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            if (IsShowDialog)
            {
                // 关闭弹窗
                DelayToken.Cancel();
                await ModalContainer.Close();
                IsShowDialog = false;
            }

            // 释放 Token
            DelayToken.Dispose();

            // 注销服务
            SwalService.UnRegister(this);
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}
