﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Dialog 对话框组件
/// </summary>
public partial class Dialog : IDisposable
{
    /// <summary>
    /// 获得/设置 Modal 容器组件实例
    /// </summary>
    [NotNull]
    private Modal? ModalContainer { get; set; }

    /// <summary>
    /// 获得/设置 弹出对话框实例集合
    /// </summary>
    private Dictionary<Dictionary<string, object>, (bool IsKeyboard, bool IsBackdrop)> DialogParameters { get; } = [];

    private bool IsKeyboard { get; set; }

    private bool IsBackdrop { get; set; }

    /// <summary>
    /// DialogServices 服务实例
    /// </summary>
    [Inject]
    [NotNull]
    private DialogService? DialogService { get; set; }

    [NotNull]
    private Func<Task>? OnShownAsync { get; set; }

    [NotNull]
    private Func<Task>? OnCloseAsync { get; set; }

    private Dictionary<string, object>? CurrentParameter { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 注册 Dialog 弹窗事件
        DialogService.Register(this, Show);
    }

    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (CurrentParameter != null)
        {
            await ModalContainer.Show();
        }
    }

    private async Task Show(DialogOption option)
    {
        OnShownAsync = async () =>
        {
            if (option.OnShownAsync != null)
            {
                await option.OnShownAsync();
            }
        };

        OnCloseAsync = async () =>
        {
            // 回调 OnCloseAsync
            if (option.OnCloseAsync != null)
            {
                await option.OnCloseAsync();
            }

            // 移除当前 DialogParameter
            if (CurrentParameter != null)
            {
                DialogParameters.Remove(CurrentParameter);

                // 多弹窗支持
                var p = DialogParameters.LastOrDefault();
                CurrentParameter = p.Key;
                IsKeyboard = p.Value.IsKeyboard;
                IsBackdrop = p.Value.IsBackdrop;

                StateHasChanged();
            }
        };

        IsKeyboard = option.IsKeyboard;
        IsBackdrop = option.IsBackdrop;

        option.Modal = ModalContainer;

        var parameters = option.ToAttributes();
        var content = option.BodyTemplate ?? option.Component?.Render();
        if (content != null)
        {
            parameters.Add(nameof(ModalDialog.BodyTemplate), content);
        }

        if (option.HeaderTemplate != null)
        {
            parameters.Add(nameof(ModalDialog.HeaderTemplate), option.HeaderTemplate);
        }

        if (option.HeaderToolbarTemplate != null)
        {
            parameters.Add(nameof(ModalDialog.HeaderToolbarTemplate), option.HeaderToolbarTemplate);
        }

        if (option.FooterTemplate != null)
        {
            parameters.Add(nameof(ModalDialog.FooterTemplate), option.FooterTemplate);
        }

        if (!string.IsNullOrEmpty(option.Class))
        {
            parameters.Add(nameof(ModalDialog.Class), option.Class);
        }

        if (option.OnSaveAsync != null)
        {
            parameters.Add(nameof(ModalDialog.OnSaveAsync), option.OnSaveAsync);
        }

        if (option.CloseButtonText != null)
        {
            parameters.Add(nameof(ModalDialog.CloseButtonText), option.CloseButtonText);
        }
        if (option.CloseButtonIcon != null)
        {
            parameters.Add(nameof(ModalDialog.CloseButtonIcon), option.CloseButtonIcon);
        }

        if (option.SaveButtonText != null)
        {
            parameters.Add(nameof(ModalDialog.SaveButtonText), option.SaveButtonText);
        }
        if (option.SaveButtonIcon != null)
        {
            parameters.Add(nameof(ModalDialog.SaveButtonIcon), option.SaveButtonIcon);
        }

        if (option is ResultDialogOption resultOption)
        {
            parameters.Add(nameof(ModalDialog.ResultTask), resultOption.ResultTask);
            if (resultOption.GetDialog != null)
            {
                parameters.Add(nameof(ModalDialog.GetResultDialog), resultOption.GetDialog);
            }
        }

        // 保存当前 Dialog 参数
        CurrentParameter = parameters;

        // 添加 ModalDialog 到容器中
        DialogParameters.Add(parameters, (IsKeyboard, IsBackdrop));
        await InvokeAsync(StateHasChanged);
    }

    private static RenderFragment RenderDialog(int index, Dictionary<string, object> parameter) => builder =>
    {
        builder.OpenComponent<ModalDialog>(100 + index);
        builder.AddMultipleAttributes(101 + index, parameter);
        builder.SetKey(parameter);
        builder.CloseComponent();
    };

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            DialogService.UnRegister(this);
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
