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

    private CancellationTokenSource? DelayToken { get; set; }

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
            IsShowDialog = false;
            await ModalContainer.Show();

            if (IsAutoHide && Delay > 0)
            {
                await DelayCloseAsync();
            }
        }

        [ExcludeFromCodeCoverage]
        async Task DelayCloseAsync()
        {
            DelayToken ??= new CancellationTokenSource();
            try
            {
                await Task.Delay(Delay, DelayToken.Token);
                await ModalContainer.Close();
            }
            catch
            {

            }
        }
    }

    private Task Show(SwalOption option)
    {
        OnCloseAsync = () =>
        {
            if (IsAutoHide && DelayToken != null)
            {
                DelayToken.Cancel();
                DelayToken = null;
            }

            // 移除当前 DialogParameter
            DialogParameter = null;
            StateHasChanged();
            return Task.CompletedTask;
        };

        IsAutoHide = option.IsAutoHide;
        Delay = option.Delay;

        option.Modal = ModalContainer;
        var parameters = option.ToAttributes();
        parameters.Add(nameof(ModalDialog.BodyTemplate), BootstrapDynamicComponent.CreateComponent<SweetAlertBody>(SweetAlertBody.Parse(option)).Render());

        DialogParameter = parameters;
        IsShowDialog = true;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private RenderFragment RenderDialog() => builder =>
    {
        if (DialogParameter != null)
        {
            var index = 0;
            builder.OpenComponent<ModalDialog>(index++);
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
            // 关闭弹窗
            if (IsShowDialog)
            {
                DelayToken.Cancel();
                DelayToken.Dispose();
                await ModalContainer.Close();
                IsShowDialog = false;
            }
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
