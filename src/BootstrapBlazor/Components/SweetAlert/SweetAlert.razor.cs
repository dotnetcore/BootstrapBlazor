// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// SweetAlert 组件
/// </summary>
public partial class SweetAlert : IDisposable
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
    private Dictionary<string, object?>? DialogParameter { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 注册 Swal 弹窗事件
        SwalService.Register(this, Show);
    }

    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (ModalContainer != null && IsShowDialog)
        {
            IsShowDialog = false;
            await ModalContainer.Show();

            if (IsAutoHide && Delay > 0)
            {
                if (DelayToken == null)
                {
                    DelayToken = new CancellationTokenSource();
                }

                await Task.Delay(Delay, DelayToken.Token);

                if (!DelayToken.IsCancellationRequested)
                {
                    // 自动关闭弹窗
                    await ModalContainer.Close();
                }
            }
        }
    }

    private Task Show(SwalOption option)
    {
        IsAutoHide = option.IsAutoHide;
        Delay = option.Delay;

        option.Dialog = ModalContainer;
        var parameters = option.ToAttributes();

        parameters.Add(nameof(ModalDialog.OnClose), new Func<Task>(async () =>
        {
            if (IsAutoHide && DelayToken != null)
            {
                DelayToken.Cancel();
                DelayToken = null;
            }
            DialogParameter = null;
            await ModalContainer.CloseOrPopDialog();
            StateHasChanged();
        }));

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
            foreach (var p in DialogParameter)
            {
                builder.AddAttribute(index++, p.Key, p.Value);
            }
            builder.AddComponentReferenceCapture(index++, dialog =>
            {
                var modal = (ModalDialog)dialog;
                ModalContainer.ShowDialog(modal);
            });
            builder.CloseComponent();
        }
    };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            DelayToken?.Dispose();
            DelayToken = null;
            SwalService.UnRegister(this);
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
